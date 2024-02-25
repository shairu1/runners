using Race;
using UnityEngine;

namespace Stuff
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll?.gameObject?.tag == "Player")
            {
                GameManager.GetPlayer(coll.gameObject.GetComponent<PlayerControl>().HeroId).Score++;
                Destroy(gameObject);
            }
        }
    }
}
