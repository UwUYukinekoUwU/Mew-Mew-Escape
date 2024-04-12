using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace DialogueSystem
{
    /// <summary>
    /// A dialogue for the yellow box NPC. Naturally, it's a child of the Dialogue class.
    /// </summary>
    public class NpcDialogue : Dialogue
    {
        public new void Awake()
        {
            base.Awake();
        }

        public new void OnEnable()
        {
            base.OnEnable();

            //check which branch to play
            FirstEncounter();          
        }
       

        //branches
        private void FirstEncounter()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            int goblinVoice = 1;
            int gobPortrait = 1;

            DialogueParameters p = _parameters.Instance();
            p.afterFinish = () => Debug.Log("hello");
            _lines.Add(WriteText("hello mom", p.Instance()));

            p.voice = goblinVoice;
            p.portrait = gobPortrait;
            p.resetLine = false;           
            _lines.Add(WriteText("bananas", p.Instance()));

            p.delay = 0.2f;
            p.afterFinish = () => Debug.Log("test");
            _lines.Add(WriteText("....", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.isAnswering = true;
            p.delay = 0.01f;
            p.afterFinish = () => Debug.Log("test2");
            _Choices.CreateNew("Fight", "Run", "Give bananas", "Cry");
            _lines.Add(WriteText("aaaaaaaaaaaaaaaaaaaaaaaaaa", p.Instance()));

           
            StartTalking();

            StartCoroutine(WaitForAnswer(dialogueAnswer =>
            {
                switch (dialogueAnswer)
                {
                    case "Fight":
                        SayBye();
                        break;

                    case "Run":
                        WhereAreYouGoing();
                        break;

                    case "Give bananas":
                        YouDontHaveAny();
                        break;

                    case "Cry":
                        Cry();
                        break;
                }
            }));            
        }

        private void SayBye()
        {
            DialogueParameters p = _parameters.Instance();
            _lines.Clear();
            p.Default();

            p.portrait = 1;
            p.voice = 1;
            p.hideDialogue = true;
            p.afterFinish = () => HideDialogue();
            _lines.Add(WriteText("Ergh! Die!", p.Instance()));

            StartTalking();           
        }

        private void WhereAreYouGoing()
        {
            DialogueParameters p = _parameters.Instance();
            _lines.Clear();

            p.portrait = 1;
            p.voice = 1;
            p.afterFinish = () => { HideDialogue(); };
            _lines.Add(WriteText("Where you goin", p.Instance()));
            StartTalking();
        }

        private void YouDontHaveAny()
        {
            DialogueParameters p = _parameters.Instance();
            _lines.Clear();

            p.voice = 2;
            p.afterFinish = () => HideDialogue();
            _lines.Add(WriteText("*You don't have any", p.Instance()));
            StartTalking();
        }

        private void Cry()
        {
            DialogueParameters p = _parameters.Instance();
            _lines.Clear();

            p.voice = 2;
            p.afterFinish = () => { HideDialogue(); };
            _lines.Add(WriteText("*You don't have any", p.Instance()));
            StartTalking();
        }
    }
}

