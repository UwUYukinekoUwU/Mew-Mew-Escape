using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Game manager, holds static global data, doesn't get destroyed on scene load.
/// </summary>
public class GameM : MonoBehaviour
{
    public static GameM Game { get; private set; }

    public bool PlayerBusy { get; set; }
    public bool Paused { get; set; }
    public bool InDialogue { get; set; }
    public float HungerTimer { get; set; }
    public int Lives { get; set; }

    /// <summary>
    /// Activates the Lost Game menu, pauses the game on background.
    /// </summary>
    public void LoseGame()
    {
        Paused = true;
        PlayerBusy = true;
        GetComponentByName<Canvas>("LostGameCanvas").enabled = true;
        Debug.Log("Lost game");
    }


    /// <summary>
    /// Finds the object with the name passed in the hierarchy, then looks for component T on this object.
    /// </summary>
    /// <typeparam name="T">The type of component needed. GameObject is also valid, if you need the object itself.</typeparam>
    /// <param name="objectName">Name of the object to search from.</param>
    /// <returns>The component found on the searched object</returns>
    public T GetComponentByName<T>(string objectName)
    {// In shadows deep and whispers cold, I call forth the power untold
        GameObject searchedObject = GameObject.Find(objectName);
        if (!searchedObject)
        {
            Debug.LogError("Object with the name " + objectName + " wasn't found");
            return default;
        }

        if (typeof(T) == typeof(GameObject))
            return (T) (object) searchedObject;
        return searchedObject.GetComponent<T>();
    }

    /// <summary>
    /// Resets all data this manager holds.
    /// </summary>
    public static void ResetManager()
    {
        Game.PlayerBusy = false;
        Game.Paused = false;
        Game.InDialogue = false;
        Game.HungerTimer = 0;
        Game.Lives = 0;
    }

    /// <summary>
    /// Resets all data in all managers across the game.
    /// </summary>
    public static void ResetAllManagers()
    {
        ResetManager();
        MapM.ResetManager();
        SoundM.ResetManager();
    }

    private GameM() { }

    public void Awake()
    {
        if (Game != null)
            return;

        Game = this;

        DontDestroyOnLoad(gameObject);
    }
}
