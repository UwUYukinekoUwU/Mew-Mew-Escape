using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : Walk
{
    [SerializeField]
    private GameObject _player;
    private PlayerController _playerController;
    private SkateAnimationHandler _skateAnimationHandler;
    private BoxCollider2D _skateboardCollider;

    private Vector2 horizontalColliderSize = new Vector2(0.16f, 0.12f);
    private Vector2 verticalColliderSize = new Vector2(0.14f, 0.16f);

    private Vector2 sidewaysColliderOffset = new Vector2(0.01435965f, -0.03722871f);
    private Vector2 downwardsColliderOffset = new Vector2(0.003526352f, -0.01556215f);
    private Vector2 upwardsColliderOffset = new Vector2(0.01f, 0.02f);

    public new void Start()
    {
        base.Start();
        _playerController = base._controller as PlayerController;
        _skateAnimationHandler = GetComponent<SkateAnimationHandler>();
        _skateAnimationHandler._InactiveSkateboard = false;
        _skateboardCollider = GetComponent<BoxCollider2D>();
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


        //move
        Move();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!enabled) return;

        Destroy(gameObject);
        _player.transform.position = gameObject.transform.position;
        _player.SetActive(true);
    }


}
