using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;


namespace DialogueSystem
{
    /// <summary>
    /// A dialogue for the Sage (white) Cat NPC. Naturally, it's a child of the Dialogue class.
    /// </summary>
    public class TutorialDialogue : Dialogue
    {
        private bool tutorialFinished = false;
        public new void OnEnable()
        {
            base.OnEnable();

            if (!tutorialFinished)
                StartTutorial();
            else
                RepeatExplanations();
        }


        private void StartTutorial()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.delay = 0.05f;
            _lines.Add(WriteText("This counts as a tutorial, by the way", p.Instance()));


            p.portrait = aPortrait;
            p.voice = aVoice;
            p.resetLine = false;
            _lines.Add(WriteText("I will try to explain the controls and...", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.delay = 0.5f;
            _lines.Add(WriteText(" ", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.resetLine = false;
            p.delay = 0.05f;
            _lines.Add(WriteText("other ", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("options you have in this demo.", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.delay = 0.05f;
            p.isAnswering = true;
            _Choices.CreateNew("Controls", "Hunger", "Other creatures", "Where do I go?");
            _lines.Add(WriteText("What do you want to hear about?", p.Instance()));

            StartTalking();

            StartCoroutine(WaitForAnswer(dialogueAnswer =>
            {
                switch (dialogueAnswer)
                {
                    case "Controls":
                        ExplainControls();
                        break;

                    case "Hunger":
                        ExplainHunger();
                        break;

                    case "Other creatures":
                        ExplainOtherCreatures();
                        break;

                    case "Where do I go?":
                        ExplainGoal();
                        break;
                }
            }));

            tutorialFinished = true;
        }

        


        private void ExplainControls()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("V - for interacting with everything", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("C - skipping dialogues", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("Arrows - move around or choose in dialogue", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("Space - sprinting (green bar = stamina)", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("Esc - activating pause menu", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.isAnswering = true;
            _Choices.CreateNew("Yes", "No");
            _lines.Add(WriteText("*Repeat tutorial?", p.Instance()));


            StartTalking();

            StartCoroutine(WaitForAnswer(dialogueAnswer =>
            {
                switch (dialogueAnswer)
                {
                    case "Yes":
                        ExplainControls();
                        break;

                    case "No":
                        HideDialogue();
                        break;
                }
            }));
        }

        private void ExplainHunger()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("See that orange bar on top?", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("If it runs out, you die", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("Don't worry though, you just need to eat...", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.hideDialogue = true;
            _lines.Add(WriteText("Try that dustbin if you're real hungry", p.Instance()));

            StartTalking();
        }

        private void ExplainOtherCreatures()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("You're not alone in this city", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("Watch out for the black cat gang", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("And if you happen to find my brother...", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.hideDialogue = true;
            _lines.Add(WriteText("Run as fast as you can", p.Instance()));

            StartTalking();
        }

        private void ExplainGoal()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("It's not exactly that large around here", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("...", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            _lines.Add(WriteText("Find the black cat", p.Instance()));

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.hideDialogue = true;
            _lines.Add(WriteText("That should be your goal for now", p.Instance()));

            StartTalking();
        }

        private void RepeatExplanations()
        {
            _lines.Clear();
            int aVoice = 0;
            int aPortrait = 0;
            DialogueParameters p = _parameters.Instance();

            p.portrait = aPortrait;
            p.voice = aVoice;
            p.delay = 0.05f;
            p.isAnswering = true;
            _Choices.CreateNew("Controls", "Hunger", "Other creatures", "Where do I go?");
            _lines.Add(WriteText("What do you want to hear about?", p.Instance()));

            StartTalking();

            StartCoroutine(WaitForAnswer(dialogueAnswer =>
            {
                switch (dialogueAnswer)
                {
                    case "Controls":
                        ExplainControls();
                        break;

                    case "Hunger":
                        ExplainHunger();
                        break;

                    case "Other creatures":
                        ExplainOtherCreatures();
                        break;

                    case "Where do I go?":
                        ExplainGoal();
                        break;
                }
            }));
        }
    }
}

