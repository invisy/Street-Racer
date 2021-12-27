using System;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField]
    private float mass = 1;
    
    [SerializeField, Range(0, 1)]
    private float friction = 0.3f;

    [SerializeField]
    private float maxAcceleration = 0.5f;
    [SerializeField]
    private float maxVelocity = 10f;

    [SerializeField]
    private float thrust = 1f;
    
    [SerializeField, Range(0, 5)]
    private float rotationSpeed = 0.5f;
    
    private Vector2 _velocity = Vector2.zero;
    
    private Vector2 _acceleration = Vector2.zero;

    public Vector2 Velocity => _velocity;
    public float MaxVelocity =>  maxVelocity;
    public float Thrust => thrust;
    public float Friction => friction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplySteeringForce();
        ApplyFriction();
        
        // Apply force
        _velocity += Vector2.ClampMagnitude(_acceleration, maxAcceleration);
        
        _velocity = Vector2.ClampMagnitude(_velocity, maxVelocity);
        _velocity = CalculateCarDrivingVector(_velocity);

        if (_velocity.magnitude < 0.05f)
        {
            _velocity = Vector2.zero;
        }
        else
        {
            _rb.rotation = Vector2.SignedAngle(Vector2.up, _velocity);
        }
        
        _rb.velocity = _velocity;
        _acceleration = Vector2.zero;
    }
    
    private void ApplyFriction()
    {
        var frictionForce = -_velocity.normalized*friction*mass;
        AddForce(frictionForce);
    }
    
    private void ApplySteeringForce()
    {
        var chases = GetComponents<Chase>();

        Vector2 steering = Vector2.zero;
        
        foreach (var chase in chases)
        {
            var desiredVelocity = chase.GetDesiredVelocity();
            steering += (desiredVelocity - _velocity);
        }
        AddForce(steering);
    }

    private Vector2 CalculateCarDrivingVector(Vector2 currentVector)
    {
        Quaternion carRotation = Quaternion.Euler(0,0, transform.eulerAngles.z);
        Vector3 carDirectVector = (carRotation * Vector2.up);
        Vector3 carSideVector = (carRotation * Vector2.right);
        Vector2 directVectorProj =  Vector3.Project(currentVector, carDirectVector);
        Vector2 sideVectorProj =  Vector3.Project(currentVector, carSideVector)
                                  *directVectorProj.magnitude*rotationSpeed*Time.fixedDeltaTime;

        Vector2 fullVector = directVectorProj + sideVectorProj;
        
        Quaternion backRotation = Quaternion.Euler(0,0, -transform.eulerAngles.z);
        Vector2 rotatedVector = backRotation * fullVector;
        float rotatedAngle = Vector2.SignedAngle(Vector2.up, rotatedVector);

        if (rotatedAngle > 90 || rotatedAngle < -90)
            fullVector = Vector2.zero;
        
        return fullVector;
    }
    
    public void AddForce(Vector2 force)
    {
        _acceleration += force * thrust / mass;
    }

    public void SetFriction(float frictionValue)
    {
        friction = frictionValue;
    }
}
