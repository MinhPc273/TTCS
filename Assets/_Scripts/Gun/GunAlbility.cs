using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAlbility : MonoBehaviour
{
    protected Gun _gun;
    protected GunSO _gunData;
    protected GunUI _gunUI;
    void Awake()
    {
        Initialization();
    }


    protected virtual void Initialization()
    {
        _gun = GetComponent<Gun>();
        if( _gun != null )
        {
            _gunData = _gun.GunData;
            _gunUI = _gun.GunUI;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
