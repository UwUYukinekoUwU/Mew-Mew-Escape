using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameM;
using static SoundM;

public class PlayerHealth : Health
{
    [SerializeField] private AudioClip mewTakeDmg;

    private TextMeshProUGUI livesText;
    public new void Start()
    {
        base.Start();
        livesText = Game.GetComponentByName<TextMeshProUGUI>("LivesNumber");
        if (Game.Lives != 0)
        {
            Lives = Game.Lives;
            livesText.text = Lives.ToString();
        }

    }
    public void OnDestroy()
    {
        Game.Lives = Lives;
    }

    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
        if (Lives < 0)
            Lives = 0;
        livesText.text = Lives.ToString();
        _Sounds.Play(mewTakeDmg);
    }

    protected override void GetKilled()
    {
        //death animation mb?
        Game.LoseGame();
    }
}
