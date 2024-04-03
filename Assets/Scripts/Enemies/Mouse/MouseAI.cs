using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class MouseAI : AIBrain
    {
        [Header("Parameters")]
        [SerializeField] private float trailTime = 1f;
        [SerializeField] private float idleSpeed = 2f;
        [SerializeField] private float runAwayPointRange = 2f;


        private Walk walk;

        private List<Vector2> idlePoints = new List<Vector2>();
        private Vector2 randomIdlePoint = Vector2.zero;
        private float originalWalkSpeed;
        private bool idleDestinationDecided = false;
        private bool invokedChange = false;
        private float runningTimer;


        public new void Start()
        {
            base.Start();
            idlePoints = navigation.VisiblePoints();
            walk = GetComponent<Walk>();
            if (walk == null)
                Debug.LogWarning("uwu");
            originalWalkSpeed = walk.Speed;
            stateChecker.SearchedTags.Add("Creature");
        }

        public new void FixedUpdate()
        {
            walk.Speed = originalWalkSpeed;
            if (stateChecker.TargetVisible)
            {
                InIdlePlace = false;
                idleDestinationDecided = false;

                runningTimer += Time.fixedDeltaTime;
                if (runningTimer > trailTime)
                    LoseTargetFollow();
                else
                    FollowTarget(DecideRunPath(), "");
             
                Debug.LogWarning("Mouse Following");
            }
            else if (!InIdlePlace)
            {
                runningTimer = 0;
                idleDestinationDecided = false;
                walk.Speed = idleSpeed;
                HeadToIdle();
                Debug.Log("Mouse Heading to Idle");
            }
            else
            {
                runningTimer = 0;
                IdleLogic();
                Debug.Log("Mouse Idling");
            }

            base.FixedUpdate();

            // rotate to the direction facing
            //float angle = Vector3.SignedAngle(
            //    Vector2.right, 
            //    new Vector2(_controller.HorizontalInput, _controller.VerticalInput), 
            //    new Vector3(0, 0, 1)
            //);
            //if (angle < 0)
            //    angle = 360 + angle;
            //transform.Rotate(0, 0, transform.rotation.z - angle);
        }

        private Vector2 DecideRunPath()
        {
            Vector2 runDirection = transform.position - stateChecker.ThingInRange.position;
            runDirection = runDirection.normalized;

            Vector2 endPoint = (Vector2)transform.position + (runDirection * runAwayPointRange);

            Vector2? contactPoint = navigation.GetContactPoint(endPoint);
            if (contactPoint == null)
                return endPoint;

            // if can't go forward, look in 90 degree directions, and if still can't go there, stay in place
            //for (int i = 0; i < 3; i++)
            //{
            //    // ignore for 180 degrees, that will be where the player is
            //    if (i == 1)
            //        continue;

            //    runDirection = new Vector2(-1 * runDirection.y, runDirection.x);
            //    endPoint = (Vector2)transform.position + (runDirection * runAwayPointRange);
            //    contactPoint = navigation.GetContactPoint(endPoint);
            //    if (contactPoint == null)
            //        return endPoint;
            //}

            return transform.position;
        }


        private void IdleLogic()
        {
            walk.Speed = idleSpeed;
            if (!idleDestinationDecided)
            {
                randomIdlePoint = GetRandomDestination();
                idleDestinationDecided = true;
            }
            if (navigation.DestinationReached(randomIdlePoint) && !invokedChange)
            {
                invokedChange = true;
                Invoke("ResetIdleDestination", 2f);
            }
            FollowTarget(randomIdlePoint, "");
        }

        private Vector2 GetRandomDestination()
        {
            return idlePoints[Random.Range(0, idlePoints.Count)];
        }

        private void ResetIdleDestination()
        {
            idleDestinationDecided = false;
            invokedChange = false;
        }
    }
}

