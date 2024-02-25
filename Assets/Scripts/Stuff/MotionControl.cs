using System.Collections.Generic;
using UnityEngine;

namespace Stuff
{
    public class MotionControl : MonoBehaviour
    {
        public List<Vector2> Points;
        public float InterpolationTime;

        private Transform _transform;
        private Vector2 _startPosition;
        private float _positionZ;
        private float _startTime;
        private int _point;

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _positionZ = _transform.position.z;   
            _startTime = Time.realtimeSinceStartup;

            if(Points.Count >= 2)
            {
                _transform.localPosition = new Vector3(Points[0].x, Points[0].y, _positionZ);
                _startPosition = Points[0];
                _startTime = Time.realtimeSinceStartup;
                _point = 1;
            }
            else
            {
                _startPosition = _transform.localPosition;
                _point = 0;
            }
        }

        private void Update()
        {
            if(Points.Count == 0)
            {
                _startTime = Time.realtimeSinceStartup;
                _point = 0;
                return; 
            }

            if(Time.realtimeSinceStartup - _startTime >= InterpolationTime)
            {
                _transform.localPosition = new Vector3(Points[_point].x, Points[_point].y, _positionZ);
                _startTime = Time.realtimeSinceStartup;
                _startPosition = Points[_point];
                _point++;
                if(_point >= Points.Count) 
                    _point = 0;
            }
            else
            {
                Vector2 vector = Utilities.Utilities.InterpolateVector2(_startPosition, Points[_point], InterpolationTime, Time.realtimeSinceStartup - _startTime);
                _transform.localPosition = new Vector3(vector.x, vector.y, _positionZ);
            }
        }

        private void OnBecameVisible() 
        {
            enabled = true;
        }
	
        private void OnBecameInvisible() 
        {
            enabled = false;
        }
    }
}
