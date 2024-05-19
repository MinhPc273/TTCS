using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : AnimController
{
    public static SlimeController Instance;
    private void Awake()
    {
        Instance = this;   
    }
}
