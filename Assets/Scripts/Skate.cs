using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : Walk
{
    private PlayerController _playerController;

    public new void Start()
    {
        base.Start();
        _playerController = base._controller as PlayerController;
    }

    public new void Update()
    {
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

        Move();
    }

}
