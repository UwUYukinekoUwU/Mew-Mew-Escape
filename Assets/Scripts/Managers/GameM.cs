using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public static GameM Game { get; private set; }

    public bool PlayerBusy { get; set; }
    public bool Paused { get; set; }
    public bool InDialogue { get; set; }
    public float HungerTimer { get; set; }
    public int Lives { get; set; }

    public void LoseGame()
    {
        Paused = true;
        PlayerBusy = true;
        GetComponentByName<Canvas>("LostGameCanvas").enabled = true;
        Debug.Log("Lost game");
    }



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

    public static void ResetManager()
    {
        Game.PlayerBusy = false;
        Game.Paused = false;
        Game.InDialogue = false;
        Game.HungerTimer = 0;
        Game.Lives = 0;
    }

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
