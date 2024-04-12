using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

/// <summary>
/// Class containing methods used by Pause Menu buttons.
/// </summary>
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

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void Pause()
    {
        Game.Paused = true;
        pausePanel.SetActive(true);
        Game.PlayerBusy = true;
    }

    /// <summary>
    /// Unpauses the game. This could be in one method together with Pause(), 
    /// but the buttons need to have a function assigned to them.
    /// </summary>
    public void Unpause()
    {
        Game.Paused = false;
        pausePanel.SetActive(false);
        Game.PlayerBusy = false;
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
