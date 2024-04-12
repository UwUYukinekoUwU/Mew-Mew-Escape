using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Child of the AIBrain class. Controls the mouse's behaviour.
    /// </summary>
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
            originalWalkSpeed = walk.Speed;
            stateChecker.SearchedTags.Add("Creature");
        }

        public new void FixedUpdate()
        {
            // the mouse is always facing the direction it's heading in
            RotateFaceForward();

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

            // because of rotation, we need to apply the movement vector in local space
            Vector2 rotatedMovement = transform.InverseTransformDirection(_controller.HorizontalInput, _controller.VerticalInput, 0);
            _controller.HorizontalInput = rotatedMovement.x;
            _controller.VerticalInput = rotatedMovement.y;
        }

        /// <summary>
        /// Returns a point in the direction stateChecker.ThingInRange is moving. Always tries to run away from everything.
        /// If this can't move forward, it stops moving.
        /// </summary>
        /// <returns>A point in the direction it's running away.</returns>
        private Vector2 DecideRunPath()
        {
            Vector2 runDirection = transform.position - stateChecker.ThingInRange.position;
            runDirection = runDirection.normalized;

            Vector2 endPoint = (Vector2)transform.position + (runDirection * runAwayPointRange);

            Vector2? contactPoint = navigation.GetContactPoint(endPoint);
            if (contactPoint == null)
                return endPoint;


            return transform.position;
        }

        /// <summary>
        /// Every 2 seconds, selects a random point on the navigation grid to follow.
        /// </summary>
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

        /// <summary>
        /// Returns a random point on the navigation grid.
        /// </summary>
        private Vector2 GetRandomDestination()
        {
            return idlePoints[Random.Range(0, idlePoints.Count)];
        }

        private void ResetIdleDestination()
        {
            idleDestinationDecided = false;
            invokedChange = false;
        }

        /// <summary>
        /// Sets the rotation to face in the direction its moving in.
        /// </summary>
        private void RotateFaceForward()
        {
            // input vector is in local space at this point, bring it back to world space
            Vector2 inputToWorldSpace = transform.TransformDirection(_controller.HorizontalInput, _controller.VerticalInput, 0);

            if (inputToWorldSpace == Vector2.zero)
                return;

            float angle = Vector2.SignedAngle(
                Vector2.right, 
                new Vector2(inputToWorldSpace.x, inputToWorldSpace.y)
            );
            if (angle < 0)
                angle = 360 + angle;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
}

