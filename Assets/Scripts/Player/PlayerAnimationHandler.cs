using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _anim;
    private int currentState;
    private float lockedTill;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Running = Animator.StringToHash("Run");
    //private static readonly int Jump = Animator.StringToHash("Jump");
    //private static readonly int Normal_attack = Animator.StringToHash("Normal_attack");

    //[SerializeField] private float attackAnimDuration;

    //[HideInInspector] public bool _Attacking;
    //[HideInInspector] public bool _Jumping;
    [HideInInspector] public bool _Running;


    public void Update()
    {
        int state = GetState();

        if (state == currentState) return;
        _anim.CrossFade(state, 0.7f, 0);
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
        if (_Running) return Running;
        return Idle;

        int LockState(int s, float t)
        {
            lockedTill = Time.time + t;
            return s;
        }
    }
}
