using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianHitHandler : MonoBehaviour
{
    private float _freezeSeconds = 2;
    private float _oldMaxVelocity = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent<CarPhysics>(out var carPhysics))
        {
            var maxVel = carPhysics.MaxVelocity;
            if(_oldMaxVelocity == 0 && maxVel > 1)
                _oldMaxVelocity = maxVel;
            
            carPhysics.MaxVelocity = 0;
            StartCoroutine(UnfreezeAfterTime(_freezeSeconds, carPhysics));
        }
    }

    IEnumerator UnfreezeAfterTime(float time, CarPhysics carPhysics)
    {
        yield return new WaitForSeconds(time);
        
        carPhysics.MaxVelocity = _oldMaxVelocity;
        _oldMaxVelocity = 0;
    }
}
