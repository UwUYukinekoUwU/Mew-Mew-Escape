using UnityEngine;

/// <summary>
/// Child of the Walk class. Handles custom logic and animations for HidingBox.
/// </summary>
public class BoxWalk : Walk
{
    private BoxAnimationHandler _handler;

    public void Awake()
    {
        _handler = GetComponent<BoxAnimationHandler>();
    }

    public void OnEnable()
    {
        _handler._Inactive = false;
    }

    public new void Update()
    {
        base.Update();

        if (horizontalInput != 0 || verticalInput != 0)
            _handler._RunningSideways = true;
        else
            _handler._RunningSideways = false;
    }


}

