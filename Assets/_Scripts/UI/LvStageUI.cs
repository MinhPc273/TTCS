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

    public float scaleLv;

    private int _level;
    public void LoadUI()
    {
        setImg();
        setScale();
    }

    public void SetValue(int Level)
    {
        _level = Level;
        txtLevel.text = "" + Level;
        setImg();
        setScale();
    }

    private void setImg()
    {
        if(_level < 1)
        {
            foreach(GameObject img in ListImg)
            {
                img.SetActive(false);
            }
            return;
        }
        if(_level % 10 == 0)
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

    public void setScale()
    {
        if(_level == Prefs.Level)
        {
            this.transform.DOScale(Vector3.one * scaleLv, 0.5f);
        }
        else
        {
            this.transform.DOScale(Vector3.one, 0.5f);
        }
        
    }

}
