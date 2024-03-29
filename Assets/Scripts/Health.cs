using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int Lives = 9;
    [SerializeField] private float immortalityTime = 2f;

    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private float _timer;
    private bool _hurting;


    public void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate()
    {
        if (GameM.Game.Paused)
            return;

        if (!_hurting)
            return;


        if (_timer < immortalityTime)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            _hurting = false;
            _collider.enabled = true;
            _spriteRenderer.color = Color.white; //TODO delete later when an animation is finihsed
        }
    }

    public void DoDamage(int damage)
    {
        if (_hurting)
            return;

        _hurting = true;
        _collider.enabled = false;
        _timer = 0;
        HurtAnimation();
        Lives -= damage;
        Debug.LogWarning(Lives);
        if (Lives <= 0)
            GetKilled();
    }

    protected virtual void GetKilled()
    {
        Debug.Log("killed " + gameObject.name);
        Destroy(gameObject);
    }


    protected virtual void HurtAnimation()
    {
        _spriteRenderer.color = Color.red;
        //IEnumerator WhiteRedFlicker()
        //{
        //    while ()
        //}
    }
}
