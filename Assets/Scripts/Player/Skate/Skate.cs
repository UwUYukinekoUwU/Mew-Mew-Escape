using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : Walk
{
    private PlayerController _playerController;
    private SkateAnimationHandler _skateAnimationHandler;

    public new void Start()
    {
        base.Start();
        _playerController = base._controller as PlayerController;
        _skateAnimationHandler = GetComponent<SkateAnimationHandler>();
        _skateAnimationHandler._InactiveSkateboard = false;
    }

    public new void Update()
    {
        //gather input
        float current_vertical = _playerController.GetVerticalInput();
        float current_horizontal = _playerController.GetHorizontalInput();

        if (current_vertical != 0)
        {
            verticalInput = current_vertical;
            horizontalInput = 0;
        }

        if (current_horizontal != 0)
        {
            horizontalInput = current_horizontal;
            verticalInput = 0;
        }


        // flip the sprite of the object
        if (horizontalInput != 0)
            transform.localScale = new Vector3(
                Mathf.Sign(horizontalInput) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );

        //animation
        _skateAnimationHandler._SkatingSideways = false;
        _skateAnimationHandler._SkatingUpwards = false;
        _skateAnimationHandler._SkatingDownwards = false;

        if (verticalInput > 0)
            _skateAnimationHandler._SkatingUpwards = true;
        if (verticalInput < 0)
            _skateAnimationHandler._SkatingDownwards = true;

        if (horizontalInput != 0)
            _skateAnimationHandler._SkatingSideways = true;


        //move
        Move();
    }

}
