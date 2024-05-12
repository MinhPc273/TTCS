using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    private int coin;
    private int coinRequired;

    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] TextMeshProUGUI txtCoinRequired;

    [SerializeField] StageController stageController;

    private void Awake()
    {
        this.setUI();
    }

    private void setValue()
    {
        coin = Prefs.Coin;
        coinRequired = Prefs.CoinRequired;
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
    }

    public void PrevLevelUI()
    {
        stageController.Pre();
    }
}
