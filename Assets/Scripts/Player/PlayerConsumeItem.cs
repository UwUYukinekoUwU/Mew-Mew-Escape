using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Consumable;

/// <summary>
/// Child of the ConsumeItem class. Binds reactions to food for player.
/// </summary>
public class PlayerConsumeItem : ConsumeItem
{
    [Header("References")]
    [SerializeField] private Hunger hunger;

    [Header("Parameters")]
    [SerializeField] private int fishSecondsAdded = 5;
    [SerializeField] private int mouseSecondsAdded = 20;


    public void Awake()
    {
        foodActions = new Dictionary<Consumables, Action>()
        {
            { Consumables.FISH, EatFish },
            { Consumables.MOUSE, EatMouse },
        };
    }

    /// <summary>
    /// If the player eats a fish, add some hunger time.
    /// </summary>
    private void EatFish()
    {
        hunger.AddTime(fishSecondsAdded);
    }

    /// <summary>
    /// If the player eats a mouse, add some hunger time
    /// </summary>
    private void EatMouse()
    {
        hunger.AddTime(mouseSecondsAdded);
    }

}
