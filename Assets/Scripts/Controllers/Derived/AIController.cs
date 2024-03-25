using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all AI controllers
/// </summary>
public class AIController : InputController
{
    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }
    public override float GetHorizontalInput()
    {
        return HorizontalInput;
    }

    public override float GetVerticalInput()
    {
        return VerticalInput;
    }
}
