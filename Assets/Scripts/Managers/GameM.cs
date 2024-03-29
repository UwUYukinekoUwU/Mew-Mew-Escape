using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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



    private GameM() { }

    public void Awake()
    {
        if (Game != null)
            return;

        Game = this;

        DontDestroyOnLoad(gameObject);
    }
}
