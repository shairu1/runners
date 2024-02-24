using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public static float Speed = 17.5f;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        Destroy(gameObject, 5);
    }

	private void Update()
    {	
		_transform.Translate(Vector3.right * Speed * Time.deltaTime, Space.Self);
	}
	
	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll?.gameObject?.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerControl>().Kill();
			Destroy(this.gameObject);
		}
	}
}
