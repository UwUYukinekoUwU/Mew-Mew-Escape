using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public enum Consumables
    {
        FISH,
        POWERUP
    }

    public Consumables ConsumableName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Creature" && collision.gameObject.tag != "Player")
            return;

        ConsumeItem creatureConsume = collision.gameObject.GetComponent<ConsumeItem>();
        if (creatureConsume == null)
        {
            Debug.LogError("Can't find ConsumeItem script on creature " + gameObject.name);
            return;
        }

        bool gotConsumed = creatureConsume.Consume(ConsumableName);

        if (gotConsumed)
            Destroy(gameObject);
    }
}
