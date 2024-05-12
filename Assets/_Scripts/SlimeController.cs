using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField] AnimancerComponent animancer;

    [SerializeField] ClipTransition win;
    [SerializeField] ClipTransition lose;
    [SerializeField] ClipTransition idle;
    [SerializeField] ClipTransition nom;

    public void PlayAnim(SlimeState state)
    {
        switch (state)
        {
            case SlimeState.Win:
                animancer.Play(win); break;
            case SlimeState.Lose:
                animancer.Play(lose); break;
            case SlimeState.Idle:
                animancer.Play(idle); break;
            case SlimeState.Nom:
                animancer.Play(nom); break;
        }
    }
}

public enum SlimeState
{
    Win,
    Lose,
    Idle,
    Nom
}
