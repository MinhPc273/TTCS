using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunUI : MonoBehaviour
{
    [SerializeField] TextMeshPro txtLevel;

    public TextMeshPro TxtLevel => txtLevel;
    
    public void setTxtLevel(int n)
    {
        txtLevel.text = "Lv " + n;
    }
}
