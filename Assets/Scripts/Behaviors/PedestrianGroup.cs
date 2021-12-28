using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianGroup : MonoBehaviour
{
    [SerializeField] private float maxArriveRadius = 30;
    [SerializeField] private float minArriveRadius = 20;

    private PedestrianController _pedestrianController; 
    private List<PedestrianController> _pedestrians;

    private void Awake()
    {
        _pedestrians = new List<PedestrianController>();
        _pedestrianController = GetComponent<PedestrianController>();
        
        GameObject[] pedestrians = GameObject.FindGameObjectsWithTag("Pedestrian");

        foreach (var pedestrian in pedestrians)
        {
            _pedestrians.Add(pedestrian.GetComponent<PedestrianController>());
        }
    }

    public Vector2 GetDesiredVelocity()
    {
        Vector2 resultVector = Vector2.zero;

        foreach (var pedestrian in _pedestrians)
        {
            Vector2 distance = (Vector2) (pedestrian.transform.position - transform.position);
            if (distance.magnitude < maxArriveRadius && distance.magnitude > minArriveRadius)
            {
                resultVector += distance;
            }
        }

        return resultVector;
    }
}
