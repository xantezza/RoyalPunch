using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : AbstractCharacter
{
    private Transform _thisTransform;

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider);
    }

    public void OnCollisionEnter(Collision collider)
    {
        Debug.Log(collider);
    }

    private void Start()
    {
        SetRagdollState(this.gameObject, false);
        _thisTransform = transform;
        _animator = GetComponent<Animator>();

        SetMaxHealth();
    }

    private void FixedUpdate()
    {
        if (!isStunned)
        {
            _thisTransform.LookAt(_playerTransform);

            AnimatorControl(_animator, new Vector2(0, 0));
        }
    }
}