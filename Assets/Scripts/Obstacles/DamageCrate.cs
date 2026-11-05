using UnityEngine;
using FoGD.Shark;

namespace FoGD.Obstacles
{
    public class DamageCrate : FallingObstacle, ISharkStunner
    {
        [SerializeField] private int healthRemoved= 1;

        public float Value
        { get => sharkStunAmount; }

        protected override void OnHitPlayer()
        {
            GameManager.Instance.RemoveScore(Score);
            GameManager.Instance.RemoveHealth(healthRemoved);
        }
    }
}
