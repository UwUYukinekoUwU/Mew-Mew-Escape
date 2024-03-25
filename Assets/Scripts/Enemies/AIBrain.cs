using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;


namespace AI
{
    [RequireComponent(typeof(StateChecker))]
    [RequireComponent(typeof(Navigation))]
    public class AIBrain : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform stationPoint;
        [SerializeField] private Transform _Player;

        [Header("Parameters")]
        [SerializeField] private float trailTime = 1f;

        protected StateChecker stateChecker;
        protected AIController _controller;
        protected Navigation navigation;

        private bool InIdlePlace = false;
        private List<Vector2> currentTarget;
        private Vector3 lastSeenPos;

        private float lastSeenTimer;


        public void Start()
        {
            stateChecker = GetComponent<StateChecker>();
            _controller = GetComponent<Controlls>().input as AIController;
            navigation = GetComponent<Navigation>();

            currentTarget = new List<Vector2>();
            currentTarget.Add(stationPoint.position);
        }

        public void FixedUpdate()
        {
            Vector2 currentPosition = transform.position;

            if (stateChecker.IsFollowing)
            {
                InIdlePlace = false;
                lastSeenTimer += Time.fixedDeltaTime;
                if (lastSeenTimer > trailTime)
                    LoseTargetFollow();
                else
                    FollowTarget(_Player.position);

                Debug.Log("Following");
                
            }
            else if (!InIdlePlace)
            {
                HeadToIdle();
                Debug.Log("Heading to Idle");
            }
            else
            {
                // idle action walking
            }


            if (DestinationReached(currentTarget.Last()))
            {
                if (currentTarget.Count != 1)
                {
                    currentTarget.RemoveAt(currentTarget.Count - 1);
                }
                else
                {
                    Debug.Log("Destination reached");
                    _controller.HorizontalInput = 0;
                    _controller.VerticalInput = 0;
                    return;
                }
            }

            if (!navigation.CanGoToPoint(currentTarget[0]))
            {
                Debug.Log("Deciding MidPoint");
                Vector2? midpoint = navigation.FindMidPoint(currentTarget[0]);
                if (midpoint != null)
                {
                    currentTarget.Add((Vector2)midpoint);
                }
                else
                {
                    LoseTargetFollow();
                    _controller.HorizontalInput = 0;
                    _controller.VerticalInput = 0;
                    return;
                }
            }
            else
            {
                Vector2 tmp = currentTarget[0];
                currentTarget.Clear();
                currentTarget.Add(tmp);
            }

            Vector2 _input = GetInputFromDirection(currentTarget.Last() - currentPosition);
            Debug.DrawRay(transform.position, _input);
            _controller.HorizontalInput = _input.x;
            _controller.VerticalInput = _input.y;


            Debug.DrawLine(transform.position, currentTarget.Last(), Color.magenta);
        }


        private void HeadToIdle()
        {
            if (DestinationReached(currentTarget.Last()) && currentTarget.Count == 1)
            {
                InIdlePlace = true;
                Debug.Log("Idle destination reached");
            }
            currentTarget[0] = stationPoint.position;
        }

        private void FollowTarget(Vector3 targetPosition)
        {
            if (stateChecker.RaycastForTarget(targetPosition))
            {
                lastSeenPos = targetPosition;
                lastSeenTimer = 0;
            }

            currentTarget[0] = lastSeenPos;
        }

        private void LoseTargetFollow()
        {
            stateChecker.IsFollowing = false;
            currentTarget[0] = stationPoint.position;
            lastSeenTimer = 0;
        }

        private Vector2 GetInputFromDirection(Vector2 direction)
        {
            float angle = Vector2.SignedAngle(Vector2.right, direction);
            if (angle < 0)
                angle = 360 + angle;
            

            int selectedAngle = 0;

            for (int i = 0; i <= 360; i += 45)
            {
                if (Mathf.Abs(i - angle) < 22.5f)
                    selectedAngle = i;
            }

            // returns a vector needed to get to the angle
            return Quaternion.Euler(0, 0, selectedAngle) * Vector2.right;
        }


        private bool DestinationReached(Vector2 destination)
        {
            Vector2 thisPosition = transform.position;
         
            if ((thisPosition - destination).sqrMagnitude < 1f)
                return true;

            return false;
        }

    }
}


















/*
 could get Vector from angle with this
public Vector2 AngleToVector2D(float angle)
{
    return Quaternion.Euler(0, 0, angle) * Vector2.right;
}
but quaternions fufu
*/
//private Dictionary<int, Vector2> angleDirectionMap = new Dictionary<int, Vector2>()
//{
//    { 0, Vector2.right},
//    { 45, Vector2.one},
//    { 90, Vector2.up },
//    { 135, new Vector2(-1, 1) },
//    { 180, Vector2.left },
//    { 225, new Vector2(-1, -1) },
//    { 270, Vector2.down }, 
//    { 315, new Vector2(1, -1) },
//    { 360, Vector2.right },
//};

