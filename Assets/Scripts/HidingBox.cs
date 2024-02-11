using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingBox : MonoBehaviour
{
    public float Duration = 15.0f;
    [SerializeField]
    private GameObject player;

    private PlayerController _controller;
    private float timer = 0f;

    public void Start()
    {
        _controller = GetComponent<ControllerHolder>().input as PlayerController;
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > Duration || _controller.Interact())
        {
            Destroy(gameObject);
            player.transform.position = gameObject.transform.position;
            player.SetActive(true);
        }
    }
}
