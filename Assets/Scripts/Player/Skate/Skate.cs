using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

/// <summary>
/// Child of the Walk class. Responsible for custom movement logic when skating.
/// </summary>
public class Skate : Walk, ITransformItem
{
    [Header("References")]
    [SerializeField] private GameObject _player;

    [Header("Parameters")]
    [SerializeField] private float stunDuration = 3f;
    [SerializeField] private int damage = 1;

    private PlayerController _playerController;
    private PlayerWalk _playerWalk;
    private PlayerHealth _playerHealth;
    private SkateAnimationHandler _skateAnimationHandler;
    private BoxCollider2D _skateboardCollider;
    private CinemachineVirtualCamera mainCamera;

    private Vector2 horizontalColliderSize = new Vector2(1.155813f, 0.9104733f);
    private Vector2 verticalColliderSize = new Vector2(0.7302036f, 0.9872522f);

    private Vector2 sidewaysColliderOffset = new Vector2(0.03432083f, -0.2417591f);
    private Vector2 downwardsColliderOffset = new Vector2(0.02771473f, -0.04217005f);
    private Vector2 upwardsColliderOffset = new Vector2(0.02695036f, 0.04542518f);

    public new void Start()
    {
        base.Start();

        _playerController = base._controller as PlayerController;
        _skateAnimationHandler = GetComponent<SkateAnimationHandler>();
        _skateAnimationHandler._InactiveSkateboard = false;
        _skateboardCollider = GetComponent<BoxCollider2D>();
        _playerWalk = _player.GetComponent<PlayerWalk>();
        _playerHealth = _player.GetComponent<PlayerHealth>();
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;

        mainCamera = Game.GetComponentByName<CinemachineVirtualCamera>("Virtual Camera");
        mainCamera.Follow = GetComponent<Transform>();
    }

    public new void Update()
    {
        //gather input
        float current_vertical = _playerController.GetVerticalInput();
        float current_horizontal = _playerController.GetHorizontalInput();

        if (current_vertical != 0)
        {
            verticalInput = current_vertical;
            horizontalInput = 0;
        }

        if (current_horizontal != 0)
        {
            horizontalInput = current_horizontal;
            verticalInput = 0;
        }


        // flip the sprite of the object
        if (horizontalInput != 0)
            transform.localScale = new Vector3(
                Mathf.Sign(horizontalInput) * Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );

        //animation + collider size changes
        _skateAnimationHandler._SkatingSideways = false;
        _skateAnimationHandler._SkatingUpwards = false;
        _skateAnimationHandler._SkatingDownwards = false;

        if (verticalInput > 0)
        {
            _skateAnimationHandler._SkatingUpwards = true;
            _skateboardCollider.offset = upwardsColliderOffset;
            _skateboardCollider.size = verticalColliderSize;
        }
        if (verticalInput < 0)
        {
            _skateAnimationHandler._SkatingDownwards = true;
            _skateboardCollider.offset = downwardsColliderOffset;
            _skateboardCollider.size = verticalColliderSize;
        }

        if (horizontalInput != 0)
        {
            _skateAnimationHandler._SkatingSideways = true;
            _skateboardCollider.offset = sidewaysColliderOffset;
            _skateboardCollider.size = horizontalColliderSize;
        }

        Move();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) return;

        Health _targetHealth;

        if (collision.gameObject.TryGetComponent(out _targetHealth))
        {
            _targetHealth.DoDamage(damage);
            _playerHealth.DoDamage(damage);
        }


        Destroy(gameObject);
        _player.transform.position = gameObject.transform.position;
        _player.SetActive(true);
        mainCamera.Follow = _player.transform;

        //stun the player for a while
        _playerWalk.Stun(stunDuration);
    }


    public void GetPlayerReference()
    {
        if (_player == null)
            _player = Game.GetComponentByName<GameObject>("Player");
    }

    public void EnablePlayer()
    {
        _player.SetActive(true);
    }
}
