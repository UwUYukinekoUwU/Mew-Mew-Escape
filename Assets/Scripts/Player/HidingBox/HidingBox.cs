using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

public class HidingBox : MonoBehaviour
{
    public float Duration = 15.0f;
    [SerializeField]
    private GameObject player;

    private PlayerController _controller;
    private float timer = 0f;

    public void Start()
    {
        _controller = GetComponent<Controlls>().input as PlayerController;
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;

        Game.mainCamera.Follow = GetComponent<Transform>();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > Duration || _controller.Interact())
        {
            Destroy(gameObject);
            player.transform.position = gameObject.transform.position;
            player.SetActive(true);
            Game.mainCamera.Follow = player.transform;
        }
    }
}
