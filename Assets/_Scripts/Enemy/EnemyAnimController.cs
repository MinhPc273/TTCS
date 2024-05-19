using Animancer;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [SerializeField] AnimancerComponent animancer;

    [SerializeField] ClipTransition run;
    [SerializeField] ClipTransition slow_Run;
    [SerializeField] ClipTransition die;
    [SerializeField] ClipTransition attack;

    private State _state;

    public void PlayAnim(State state)
    {
        _state = state;
        switch (_state)
        {
            case State.Run :
                animancer.Play(run); break;
            case State.SlowRun:
                animancer.Play(slow_Run); break;
            case State.Die:
                animancer.Play(die); break;
            case State.Attack:
                animancer.Play(attack); break;
        }
    }

    public void ReloadRun()
    {
        if (_state == State.Attack) return;
        _state = State.Run;
        animancer.Play(run);
    }
}

public enum State
{
    Run,
    SlowRun,
    Die, 
    Attack
}
