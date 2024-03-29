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
        private bool _finishedLerping;

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
            // always detect and hurt the player, even when not attacking
            if (!_attacking && collision.gameObject.tag != "Player")
                return;

            if (collision.gameObject.TryGetComponent(out _targetHealth))
                _targetHealth.DoDamage(damage);
        }


        private IEnumerator DashAttack()
        {
            Vector2 currentPosition = transform.position;

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

            // first get to prepare point
            StartCoroutine(TransformByLerp(startPoint, preparePoint, prepareTime));
            yield return new WaitUntil(() => _finishedLerping);
            _finishedLerping = false;

            // then jump after the player
            StartCoroutine(TransformByLerp(preparePoint, endPoint, attackTime));
            yield return new WaitUntil(() => _finishedLerping);
            _finishedLerping = false;

            _aiBrain.enabled = true;
            _attacking = false;
        }

        private IEnumerator TransformByLerp(Vector2 startPoint, Vector2 endPoint, float _time)
        {
            Vector2 moveVector;
            float elapsedTime = 0f;
            while (elapsedTime < _time)
            {
                elapsedTime += Time.deltaTime;

                moveVector.x = Mathf.Lerp(startPoint.x, endPoint.x, (elapsedTime / _time));
                moveVector.y = Mathf.Lerp(startPoint.y, endPoint.y, (elapsedTime / _time));

                transform.position = moveVector;

                yield return null;
            }
            _finishedLerping = true;
        }

    }
}

