using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPool : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPooler.EnqueueObject(this, this.name);
    }
}
