using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using static GameM;

namespace DialogueSystem
{
    /// <summary>
    /// Checks if player entered the talking range, and once they interact, starts the dialogue script on this object.
    /// </summary>
    [RequireComponent (typeof(Dialogue))]
    public class TalkingRange : MonoBehaviour
    {

        [SerializeField] private GameObject Player;

        private PlayerController playerController;
        private Walk playerMovement;
        private CircleCollider2D talkingRange;
        private Dialogue dialogue;

        public void Awake()
        {
            talkingRange = GetComponent<CircleCollider2D>();
            dialogue = GetComponent<Dialogue>();

            playerController = Player.GetComponent<Controlls>().input as PlayerController;
            playerMovement = Player.GetComponent<PlayerWalk>();
        }

        //sadly can't use OnTriggerStay as the player rigidbody goes to sleep after not moving for a while
        //and waking it up is cumbersome
        public void Update()
        {
            if (playerController.Interact() && !Game.PlayerBusy)
            {
                Game.PlayerBusy = true;
                Game.InDialogue = true;
                playerMovement.enabled = false;
                dialogue.enabled = true;
                talkingRange.enabled = false;
                enabled = false;
            }
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Player") return;

            //animation that the dialogue is available
            enabled = true;
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player") return;

            //stop the animation
            enabled = false;
        }

        /// <summary>
        /// Enables the talking range again
        /// </summary>
        public void ReEnableTalkingRange()
        {   
            talkingRange.enabled = true;
            Game.InDialogue = false;

            // don't want to make the player free to interact immediately,
            // the interaction to confirm the last dialogue line could activate something else otherwise
            Invoke("FreePlayer", 0.5f);
        }
        private void FreePlayer()
        {
            Game.PlayerBusy = false;
            playerMovement.enabled = true;
        }
    }

}
