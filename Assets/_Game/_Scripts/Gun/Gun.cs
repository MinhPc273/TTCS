using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GunSO gunDataBase;
    //[SerializeField] GunData gunData;
    [SerializeField] GunUI gunUI;

    public GunSO GunDataBase => gunDataBase;
    public GunUI GunUI => gunUI;

    //public GunData GunData => gunData;
}
