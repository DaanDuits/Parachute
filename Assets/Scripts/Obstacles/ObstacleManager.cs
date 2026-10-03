using FoGD.Obstacles;
using UnityEngine;

namespace FoGD
{
    public class ObstacleManager : MonoBehaviour
    {
        [SerializeField] private int size = 30;
        [SerializeField] private Obstacle obstacleObject;

        private ObstaclePool _obstaclePool;

        private void Awake()
        {
            _obstaclePool = new ObstaclePool(size, CreateObstacle);
        }

        private Obstacle CreateObstacle()
        {
            return Instantiate(obstacleObject, transform);
        }
    }
}
