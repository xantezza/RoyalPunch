using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{
    private Transform _thisTransform;
    private Transform _playerTransform;

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
        _playerTransform = FindObjectOfType<PlayerCharacter>().transform;
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isStunned)
        {
            _thisTransform.LookAt(_playerTransform);

            AnimatorControl(this.gameObject, new Vector2(0, 0), _playerTransform.localPosition);
        }
    }
}