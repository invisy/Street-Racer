using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CarPhysics))]
public class CarController : MonoBehaviour
{
    private CarPhysics _carPhysics;

    private bool _moveDirect, _moveBack, _moveLeft, _moveRight;

    private void Awake()
    {
        _carPhysics = GetComponent<CarPhysics>();
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        _moveDirect = context.performed || !context.canceled;
    }
    
    public void OnMoveDown(InputAction.CallbackContext context)
    {
        _moveBack = context.performed || !context.canceled;
    }
    
    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        _moveLeft = context.performed || !context.canceled;
    }
    
    public void OnMoveRight(InputAction.CallbackContext context)
    {
        _moveRight = context.performed || !context.canceled;
    }

    private void FixedUpdate()
    {
        Quaternion carRotation = Quaternion.Euler(0,0,_carPhysics.gameObject.transform.eulerAngles.z);
        
        if(_moveDirect)
            _carPhysics.AddForce(carRotation*Vector2.up);

        if(_moveBack)
            _carPhysics.AddForce(carRotation*Vector2.down);
        
        if(_moveLeft)
            _carPhysics.AddForce(carRotation*Vector2.left);
        
        if(_moveRight)
            _carPhysics.AddForce(carRotation*Vector2.right);
    }
}
