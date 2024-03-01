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
        if (horizontalInput != 0)
            _handler._RunningSideways = true;
        else 
            _handler._RunningSideways = false;

        if (verticalInput > 0)
            _handler._RunningUpwards = true;
        else 
            _handler._RunningUpwards = false;

        if (verticalInput < 0)
            _handler._RunningDownwards = true;
        else
            _handler._RunningDownwards = false;

    }
}
