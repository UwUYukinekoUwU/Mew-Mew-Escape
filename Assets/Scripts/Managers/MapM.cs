using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Map Manager, holds data about the current map setup, doesn't get destroyed on scene load.
/// </summary>
public class MapM : MonoBehaviour
{
    public static MapM Map { get; private set; }

    public int[,] Coordinates { get; set; }
    public Vector2 CurrentPosition { get; set; }
    public Vector2 WantedPosition { get; set; }

    private Dictionary<int, string> indexedScenes = new Dictionary<int, string>() 
    {
        { 0, "EntryScene" },
        { 1, "GameplayTest" },
        { 2, "bottom_left_turn" },
        { 3, "bottom_right_turn" },
        { 4, "left_up_turn" },
        { 5, "right_up_turn" },
    };

    /// <summary>
    /// Loads the scene player is heading to, based on the Coordinates grid.
    /// </summary>
    public void LoadNextScene()
    {
        Debug.LogWarning(Map.WantedPosition);
        
        int wantedSceneIndex = Coordinates[(int)Map.WantedPosition.x, (int)Map.WantedPosition.y];
        SceneManager.LoadScene(indexedScenes[wantedSceneIndex]);
    }

    /// <summary>
    /// Resets all values of this manager to default.
    /// </summary>
    public static void ResetManager()
    {
        Map.CurrentPosition = new Vector2(1, 0);
        Map.WantedPosition = new Vector2(1, 0);

        //reset whole map generation later
    }
    private MapM() { }

    public void Awake()
    {
        if (Map != null)
            return;

        Map = this;

        CurrentPosition = new Vector2(1, 0);
        WantedPosition = new Vector2(1, 0);

        Coordinates = new int[10, 10];

        //temporary hardcoded level setup
        Coordinates[1, 0] = 0;
        Coordinates[0, 0] = 4;
        Coordinates[0, 1] = 3;
        Coordinates[1, 1] = 1;
        Coordinates[2, 0] = 5;
        Coordinates[2, 1] = 2;
        //call procedural generation (perlin noise?)
    }
}
