using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //this.transform.parent.GetComponent<Move>().Attack();
            GUIManager.Instance.Lose();
            WaveManager.Instance.DisableEnemyList();

        }
    }
}
