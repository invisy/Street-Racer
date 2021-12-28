using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Road
{
    public class RoadEntity : MonoBehaviour
    {
        private readonly List<Vector2> _path;
        [SerializeField]
        private float roadWidth = 4f;
        [SerializeField]
        private bool isClosed = true;

        public List<Vector2> Path => _path;
        public bool IsClosed => _path.Count > 2 && isClosed;
        public float RoadWidth => roadWidth;

        public RoadEntity()
        {
            _path = new List<Vector2>();
            _path.Add(new Vector2(-50,-10));
            _path.Add(new Vector2(-35,-25));
            _path.Add(new Vector2(25,-20));
            _path.Add(new Vector2(50,0));
            _path.Add(new Vector2(25,20));
            _path.Add(new Vector2(-20,10));
        }

        /*broken*/
        /*public Vector2 FindNormalFromPoint(Vector2 point)
        {
            Vector2 firstPoint = Vector2.zero;
            Vector2 secondPoint = Vector2.zero;
            float minDistance = float.MaxValue;
            
            for (int i = 0; i < _path.Count-1;i++)
            {
                var scale = transform.localScale;
                //Debug.Log(scale);
                float currentDistance = (point - _path[i]*scale).magnitude;
                
                if (currentDistance < minDistance && IsClosed)
                {
                    Vector2 previousPoint = _path[(i-1 + _path.Count) % _path.Count]*scale;
                    Vector2 centerPoint = _path[i]*scale;
                    Vector2 nextPoint = _path[i+1 % _path.Count]*scale;
                    
                    var possibleAngle1 = Vector2.Angle(previousPoint - point, centerPoint-point);
                    var possibleAngle2 = Vector2.Angle(nextPoint - point, centerPoint-point);

                    if (possibleAngle1 > possibleAngle2)
                    {
                        firstPoint = previousPoint;
                        secondPoint = centerPoint;
                    }
                    else
                    {
                        firstPoint = centerPoint;
                        secondPoint = nextPoint;
                    }
                    
                    minDistance = currentDistance;
                }
            }

            Vector2 roadPart = firstPoint - secondPoint;
            Vector2 hypotenuse = (point - secondPoint);
            float normalLength = math.abs(hypotenuse.magnitude*math.sin(Vector2.Angle(roadPart, hypotenuse)));
            Vector2 normal = new Vector2(-roadPart.y, roadPart.x).normalized*normalLength;
            
            if(IsLeft(firstPoint, secondPoint, point))
                normal = - new Vector2(-roadPart.y, roadPart.x).normalized*normalLength;
            
            return normal;
        }*/

        public bool IsLeft(Vector2 a, Vector2 b, Vector2 c)
        {
            return ((b.x - a.x)*(c.y - a.y) - (b.y - a.y)*(c.x - a.x)) > 0;
        }
    }
}
