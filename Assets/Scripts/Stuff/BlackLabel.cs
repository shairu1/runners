using Race;
using Stuff.Manager;
using UnityEngine;

namespace Stuff
{
    public class BlackLabel : MonoBehaviour
    {
        private CollisionAction _collisionAction;
        private PlayerControl _playerControl;

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if(coll?.gameObject?.tag == "Player")
            {
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                transform.SetParent(coll.gameObject.transform);
                transform.localPosition = new Vector3(0, 0.2f, 0);
                transform.localScale = new Vector3(0.15f, 0.15f, 0);
                _playerControl = coll.gameObject.GetComponent<PlayerControl>();
                _playerControl.onDeath += OnPlayerDeath;
                _collisionAction = _playerControl.AddCollisionAction("Player", OnPlayerCollide, false);
            }
        }

        private void OnPlayerDeath()
        {
            _playerControl.onDeath -= OnPlayerDeath;
            _playerControl.RemoveCollisionAction(_collisionAction);
            _collisionAction = null;

            Vector3 position = _playerControl.transform.position;
            StuffManager.CreateFireball(position, new Vector3(0,1), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(1,0), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(0,-1), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(-1,0), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(1,1), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(1,-1), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(-1,-1), Space.Self);
            StuffManager.CreateFireball(position, new Vector3(-1,1), Space.Self);

            Destroy(gameObject);
        }

        private void OnPlayerCollide(PlayerControl player, CollisionAction collisionAction, Collision2D collision)
        {
            collision.gameObject.GetComponent<PlayerControl>().Kill();
            player.Kill();
        }
    }
}
