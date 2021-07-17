using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private Transform _thisTransform;
    public Transform _parentTransform;
    public VariableJoystick _joystick;

    private void Awake()
    {
        SetRagdollState(this.gameObject, false);
    }

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
        _thisTransform = transform;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isStunned)
        {
            Vector2 input = _joystick.Direction;

            MovementControl(input);

            AnimatorControl(this.gameObject, input, _thisTransform.localPosition);
        }
    }

    private void MovementControl(Vector2 input)
    {
        _parentTransform.Rotate(Vector3.up, input.x / 3f * -1);

        _thisTransform.localPosition += new Vector3(0, 0, input.y / 25f);

        _thisTransform.localPosition = new Vector3(0, 0.5F, Mathf.Clamp(
            _thisTransform.localPosition.z,
            _farPlayerZPosition,
            _nearPlayerZPosition
            ));
    }
}