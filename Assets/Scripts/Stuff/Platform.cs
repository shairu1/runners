using Stuff.Manager;
using UnityEngine;

namespace Stuff
{
    public class Platform : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D coll)
        {
            if(coll?.gameObject?.tag == "Player")
            {
                Invoke("Delete", 0.3f);
            }
        }

        private void Delete()
        {
            StuffManager.CreateBang(transform.position + Vector3.up * 1.5f);
            Destroy(gameObject);
        }
    }
}
