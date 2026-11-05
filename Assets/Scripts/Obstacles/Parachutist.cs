using FoGD.Shark;
using UnityEngine;

namespace FoGD.Obstacles
{
    public class Parachutist : FallingObstacle, ISharkFood
    {
        public float Value
        { get => sharkHungerValue; }

        protected override void OnHitPlayer()
        {
            GameManager.Instance.AddScore(Score);
        }

        protected override void OnMiss()
        {
            GameManager.Instance.RemoveScore(MissPenalty);
        }
    }
}
