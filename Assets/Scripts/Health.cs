using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int Lives = 9;
    [SerializeField] private float immortalityTime = 2f;

    private SpriteRenderer _spriteRenderer;
    private LayerMask _originalLayermask;
    private LayerMask _ignoreCreaturesMask;
    private int thisLayer;
    private float _timer;
    private bool _hurting;



    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        thisLayer = gameObject.layer;
        _originalLayermask = Physics2D.GetLayerCollisionMask(thisLayer);
        _ignoreCreaturesMask = _originalLayermask;
        _ignoreCreaturesMask &= ~(1 << LayerMask.NameToLayer("Player"));
        _ignoreCreaturesMask &= ~(1 << LayerMask.NameToLayer("Enemy"));

        SceneManager.activeSceneChanged += ChangedActiveScene;
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
            Physics2D.SetLayerCollisionMask(thisLayer, _originalLayermask);
            _spriteRenderer.color = Color.white; //TODO delete later when an animation is finihsed
        }
    }

    public virtual void DoDamage(int damage)
    {
        if (_hurting)
            return;

        _hurting = true;
        _timer = 0;
        Physics2D.SetLayerCollisionMask(thisLayer, _ignoreCreaturesMask);
        HurtAnimation();
        Lives -= damage;
        if (Lives <= 0)
            GetKilled();
    }

    protected virtual void GetKilled()
    {
        //Debug.Log("killed " + gameObject.name);
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

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if (Physics2D.GetLayerCollisionMask(thisLayer) == _originalLayermask)
            return;
        Physics2D.SetLayerCollisionMask(thisLayer, _originalLayermask);
    }
}
