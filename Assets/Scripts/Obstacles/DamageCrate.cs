using UnityEngine;

namespace FoGD.Obstacles
{
    public class DamageCrate : FallingObstacle
    {
        [SerializeField] private int healthRemoved= 1;
        protected override void OnHitPlayer()
        {
            GameManager.Instance.RemoveScore(Score);
            GameManager.Instance.RemoveHealth(healthRemoved);
        }
    }
}
