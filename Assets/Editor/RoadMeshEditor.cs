using Road;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadMeshGenerator))]
public class RoadEditor : Editor {

    RoadMeshGenerator _meshGenerator;

    void OnSceneGUI()
    {
        if (_meshGenerator.EditorContinuousRepaint && Event.current.type == EventType.Repaint)
        {
            _meshGenerator.UpdateRoadMesh();
        }
    }
    
    void OnEnable()
    {
        _meshGenerator = (RoadMeshGenerator)target;
        _meshGenerator.UpdateRoadMesh();
    }
}