namespace FoGD.Obstacles
{
    public class HealthCrate : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            Context.LevelUI.AddScore(100);
        }
    }
}
