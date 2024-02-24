using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D coll)
    {
        if(coll?.gameObject?.tag == "Player")
        {
            coll.gameObject.transform.Translate(Vector3.right * PlayerControl.MaxSpeedX * Time.deltaTime / 4);
        }
    }
}
