using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : Walk
{
    private PlayerController _player_controller;


    public new void Start()
    {
        base.Start();
        _player_controller = base._controller as PlayerController;
    }

    public new void Update()
    {
        float current_vertical = _player_controller.GetVerticalInput();
        if (current_vertical != 0f)
        {
            horizontalInput = 0f;
            verticalInput = current_vertical;
        }


        float current_horizontal = _player_controller.GetHorizontalInput();
        if (current_horizontal != 0f)
        {
            verticalInput = 0f;
            horizontalInput = current_horizontal;
        }


        Move();
    }

}
