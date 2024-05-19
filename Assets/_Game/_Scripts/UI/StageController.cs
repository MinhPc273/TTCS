using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{

    [SerializeField] Transform StageParent;

    [SerializeField] List<LvStageUI> ListLvStageUI;

    public float scaleLv;

    private int curLevel;
    private int Level;

    private Vector3 vector3;
    private int Count;

    private void Awake()
    {
        GetListLvStageUI();
        curLevel = Prefs.Level;
        Level = curLevel - 3;
        Count = ListLvStageUI.Count;
    }

    private void Start()
    {
        SetValueListLvStageUI();
        curLevel = Prefs.Level;
    }

    public void LoadStageUI()
    {
        if (curLevel == Prefs.Level) return;
        if(curLevel < Prefs.Level)
        {
            Next();
        }
        else
        {
            Pre(); 
        }
    }


    public void Pre()
    {
        curLevel = Prefs.Level;
        vector3 = ListLvStageUI[0].transform.position;
        for(int i = 0; i < Count - 1; i++)
        {
            ListLvStageUI[i].transform.DOMove(ListLvStageUI[i + 1].transform.position, 0.5f);
        }
        ListLvStageUI[Count - 1].transform.position = vector3;
        ListLvStageUI[Count - 1].SetValue(curLevel - 3);

        for (int i = 0; i < Count - 1; i++)
        {
            LvStageUI tmp = ListLvStageUI[i];
            ListLvStageUI[i] = ListLvStageUI[i + 1];
            ListLvStageUI[i + 1] = tmp;
        }

        for (int i = Count - 1; i > 0; i--)
        {
            LvStageUI tmp = ListLvStageUI[i];
            ListLvStageUI[i] = ListLvStageUI[i - 1];
            ListLvStageUI[i - 1] = tmp;
        }

        LoadListLvStageUI();
    }

    public void Next()
    {
        curLevel = Prefs.Level;
        vector3 = ListLvStageUI[Count - 1].transform.position;
        for (int i = Count - 1; i > 0; i--)
        {
            Debug.Log(i);
            ListLvStageUI[i].transform.DOMove(ListLvStageUI[i-1].transform.position, 0.5f);
        }
        ListLvStageUI[0].transform.position = vector3;
        ListLvStageUI[0].SetValue(curLevel + 3);

        for (int i = 0; i < Count - 1; i++)
        {
            LvStageUI tmp = ListLvStageUI[i];
            ListLvStageUI[i] = ListLvStageUI[i + 1];
            ListLvStageUI[i + 1] = tmp;
        }

        LoadListLvStageUI();
    }

    private void GetListLvStageUI()
    {
        foreach(Transform t in StageParent)
        {
            ListLvStageUI.Add(t.GetComponent<LvStageUI>());
        }
    }

    private void SetValueListLvStageUI()
    {
        foreach(LvStageUI t in ListLvStageUI)
        {
            t.SetValue(Level++);
        }
    }

    private void LoadListLvStageUI()
    {
        foreach (LvStageUI t in ListLvStageUI)
        {
            t.LoadUI();
        }
    }
}
