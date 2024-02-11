using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private HidingBoxCheck hidingBox;
    private PlayerController _controller;
    private bool interacting = false;

    public void Start()
    {
        hidingBox = GetComponentInChildren<HidingBoxCheck>();
        _controller = GetComponent<ControllerHolder>().input as PlayerController;
    }

    public void Update()
    {
        interacting |= _controller.Interact();
    }

    public void FixedUpdate()
    {
        if (interacting && hidingBox.InRange)
            BoxTransform();
        interacting = false;
    }


    private void BoxTransform()
    {
        gameObject.SetActive(false);
        hidingBox.GetClosestBox().SetActive(true);
        hidingBox.GetClosestBox().GetComponent<Walk>().enabled = true;
        hidingBox.GetClosestBox().GetComponent<HidingBox>().enabled = true;
    }
}
