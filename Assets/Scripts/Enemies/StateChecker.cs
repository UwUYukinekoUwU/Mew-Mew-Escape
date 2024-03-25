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

        public bool RaycastForTarget(Vector3 targetPosition)
        {
            Vector2 direction = targetPosition - transform.position;
            //Debug.DrawRay(transform.position, direction.normalized * rangeForRaycast, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rangeForRaycast, ignoreBoundsLayer);
            if (hit)
            {
                if (hit.collider.gameObject.tag == "Player")
                    return true;
            }

            return false;
        }

    }

}
