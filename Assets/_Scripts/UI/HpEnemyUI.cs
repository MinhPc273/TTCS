using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpEnemyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;

    //blue = single
    //red = dual
    //green = cap

    private void OnEnable()
    {
        Invoke("Destroy", 0.8f);
    }

    public void setValue(float dame, TurretAI.TurretType type)
    {
        txt.text = FormatValue(dame);
        if(type == TurretAI.TurretType.Single)
        {
            txt.color = Color.blue;
        }
        else if(type == TurretAI.TurretType.Dual) 
        {
            txt.color = Color.red;
        }
        else
        {
            txt.color = Color.green;
        }

    }

    private string FormatValue(float dame)
    {
        if(dame < 1000)
        {
            return dame.ToString();
        }
        if(dame < 1000000)
        {
            int first = (int)(dame / 1000);
            int second = (int)(dame % 1000);
            return first + "K" + second;
        }
        return dame.ToString();
    }

    private void Destroy()
    {
        ObjectPooler.EnqueueObject(this, this.name);
        //this.gameObject.SetActive(false);
    }
}
