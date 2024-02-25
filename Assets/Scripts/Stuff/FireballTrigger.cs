using System.Collections;
using System.Collections.Generic;
using Stuff.Manager;
using UnityEngine;

namespace Stuff
{
    public class FireballTrigger : MonoBehaviour
    {
        public FireballType Type;
        public FireballDirection From;

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll?.gameObject?.tag == "Player")
            {
                Vector3 position = new Vector3();
                Vector3 direction = new Vector3();

                if(From == FireballDirection.Left)
                {
                    position = UnityEngine.Camera.main.ScreenToWorldPoint(Vector3.zero);
                    position.x -= 5;
                    position.y = transform.position.y;
                    direction = Vector3.right;
                }
                else
                {
            
                    position = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0));
                    position.x += 5;
                    position.y = transform.position.y;
                    direction = Vector3.left;
                }

                position.z = -5;

                if(Type == FireballType.Fireball)
                    StuffManager.CreateFireball(position, direction, Space.Self);
                else
                    StuffManager.CreateFireball(position, direction, Space.Self);

                Destroy(gameObject);
            }
        }
    }

    public enum FireballType
    {
        Fireball,
        Iceball
    }

    public enum FireballDirection
    {
        Left,
        Right
    }
}