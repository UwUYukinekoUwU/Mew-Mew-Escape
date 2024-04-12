using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameM;

/// <summary>
/// Class containing methods used by main menu buttons, such as StartFirstScene or ExitGame.
/// </summary>
public class StartMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the Entry scene, where the player starts.
    /// </summary>
    public void StartFirstScene()
    {
        if (Game != null)
            ResetAllManagers();
        SceneManager.LoadScene("EntryScene");
    }

    /// <summary>
    /// Exits the game.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
