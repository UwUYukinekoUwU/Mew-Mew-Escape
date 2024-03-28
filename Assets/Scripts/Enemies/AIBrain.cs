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


        [HideInInspector]
        public Navigation navigation;
        protected StateChecker stateChecker;
        protected AIController _controller;
        protected bool InIdlePlace = false;
        protected float lastSeenTimer;

        private List<Vector2> currentTarget;
        private Vector3 lastSeenPos;



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
            Debug.DrawLine(transform.position, currentTarget[0], Color.blue);

            if (navigation.DestinationReached(currentTarget.Last()))
            {
                if (currentTarget.Count != 1)
                {
                    Debug.Log("cleared target list");
                    TrimTargetList();
                    _controller.HorizontalInput = 0;
                    _controller.VerticalInput = 0;
                }
                else
                {
                    Debug.Log("Destination reached");
                    _controller.HorizontalInput = 0;
                    _controller.VerticalInput = 0;
                    return;
                }
            }

            else if (!navigation.CanGoToPoint(currentTarget[0]))
            {
                //Debug.Log("Deciding MidPoint");
                Vector2? midpoint = navigation.FindMidPoint(currentTarget[0]);
                if (midpoint != null)
                {
                    currentTarget.Add((Vector2)midpoint);
                    //Debug.LogWarning(currentTarget.Count + " " + navigation.DestinationReached((Vector2)midpoint));
                }
                else
                {
                    LoseTargetFollow();
                    _controller.HorizontalInput = 0;
                    _controller.VerticalInput = 0;
                    Debug.LogWarning("midpoint was null");
                    return;
                }
                //Debug.Log("Destination reached " + navigation.DestinationReached((Vector2)midpoint)
                //    + " " + midpoint);
            }
            else
            {
                Vector2 tmp = currentTarget[0];
                currentTarget.Clear();
                currentTarget.Add(tmp);
            }

            Vector2 _input = GetInputFromDirection(currentTarget.Last() - currentPosition);
            _controller.HorizontalInput = _input.x;
            _controller.VerticalInput = _input.y;


            Debug.DrawLine(transform.position, currentTarget.Last(), Color.magenta);
        }


        public void OnDisable()
        {
            _controller.HorizontalInput = 0;
            _controller.VerticalInput = 0;
            TrimTargetList();
        }


        public void HeadToIdle()
        {
            if (navigation.DestinationReached(currentTarget.Last()) && currentTarget.Count == 1)
            {
                InIdlePlace = true;
                Debug.Log("Idle destination reached");
            }
            currentTarget[0] = stationPoint.position;
        }

        public void FollowTarget(Vector3 targetPosition, string targetTag = null)
        {
            if (stateChecker.RaycastForTarget(targetPosition, targetTag))
            {
                lastSeenPos = targetPosition;
                lastSeenTimer = 0;
            }
            else Debug.LogWarning("Raycast for target failed");

            currentTarget[0] = lastSeenPos;
        }

        public void LoseTargetFollow()
        {
            stateChecker.IsFollowing = false;
            currentTarget[0] = stationPoint.position;
            lastSeenTimer = 0;
        }

        private void TrimTargetList()
        {
            if (currentTarget.Count <= 1)
                return;

            Vector2 tmp = currentTarget[0];
            currentTarget.Clear();
            currentTarget.Add(tmp);
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

            //// returns a vector needed to get to the angle
            //return Quaternion.Euler(0, 0, selectedAngle) * Vector2.right;
            return direction.normalized;
            //return new Vector2(Mathf.Cos(selectedAngle), Mathf.Sin(selectedAngle));
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

