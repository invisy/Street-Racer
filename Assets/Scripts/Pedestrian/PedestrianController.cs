using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianController : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    private Vector2 _velocity = Vector2.zero;

    private float maxVelocity = 20f;

    public Vector2 Velocity => _velocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyVector();
        _velocity.Normalize();
        _velocity *= maxVelocity;

        _rb.rotation = Vector2.SignedAngle(Vector2.up, _velocity);
        
        _rb.velocity = _velocity;
        _velocity = Vector2.zero;
    }

    public void ApplyVector()
    {
        var pedestrianBehaviors = GetComponents<PedestrianBehavior>();
        var pedestrianGroups = GetComponents<PedestrianGroup>();
        
        Vector2 velocity = Vector2.zero;
        
        foreach (var behavior in pedestrianBehaviors)
        {
            var desiredVelocity = behavior.GetDesiredVelocity();
            velocity += (desiredVelocity - _velocity);
        }
        
        foreach (var behavior in pedestrianGroups)
        {
            var desiredVelocity = behavior.GetDesiredVelocity();
            velocity += (desiredVelocity - _velocity);
        }

        _velocity = velocity;
    }
}
