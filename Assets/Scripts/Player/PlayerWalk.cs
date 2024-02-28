using UnityEngine;

public class PlayerWalk : Walk
{
    private PlayerAnimationHandler _handler;
    public new void Start()
    {
        base.Start();
        _handler = GetComponent<PlayerAnimationHandler>();
    }

    public new void Update()
    {
        base.Update();
        if (horizontalInput != 0 || verticalInput != 0)
            _handler._Running = true;
        else
            _handler._Running = false;
    }
}
