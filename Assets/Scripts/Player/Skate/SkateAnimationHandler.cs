using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles Skate Animations.
/// </summary>
public class SkateAnimationHandler : MonoBehaviour
{
    private Animator _anim;
    private int currentState;
    private float lockedTill;

    private static readonly int InactiveSkateboard = Animator.StringToHash("InactiveSkateboard");
    private static readonly int IdleSkateboard = Animator.StringToHash("IdleSkateboard");
    private static readonly int SidewaysSkateboard = Animator.StringToHash("SidewaysSkateboard");
    private static readonly int UpwardsSkateboard = Animator.StringToHash("UpwardsSkateboard");
    private static readonly int DownwardsSkateboard = Animator.StringToHash("DownwardsSkateboard");

    [HideInInspector] public bool _InactiveSkateboard;
    [HideInInspector] public bool _SkatingSideways;
    [HideInInspector] public bool _SkatingUpwards;
    [HideInInspector] public bool _SkatingDownwards;

    public void Update()
    {
        int state = GetState();

        if (state == currentState) return;
        _anim.CrossFade(state, 0, 0);
        currentState = state;
    }

    public void Awake()
    {
        _InactiveSkateboard = true;
        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Determines the animation that should be used.
    /// </summary>
    private int GetState()
    {
        if (Time.time < lockedTill)
            return currentState;

        if (_InactiveSkateboard) return InactiveSkateboard;
        if (_SkatingSideways) return SidewaysSkateboard;
        if (_SkatingUpwards) return UpwardsSkateboard;
        if (_SkatingDownwards) return DownwardsSkateboard;
        return IdleSkateboard;
    }
}
