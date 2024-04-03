using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI
{
    public class Navigation : MonoBehaviour
    {
        [SerializeField] private int searchGridSize = 5;
        [SerializeField] private float gridPointDistance = 0.35f;

        private BoxCollider2D _hitbox;
        private ContactFilter2D ignoreBoundsFilter = new ContactFilter2D();


        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.red;
            //List<Vector2> visiblePoints = VisiblePoints();
            //Debug.LogWarning(visiblePoints.Count);
            //foreach (Vector2 point in visiblePoints)
            //{
            //    Gizmos.DrawSphere(point, 0.1f);
            //    Debug.Log(point);
            //}
            float startingPointX = transform.position.x - gridPointDistance * (searchGridSize / 2);
            float startingPointY = transform.position.y - gridPointDistance * (searchGridSize / 2);
            Vector2 currentPoint;
            for (int i = 0; i < searchGridSize; i++)
            {
                currentPoint.x = startingPointX + i * gridPointDistance;
                for (int j = 0; j < searchGridSize; j++)
                {
                    currentPoint.y = startingPointY + j * gridPointDistance;
                    Gizmos.DrawSphere(currentPoint, 0.1f);
                }
            }
        }

        public void Awake()
        {
            _hitbox = GetComponent<BoxCollider2D>();
            LayerMask ignoreBoundsLayer = ~(1 << LayerMask.NameToLayer("Bounds"));
            ignoreBoundsLayer &= ~(1 << LayerMask.NameToLayer("Ignore Raycast"));
            ignoreBoundsLayer &= ~(1 << LayerMask.NameToLayer("Player"));
            ignoreBoundsFilter.useLayerMask = true;
            ignoreBoundsFilter.SetLayerMask(ignoreBoundsLayer);
        }

        public Vector2? FindMidPoint(Vector2 destination)
        {
            Vector2 closestPoint = Vector2.positiveInfinity;
            float closestDistance = float.MaxValue;
            List<Vector2> visiblePoints = VisiblePoints();

            for (int i = 0; i < visiblePoints.Count; i++)
            {
                float currentDistance = (visiblePoints[i] - destination).sqrMagnitude;
                if (CanGoToPoint(visiblePoints[i], destination) && currentDistance < closestDistance) 
                {
                    if (DestinationReached(visiblePoints[i]))
                        continue;
                    closestPoint = visiblePoints[i];
                    closestDistance = currentDistance;
                }
            }

            if (closestPoint != Vector2.positiveInfinity)
                return closestPoint;
            return null;
        }


        public bool CanGoToPoint(Vector2 destination, Vector2? sourcePosition=null)
        {
            if (GetContactPoint(destination, sourcePosition) == null) 
                return true;

            return false;
        }

        public Vector2? GetContactPoint(Vector2 destination, Vector2? sourcePosition=null)
        {
            if (sourcePosition == null)
                sourcePosition = transform.position;
            Vector2 _sourcePosition = (Vector2)sourcePosition;

            RaycastHit2D[] _results = new RaycastHit2D[2];

            Vector2 _direction = (destination - _sourcePosition);

            if (_hitbox.Cast(_direction, ignoreBoundsFilter, _results, Vector2.Distance(_sourcePosition, destination)) != 0)
            {
                foreach (RaycastHit2D r in _results)
                {
                    if (r == false) break;
                    return r.point;
                }
            }

            return null;
        }


        public bool DestinationReached(Vector2 destination)
        {
            Vector2 thisPosition = transform.position;

            if ((thisPosition - destination).sqrMagnitude < 1f)
                return true;

            return false;
        }

        public List<Vector2> VisiblePoints()
        {
            List<Vector2> possiblePoints = new List<Vector2>(searchGridSize * searchGridSize);

            float startingPointX = transform.position.x - gridPointDistance * (searchGridSize / 2);
            float startingPointY = transform.position.y - gridPointDistance * (searchGridSize / 2);
            Vector2 currentPoint;
            for (int i = 0; i < searchGridSize; i++)
            {
                currentPoint.x = startingPointX + i * gridPointDistance;
                for (int j = 0; j < searchGridSize; j++)
                {
                    currentPoint.y = startingPointY + j * gridPointDistance;
                    if (CanGoToPoint(currentPoint))
                    {
                        possiblePoints.Add(currentPoint);
                    }
                }
            }
            return possiblePoints;
        }


    }

}
