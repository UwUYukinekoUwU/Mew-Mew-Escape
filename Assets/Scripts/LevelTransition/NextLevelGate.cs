using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MapM;
using static GameM;

/// <summary>
/// If something enters its trigger range, update MapM.WantedPosition accordingly and load the next scene this gate leads into.
/// </summary>
public class NextLevelGate : MonoBehaviour
{
    /// <summary>
    /// The 4 directions a gate can lead to.
    /// </summary>
    public enum Direction
    {
        TOP, BOTTOM, LEFT, RIGHT
    }
    [SerializeField] private Direction direction;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = false;
        foreach(string option in TransformationCheck.TransformOptions)
        {
            if (option == collision.gameObject.tag)
            {
                isPlayer = true;
                break;
            }   
        }
        if (!isPlayer && collision.gameObject.tag != "Player")
            return;

        Game.SetPlayerTransformationItem(collision.gameObject.tag);


        Debug.Log("Entered exit on " + SceneManager.GetActiveScene().name); 
        if (direction == Direction.TOP)
            Map.WantedPosition = new Vector2(Map.CurrentPosition.x, Map.CurrentPosition.y + 1);
        if (direction == Direction.BOTTOM)
            Map.WantedPosition = new Vector2(Map.CurrentPosition.x, Map.CurrentPosition.y -1);
        if (direction == Direction.LEFT)
            Map.WantedPosition = new Vector2(Map.CurrentPosition.x - 1, Map.CurrentPosition.y);
        if (direction == Direction.RIGHT)
            Map.WantedPosition = new Vector2(Map.CurrentPosition.x + 1, Map.CurrentPosition.y);


        Map.LoadNextScene();
    }
    
}
