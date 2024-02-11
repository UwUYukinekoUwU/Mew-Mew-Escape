
using UnityEngine;


public class Walk : MonoBehaviour
{
    public float Speed = 1;
    [SerializeField]
    private BoxCollider2D _hitbox;


    protected float verticalInput;
    protected float horizontalInput;

    private InputController _controller;
    private Vector2 _direction;
    private ContactFilter2D triggerCollidersFilter = new ContactFilter2D();

    public void Start()
    {
        _controller = GetComponent<Controlls>().input;
        _hitbox = GetComponent<BoxCollider2D>();
        triggerCollidersFilter.useTriggers = false;
    }

    public void Update()
    {
        horizontalInput = _controller.GetHorizontalInput();
        verticalInput = _controller.GetVerticalInput();

        Move();
    }

    /// <summary>
    /// Raycasts if the player can move and if yes, moves him using the Translate() method.
    /// This is because otherwise player glitches in walls. Translate() is broken.
    /// </summary>
    protected void Move()
    {
        RaycastHit2D[] _results = new RaycastHit2D[2];

        //check x 
        _direction = new Vector2(horizontalInput, 0);
        if (_hitbox.Cast(_direction, triggerCollidersFilter, _results, Speed * Time.deltaTime + 0.01f) != 0)
        {
            foreach (RaycastHit2D r in _results)
            {
                if (r == false) break;
                horizontalInput = 0;
            }
        }

        //check y
        _direction = new Vector2(0, verticalInput);
        if (_hitbox.Cast(_direction, triggerCollidersFilter, _results, Speed * Time.deltaTime + 0.01f) != 0)
        {
            foreach (RaycastHit2D r in _results)
            {
                if (r == false) break;
                verticalInput = 0;
            }
        }

        //perform the move
        transform.Translate(horizontalInput * Speed * Time.deltaTime, verticalInput * Speed * Time.deltaTime, 0f);
    }
}

