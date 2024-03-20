using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject pausePanel;
    public void Update()
    {
        if (playerController.Escape() && (!Game.PlayerBusy || Game.Paused))
        {
            if (Game.Paused)
                Unpause();
            else
                Pause();
        }
    }

    public void Pause()
    {
        Game.Paused = true;
        pausePanel.SetActive(true);
        Game.PlayerBusy = true;
    }

    public void Unpause()
    {
        Game.Paused = false;
        pausePanel.SetActive(false);
        Game.PlayerBusy = false;
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
