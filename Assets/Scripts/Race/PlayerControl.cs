using System.Collections.Generic;
using Stuff.Manager;
using UnityEngine;

namespace Race
{
    public class PlayerControl : MonoBehaviour
    {
        public static float MaxSpeedX = 8.5f;

        public int HeroId { private set; get; }
        public event System.Action onDeath;

        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private Vector3[] _positionsObstacles;
        [SerializeField] private float _minDistanceToObstacles;
        [SerializeField] private float _minDistanceToGround;
        private TypeAnimation _typeAnimation;
        private Transform _transform;
        private Animator _animator;
        private Rigidbody2D _rigidbody2d;
        private List<CollisionAction> _collisionActions;
        private bool _jump;
        private bool _death;
        private bool _enabled;


        public void Init(int heroId)
        {
            HeroId = heroId;
            onDeath = null;
            _jump = true;
            _death = false;
            enabled = true;

            _transform = GetComponent<Transform>();
            _animator = GetComponent<Animator>();
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _collisionActions = new List<CollisionAction>();

            _typeAnimation = TypeAnimation.Run;
        }

        private void Update()
        {
            if (_enabled)
            {
                if (_typeAnimation == TypeAnimation.Idle)
                    SetAnimation(TypeAnimation.Run);
            }
            else
            {
                if (_typeAnimation != TypeAnimation.Idle)
                    SetAnimation(TypeAnimation.Idle);
            }

            if(!ObstacleCheck() && _enabled)
            {
                _transform.Translate(Vector3.right * MaxSpeedX * Time.deltaTime);
            }

            GroundCheck();

            _rigidbody2d.velocityX = 0;
        }

        public void SetEnabled(bool enabled)
        { 
            _enabled = enabled;
        }

        public void Flip()
        {
            if(_jump)
            {
                _rigidbody2d.AddForce(Vector2.up * _rigidbody2d.gravityScale * 250);
                Vector2 scale = _transform.localScale;
                scale.y = -scale.y;
                _transform.localScale = scale;
                _rigidbody2d.gravityScale = -_rigidbody2d.gravityScale;
                _jump = false;
            }
        }

        public void Kill()
        {
            if(!_death)
            {
                _death = true;
                onDeath?.Invoke();
                onDeath = null;
                _collisionActions.Clear();

                RaceManager.PlayerLost(HeroId);

                GameObject go = StuffManager.CreateBang(_transform.position);
                Vector3 scale = go.transform.localScale;
                scale.y = _transform.localScale.y >= 0 ? scale.y : -scale.y;
                go.transform.localScale = scale;

                Destroy(gameObject);  
            }
        }

        public void Finish()
        {
            if(!_death)
            {
                _death = true;
                onDeath = null;
                _collisionActions.Clear();
            
                RaceManager.PlayerFinished(HeroId);
            
                enabled = false;
                _animator.SetTrigger("D");
                Destroy(gameObject, 0.5f);
                Destroy(gameObject.GetComponent<BoxCollider2D>());
                Destroy(_rigidbody2d);
                Destroy(this);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if(collision2D != null && collision2D.gameObject != null)
            {   
                if(collision2D.gameObject.CompareTag("Ground") || collision2D.gameObject.CompareTag("Player")) _jump = true;

                List<CollisionAction> newCollisionActions = new List<CollisionAction>();
                for (int i = 0; i < _collisionActions.Count; i++)
                {
                    if(collision2D.gameObject.CompareTag(_collisionActions[i].Tag))
                    {
                        _collisionActions[i].Invoke(this, collision2D);
                        if(_collisionActions[i].Repeat)
                            newCollisionActions.Add(_collisionActions[i]);
                    }
                    else
                    {
                        newCollisionActions.Add(_collisionActions[i]);
                    }
                }
                _collisionActions.Clear();
                _collisionActions = newCollisionActions;
            }
        }

        public CollisionAction AddCollisionAction(string tag, CollisionAction.Action action, bool repeat)
        {
            CollisionAction ca = new CollisionAction(tag, action, repeat);
            _collisionActions.Add(ca);
            return ca;
        }

        public void RemoveCollisionAction(CollisionAction action)
        {
            for (int i = 0; i < _collisionActions.Count; i++)
            {
                if(_collisionActions[i] == action)
                {
                    _collisionActions.RemoveAt(i);
                    return;
                }
            }
        }

        private bool ObstacleCheck()
        {
            for (int i = 0; i < _positionsObstacles.Length; i++)
            {
                Vector3 reycastPosition = _transform.position + _positionsObstacles[i];
                RaycastHit2D coll = Physics2D.Raycast(reycastPosition, Vector2.right, _minDistanceToObstacles, _groundMask);

                if(coll.collider != null && coll.collider.gameObject != null && coll.collider.gameObject.CompareTag("Ground"))
                {
                    return true;
                }
            }

            return false;
        } 

        private void GroundCheck()
        {
            RaycastHit2D coll;

            if(_rigidbody2d.gravityScale > 0)
                coll = Physics2D.Raycast(_transform.position, Vector2.down, _minDistanceToGround, _groundMask);
            else
                coll = Physics2D.Raycast(_transform.position, Vector2.up, _minDistanceToGround, _groundMask);

        
            if(coll.collider != null && coll.collider.gameObject != null)
            {
                if(_typeAnimation == TypeAnimation.Fall)
                    SetAnimation(TypeAnimation.Run);
            }
            else
            {
                if(_typeAnimation == TypeAnimation.Run)
                    SetAnimation(TypeAnimation.Fall);
            }
        }

        public void SetAnimation(TypeAnimation anim)
        {
            switch(anim)
            {
                case TypeAnimation.Fall:
                    _animator.SetTrigger("F");
                    _typeAnimation = TypeAnimation.Fall;
                    break;
                case TypeAnimation.Run:
                    _animator.SetTrigger("R");
                    _typeAnimation = TypeAnimation.Run;
                    break;
                case TypeAnimation.Idle:
                    _animator.SetTrigger("I");
                    _typeAnimation = TypeAnimation.Idle;
                    break;
            }
        }

        private void OnBecameInvisible() 
        {
            Kill();
        }

        public enum TypeAnimation
        {
            Run,
            Fall,
            Idle
        }
    }
}
