using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

/// <summary>
/// Base class for all AI controllers
/// </summary>
public class AIController : InputController
{
    public float HorizontalInput { get; set; }
    public float VerticalInput { get; set; }
    public override float GetHorizontalInput()
    {
        if (Game.PlayerBusy) 
            return 0;

        return HorizontalInput;
    }

    public override float GetVerticalInput()
    {
        if (Game.PlayerBusy)
            return 0;

        return VerticalInput;
    }
}
