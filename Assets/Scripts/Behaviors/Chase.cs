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
            return distance.normalized*_carPhysics.MaxVelocity;
        }

        return Vector2.zero;
    }
}
