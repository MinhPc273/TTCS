using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayResultBoss : PlayResutBase
{

    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private TextMeshProUGUI txtTimeWin;
    [SerializeField] private float timeWin;

    [SerializeField] private TextMeshProUGUI txtTimeLose;
    [SerializeField] private float timeLose;

    private TextMeshProUGUI txtTime;

    private int coin;
    private float _time;
    private bool canPlay;

    protected override void OnEnable()
    {
        base.OnEnable();
        canPlay = false;
        coin = Prefs.CoinRequired * 2 * 5;
        txtGold.text = Prefs.FormatIntValue(coin);
    }

    protected override void PlayAnim()
    {
        result.gameObject.SetActive(true);
        result.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            if (IsWin)
            {
                _time = timeWin;
                txtTime = txtTimeWin;
            }
            else
            {
                _time = timeLose;
                txtTime = txtTimeLose;
            }
            //txtTime.text = $"00:0{(int)_time}";
            txtTime.text = "00:" + String.Format("{0:00}", (int)_time);
            canPlay = true;
        });
    }

    private void Update()
    {
        if (canPlay)
        {
            if(_time > 0)
            {
                _time -= Time.deltaTime;
                txtTime.text = "00:" + String.Format("{0:00}", (int)_time);
            }
            else
            {
                canPlay = false;
                this.Done();
            }
        }
    }

    private void Done()
    {
        result.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
        {

            if (IsWin)
            {
                Prefs.LvGunSpawn++;
                Prefs.CoinRequired *= 2;
                txtTime = txtTimeWin;
            }
            this.gameObject.SetActive(false);
        });
    }

    public void ButtonOK()
    {
        this.Done();
    }

    public void ButtonClaim()
    {
        Prefs.Coin += coin;
        this.Done();
    }

    public void ButtonClaimX2()
    {
        Prefs.Coin += coin * 2;
        this.Done();
    }
}
