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
        private PlayerHealth playerHealth;
        public void Start()
        {
            playerHunger = Game.GetComponentByName<Hunger>("PlayerHunger");
            playerHealth = Game.GetComponentByName<PlayerHealth>("Player");
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
            int chance = Random.Range(0, 2);

            _lines.Clear();
            DialogueParameters p = _parameters.Instance();
            p.hideDialogue = true;

            if (chance == 0)
            {
                _lines.Add(WriteText("*You found wonders", p.Instance()));
                playerHunger.AddTime(float.MaxValue);
            }
            else
            {
                _lines.Add(WriteText("*Something didn't taste well", p.Instance()));
                playerHealth.DoDamage(1);
            }
            
            StartTalking();
        }

        private void DontEatFromDustbin()
        {
            _lines.Clear();
            DialogueParameters p = _parameters.Instance();
            p.hideDialogue = true;
            _lines.Add(WriteText("*You're not that hungry yet", p.Instance()));
            StartTalking();
        }
    }
}

