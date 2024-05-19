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

    [SerializeField] StageController stageController;
    [SerializeField] GameObject pnanelNextStage;

    [SerializeField] CoinReward coinRewardPrefab;
    public CoinReward CoinRewardPrefab => coinRewardPrefab;

    [SerializeField] Transform coinRewardParent;
    public Transform CoinRewardParent => coinRewardParent;

    [SerializeField] RectTransform coinTarget;
    public RectTransform CoinTarget => coinTarget;

    private void Awake()
    {
        Instance = this;
        this.setUI();
    }

    private void setValue()
    {
        coin = Prefs.Coin;
        coinRequired = Prefs.CoinRequired;
    }

    public void setGunUI(Sprite sprite)
    {
        imgGunSpawn.sprite = sprite; 
    }

    private void setUI()
    {
        setValue();
        txtCoin.text = coin.ToString();
        txtCoinRequired.text = coinRequired.ToString();
    }

    public void TapButton()
    {
        GameManager.Instance.SpawnGun();
    }

    public void NextLevelUI()
    {
        stageController.Next();
        EnableLSatgeUI();
    }

    public void PrevLevelUI()
    {
        stageController.Pre();
        EnableLSatgeUI();
    }

    public void EnableLSatgeUI()
    {
        pnanelNextStage.SetActive(true);
    }
}
