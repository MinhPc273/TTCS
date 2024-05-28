using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [SerializeField] private Transform Hand;
    private Tween tween;

    [Header("Step1")]
    [SerializeField] private GameObject panelStep1;
    private int indexStep1;


    [Header("Step2")]
    [SerializeField] private GameObject panelStep2;
    [SerializeField] private PointSpawn point1;
    [SerializeField] private PointSpawn point2;

    private Transform Gun1;
    private Transform Gun2;

    [Header("Step3")]
    [SerializeField] private GameObject panelStep3;

    private Transform Gun3;
    [SerializeField] private PointSpawn posGun;


    private void Awake()
    {
        Instance = this;
        if(Prefs.IsTutorials == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        indexStep1 = 0;
    }

    private void Start()
    {
        Prefs.IsTutorials = 0;
    }

    public void Step1()
    {
        indexStep1++;
        if(indexStep1 == 2)
        {
            Step2();
        }
    }

    private void Step2()
    {
        panelStep1.SetActive(false);
        panelStep2.SetActive(true);
        Gun1 = point1.Gun;
        Gun2 = point2.Gun;
        LoopHandStep2();
    }

    private void LoopHandStep2()
    {
        Vector3 gun1 = Camera.main.WorldToScreenPoint(Gun1.position);
        Vector3 gun2 = Camera.main.WorldToScreenPoint(Gun2.position);

        Hand.transform.position = gun1;
        tween = Hand.transform.DOMove(gun2, 1.5f).OnComplete(()=>
        {
            LoopHandStep2();
        });
    }

    public void Step3(Transform gun)
    {
        panelStep2.SetActive(false);
        panelStep3.SetActive(true);
        tween.Kill();
        Gun3 = gun;
        LoopHandStep3();
    }

    private void LoopHandStep3()
    { 
        Vector3 gun3 = Camera.main.WorldToScreenPoint(Gun3.position);
        Vector3 pos = Camera.main.WorldToScreenPoint(posGun.transform.position);

        Hand.transform.position = gun3;
        tween = Hand.transform.DOMove(pos, 1.5f).OnComplete(() =>
          {
              LoopHandStep3();
              if(posGun.Gun != null)
              {
                  tween.Kill();
                  GUIManager.Instance.EnableLSatgeUI();
                  this.gameObject.SetActive(false);
              }
          });
    }
}
