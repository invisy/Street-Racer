using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Road
{
    [RequireComponent(typeof(RoadEntity))]
    [RequireComponent(typeof(MeshFilter))]
    public class RoadMeshGenerator : MonoBehaviour
    {
        private RoadEntity _roadEntity;
        [SerializeField]
        private bool editorContinuousRepaint = false;
        public bool EditorContinuousRepaint => editorContinuousRepaint;
        void Start()
        {
            UpdateRoadMesh();
        }

        public void UpdateRoadMesh()
        {
            _roadEntity = GetComponent<RoadEntity>();
            Mesh mesh = new Mesh();
            GetComponent<MeshFilter>().sharedMesh = mesh;
            
            List<Vector2> roadPoints = _roadEntity.Path;
            float roadWidth = _roadEntity.RoadWidth;
            bool isClosed = _roadEntity.IsClosed;
            
            Vector3[] verts = new Vector3[roadPoints.Count * 2];
            Vector2[] uvs = new Vector2[verts.Length];
            
            int trisCount = 6 * (roadPoints.Count - 1) + (isClosed ? 6 : 0);
            int[] tris = new int[trisCount];

            for (int i = 0; i < roadPoints.Count; i++)
            {
                Vector2 vectorBetweenParts = Vector2.zero;
                
                Vector2 leftPart = roadPoints[i] - roadPoints[(i-1+roadPoints.Count) % roadPoints.Count];
                Vector2 rightPart = roadPoints[(i + 1) % roadPoints.Count] - roadPoints[i];

                if (i > 0 && i < roadPoints.Count-1 || isClosed)
                    vectorBetweenParts = GetVectorBetweenRoadParts(leftPart, rightPart, roadWidth);
                else if (i == 0)
                    vectorBetweenParts = new Vector2(-rightPart.y, rightPart.x).normalized * roadWidth / 2;
                else if (i == roadPoints.Count-1)
                    vectorBetweenParts = new Vector2(-leftPart.y, leftPart.x).normalized * roadWidth / 2;
                
                verts[i*2] = roadPoints[i]+vectorBetweenParts;
                verts[i*2+1] = roadPoints[i]-vectorBetweenParts;
                
                //float roadLengthPercent = i % roadPoints.Count;
                //float v = 1 - Mathf.Abs(2 * roadLengthPercent - 1);
                uvs[i*2] = new Vector2(0, i % 2);
                uvs[i*2+1] = new Vector2(1, i % 2);

                if (i < roadPoints.Count-1 || isClosed)
                {
                    int trisStartIndex = i * 6;

                    tris[trisStartIndex] = i * 2;
                    tris[trisStartIndex + 1] = (i * 2 + 2) % verts.Length;
                    tris[trisStartIndex + 2] = i * 2 + 1;
                    tris[trisStartIndex + 3] = (i * 2 + 2) % verts.Length;
                    tris[trisStartIndex + 4] = (i * 2 + 3) % verts.Length;
                    tris[trisStartIndex + 5] = i * 2 + 1;
                }
            }

            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.uv = uvs;
        }

        private Vector2 GetVectorBetweenRoadParts(Vector2 leftPart, Vector2 rightPart, float roadWidth)
        {
            Vector2 normalizedLeftPart = -leftPart.normalized; //change direction of vector and normalize
            Vector2 normalizedRightPart = rightPart.normalized; // normalize

            float halfAngleRad = Vector2.Angle(normalizedLeftPart, normalizedRightPart)/2*math.PI/180;
            float vectorLength = (roadWidth/2)/(math.sin(halfAngleRad));

            Vector2 resultVector = (normalizedLeftPart + normalizedRightPart).normalized * vectorLength;
            
            return resultVector;
        }
    }
}