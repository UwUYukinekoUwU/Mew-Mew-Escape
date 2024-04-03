using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


namespace AI
{
    public class StateChecker : MonoBehaviour
    {
        public bool TargetVisible { get; set; }
        public bool InRange { get; private set; }
        public List<string> SearchedTags { get; set; }
        public Transform ThingInRange { get; private set; }

        private CircleCollider2D fieldOfVision;
        private LayerMask ignoreBoundsLayer;

        private float rangeForRaycast;
        private int checkEveryNthFrame = 10;
        private int nthFrameCounter = 0;

        public void Awake()
        {
            fieldOfVision = GetComponent<CircleCollider2D>();
            ignoreBoundsLayer = ~(1 << LayerMask.NameToLayer("Bounds"));
            ignoreBoundsLayer &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));

            rangeForRaycast = fieldOfVision.radius;
            SearchedTags = new List<string>();
            SearchedTags.Add("Player");
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!IsSearchedTag(collision.gameObject.tag))
                return;

            ThingInRange = collision.transform;
            InRange = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (!IsSearchedTag(collision.gameObject.tag))
                return;

            InRange = false;
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (!InRange)
                return;

            if (!IsSearchedTag(collision.gameObject.tag))
                return;

            if (TargetVisible)
                return;


            // raycasting for all tags in every frame is pretty expensive, so do it only every nth frame
            if (nthFrameCounter < checkEveryNthFrame)
            {
                nthFrameCounter++;
                return;
            }
            nthFrameCounter = 0;


            foreach (string tag in SearchedTags)
            {
                if (RaycastForTarget(collision.gameObject.transform.position, tag))
                {
                    TargetVisible = true;
                    break;
                }
            }
        }


        /// <summary>
        /// Raycasts from current transform.position to target position, returning true if it found its target tag.
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="targetTag">
        /// Leave empty "" for target points without collider. 
        /// If not supplied, Player tag will be used instead.</param>
        /// <returns>Whether the raycast reach its target or not</returns>
        public bool RaycastForTarget(Vector3 targetPosition, string targetTag = null)
        {
            Vector2 direction = targetPosition - transform.position;
            //Debug.DrawRay(transform.position, direction.normalized * rangeForRaycast, Color.red);

            if (targetTag == null)
                targetTag = "Player";

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rangeForRaycast, ignoreBoundsLayer);
            if (hit)
            {
                if (hit.collider.gameObject.tag == targetTag)
                    return true;
            }

            if (targetTag == "")
                return true;

            return false;
        }


        private bool IsSearchedTag(string tag)
        {
            foreach (string t in SearchedTags)
            {
                if (t == tag) 
                    return true;
            }
            return false;
        }

    }

}
