using FoGD.Obstacles.Manager;
using UnityEngine;

namespace FoGD.Obstacles
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class FallingObstacle : MonoBehaviour
    {
        [SerializeField] private float velocityY = 3;
        [SerializeField] private float minHeight = -6;

        public ObstacleManager Context
        {
            protected get;
            set;
        }

        private void Update()
        {
            transform.position += Vector3.down * (velocityY * Time.deltaTime);

            if (transform.position.y <= minHeight)
                Context.DestroyObstacle(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Context.DestroyObstacle(this);
        }
    }
}
