using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Шип
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
