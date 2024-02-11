using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : Walk
{
    private PlayerController _controller;


    public new void Start()
    {
        _controller = GetComponent<Controlls>().input as PlayerController;
        base.Start();
    }

    public new void Update()
    {
        float current_vertical = _controller.GetVerticalInput();
        if (current_vertical != 0f)
        {
            horizontalInput = 0f;
            verticalInput = current_vertical;
        }


        float current_horizontal = _controller.GetHorizontalInput();
        if (current_horizontal != 0f)
        {
            verticalInput = 0f;
            horizontalInput = current_horizontal;
        }


        Move();
    }

}
