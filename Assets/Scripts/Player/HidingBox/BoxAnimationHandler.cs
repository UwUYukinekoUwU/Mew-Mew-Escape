using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles animations for HidingBox
/// </summary>
public class BoxAnimationHandler : MonoBehaviour
{
    private Animator _anim;
    private int currentState;
    private float lockedTill;

    private static readonly int Inactive = Animator.StringToHash("BoxInactive");
    private static readonly int Idle = Animator.StringToHash("BoxIdle");
    private static readonly int RunningSideways = Animator.StringToHash("BoxMovingSideways");

    [HideInInspector] public bool _RunningSideways;
    [HideInInspector] public bool _Inactive;


    public void Update()
    {
        int state = GetState();

        if (state == currentState) return;
        _anim.CrossFade(state, 0, 0);
        currentState = state;
    }

    public void Awake()
    {
        _anim = GetComponent<Animator>();
        _Inactive = true;
    }

    /// <summary>
    /// Determines the animation that should be used.
    /// </summary>
    private int GetState()
    {
        if (Time.time < lockedTill)
            return currentState;

        if (_RunningSideways) return RunningSideways;
        if (_Inactive) return Inactive;
        return Idle;
    }
}

