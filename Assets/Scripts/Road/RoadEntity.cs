using System.Collections.Generic;
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
    }
}
