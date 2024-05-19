using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClipAnim
{
    public StateAnim state;
    public ClipTransition clip;

    public ClipAnim() { }
}

public class AnimController : MonoBehaviour
{
    [SerializeField] AnimancerComponent animancer;
    [SerializeField] List<ClipAnim> clipAnims = new List<ClipAnim>();
    private ClipAnim clipAnim;

    private void OnDisable()
    {
        clipAnim = null;
    }

    public void PlayAnim(StateAnim state,float SpeedAnim = 1)
    {
        if (clipAnim != null )
        {
            if(clipAnim.state == StateAnim.Attack) return;
        }
        clipAnim = clipAnims.Find(x => x.state == state);
        if(clipAnim.clip != null)
        {
            animancer.Play(clipAnim.clip);
            animancer.States.Current.Speed = SpeedAnim;
        }
    }
}

public enum StateAnim
{
    Win,
    Die,
    Idle,
    Nom,
    Run,
    Slow_Run,
    Attack
}

