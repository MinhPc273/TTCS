using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukeBoom : MonoBehaviour
{
    public float Atk;

    private void OnEnable()
    {
        Invoke(nameof(Destruction), 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.transform.parent.GetComponent<EnemyData>().GetDamage(Atk, TurretAI.TurretType.Catapult);
        }
    }

    private void Destruction()
    {
        this.gameObject.SetActive(false);
    }
}
