using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartFirstScene()
    {
        SceneManager.LoadScene("EntryScene");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
