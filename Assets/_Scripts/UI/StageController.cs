using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public static StageController Instance;

    [SerializeField] Transform StageParent;

    [SerializeField] List<LvStageUI> ListLvStageUI;

    public int LevelCur;
    public float scaleLv;

    private int Level;

    private void Awake()
    {
        Instance = this;

        Level = LevelCur - 3;
        GetListLvStageUI();
    }

    private void Start()
    {
        SetValueListLvStageUI();
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
}
