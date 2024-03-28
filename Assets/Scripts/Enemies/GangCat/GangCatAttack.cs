using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AI
{
    public class GangCatAttack : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _player;

        [Header("Parameters")]
        [SerializeField] private float dashAttackRange = 1f;
        [SerializeField] private float prepareTime = 0.2f;
        [SerializeField] private float attackTime = 2.5f;
        [SerializeField] private float dashInertia = 0.3f;
        [SerializeField] private float dashPrepareDistance = 0.1f;


        private AIBrain _aiBrain;
        private Vector2 _direction;
        private Vector2 _playerPoint;
        private bool _attacking;

        public void Start()
        {
            _aiBrain = GetComponent<AIBrain>();
        }


        public void Update()
        {
            if (_attacking)
            {
                Debug.Log("Attacking");
                return;
            }

            _direction = _player.position - transform.position;
            if (_direction.magnitude > dashAttackRange)
            {
                Debug.Log("Not close enough");
                return;
            }

            _aiBrain.enabled = false;
            _attacking = true;
            _playerPoint = _player.transform.position;
            StartCoroutine(StartAttack());
        }


        private IEnumerator StartAttack()
        {
            Vector2 moveVector;
            Vector2 startPoint = transform.position;

            Vector2 preparePoint = _playerPoint - new Vector2(dashPrepareDistance, dashPrepareDistance);
            Vector2 endPoint = _playerPoint + new Vector2(dashInertia, dashInertia);

            Vector2? contactPoint = _aiBrain.navigation.GetContactPoint(endPoint);
            if (contactPoint != null)
                endPoint = (Vector2)contactPoint;

            float elapsedTime = 0f;
            while (elapsedTime < prepareTime)
            {
                elapsedTime += Time.deltaTime;

                moveVector.x = Mathf.Lerp(startPoint.x, preparePoint.x, (elapsedTime / prepareTime));
                moveVector.y = Mathf.Lerp(startPoint.y, preparePoint.x, (elapsedTime / prepareTime));

                transform.Translate(moveVector - (Vector2)transform.position);

                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < attackTime)
            {
                elapsedTime += Time.deltaTime;

                moveVector.x = Mathf.Lerp(startPoint.x, endPoint.x, (elapsedTime / attackTime));
                moveVector.y = Mathf.Lerp(startPoint.y, endPoint.y, (elapsedTime / attackTime));

                transform.Translate(moveVector - (Vector2)transform.position);

                yield return null;
            }
            _aiBrain.enabled = true;
            _attacking = false;
        }

    }
}

