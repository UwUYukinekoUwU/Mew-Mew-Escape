using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static GameM;

namespace DialogueSystem
{
    public class DustbinDialogue : Dialogue
    {
        private Hunger playerHunger;
        public void Start()
        {
            playerHunger = Game.GetComponentByName<Hunger>("PlayerHunger");
        }

        public new void OnEnable()
        {
            base.OnEnable();

            AskToEatFromDustbin();
        }


        //branches
        private void AskToEatFromDustbin()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;

            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.isAnswering = true;
            _Choices.CreateNew("Yes", "No");
            _lines.Add(WriteText("*Eat from dustbin?", p.Instance()));


            StartTalking();

            StartCoroutine(WaitForAnswer(dialogueAnswer =>
            {
                switch (dialogueAnswer)
                {
                    case "Yes":
                        EatFromDustbin();
                        break;

                    case "No":
                        DontEatFromDustbin();
                        break;
                }
            }));
        }

        private void EatFromDustbin()
        {
            _lines.Clear();
            DialogueParameters p = _parameters.Instance();
            p.hideDialogue = true;
            _lines.Add(WriteText("*You found wonders", p.Instance()));
            StartTalking();
            playerHunger.AddTime(float.MaxValue);
        }

        private void DontEatFromDustbin()
        {
            _lines.Clear();
            DialogueParameters p = _parameters.Instance();
            p.hideDialogue = true;
            _lines.Add(WriteText("*You decided not to eat from a dustbin", p.Instance()));
            StartTalking();
        }
    }
}

