using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private bool _isInitialized = false;
    private Rigidbody2D _rb;

    private float MovX = 1;
    private Vector2 direction;
    private float cross;
    private Vector2 velocity;
    private float thrustForce;
    private Vector2 relativeForce;

    public float thrustModifier;
    public float rotationControl;
    public float acceleration;
    public float speed;

    public Transform target;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_isInitialized)
        {
            direction = transform.position - target.position;
            direction.Normalize();
            cross = Vector3.Cross(direction, transform.right).z;
            _rb.angularVelocity = rotationControl * cross;

            velocity = transform.right * MovX * acceleration;
            _rb.AddForce(velocity);
            thrustForce = Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.down)) * thrustModifier;
            relativeForce = Vector2.up * thrustForce;
            _rb.AddForce(_rb.GetRelativeVector(relativeForce));

            if (_rb.velocity.magnitude > speed)
            {
                _rb.velocity = _rb.velocity.normalized * speed;
            }
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
        _isInitialized = true;
    }
}
