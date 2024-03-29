using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

public class PlayerHealth : Health
{
    public void Start()
    {
        if (Game.Lives != 0)
            Lives = Game.Lives;
    }
    public void OnDestroy()
    {
        Game.Lives = Lives;
    }

    protected new void GetKilled()
    {
        //death animation mb?
        Game.LoseGame();
    }
}
