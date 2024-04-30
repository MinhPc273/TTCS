using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GunSO gunDataBase;
    [SerializeField] GunUI gunUI;

    public GunSO GunData => gunDataBase;
    public GunUI GunUI => gunUI;
}
