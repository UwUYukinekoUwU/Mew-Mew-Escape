using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueParameters", menuName = "Dialogue/DialogueParameters")]
    /// <summary>
    /// Holds parameters for a dialogue line.
    /// </summary>
    public class DialogueParameters : ScriptableObject
    {
        public float delay = 0.07f;
        public int voice = 0;
        public int portrait = 0;
        public bool isAnswering = false;
        public bool hideDialogue = false;
        public bool resetLine = true;
        public Action afterFinish;

        /// <summary>
        /// Return everything back to default, except for voice and portrait.
        /// </summary>
        public void Default()
        {
            delay = 0.07f;
            isAnswering = false;
            hideDialogue = false;
            resetLine = true;
            afterFinish = null;
        }

        /// <summary>
        /// Returns parameters current parameters as a new object, and resets it to default,
        /// so that the parameters.Default() method doesn't need to be called after every line.
        /// </summary>
        public DialogueParameters Instance()
        {
            DialogueParameters theseParameters = Instantiate(this);
            theseParameters.afterFinish = afterFinish;
            this.Default();

            return theseParameters;
        }
    }
}
