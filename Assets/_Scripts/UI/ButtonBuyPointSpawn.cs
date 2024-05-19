using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ButtonBuyPointSpawn : MonoBehaviour
{

    private Vector3 localScale;

    private void Awake()
    {
        localScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        this.transform.DOScale(localScale * 1.1f, 0.2f);
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("Buy");
    }

    private void OnMouseUp()
    {
        this.transform.DOScale(localScale * 1f, 0.2f);
    }
}
