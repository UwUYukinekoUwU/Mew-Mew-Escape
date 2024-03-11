using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using static GameM;

namespace DialogueSystem
{
    [RequireComponent (typeof(Dialogue))]
    public class TalkingRange : MonoBehaviour
    {
        [SerializeField] private PlayerController Player;

        private CircleCollider2D talkingRange;
        private Dialogue dialogue;

        public void Awake()
        {
            talkingRange = GetComponent<CircleCollider2D>();
            dialogue = GetComponent<Dialogue>();
        }

        //sadly can't use OnTriggerStay as the player rigidbody goes to sleep after not moving for a while
        //and waking it up is cumbersome
        public void Update()
        {
            if (Player.Interact())
            {
                Game.PlayerBusy = true;
                dialogue.enabled = true;
                talkingRange.enabled = false;
                enabled = false;
            }
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag != "Player") return;

            //animation that the dialogue is available
            Debug.Log("player in collision range");
            enabled = true;
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player") return;

            //stop the animation
            Debug.Log("player out of talking range");
            enabled = false;
        }
        public void ReEnableTalkingRange()
        {   
            Debug.Log("Talking range reenabled");
            talkingRange.enabled = true;

            // don't want to make the player free to interact immediately,
            // the interaction to confirm the last dialogue line could activate something else otherwise
            Invoke("FreePlayer", 1f);

        }
        private void FreePlayer()
        {
            Game.PlayerBusy = false;
        }
    }

}
