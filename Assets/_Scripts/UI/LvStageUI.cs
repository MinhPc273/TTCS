using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LvStageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] List<GameObject> ListImg;

    public void SetValue(int Level)
    {
        txtLevel.text = "" + Level;
        setImg(Level);
        setScale(Level);
    }

    private void setImg(int Level)
    {
        if(Level < 1)
        {
            foreach(GameObject img in ListImg)
            {
                img.SetActive(false);
            }
            return;
        }
        if(Level % 10 == 0)
        {
            ListImg[0].SetActive(false);
            ListImg[1].SetActive(true);
        }
        else
        {
            ListImg[0].SetActive(true);
            ListImg[1].SetActive(false);
        }
    }

    public void setScale(int Level)
    {
        if(Level == StageController.Instance.LevelCur)
        {
            this.transform.DOScale(Vector3.one * StageController.Instance.scaleLv, 0.5f);
        }
        else
        {
            this.transform.DOScale(Vector3.one, 0.5f);
        }
        
    }

}
