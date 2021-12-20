using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    [SerializeField] private GameObject objectToFollow;

    [SerializeField] private float arriveRadius;

    [SerializeField] private float maxAllowedSpeed;
    
    private bool isChasing = false;
    
    private CarPhysics _carPhysics;
    private CarPhysics _carPhysicsToFollow;

    private void Awake()
    {
        _carPhysics = GetComponent<CarPhysics>();
        _carPhysicsToFollow = objectToFollow.GetComponent<CarPhysics>();
    }

    public Vector2 GetDesiredVelocity()
    {
        Vector2 distance = (Vector2) (_carPhysicsToFollow.transform.position - transform.position);

        Debug.Log("");
        
        if (distance.magnitude < arriveRadius
            && _carPhysicsToFollow.Velocity.magnitude >= maxAllowedSpeed)
        {
            isChasing = true;
        }
        
        if (distance.magnitude > arriveRadius)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            return distance.normalized;
        }
        if (_carPhysics.IsOnRoad)
        {
            return Vector2.up;
        }

        return Vector2.zero;
    }
    
    
}
