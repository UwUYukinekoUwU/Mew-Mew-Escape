using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameM : MonoBehaviour
{
    public static GameM Game { get; private set; }



    public CinemachineVirtualCamera mainCamera;
    public bool PlayerBusy { get; set; }
    public bool Paused { get; set; }




    private GameM() { }

    public void Awake()
    {
        Game = this;
    }
}
