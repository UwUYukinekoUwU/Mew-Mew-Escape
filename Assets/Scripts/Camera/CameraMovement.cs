using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static GameM;


public class CameraMovement : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float followOffset = 1;
    [SerializeField] private float _flipRotationTime = 0.5f;

    [Header("References")]
    [SerializeField] private InputController _input;

    private CinemachineVirtualCamera mainCamera;
    private Vector2 followVector = Vector2.zero;
    private CinemachineFramingTransposer _cinemachineTransposer;
    private Coroutine _pointTransitionCoroutine;

    public void Start()
    {
        mainCamera = Game.GetComponentByName<CinemachineVirtualCamera>("Virtual Camera");
        mainCamera.Follow = Game.GetComponentByName<Transform>("Player");
        _cinemachineTransposer = mainCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void FixedUpdate()
    {
        if (Game.PlayerBusy) return;

        float currentHorizontal = _input.GetHorizontalInput();
        float currentVertical = _input.GetVerticalInput();
        Vector2 currentVector = Vector2.zero;

        if (currentHorizontal != 0)
        {
            followVector.x = Mathf.Sign(currentHorizontal) * followOffset;
            followVector.y = 0;
        }
        else if (currentVertical != 0)
        {
            followVector.y = Mathf.Sign(currentVertical) * followOffset;
            followVector.x = 0;
        }
        
        if (currentVector != followVector)
        {
            if (_pointTransitionCoroutine != null)
                StopCoroutine(_pointTransitionCoroutine);
            _pointTransitionCoroutine = StartCoroutine("ChangeFollowPoint");
        }

        currentVector = followVector;
    }


    private IEnumerator ChangeFollowPoint()
    {
        Vector2 startPoint = _cinemachineTransposer.m_TrackedObjectOffset;
        Vector2 endPoint = followVector;
        Vector2 moveVector;

        float elapsedTime = 0f;
        while (elapsedTime < _flipRotationTime)
        {
            elapsedTime += Time.deltaTime;

            moveVector.x = Mathf.Lerp(startPoint.x, endPoint.x, (elapsedTime / _flipRotationTime));
            moveVector.y = Mathf.Lerp(startPoint.y, endPoint.y, (elapsedTime / _flipRotationTime));

            _cinemachineTransposer.m_TrackedObjectOffset = moveVector;

            yield return null;
        }
    }
}

