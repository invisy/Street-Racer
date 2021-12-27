using UnityEngine;

public class RoadColisionHandler : MonoBehaviour
{
    private float _initialCarFriction = 0.0f;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarPhysics>(out var carPhysics))
        {
            _initialCarFriction = carPhysics.Friction;
            carPhysics.SetFriction(0.25f);
        }
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarPhysics>(out var carPhysics))
        {
            carPhysics.SetFriction(_initialCarFriction);
        }
    }
}
