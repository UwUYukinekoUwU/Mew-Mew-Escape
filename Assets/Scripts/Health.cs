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

    public void DoDamage(int damage)
    {
        if (_hurting)
        {
            if (_timer < immortalityTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _hurting = false;
                _collider.enabled = true;
            }
                
            return;
        }

        _hurting = true;
        _collider.enabled = false;
        _timer = 0;
        HurtAnimation();
        Lives -= damage;
        if (Lives <= 0)
            GetKilled();
    }

    protected void GetKilled()
    {
        Debug.Log("killed " + gameObject.name);
        Destroy(gameObject);
    }


    protected void HurtAnimation()
    {
        _spriteRenderer.color = Color.red;
        //IEnumerator WhiteRedFlicker()
        //{
        //    while ()
        //}
    }
}
