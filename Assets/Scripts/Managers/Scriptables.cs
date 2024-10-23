using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptables : MonoBehaviour
{
    public static Scriptables SObjects;

    [Header("Scriptable objects list")]
    public PlayerController playerController;
    public DialogueParameters dialogueParameters;

    private Scriptables() { }

    public void Awake()
    {
        if (SObjects != null)
            return;

        SObjects = this;

        DontDestroyOnLoad(gameObject);
    }
}
