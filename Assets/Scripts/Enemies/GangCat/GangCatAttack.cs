using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AI
{
    public class GangCatAttack : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _player;

        [Header("Parameters")]
        [SerializeField] private float dashAttackRange = 5f;
        [SerializeField] private float prepareTime = 0.2f;
        [SerializeField] private float attackTime = 2.5f;
        [SerializeField] private float dashInertia = 0.3f;
        [SerializeField] private float dashPrepareDistance = 0.1f;
        [SerializeField] private int damage = 1;


        private AIBrain _aiBrain;
        private StateChecker _stateChecker;
        private Health _targetHealth;
        private Vector2 _distance;
        private Vector2 _playerPoint;
        private bool _attacking;

        public void Start()
        {
            _aiBrain = GetComponent<AIBrain>();
            _stateChecker = GetComponent<StateChecker>();
        }


        public void Update()
        {
            if (GameM.Game.Paused)
                return;

            if (!_stateChecker.IsFollowing)
                return;

            if (_attacking)
                return;

            _distance = _player.position - transform.position;
            if (_distance.magnitude > dashAttackRange)
                return;


            _aiBrain.enabled = false;
            _attacking = true;
            _playerPoint = _player.transform.position;
            StartCoroutine(DashAttack());
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            // always detect and hurt the player
            if (!_attacking)
            {
                if (collision.gameObject.tag != "Player")
                    return;
                if (gameObject.TryGetComponent(out _targetHealth))
                    _targetHealth.DoDamage(damage);
                return;
            }

            // damage everything when attacking
            if (collision.gameObject.tag != "Enemy" && collision.gameObject.tag != "Hitable")
                return;

            if(gameObject.TryGetComponent(out _targetHealth))
                _targetHealth.DoDamage(damage);
        }


        private IEnumerator DashAttack()
        {
            Vector2 currentPosition = transform.position;
            Vector2 moveVector;

            Vector2 prepareDirection = (_playerPoint - currentPosition) * dashPrepareDistance * -1;
            Vector2 targetDirection = (_playerPoint - currentPosition) * (1 + dashInertia);

            Vector2 startPoint = currentPosition;
            Vector2 preparePoint = startPoint + prepareDirection;
            Vector2 endPoint = preparePoint + targetDirection;

            Vector2? contactPoint = _aiBrain.navigation.GetContactPoint(endPoint);
            if (contactPoint != null)
                endPoint = (Vector2)contactPoint;

            Debug.DrawLine(startPoint, preparePoint, Color.yellow);
            Debug.DrawLine(preparePoint, endPoint, Color.cyan);

            float elapsedTime = 0f;
            while (elapsedTime < prepareTime)
            {
                elapsedTime += Time.deltaTime;

                moveVector.x = Mathf.Lerp(startPoint.x, preparePoint.x, (elapsedTime / prepareTime));
                moveVector.y = Mathf.Lerp(startPoint.y, preparePoint.y, (elapsedTime / prepareTime));

                transform.position = moveVector;

                Debug.DrawLine(startPoint, preparePoint, Color.yellow);
                Debug.DrawLine(preparePoint, endPoint, Color.cyan);

                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < attackTime)
            {
                elapsedTime += Time.deltaTime;

                moveVector.x = Mathf.Lerp(preparePoint.x, endPoint.x, (elapsedTime / attackTime));
                moveVector.y = Mathf.Lerp(preparePoint.y, endPoint.y, (elapsedTime / attackTime));

                transform.position = moveVector;

                Debug.DrawLine(startPoint, preparePoint, Color.yellow);
                Debug.DrawLine(preparePoint, endPoint, Color.cyan);

                yield return null;
            }
            _aiBrain.enabled = true;
            _attacking = false;
        }

        //private IEnumerator LerpPoints(Vector2 startPoint, Vector2 endPoint, prepareTime)

    }
}

