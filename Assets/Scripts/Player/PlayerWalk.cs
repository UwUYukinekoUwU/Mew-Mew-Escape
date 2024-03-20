using Cinemachine;
using UnityEngine;
using static GameM;

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

        _handler._RunningSideways = false;
        _handler._RunningUpwards = false;
        _handler._RunningDownwards = false;

        if (verticalInput > 0)
            _handler._RunningUpwards = true;
        if (verticalInput < 0)
            _handler._RunningDownwards = true;

        if (horizontalInput != 0)
            _handler._RunningSideways = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string s = LayerMask.LayerToName(collision.transform.gameObject.layer);
        Debug.Log("collided with " + s);
        Debug.Log("this layer " + gameObject.layer);
    }
}
