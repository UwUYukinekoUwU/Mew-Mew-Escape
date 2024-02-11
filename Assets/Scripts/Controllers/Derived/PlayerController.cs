using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "PlayerController", menuName = "MovementControllers/PlayerController")]
public class PlayerController : InputController
{
    public override float GetHorizontalInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override float GetVerticalInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool Interact()
    {
        return Input.GetKeyDown(KeyCode.V);
    }

}


