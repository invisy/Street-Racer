using System;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    [SerializeField]
    private float mass = 1;
    
    [SerializeField, Range(0, 1)]
    private float friction = 0.25f;

    [SerializeField]
    private float maxVelocity = 20f;
    
    [SerializeField]
    private float thrust = 1f;
    
    [SerializeField]
    private float rotationSpeed = 0.5f;
    
    private Vector2 _velocity = Vector2.zero;
    private Vector2 _acceleration = Vector2.zero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Apply force
        _velocity += _acceleration*rotationSpeed;
        
        // Apply friction
        _velocity = _velocity.magnitude >= friction ? _velocity-(friction*_velocity.normalized) : Vector2.zero;

        _velocity = Vector2.ClampMagnitude(_velocity, maxVelocity);

        _rb.velocity = _velocity;

        _rb.rotation = Vector2.SignedAngle(Vector2.up, _velocity);
    }
    
    public void AddForce(Vector2 force)
    {
        float angle = transform.eulerAngles.z;
        _acceleration += force / mass;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _velocity = Vector2.zero;
        Debug.Log("TRIGGER ENTER!");
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        _velocity = Vector2.zero;
        Debug.Log("TRIGGER EXIT!");
    }
}
