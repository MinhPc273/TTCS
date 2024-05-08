using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;

    [SerializeField] GunManager gunManager;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnGun()
    {
        gunManager.SpawnGun();
    }
}
