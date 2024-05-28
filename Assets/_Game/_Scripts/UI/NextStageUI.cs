using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextStageUI : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Boss;

    private GameObject GameActive;

    [SerializeField] private TextMeshProUGUI txtLv;

    [SerializeField] private float minScale;

    [SerializeField] private float maxScale;

    private void OnEnable()
    {
        txtLv.text = "Level." + Prefs.Level;
        if(Prefs.Level % 10 == 0)
        {
            GameActive = Boss;
        }
        else
        {
            GameActive = Enemy;
        }
        GameActive.SetActive(true);
        GameActive.transform.localScale = Vector3.one * minScale;
        GameActive.transform.DOScale(maxScale, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(0.2f, () =>
            {
                GameActive.transform.DOScale(minScale, 0.5f).OnComplete(() =>
                {
                    this.gameObject.SetActive(false);
                    GameManager.Instance.NewWave(0.5f);
                });
            });
        });
    }

    private void OnDisable()
    {
        GameActive?.SetActive(false);
    }
}
