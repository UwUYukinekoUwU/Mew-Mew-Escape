using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class InputController : ScriptableObject
{
    /// <summary>
    /// Returns a number between -1 and 1.
    /// </summary>
    public abstract float GetVerticalInput();

    /// <summary>
    /// Returns a number between -1 and 1.
    /// </summary>
    public abstract float GetHorizontalInput();
}


