using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static GameM;

/// <summary>
/// Camera moves using cinemachine, but this ensures a smooth transition of the tracked object offset.
/// </summary>
public class CameraMovement : MonoBehaviour
{
    [Header("Parameters")]
    public float FollowOffset = 1f;
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
            followVector.x = Mathf.Sign(currentHorizontal) * FollowOffset;
            followVector.y = 0;
        }
        else if (currentVertical != 0)
        {
            followVector.y = Mathf.Sign(currentVertical) * FollowOffset;
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

    /// <summary>
    /// Camera is always facing a bit in front of the player, and tracked object offset property is responsible for that.
    /// Once we rotate our player though, don't want the tracked object offset to snap along, so it smoothly transitions to its new
    /// position, using Mathf.Lerp (just a simple linear interpolation).
    /// </summary>
    /// <returns></returns>
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

