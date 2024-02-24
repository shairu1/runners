using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform _transform;
    private Transform[] _players;

    public void Init()
    {
       _transform = GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector2 target = _transform.position;

        for (int i = 0; i < _players.Length; i++)
        {
            if(_players[i] == null) continue;
            if(_players[i].position.x > target.x)
            {
                target.x = _players[i].position.x;
            }
        }

        Vector3 v = Vector2.Lerp(_transform.position, target, 10*Time.deltaTime);
        v.y = 0;
        v.z = _transform.position.z;
        _transform.position = v;
    }

    public void SetStartPosition()
    {
        Vector3 pos = _transform.position;
        pos.x = 0;
        pos.y = 0;
        _transform.position = pos;
    }

    public void SetActive(bool value)
    {
        if(value)
        {
            var pl = RaceManager.GetPlayerControls();
            _players = new Transform[pl.Length];

            for (int i = 0; i < pl.Length; i++)
                _players[i] = pl[i].transform;

            enabled = true;
        }
        else
        {
            _players = null;
            enabled = false;
        }
    }
}
