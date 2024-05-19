using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReward : MonoBehaviour
{
    public void setData(Transform _transform, float coin)
    {
        Debug.Log("coin");
        //this.transform.position = Camera.main.WorldToScreenPoint(_transform.position);
        this.transform.position = _transform.position;
        this.transform.DOMove(GUIManager.Instance.CoinTarget.position, 3f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
