using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Road;

public class PedestrianBehavior : MonoBehaviour
{

    private float circleDistance = 1;
    private float circleRadius = 2;
    private int angleChangeStep = 15;
    private int angle = 0;
    
    [SerializeField] private float arriveRadius = 10;

    private RoadEntity _roadEntity;
    private PedestrianController _pedestrianController; 
    private List<CarPhysics> _carsPhysics;

    private void Awake()
    {
        _carsPhysics = new List<CarPhysics>();
        _pedestrianController = GetComponent<PedestrianController>();
        
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        _roadEntity = GameObject.FindGameObjectWithTag("Road").GetComponent<RoadEntity>();

        foreach (var car in cars)
        {
            _carsPhysics.Add(car.GetComponent<CarPhysics>());
        }
    }

    public Vector2 GetDesiredVelocity()
    {
        Vector2 resultVector = Vector2.zero;

        System.Random rnd = new System.Random();

        resultVector.x = rnd.Next(100);
        if (randomBool(rnd))
        {
            angle+= angleChangeStep;
        } else
        {
            angle-= angleChangeStep;
        }
            
        var futurePos = (Vector2) _pedestrianController.transform.position + _pedestrianController.Velocity.normalized * circleDistance;
        var vector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * circleRadius;

        resultVector += (futurePos + vector - (Vector2)transform.position);

        foreach (var car in _carsPhysics)
        {
            Vector2 distance = (Vector2) (car.transform.position - transform.position);
            if (distance.magnitude < arriveRadius)
            {
                Vector2 hypothetic = new Vector2(-car.Velocity.y, car.Velocity.x);
                if (hypothetic.magnitude > 0)
                {
                    if (((Vector2)car.transform.position - ((Vector2)transform.position + hypothetic.normalized))
                        .magnitude >
                        ((Vector2)car.transform.position - ((Vector2)transform.position - hypothetic.normalized))
                        .magnitude)
                    {
                        resultVector = hypothetic;
                    }
                    else
                    {
                        resultVector = hypothetic * -1;
                    }
                }
            }
        }

        return resultVector;
    }

    private bool randomBool(System.Random rnd)
    {
        return rnd.NextDouble() >= 0.5;
    }
}
