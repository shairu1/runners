using UnityEngine;


public partial class LobbyManager
{
    public class LobbyHeroController : MonoBehaviour
    {
        private const float DistanceToButton = 2.75f;

        private int _heroId;
        private Transform _transform;
        private Vector3 _offset;
        private bool _isDestroyed;
        
        public void Init(int heroId) 
        {
            _isDestroyed = false;
            _heroId = heroId;
            _transform = GetComponent<Transform>();
        }

        private void Update()
        {
            foreach (var item in instance._playerContents)
            {
                if (Vector2.Distance(_transform.position, item._transform.position) < DistanceToButton)
                {
                    if (!item.used)
                    {
                        instance.SetPlayerInContent(item, _heroId);
                        if (!_isDestroyed) Destroy(gameObject);
                        return;
                    }
                }
            }
        }

        private void OnMouseDown()
        {
            _offset = gameObject.transform.position -
                Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }

        private void OnMouseDrag()
        {
            Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            _transform.position = Camera.main.ScreenToWorldPoint(newPosition) + _offset;
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }
    }
}