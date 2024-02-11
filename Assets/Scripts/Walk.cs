
using UnityEngine;


public class Walk : MonoBehaviour
{
    public float Speed = 1;
    [SerializeField]
    private BoxCollider2D _hitbox;


    private float _verticalInput;
    private float _horizontalInput;

    private InputController _controller;
    private Vector2 _direction;
    private ContactFilter2D triggerCollidersFilter = new ContactFilter2D();

    void Start()
    {
        _controller = GetComponent<ControllerHolder>().input;
        _hitbox = GetComponent<BoxCollider2D>();
        triggerCollidersFilter.useTriggers = false;
    }

    public void Update()
    {
        _horizontalInput = _controller.GetHorizontalInput();
        _verticalInput = _controller.GetVerticalInput();

        Move();
    }

    /// <summary>
    /// Raycasts if the player can move and if yes, moves him using the Translate() method.
    /// This is because otherwise player glitches in walls. Translate() is broken.
    /// </summary>
    private void Move()
    {
        RaycastHit2D[] _results = new RaycastHit2D[2];

        //check x 
        _direction = new Vector2(_horizontalInput, 0);
        if (_hitbox.Cast(_direction, triggerCollidersFilter, _results, Speed * Time.deltaTime + 0.01f) != 0)
        {
            foreach (RaycastHit2D r in _results)
            {
                if (r == false) break;
                _horizontalInput = 0;
            }
        }

        //check y
        _direction = new Vector2(0, _verticalInput);
        if (_hitbox.Cast(_direction, triggerCollidersFilter, _results, Speed * Time.deltaTime + 0.01f) != 0)
        {
            foreach (RaycastHit2D r in _results)
            {
                if (r == false) break;
                _verticalInput = 0;
            }
        }

        //perform the move
        transform.Translate(_horizontalInput * Speed * Time.deltaTime, _verticalInput * Speed * Time.deltaTime, 0f);
    }
}

