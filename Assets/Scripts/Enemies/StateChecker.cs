using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace AI
{
    public class StateChecker : MonoBehaviour
    {
        public bool IsFollowing { get; set; }
        public bool InRange { get; private set; }

        private CircleCollider2D fieldOfVision;
        private LayerMask ignoreBoundsLayer;


        private float rangeForRaycast;

        public void Start()
        {
            fieldOfVision = GetComponent<CircleCollider2D>();
            ignoreBoundsLayer = ~(1 << LayerMask.NameToLayer("Bounds"));
            ignoreBoundsLayer &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));

            rangeForRaycast = fieldOfVision.radius;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
                return;

            InRange = true;
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag != "Player")
                return;

            InRange = false;
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (!InRange)
                return;

            if (collision.gameObject.tag != "Player")
                return;

            if (!IsFollowing)
                IsFollowing = RaycastForTarget(collision.gameObject.transform.position);
        }


        /// <summary>
        /// 
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

    }

}
