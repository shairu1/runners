using Race;
using UnityEngine;

namespace Stuff
{
    public class Finish : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D coll)
        {
            if(coll?.gameObject?.tag == "Player")
            {
                coll.gameObject.GetComponent<PlayerControl>().Finish();
            }
        }
    }
}
