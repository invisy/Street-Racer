using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CarPhysics))]
public class CarController : MonoBehaviour
{
    private CarPhysics _carPhysics;

    private void Awake()
    {
        _carPhysics = GetComponent<CarPhysics>();
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if(context.performed)
            _carPhysics.AddForce(Vector2.up);
        else if (context.canceled)
            _carPhysics.AddForce(-Vector2.up);
    }
    
    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if(context.performed)
            _carPhysics.AddForce(Vector2.down);
        else if (context.canceled)
            _carPhysics.AddForce(-Vector2.down);
    }
    
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if(context.performed)
            _carPhysics.AddForce(Vector2.left);
        else if (context.canceled)
            _carPhysics.AddForce(-Vector2.left);
    }
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if(context.performed)
            _carPhysics.AddForce(Vector2.right);
        else if (context.canceled)
            _carPhysics.AddForce(-Vector2.right);
    }
}
