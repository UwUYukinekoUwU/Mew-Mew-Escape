using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GangCatController", menuName = "MovementControllers/GangCatController")]
public class GangCatController : AIController
{
    public override float GetHorizontalInput()
    {
        return HorizontalInput;
    }

    public override float GetVerticalInput()
    {
        return VerticalInput;
    }
}
