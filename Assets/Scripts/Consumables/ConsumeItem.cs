using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Consumable;

/// <summary>
/// On Creature, you need to define your own food actions in an Awake or Start method.
/// </summary>
public class ConsumeItem : MonoBehaviour
{
    protected Dictionary<Consumables, Action> foodActions;

    public bool Consume(Consumables food)
    {
        try
        {
            foodActions[food].Invoke();
            return true;
        }
        catch (KeyNotFoundException ex) 
        {
            return false;
        }
    }


}
