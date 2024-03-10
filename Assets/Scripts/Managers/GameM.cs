using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public static GameM Game { get; private set; }

    public bool PlayerBusy { get; set; }

    private GameM() { }

    public void Awake()
    {
        Game = this;
    }
}
