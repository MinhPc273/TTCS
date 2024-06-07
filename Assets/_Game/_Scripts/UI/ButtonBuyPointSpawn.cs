using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class ButtonBuyPointSpawn : MonoBehaviour
{
    [SerializeField] private int coin;
    [SerializeField] private TextMeshPro txtCoin;
    [SerializeField] private PointSpawn pointBuy;
    [SerializeField] private PointSpawn pointUnlock;

    private Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;
        //txtCoin.text = coin.ToString();
    }

    private void OnMouseDown()
    {
        if (Common.IsUI()) return;
        this.transform.DOScale(localScale * 1.1f, 0.2f);
    }

    private void OnMouseUpAsButton()
    {
        if (Common.IsUI()) return;
        if (coin > Prefs.Coin) return;
        Prefs.Coin -= coin;
        pointBuy.Buy();
        if(pointUnlock != null) pointUnlock.Unlock();
        this.gameObject.SetActive(false);
    }

    private void OnMouseUp()
    {
        if (Common.IsUI()) return;
        this.transform.DOScale(localScale * 1f, 0.2f);
    }
}
