using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class Choices : MonoBehaviour
    {
        [SerializeField] private Image choicesImage;
        [SerializeField] private List<TextMeshProUGUI> choiceHolders;
        [SerializeField] private PlayerController Player;

        private string[] choices;
        private int selectedID;
        private TextMeshProUGUI selected;

        public void Update()
        {
            if (Player.LeftArrow())
                selectedID -= 1;
            if (Player.RightArrow())
                selectedID += 1;            
            if (Player.UpArrow())
                selectedID -= 2;             
            if (Player.DownArrow())
                selectedID += 2;

            if (selectedID == -1) selectedID = choices.Length - 1;
            if (selectedID == choices.Length) selectedID = 0;
            if (selectedID == -2) selectedID = choices.Length - 2;
            if (selectedID == choices.Length + 1) selectedID = 1;

            ChangeSelected(selectedID);
        }
        public void Awake()
        {
            selected = choiceHolders[0];
            selectedID = 0;
        }

        /// <summary>
        /// Initiates choices with the parameters passed, prepares them to be displayed.
        /// </summary>      
        public void CreateNew(params string[] choices)
        {
            if (choices == null) throw new Exception("No choice supplied");

            this.choices = choices;

            AssignChoices();
            selectedID = 0;
            ChangeSelected(0);
        }

        /// <summary>
        /// Updates textboxes to display choices passed to the constructor.
        /// </summary>
        private void AssignChoices()
        {
            if (choices.Length > choiceHolders.Count)
                throw new Exception("Not enough textholders for those choices!");

            int i;
            for (i = 0; i < choices.Length; i++)
            {
                choiceHolders[i].SetText(choices[i]);
            }
            //empty the remaining choice texts
            for (; i < choiceHolders.Count; i++)
            {
                choiceHolders[i].SetText("");
            }
        }

        private void ChangeSelected(int id)
        {
            selected.fontStyle = FontStyles.Normal;
            selected.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 0f);
            selected.outlineWidth = 0f;

            selected = choiceHolders[id];

            selected.fontStyle = FontStyles.Bold;
            selected.fontMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1.0f);
            selected.outlineWidth = 0.2f;
        }

        /// <summary>
        /// Shows or hides the choices box.
        /// </summary>
        public void Activate(bool activate)
        {
            //TODO animation           
            choicesImage.gameObject.SetActive(activate);
            this.enabled = activate;
        }
                  
        /// <summary>
        /// Returns text inside the selected textbox.
        /// </summary>
        public string GetSelected()
        {
            return selected.text;
        }
    }
}


