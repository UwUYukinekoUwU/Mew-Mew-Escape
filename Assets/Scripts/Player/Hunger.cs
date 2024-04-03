using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameM;
using static SoundM;

public class Hunger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image hungerLevelImage;
    [SerializeField] private AudioClip fillHungerSound;

    [Header("Parameters")]
    [SerializeField] private int secondsToDeath = 1500;

    private float hungerLevel;
    private float imageFillOffset = 0.2f;
    private float timer;

    void Start()
    {
        timer = secondsToDeath;
        if (Game.HungerTimer > 0)
            timer = Game.HungerTimer;
    }

    void FixedUpdate()
    {
        if (Game.InDialogue || Game.Paused)
            return;

        timer -= Time.fixedDeltaTime;
        if (timer < 0)
            DieFromHunger();

        hungerLevel = (timer / secondsToDeath) * (1f - imageFillOffset);
        hungerLevelImage.fillAmount = hungerLevel + imageFillOffset;
    }

    public void AddTime(float addedSeconds)
    {
        timer += addedSeconds;
        if (timer > secondsToDeath)
            timer = secondsToDeath;

        _Sounds.Play(fillHungerSound);
    }
    private void DieFromHunger()    
    {
        //TODO animation
        Game.LoseGame();
    }

    public void OnDestroy()
    {
        Game.HungerTimer = timer;
    }
}
