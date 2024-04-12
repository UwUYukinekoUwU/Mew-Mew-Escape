using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using static GameM;

/// <summary>
/// Listens for input, and if a transformation option is in range, it will transform the creature into it.
/// </summary>
public class AbilityController : MonoBehaviour
{
    private TransformationCheck transformItem;
    private PlayerController _controller;
    private bool interacting = false;

    private Dictionary<string, Action> transformOptions = new Dictionary<string, Action>();


    public void Start()
    {
        transformItem = GetComponentInChildren<TransformationCheck>();
        _controller = GetComponent<Controlls>().input as PlayerController;

        transformOptions["HidingBox"] = BoxTransform;
        transformOptions["Skateboard"] = SkateTransform;
    }

    public void Update()
    {
        interacting |= _controller.Interact();
    }

    public void FixedUpdate()
    {
        if (interacting && transformItem.InRange && !Game.PlayerBusy)
        {
            string tag = transformItem.GetClosestTransform().tag;
            transformOptions[tag]();
        }

        interacting = false;
    }

    /// <summary>
    /// Disable this object and activate the hiding box.
    /// </summary>
    private void BoxTransform()
    {
        gameObject.SetActive(false);
        transformItem.GetClosestTransform().SetActive(true);
        transformItem.GetClosestTransform().GetComponent<Walk>().enabled = true;
        transformItem.GetClosestTransform().GetComponent<HidingBox>().enabled = true;
    }

    /// <summary>
    /// Disable this object and activate the skateboard.
    /// </summary>
    private void SkateTransform()
    {
        gameObject.SetActive(false);
        transformItem.GetClosestTransform().GetComponent<Skate>().enabled = true;
    }
}
