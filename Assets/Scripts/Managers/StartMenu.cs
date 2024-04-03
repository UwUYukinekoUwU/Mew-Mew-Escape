using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameM;

public class StartMenu : MonoBehaviour
{
    public void StartFirstScene()
    {
        if (Game != null)
            ResetAllManagers();
        SceneManager.LoadScene("EntryScene");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
