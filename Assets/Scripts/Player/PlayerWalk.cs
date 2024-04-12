using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using static GameM;

/// <summary>
/// Child of the Walk class. Responsible for player's animation and sprint logic.
/// </summary>
public class PlayerWalk : Walk
{
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float fullSprintDuration = 10f;
    [SerializeField] private float newCameraOffset = 2f;

    private PlayerAnimationHandler _handler;
    private Image _sprintBarImage;
    private PlayerController _playerController;
    private CameraMovement _cameraMovement;
    private float _originalSpeed;
    private float _originalCameraOffset;
    private float _sprintEnergy;

    public new void Start()
    {
        base.Start();
        _handler = GetComponent<PlayerAnimationHandler>();
        _cameraMovement = Game.GetComponentByName<CameraMovement>("Main Camera");
        _sprintBarImage = Game.GetComponentByName<Image>("SprintBar");
        _playerController = _controller as PlayerController;

        _originalSpeed = Speed;
        _originalCameraOffset = _cameraMovement.FollowOffset;
        _sprintEnergy = fullSprintDuration;
    }

    public new void Update()
    {
        if (Game.PlayerBusy)
            return;

        base.Update();

        // sprinting logic
        if (_playerController.Sprinting() && _sprintEnergy > 0)
        {
            _sprintEnergy -= Time.deltaTime;
            Speed = sprintSpeed;
            _cameraMovement.FollowOffset = newCameraOffset;
        }
        else
        {
            if (!_playerController.Sprinting())
                _sprintEnergy += Time.deltaTime;
            if (_sprintEnergy > fullSprintDuration)
                _sprintEnergy = fullSprintDuration;

            Speed = _originalSpeed;
            _cameraMovement.FollowOffset = _originalCameraOffset;
        }
        _sprintBarImage.fillAmount = _sprintEnergy / fullSprintDuration;


        // animation
        _handler._RunningSideways = false;
        _handler._RunningUpwards = false;
        _handler._RunningDownwards = false;

        if (verticalInput > 0)
            _handler._RunningUpwards = true;
        if (verticalInput < 0)
            _handler._RunningDownwards = true;

        if (horizontalInput != 0)
            _handler._RunningSideways = true;
    }

    /// <summary>
    /// Disable player's movement for a while.
    /// </summary>
    /// <param name="duration">duration of the stun in seconds</param>
    public void Stun(float duration)
    {
        //add some starry animation here mb
        enabled = false;
        Invoke("ReEnableMovement", duration);
    }

    private void ReEnableMovement()
    {
        enabled = true;
    }
}
