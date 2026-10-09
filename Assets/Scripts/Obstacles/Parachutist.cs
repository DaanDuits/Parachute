namespace FoGD.Obstacles
{
    public class Parachutist : FallingObstacle
    {
        protected override void OnHitPlayer()
        {
            Context.LevelUI.AddScore(50);
        }
    }
}
