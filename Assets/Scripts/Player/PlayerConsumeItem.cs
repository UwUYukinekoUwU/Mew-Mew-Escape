using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Consumable;

public class PlayerConsumeItem : ConsumeItem
{
    [Header("References")]
    [SerializeField] private Hunger hunger;

    [Header("Parameters")]
    [SerializeField] private int fishSecondsAdded = 5;


    public void Awake()
    {
        foodActions = new Dictionary<Consumables, Action>()
        {
            { Consumables.FISH, EatFish },
        };
    }


    private void EatFish()
    {
        hunger.AddTime(fishSecondsAdded);
    }

}
