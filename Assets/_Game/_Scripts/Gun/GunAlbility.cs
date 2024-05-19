using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAlbility : MonoBehaviour
{
    protected Gun _gun;
    protected GunSO _gunDataBase;
    protected GunUI _gunUI;
    //protected GunData _gunData;
    void Awake()
    {
        Initialization();
    }


    protected virtual void Initialization()
    {
        _gun = GetComponent<Gun>();
        if( _gun != null )
        {
            _gunDataBase = _gun.GunDataBase;
            _gunUI = _gun.GunUI;
            //_gunData = _gun.GunData;
        }
    }
}
