using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using static SoundM;

namespace DialogueSystem
{
    /// <summary>
    /// Base class for dialogues.
    /// </summary>
    public class Dialogue : MonoBehaviour
    {
        [Header("References")]
        [Tooltip("Needed for disabling canvas, it would draw over our dialogues")]
        [SerializeField] private GameObject playerStatsCanvas;

        [SerializeField] private PlayerController Player;
        [SerializeField] private Image imageHolder;
        [SerializeField] private TextMeshProUGUI textHolder;
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] public DialogueParameters _parameters;

        [Header("Dialogue settings")]
        [SerializeField] private List<Sprite> characterSprites;
        [SerializeField] private List<AudioClip> voices;              

        private TalkingRange talkingRange;
        private string dialogueAnswer;
        public Choices _Choices { get; private set; }

        private string desiredText;
        private bool skipped;
        private int currentLine;
        public List<IEnumerator> _lines = new List<IEnumerator>();

        /// <summary>
        /// Displays passed text into the textbox in an oldschool rpg style.
        /// </summary>
        /// <param name="input">String to display</param>  
        /// <param name="parameters">Object containing nescessary data</param>
        public IEnumerator WriteText(string input, DialogueParameters parameters)
        {    
            desiredText += input;
            SetPortrait(parameters.portrait);

            for (int i = 0; i < input.Length; i++)
            {
                if (skipped) break;

                textHolder.text += input[i];
                if (input[i] != ' ') PlayVoice(parameters.voice);

                yield return new WaitForSeconds(parameters.delay);
            }

            if (!parameters.resetLine) { NextLine(); yield break; }
            if (skipped) textHolder.text = desiredText;
            if (parameters.isAnswering) _Choices.Activate(true);


            yield return new WaitUntil(Player.Interact);

            if (parameters.hideDialogue) HideDialogue();
            if (parameters.afterFinish != null) parameters.afterFinish();
            if (parameters.isAnswering) dialogueAnswer = _Choices.GetSelected();
            _Choices.Activate(false);
            skipped = false;
            ResetLine();
            NextLine();
        }

        public void Update()
        {
            //skip dialogue line if the player wishes it
            if (Player.SkipDialogue())
            {
                skipped = true;
            }
        }
        public void OnEnable()
        {
            skipped = false;
            ResetLine();
            _lines.Clear();

            ShowDialogue();
        }
        public void Awake()
        {
            talkingRange = GetComponent<TalkingRange>();
            _Choices = GetComponent<Choices>();
        }

        /// <summary>
        /// Hides the dialogue box.
        /// </summary>
        public void HideDialogue()
        {
            ActivateDialogue(false);
            talkingRange.ReEnableTalkingRange();
            this.enabled = false;
            playerStatsCanvas.SetActive(true);
        }
        /// <summary>
        /// Makes the dialogue box visible.
        /// </summary>
        public void ShowDialogue()
        {
            ActivateDialogue(true);
            playerStatsCanvas.SetActive(false);
        }
        /// <summary>
        /// After player answers, this invokes the action passed.
        /// </summary>      
        public IEnumerator WaitForAnswer(Action<string> afterAnswer)
        {
            yield return new WaitUntil(() => dialogueAnswer != null);

            string answer = dialogueAnswer;
            dialogueAnswer = null;
            afterAnswer(answer);
        }

        public void StartTalking()
        {
            StartCoroutine(_lines[0]);
        }

        /// <summary>
        /// Starts saying the next line in the batch, if there is one.
        /// </summary>
        private void NextLine()
        {
            currentLine++;
            if (currentLine < _lines.Count)
                StartCoroutine(_lines[currentLine]);
            else
                currentLine = 0;
        }    
        /// <summary>
        /// Determines whether the dialogue box should be displayed or not based on the parameter passed.
        /// </summary>
        private void ActivateDialogue(bool isActive)
        {
            //TODO some animation
            for (int i = 0; i < dialogueBox.transform.childCount; i++)
            {
                dialogueBox.transform.GetChild(i).gameObject.SetActive(isActive);
            }
            dialogueBox.SetActive(isActive);
        }
        /// <summary>
        /// Sets the active character portrait to the one corresponding to the index passed.
        /// </summary>
        private void SetPortrait(int i)
        {
            if (i < 0 || i >= characterSprites.Count)
                Debug.Log("Dialogue tried to access an invalid portrait index: " + i);
            else
                imageHolder.sprite = characterSprites[i];
            imageHolder.preserveAspect = true;
        }
        /// <summary>
        /// Clears the textbox.
        /// </summary>
        private void ResetLine()
        {
            textHolder.SetText("");
            desiredText = "";
        }  
        /// <summary>
        /// Returns a voice with the index passed.
        /// </summary>    
        private void PlayVoice(int i)
        {
            if (i < 0 || i >= voices.Count)
                Debug.Log("Dialogue tried to access an invalid voice index: " + i);
            else
                _Sounds.Play(voices[i]);
        }
    }
}
