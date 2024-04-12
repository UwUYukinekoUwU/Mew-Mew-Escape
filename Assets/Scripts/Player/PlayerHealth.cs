using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameM;
using static SoundM;

/// <summary>
/// Child of the Health class. Sets custom damage effects and calls GameM.LoseGame() if HP drops to zero.
/// </summary>
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

    /// <summary>
    /// Calls Health.DoDamage and if HP dropped to zero, calls GameM.LoseGame(). Plays a custom sound.
    /// </summary>
    public override void DoDamage(int damage)
    {
        base.DoDamage(damage);
        if (Lives < 0)
            Lives = 0;
        livesText.text = Lives.ToString();
        _Sounds.Play(mewTakeDmg);
    }

    /// <summary>
    /// Plays death animation and calls GameM.LoseGame()
    /// </summary>
    protected override void GetKilled()
    {
        //death animation mb?
        Game.LoseGame();
    }
}
