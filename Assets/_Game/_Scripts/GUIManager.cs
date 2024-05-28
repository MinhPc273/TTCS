using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    private int coin;
    private int coinRequired;

    [SerializeField] Image imgGunSpawn;
    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] TextMeshProUGUI txtCoinRequired;
    [SerializeField] TextMeshProUGUI txtLvGun;

    public TextMeshProUGUI TxtLvGun => txtLvGun;

    [SerializeField] StageController stageController;
    [SerializeField] GameObject pnanelNextStage;

    [SerializeField] CoinReward coinRewardPrefab;
    public CoinReward CoinRewardPrefab => coinRewardPrefab;

    [SerializeField] Transform coinRewardParent;
    public Transform CoinRewardParent => coinRewardParent;

    [SerializeField] Transform coinTarget;
    public Transform CoinTarget => coinTarget;

    private bool stateCoinTarget;

    [SerializeField] private ResultUI ResultUI;

    private void Awake()
    {
        Instance = this;
        this.SetUI();
        stateCoinTarget = false;
        txtLvGun.text = "Lv." + Prefs.LvGunSpawn;
    }


    public void setGunUI(Sprite sprite)
    {
        imgGunSpawn.sprite = sprite; 
    }

    public void SetUI()
    {
        txtCoin.text = Prefs.Coin.ToString();
        txtCoinRequired.text = Prefs.CoinRequired.ToString();
    }

    public void TapButton()
    {
        if (Prefs.CoinRequired > Prefs.Coin) return;
        Prefs.Coin -= Prefs.CoinRequired;
        GameManager.Instance.SpawnGun();
        SetUI();

        if(TutorialManager.Instance.gameObject.activeInHierarchy)
        {
            TutorialManager.Instance.Step1();
        }
    }

    public void Win()
    {
        SlimeController.Instance.PlayAnim(StateAnim.Win);
        if (Prefs.Level % 10 == 0)
        {
            ResultUI.Boss.gameObject.SetActive(true);
            ResultUI.Boss.Win();
        }
        else
        {
            ResultUI.Enemy.gameObject.SetActive(true);
            ResultUI.Enemy.Win();
        }
    }

    public void Lose()
    {
        SlimeController.Instance.PlayAnim(StateAnim.Die);
        if (Prefs.Level % 10 == 0)
        {
            ResultUI.Boss.gameObject.SetActive(true);
            ResultUI.Boss.Lose();
        }
        else
        {
            ResultUI.Enemy.gameObject.SetActive(true);
            ResultUI.Enemy.Lose();
        }
    }

    public void LoadLevelUI()
    {
        stageController.LoadStageUI();
        EnableLSatgeUI();
    }

    public void EnableLSatgeUI()
    {
        pnanelNextStage.SetActive(true);
    }

    public void CoinTargetUI()
    {
        SetUI();
        if (!stateCoinTarget)
        {
            stateCoinTarget = true;
            CoinTarget.DOPunchScale(Vector3.one * 0.2f, 0.1f).SetEase(Ease.InOutElastic).OnComplete(() =>
            {
                stateCoinTarget = false;
            });
        }
    }
}
