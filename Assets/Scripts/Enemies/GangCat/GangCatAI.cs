using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace AI
{
    public class GangCatAI : AIBrain
    {
        [SerializeField] private Transform _Player;

        [Header("Parameters")]
        [SerializeField] private float trailTime = 1f;
        [SerializeField] private float idleSpeed = 2f;


        private Walk walk;
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
                Invoke("ResetIdleDestination", 8f);
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
