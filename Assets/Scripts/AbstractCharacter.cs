using UnityEngine;

public abstract class AbstractCharacter : MonoBehaviour
{
    [SerializeField] private protected HealthBar _healthBar;
    [SerializeField] private protected float _health;
    private protected float _farPlayerZPosition = -4.3f;
    private protected float _nearPlayerZPosition = -1.3f;
    private protected static Transform _playerTransform;
    protected Animator _animator { get; set; }

    internal bool isStunned = false;

    public void ReceiveDamage(float value)
    {
        _health -= value;
        _healthBar.SetHealth(_health);
    }

    protected void SetMaxHealth()
    {
        _healthBar.SetMaxHealth(_health);
    }

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

    protected void AnimatorControl(Animator animator, Vector2 input)
    {
        animator.SetBool("isAtacking", _nearPlayerZPosition - _playerTransform.position.z < 0.1f);
        animator.SetBool("isMoving", input.magnitude != 0);
    }
}