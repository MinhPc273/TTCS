using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReward : MonoBehaviour
{
    public void setData(Transform _transform, int coin)
    {
        //this.transform.position = Camera.main.WorldToScreenPoint(_transform.position);
        this.transform.position = _transform.position;
        this.transform.DOMove(GUIManager.Instance.CoinTarget.position, 1.5f).SetEase(Ease.InBack).SetDelay(0.1f).OnComplete(() =>
        {
            Prefs.Coin += coin;

            GUIManager.Instance.CoinTargetUI();
            ObjectPooler.EnqueueObject(this, GUIManager.Instance.CoinRewardParent, this.name);
        });
    }
}
