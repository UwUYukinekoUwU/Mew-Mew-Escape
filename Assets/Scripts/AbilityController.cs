using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    private HidingBox hidingBox;

    public void Start()
    {
        hidingBox = GetComponentInChildren<HidingBox>();
    }

    public void Update()
    {
        //if (hidingBox.InRange)
        //    Debug.Log(hidingBox.GetClosestBox().transform.position);
    }
}
