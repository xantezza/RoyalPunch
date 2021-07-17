using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected float _farPlayerZPosition = -4.3f;
    protected float _nearPlayerZPosition = -0.8f;
    protected Animator _animator;
    [HideInInspector] public bool isStunned = false;

    protected void SetRagdollState(GameObject gameObject, bool isRagdoll)
    {
        foreach (var collider in gameObject.GetComponentsInChildren<Collider>())
        {
            if (collider.CompareTag("BoxingGlove"))
                collider.isTrigger = false;
            else
                collider.isTrigger = !isRagdoll;
        }
        foreach (var rigidBody in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.isKinematic = !isRagdoll;
            rigidBody.useGravity = isRagdoll;
        }
        if (gameObject.TryGetComponent<Animator>(out var animator))
        {
            animator.enabled = !isRagdoll;
        }
    }

    protected void AnimatorControl(GameObject _this, Vector2 input, Vector3 playerPosition)
    {
        if (_nearPlayerZPosition - playerPosition.z < 0.1f)
        {
            _animator.Play("Punch");
        }
        else if (input.magnitude != 0)
        {
            _animator.Play("Run");
        }
        else
            _animator.Play("Idle");
    }
}