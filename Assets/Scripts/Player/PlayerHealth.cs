using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameM;

public class PlayerHealth : Health
{
    [SerializeField] private TextMeshProUGUI livesText;
    public new void Start()
    {
        base.Start();
        if (Game.Lives != 0)
            Lives = Game.Lives;
    }
    public void OnDestroy()
    {
        Game.Lives = Lives;
    }

    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
        livesText.text = Lives.ToString();
    }

    protected override void GetKilled()
    {
        //death animation mb?
        Game.LoseGame();
    }
}
