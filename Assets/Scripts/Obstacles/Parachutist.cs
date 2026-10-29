using UnityEngine;

namespace FoGD.Obstacles
{
    public class Parachutist : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            GameManager.Instance.AddScore(Score);
        }
    }
}
