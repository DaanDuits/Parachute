using System.Collections.Generic;

namespace FoGD.Obstacles.Factory
{

    public class ObstacleFactory
    {
        private enum EObstacleType
        {
            Parachutist,
            HealthCrate,
            DamageCrate
        }

        [System.Serializable]
        public struct Settings
        {
            public FallingObstacle parachutist, healthCrate, damageCrate;
        }

        Dictionary<EObstacleType, FallingObstacle> _obstacles;

        public ObstacleFactory(Settings settings)
        {
            _obstacles = new Dictionary<EObstacleType, FallingObstacle>
            {
                { EObstacleType.Parachutist, settings.parachutist },
                { EObstacleType.HealthCrate, settings.healthCrate },
                { EObstacleType.DamageCrate, settings.damageCrate }
            };
        }

        public FallingObstacle Parachutist()
        {
            return _obstacles[EObstacleType.Parachutist];
        }
        public FallingObstacle HealthCrate()
        {
            return _obstacles[EObstacleType.HealthCrate];
        }
        public FallingObstacle DamageCrate()
        {
            return _obstacles[EObstacleType.DamageCrate];
        }
    }
}
