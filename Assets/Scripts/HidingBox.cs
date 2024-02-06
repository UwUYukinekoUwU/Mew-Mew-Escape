using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingBox : MonoBehaviour
{
    public bool InRange { get; set; } = false;


    private GameObject closestHidingBox;
    private int currentlyInRange = 0;
    private Transform player;

    public void Start()
    {
        player = GetComponent<Transform>();

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "HidingBox")
            return;

        if (currentlyInRange == 0)
            closestHidingBox = collision.gameObject;

        currentlyInRange++; 
        InRange = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "HidingBox")
            return;

        currentlyInRange--;
        if (currentlyInRange == 0)
            InRange = false;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "HidingBox")
            return;


        if (collision.gameObject == closestHidingBox)
            return;


        float distance_from_player = Vector2.Distance(player.position, collision.gameObject.transform.position);
        float closest_box_distance = Vector2.Distance(player.position, closestHidingBox.transform.position);
        if (distance_from_player < closest_box_distance)
        {
            closestHidingBox = collision.gameObject;
            Debug.Log("changed");
        }

    }


    public GameObject GetClosestBox()
    {
        return closestHidingBox;
    }


}
