using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameM;

/// <summary>
/// Custom logic for HidingBox.
/// </summary>
public class HidingBox : MonoBehaviour
{
    public float Duration = 15.0f;

    [Header("References")]
    [SerializeField] private GameObject player;

    private CinemachineVirtualCamera mainCamera;
    private PlayerController _controller;
    private float timer = 0f;

    public void Start()
    {
        _controller = GetComponent<Controlls>().input as PlayerController;
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        _rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;

        mainCamera = Game.GetComponentByName<CinemachineVirtualCamera>("Virtual Camera");
        mainCamera.Follow = GetComponent<Transform>();
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > Duration || _controller.Interact())
        {
            Destroy(gameObject);
            player.transform.position = gameObject.transform.position;
            player.SetActive(true);
            mainCamera.Follow = player.transform;
        }
    }
}
