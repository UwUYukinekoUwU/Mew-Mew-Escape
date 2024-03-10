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

    public bool SkipDialogue()
    {
        return Input.GetKeyDown(KeyCode.C);
    }

    public bool LeftArrow()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }
    public bool RightArrow()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }
    public bool UpArrow()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }
    public bool DownArrow()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

}


