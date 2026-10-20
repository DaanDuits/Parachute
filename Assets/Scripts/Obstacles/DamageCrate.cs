namespace FoGD.Obstacles
{
    public class DamageCrate : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            GameManager.Instance.RemoveScore(150);
            GameManager.Instance.RemoveHealth(1);
        }
    }
}
