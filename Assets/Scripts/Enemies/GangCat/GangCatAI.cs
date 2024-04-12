using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace AI
{
    /// <summary>
    /// Child of the AIBrain class. Responsible for GangCat (black cat) behaviour.
    /// </summary>
    public class GangCatAI : AIBrain
    {
        [SerializeField] private Transform _Player;

        [Header("Parameters")]
        [SerializeField] private float trailTime = 1f;
        [SerializeField] private float idleSpeed = 2f;


        private Walk walk;
        private GangCatAnimationHandler _animation;
        private float originalWalkSpeed;
        private List<Vector2> idlePoints = new List<Vector2>();
        private Vector2 randomIdlePoint = Vector2.zero;
        private bool idleDestinationDecided = false;
        private bool invokedChange = false;


        public new void Start()
        {
            base.Start();
            idlePoints = navigation.VisiblePoints();
            walk = GetComponent<Walk>();
            _animation = GetComponent<GangCatAnimationHandler>();
            originalWalkSpeed = walk.Speed;
        }

        public new void FixedUpdate()
        {
            walk.Speed = originalWalkSpeed;
            if (stateChecker.TargetVisible)
            {
                InIdlePlace = false;
                idleDestinationDecided = false;
                lastSeenTimer += Time.fixedDeltaTime;
                if (lastSeenTimer > trailTime)
                    LoseTargetFollow();
                else
                    FollowTarget(_Player.position);

                Debug.Log("Following");

            }
            else if (!InIdlePlace)
            {
                idleDestinationDecided = false;
                walk.Speed = idleSpeed;
                HeadToIdle();
                Debug.Log("Heading to Idle");
            }
            else
            {
                IdleLogic();
                Debug.Log("Idling");
            }

            base.FixedUpdate();

            AnimationUpdate();
        }

        /// <summary>
        /// Every 8 seconds chooses a random destination to go to.
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
                Invoke("ResetIdleDestination", 8f);
            }
            FollowTarget(randomIdlePoint, "");
        }

        /// <summary>
        /// Returns a random point on the navigation grid.
        /// </summary>
        /// <returns></returns>
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
        /// Updates values of GangCatAnimationHandler so animations match current movement.
        /// </summary>
        private void AnimationUpdate()
        {
            _animation._RunningSideways = false;
            _animation._RunningUpwards = false;
            _animation._RunningDownwards = false;

            if (_controller.VerticalInput == 0 && _controller.HorizontalInput == 0)
                return;

            if (Mathf.Abs(_controller.HorizontalInput) > Mathf.Abs(_controller.VerticalInput))
                _animation._RunningSideways = true;
            else if (_controller.VerticalInput < 0)
                _animation._RunningDownwards = true;
            else
                _animation._RunningUpwards = true;
        }
    }

}
