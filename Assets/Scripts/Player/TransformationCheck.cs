using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For checking if there are transform options in trigger range.
/// </summary>
public class TransformationCheck : MonoBehaviour
{
    public static string[] TransformOptions = { "HidingBox", "Skateboard" };

    public bool InRange { get; set; } = false;

    private GameObject closestTransform;
    private Transform player;

    private int currentlyInRange = 0;

    public void Start()
    {
        player = GetComponent<Transform>();      
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

    /// <summary>
    /// Returns the closest transformation item.
    /// </summary>
    public GameObject GetClosestTransform()
    {
        return closestTransform;
    }

    /// <summary>
    /// Checks if this tag is a transformation option or not.
    /// </summary>
    private bool isOption(string tag)
    {
        foreach (string option in TransformOptions)
        {
            if (tag == option)
                return true;
        }
        return false;
    }


}
