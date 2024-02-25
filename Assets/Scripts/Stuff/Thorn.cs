using Race;
using UnityEngine;

// Шип
namespace Stuff
{
    public class Thorn : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if(collision2D?.gameObject?.tag == "Player")
            {
                collision2D.gameObject.GetComponent<PlayerControl>().Kill();
            }
        }
    }
}
