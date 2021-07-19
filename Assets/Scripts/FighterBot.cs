using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FighterBot : Fighter
{
    private Transform _playerTransform;

    private void Start()
    {
        _thisTransform = transform;
        _animator = GetComponent<Animator>();
        _playerTransform = FindObjectOfType<Fighter>().transform;
        SetRagdollState(this.gameObject, false);
    }

    private void Update()
    {
        if (!isStunned)
        {
            _thisTransform.LookAt(_playerTransform);
            if (isAbleToAttack)
            {
                _playerTransform.GetComponent<Fighter>().ReceiveDamage(10);
                Instantiate(_hitPrefab, _playerTransform.position + new Vector3(0, 3, 0.72f), Quaternion.identity, _playerTransform);
            }
            AnimatorControl(_animator, false);
        }
    }
}