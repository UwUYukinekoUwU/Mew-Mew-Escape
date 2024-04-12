using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Every object meant to be consumed should have this script attached. Once something enters its trigger range,
/// it will attempt to call Consume() on the thing that entered. If it succeeds, it deletes itself and all its parent objects.
/// </summary>
public class Consumable : MonoBehaviour
{
    /// <summary>
    /// Consumables available in the game
    /// </summary>
    public enum Consumables
    {
        FISH,
        MOUSE,
        POWERUP
    }

    public Consumables ConsumableName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;

        if (collision.gameObject.tag != "Creature" && collision.gameObject.tag != "Player")
            return;

        ConsumeItem creatureConsume;
        if (!collision.gameObject.TryGetComponent(out creatureConsume))
        {
            Debug.LogWarning("Can't find ConsumeItem script on creature " + collision.gameObject.name);
            return;
        }

        bool gotConsumed = creatureConsume.Consume(ConsumableName);

        if (gotConsumed)
            DeleteObjectWithParents(gameObject);
    }

    /// <summary>
    /// Recursively destroys this object and all of its parents.
    /// </summary>
    /// <param name="toDelete">The object to destroy</param>
    private void DeleteObjectWithParents(GameObject toDelete)
    {
        if(toDelete.transform.parent != null)
            DeleteObjectWithParents(toDelete.transform.parent.gameObject);
        else
            Destroy(toDelete.gameObject);
    }
}
