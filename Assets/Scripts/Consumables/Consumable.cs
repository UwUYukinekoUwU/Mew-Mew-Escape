using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
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

    private void DeleteObjectWithParents(GameObject toDelete)
    {
        if(toDelete.transform.parent != null)
            DeleteObjectWithParents(toDelete.transform.parent.gameObject);
        else
            Destroy(toDelete.gameObject);
    }
}
