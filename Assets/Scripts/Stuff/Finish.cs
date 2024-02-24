using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
