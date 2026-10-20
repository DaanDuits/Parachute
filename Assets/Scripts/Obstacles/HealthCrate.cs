namespace FoGD.Obstacles
{
    public class HealthCrate : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            GameManager.Instance.AddScore(50);
            GameManager.Instance.AddHealth(1);
        }
    }
}
