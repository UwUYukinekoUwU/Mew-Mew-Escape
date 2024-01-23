using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private Transform _transform;
    private float current_horizontal = 0;
    private float current_vertical = 0;
    void Start()
    {
        _transform = GetComponent<Transform>();        
    }

    public void Update()
    {
        current_horizontal = Input.GetAxis("Horizontal");
        current_vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        _transform.Translate(current_horizontal * speed, current_vertical * speed, 0);
    }
}
