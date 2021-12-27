using System.Collections.Generic;
using Road;
using UnityEngine;

public class CheckPointsController : MonoBehaviour
{
    public GameObject checkPointPrefab;
    private List<GameObject> _checkpoints;
    private RoadEntity _road;

    public CheckPointsController()
    {
        _checkpoints = new List<GameObject>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _road = new RoadEntity();   //TODO
    }

    public void InstantiateCheckpoints(List<Vector2> checkPointsPositions)
    {
        foreach (var checkpointPos in checkPointsPositions)
        {
            var checkPointObj  = Instantiate(checkPointPrefab, checkpointPos, Quaternion.identity);
            _checkpoints.Add(checkPointObj);
        }
    }

    /*public void CheckpointPassed(Checkpoint checkpoint)
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
