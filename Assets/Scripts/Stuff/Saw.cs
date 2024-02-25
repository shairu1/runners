using Race;
using UnityEngine;

namespace Stuff
{
    public class Saw : MonoBehaviour
    {
        private Transform _transform;

        private void Start()
        {
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            _transform.Rotate(new Vector3(0,0,2), Space.Self);
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if(collision2D?.gameObject?.tag == "Player")
            {
                collision2D.gameObject.GetComponent<PlayerControl>().Kill();
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
