using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public void TapButton()
    {
        GameManager.Instance.SpawnGun();
    }
}
