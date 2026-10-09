using FoGD.Obstacles.Manager;
using UnityEngine;

namespace FoGD.Obstacles
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class FallingObstacle : MonoBehaviour
    {
        [SerializeField] private float velocityY = 3;
        [SerializeField] private float minVelocityX = -4, maxVelocityX = 4;
        [SerializeField] private float minPosX = -6, maxPosX = 6;
        [SerializeField] private float minHeight = -6;

        private float _velocityX;

        private float _radius;

        public ObstacleManager Context
        {
            protected get;
            set;
        }

        private void OnEnable()
        {
            _velocityX = Random.Range(minVelocityX, maxVelocityX);
            _radius = GetComponent<CircleCollider2D>().radius;
        }

        private void Update()
        {
            if (transform.position.x - _radius <= minPosX || transform.position.x + _radius >= maxPosX)
                _velocityX = -_velocityX;

            transform.position += Vector3.down * (velocityY * Time.deltaTime) + Vector3.right * (_velocityX * Time.deltaTime);

            if (transform.position.y <= minHeight)
                Context.DestroyObstacle(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.position.y > transform.position.y)
                return;
            OnHitPlayer();
            Context.DestroyObstacle(this);
        }

        /// <summary>
        /// OnHitPlayer is called when the obstacle hits the player
        /// </summary>
        protected abstract void OnHitPlayer();
    }
}
