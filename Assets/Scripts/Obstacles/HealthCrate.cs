using UnityEngine;

namespace FoGD.Obstacles
{
    public class HealthCrate : FallingObstacle
    {
        [SerializeField] private int healthAdded = 1;
        protected override void OnHitPlayer()
        {
            GameManager.Instance.AddScore(Score);
            GameManager.Instance.AddHealth(healthAdded);
        }
    }
}
