using Animancer;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [SerializeField] AnimancerComponent animancer;

    [SerializeField] ClipTransition run;
    [SerializeField] ClipTransition slow_Run;
    [SerializeField] ClipTransition die; 
    


    public void PlayAnim(State state)
    {
        switch (state)
        {
            case State.Run :
                animancer.Play(run); break;
            case State.SlowRun:
                animancer.Play(slow_Run); break;
            case State.Die:
                animancer.Play(die); break;
        }
    }
}

public enum State
{
    Run,
    SlowRun,
    Die
}
