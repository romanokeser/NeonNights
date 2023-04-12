using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovementController : MonoBehaviour
{
    [SerializeField] private float _rearForceMultiplier;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _carLength;
    [SerializeField] private Transform _rearEndOfCar;


    private Rigidbody _rigidbody;
    private Transform _transform;
    private bool _isMoving;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        IsTransformMoving();
    }

    private bool IsTransformMoving()
    {
        if (_transform.hasChanged)
            return true;
        else
            return false;
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the force direction based on the orientation of the car
        Vector3 forceDirection = _transform.forward;
        if (horizontalInput < 0)
        {
            forceDirection = -_transform.right;
        }
        else if (horizontalInput > 0)
        {
            forceDirection = _transform.right;
        }

        // Apply the force to the back of the car
        Vector3 rearForce = _rearForceMultiplier * forceDirection;
        _rigidbody.AddForceAtPosition(rearForce, _rearEndOfCar.position - _transform.forward * _carLength);

        // Clamp the velocity to the maximum speed
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
    }
}
