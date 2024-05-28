using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayResutBase : MonoBehaviour
{

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject lose;
    protected GameObject result;

    protected bool IsWin;

    protected virtual void OnEnable()
    {
        win.SetActive(false);
        lose.SetActive(false);
        IsWin = false;
    }

    private void OnDisable()
    {
        if (IsWin)
        {
            GameManager.Instance.Win();
        }
        else
        {
            GameManager.Instance.Lose();
        }
    }

    public void Win()
    {
        IsWin = true;
        result = win;
        result.transform.localScale = Vector3.zero;
        PlayAnim();
    }

    public void Lose()
    {
        IsWin = false;
        result = lose;
        result.transform.localScale = Vector3.zero;
        PlayAnim();
    }

    protected virtual void PlayAnim()
    {
        result.gameObject.SetActive(true);
        result.transform.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        {
            DOVirtual.DelayedCall(0.5f, () =>
            {
                result.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
                {
                    this.gameObject.SetActive(false);
                });
            });
            
        });
    }
}
