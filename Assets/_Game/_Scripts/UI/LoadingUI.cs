using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] Transform Fill;
    private float i = 0;

    private void OnEnable()
    {
        i += Random.Range(0.2f, 0.7f);
        Fill.transform.DOScaleX(i, 0.8f).OnComplete(() =>
        {
            Fill.transform.DOScale(1, 0.7f).OnComplete(() =>
            {
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    this.gameObject.SetActive(false);
                    if(!TutorialManager.Instance.gameObject.activeInHierarchy)
                    {
                        GUIManager.Instance.EnableLSatgeUI();
                    }
                });
            });
        });
    }
}
