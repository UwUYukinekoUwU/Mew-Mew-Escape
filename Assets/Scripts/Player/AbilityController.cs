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

    private Dictionary<string, Action<GameObject>> transformOptions = new Dictionary<string, Action<GameObject>>();


    public void Start()
    {
        transformItem = GetComponentInChildren<TransformationCheck>();
        _controller = GetComponent<Controlls>().input as PlayerController;

        transformOptions["HidingBox"] = BoxTransform;
        transformOptions["Skateboard"] = SkateTransform;
        //ADD MORE HERE
    }

    public void Update()
    {
        interacting |= _controller.Interact();
    }

    public void FixedUpdate()
    {
        if (interacting && transformItem.InRange && !Game.PlayerBusy)
        {
            GameObject closestTransform = transformItem.GetClosestTransform();
            string tag = closestTransform.tag;
            transformOptions[tag](closestTransform);
        }

        interacting = false;
    }

    /// <summary>
    /// Disable this object and activate the hiding box.
    /// </summary>
    public void BoxTransform(GameObject item)
    {
        item.SetActive(true);
        item.GetComponent<HidingBox>().enabled = true;
        item.GetComponent<Walk>().enabled = true;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Disable this object and activate the skateboard.
    /// </summary>
    public void SkateTransform(GameObject item)
    {
        item.GetComponent<Skate>().enabled = true;

        gameObject.SetActive(false);
    }
}
