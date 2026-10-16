namespace FoGD.Obstacles
{
    public class HealthCrate : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            GameManager.Instance.AddScore(100);
        }
    }
}
