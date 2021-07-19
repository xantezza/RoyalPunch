using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;
    [SerializeField] private VariableJoystick _joystick;

    [SerializeField] protected GameObject _hitPrefab;
    [SerializeField] private protected HealthBar _healthBar;
    [SerializeField] protected float _health;

    internal bool isStunned = false;

    private protected Animator _animator;
    private protected Transform _thisTransform;
    private protected static bool isAbleToAttack = false;

    private void Awake()
    {
        _healthBar.SetMaxHealth(_health);
        _thisTransform = transform;
        _animator = GetComponent<Animator>();

        SetRagdollState(this.gameObject, false);
    }

    private void Update()
    {
        if (!isStunned)
        {
            bool isAbleToAttack = GeneralData._nearPlayerZPosition - _thisTransform.localPosition.z < 0.1f;

            var input = _joystick.Direction;

            MovementControl(input);
            AnimatorControl(_animator, input.magnitude != 0);
        }
    }

    public void ReceiveDamage(float value)
    {
        _health -= value;
        _healthBar.SetHealth(_health);
    }

    private void MovementControl(Vector2 input)
    {
        _parentTransform.Rotate(Vector3.up, input.x / 3f * -1);

        _thisTransform.localPosition += new Vector3(0, 0, input.y / 25f);

        _thisTransform.localPosition = new Vector3(0, 0.5F, Mathf.Clamp(
            _thisTransform.localPosition.z,
            GeneralData._farPlayerZPosition,
            GeneralData._nearPlayerZPosition
            ));
    }

    private protected void AnimatorControl(Animator animator, bool isMoving)
    {
        animator.SetBool("isAtacking", isAbleToAttack);
        animator.SetBool("isMoving", isMoving);
    }

    private protected void SetRagdollState(GameObject gameObject, bool isRagdoll)
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
}