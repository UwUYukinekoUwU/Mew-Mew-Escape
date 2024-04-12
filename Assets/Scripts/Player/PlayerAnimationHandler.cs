using System.Collections.Generic;
using UnityEngine;
using static GameM;

/// <summary>
/// Responsible for animating the player.
/// </summary>
public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _anim;
    private int currentState;
    private float lockedTill;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int RunningSideways = Animator.StringToHash("RunSideways");
    private static readonly int RunningUpwards = Animator.StringToHash("RunUpwards");
    private static readonly int RunningDownwards = Animator.StringToHash("RunDownwards");
    //private static readonly int Jump = Animator.StringToHash("Jump");
    //private static readonly int Normal_attack = Animator.StringToHash("Normal_attack");

    //[SerializeField] private float attackAnimDuration;

    //[HideInInspector] public bool _Attacking;
    //[HideInInspector] public bool _Jumping;
    [HideInInspector] public bool _RunningSideways;
    [HideInInspector] public bool _RunningUpwards;
    [HideInInspector] public bool _RunningDownwards;


    private Dictionary<int, float> transitionDurations = new Dictionary<int, float>()
    { 
        { Idle, 0.2f },
        { RunningSideways, 0f },
        { RunningUpwards, 0f },
        { RunningDownwards, 0f }
    };


    public void Update()
    {
        int state = GetState();

        if (state == currentState) return;
        _anim.CrossFade(state, transitionDurations[state], 0);
        currentState = state;
    }

    public void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Determines the animation that should be used.
    /// </summary>
    private int GetState()
    {
        if (Time.time < lockedTill)
            return currentState;

        //if (_Attacking) return LockState(Normal_attack, attackAnimDuration);
        //if (_Jumping) return Jump;
        if (Game.PlayerBusy) return Idle;
        if (_RunningSideways) return RunningSideways;
        if (_RunningUpwards) return RunningUpwards;
        if (_RunningDownwards) return RunningDownwards;
        return Idle;

        int LockState(int s, float t)
        {
            lockedTill = Time.time + t;
            return s;
        }
    }
}
