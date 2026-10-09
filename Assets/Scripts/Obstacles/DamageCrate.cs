namespace FoGD.Obstacles
{
    public class DamageCrate : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            Context.LevelUI.RemoveScore(50);
        }
    }
}
