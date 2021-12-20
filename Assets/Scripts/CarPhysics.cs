using System;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    private bool _isOnRoad;
    
    [SerializeField]
    private float mass = 1;
    
    [SerializeField, Range(0, 1)]
    private float friction = 0.3f;

    private float maxVelocity = 10f;
    
    [SerializeField]
    private float maxVelocityOnGround = 5f;
    [SerializeField]
    private float maxVelocityOnTrack = 10f;
    
    [SerializeField]
    private float thrust = 1f;
    
    [SerializeField]
    private float rotationSpeed = 0.5f;
    
    private Vector2 _velocity = Vector2.zero;
    
    private Vector2 _acceleration = Vector2.zero;

    public Vector2 Velocity => _velocity;
    public bool IsOnRoad => _isOnRoad;
    public float Thrust => thrust;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplySteeringForce();
        
        // Apply force
        _velocity += _acceleration * rotationSpeed;

        // Apply friction
        _velocity = _velocity.magnitude >= friction ? _velocity-(friction*_velocity.normalized) : Vector2.zero;

        _velocity = Vector2.ClampMagnitude(_velocity, maxVelocity);

        _rb.velocity = _velocity;

        _rb.rotation = Vector2.SignedAngle(Vector2.up, _velocity);
    }

    private void ApplySteeringForce()
    {
        var chases = GetComponents<Chase>();

        Vector2 steering = Vector2.zero;
        
        foreach (var chase in chases)
        {
            var desiredVelocity = chase.GetDesiredVelocity(); //
            steering += desiredVelocity - _velocity;
        }
        AddForce(steering);
    }
    
    public void AddForce(Vector2 force)
    {
        float angle = transform.eulerAngles.z;
        _acceleration += force * thrust / mass;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        friction = 0.25f;
        maxVelocity = maxVelocityOnTrack;
        _isOnRoad = true;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        friction = 0.3f;
        maxVelocity = maxVelocityOnGround;
        _isOnRoad = false;
    }
}
