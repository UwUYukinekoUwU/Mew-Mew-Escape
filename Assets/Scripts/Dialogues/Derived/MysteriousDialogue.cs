using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static GameM;

namespace DialogueSystem
{
    public class MysteriousDialogue : Dialogue
    {
        private BoxCollider2D _collider;

        public new void OnEnable()
        {
            base.OnEnable();

            FirstEncounter();
        }

        public void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void FirstEncounter()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.voice = aVoice;
            p.portrait = aPortrait;
            _lines.Add(WriteText("Congratulations...", p.Instance()));

            p.voice = aVoice;
            p.portrait = aPortrait;
            _lines.Add(WriteText("you've found...", p.Instance()));


            p.voice = aVoice;
            p.portrait = aPortrait;
            p.delay = 0.2f;
            _lines.Add(WriteText(".....", p.Instance()));


            p.voice = aVoice;
            p.portrait = aPortrait;
            _lines.Add(WriteText("a mysterious square.", p.Instance()));


            p.portrait = aPortrait;
            p.voice = aVoice;
            p.isAnswering = true;
            _Choices.CreateNew("Yes", "No", "Don't care");
            _lines.Add(WriteText("Do you think your choices matter?", p.Instance()));


            StartTalking();

            StartCoroutine(WaitForAnswer(dialogueAnswer =>
            {
                switch (dialogueAnswer)
                {
                    case "Don't care":
                        DivinePunishment();
                        break;
                    default:
                        TheyDont();
                        break;
                }
            }));
        }

        private void TheyDont()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.voice = aVoice;
            p.portrait = aPortrait;
            p.afterFinish = () =>
            {
                _collider.enabled = false;
                StartCoroutine(Leave());
            };
            p.hideDialogue = true;
            _lines.Add(WriteText("They dont.", p.Instance()));

            StartTalking();
        }

        private void DivinePunishment()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.voice = aVoice;
            p.portrait = aPortrait;
            p.afterFinish = () => Game.LoseGame();
            p.hideDialogue = true;
            _lines.Add(WriteText("And that's a mistake.", p.Instance()));

            StartTalking();
        }

        private IEnumerator Leave()
        {
            int i = 0;
            while (i < 30000)
            {
                transform.Translate(new Vector2(0, 1));
                i++;
                yield return null;
            }
        }
    }
}

