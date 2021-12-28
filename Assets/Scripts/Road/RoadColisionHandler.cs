using UnityEngine;

public class RoadColisionHandler : MonoBehaviour
{
    private float frictionDelta = 0.1f;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarPhysics>(out var carPhysics))
        {
            float currentCarFriction = carPhysics.Friction;
            carPhysics.SetFriction(currentCarFriction - frictionDelta);
            carPhysics.MaxVelocity *= 1.5f;
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarPhysics>(out var carPhysics))
        {
            float currentCarFriction = carPhysics.Friction;
            carPhysics.SetFriction(currentCarFriction + frictionDelta);
            carPhysics.MaxVelocity /= 1.5f;
        }
    }
}
