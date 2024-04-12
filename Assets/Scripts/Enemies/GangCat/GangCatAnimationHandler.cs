using System.Collections.Generic;
using UnityEngine;
using static GameM;

/// <summary>
/// Responsible for animating the black cat.
/// </summary>
public class GangCatAnimationHandler : MonoBehaviour
{
    private Animator _anim;
    private int currentState;
    private float lockedTill;

    private static readonly int Idle = Animator.StringToHash("IdleBlackCat");
    private static readonly int RunningSideways = Animator.StringToHash("RunSidewaysBlackCat");
    private static readonly int RunningUpwards = Animator.StringToHash("RunUpwardsBlackCat");
    private static readonly int RunningDownwards = Animator.StringToHash("RunDownwardsBlackCat");

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

        if (Game.PlayerBusy) return Idle;
        if (_RunningSideways) return RunningSideways;
        if (_RunningUpwards) return RunningUpwards;
        if (_RunningDownwards) return RunningDownwards;
        return Idle;
    }
}
