using UnityEngine;
using FoGD.Obstacles.Manager;

namespace FoGD.Obstacles
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class FallingObstacle : MonoBehaviour
    {
        [SerializeField] private int score = 100;
        [SerializeField] private float yVelocity = 3f;
        [SerializeField] private Vector2 xVelocityRange = new Vector2(-4f, 4f);
        [SerializeField] private Vector2 xPosRange = new Vector2(-6f, 6f);
        [SerializeField] private float minYPos = -6f;

        private float _xVelocity;

        private float _radius;

        public ObstacleManager Context
        {
            protected get;
            set;
        }

        protected int Score
        { get => score; }

        private void OnEnable()
        {
            // Initialize values
            _xVelocity = Random.Range(xVelocityRange.x, xVelocityRange.y);
            _radius = GetComponent<CircleCollider2D>().radius;
        }

        private void Update()
        {
            // Check if collider is in bounds, flip the x velocity
            if ((transform.position.x - _radius <= xPosRange.x && _xVelocity < 0f) || 
                (transform.position.x + _radius >= xPosRange.y && _xVelocity > 0f))
                _xVelocity = -_xVelocity;

            // Move the obstacle based on x and y velocity
            transform.position += Vector3.down * (yVelocity * Time.deltaTime) + Vector3.right * (_xVelocity * Time.deltaTime);

            // Destroy if below minimum y position
            if (transform.position.y <= minYPos)
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
