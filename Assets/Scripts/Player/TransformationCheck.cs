using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationCheck : MonoBehaviour
{
    public bool InRange { get; set; } = false;


    private GameObject closestTransform;
    private Transform player;
    private PlayerWalk playerWalk;
    private string[] transformOptions = { "HidingBox", "Skateboard" };
    private int currentlyInRange = 0;

    public void Start()
    {
        player = GetComponent<Transform>();      
        playerWalk = GetComponentInParent<PlayerWalk>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOption(collision.gameObject.tag))
            return;

        if (currentlyInRange == 0)
            closestTransform = collision.gameObject;

        currentlyInRange++;
        InRange = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!isOption(collision.gameObject.tag))
            return;

        currentlyInRange--;

        if (currentlyInRange == 0)
            InRange = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOption(collision.gameObject.tag))
            return;

        if (collision.gameObject == closestTransform)
            return;


        float distance_from_player = Vector2.Distance(player.position, collision.gameObject.transform.position);
        float closest_box_distance = Vector2.Distance(player.position, closestTransform.transform.position);
        if (distance_from_player < closest_box_distance)
            closestTransform = collision.gameObject;

    }


    public GameObject GetClosestTransform()
    {
        return closestTransform;
    }


    private bool isOption(string tag)
    {
        bool isOption = false;
        foreach (string option in transformOptions)
        {
            if (tag == option)
            {
                isOption = true;
                break;
            }
        }
        return isOption;
    }


}
