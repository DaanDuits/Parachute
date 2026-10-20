using System.Collections.Generic;

namespace FoGD.Obstacles.Bank
{
    public class ObstacleBank
    {
        private enum ObstacleType
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

        Dictionary<ObstacleType, FallingObstacle> _obstacles;

        public ObstacleBank(Settings settings)
        {
            // Set all obstacle types with their prefab
            _obstacles = new Dictionary<ObstacleType, FallingObstacle>
            {
                { ObstacleType.Parachutist, settings.parachutist },
                { ObstacleType.HealthCrate, settings.healthCrate },
                { ObstacleType.DamageCrate, settings.damageCrate }
            };
        }

        public FallingObstacle Parachutist()
        {
            return _obstacles[ObstacleType.Parachutist];
        }
        public FallingObstacle HealthCrate()
        {
            return _obstacles[ObstacleType.HealthCrate];
        }
        public FallingObstacle DamageCrate()
        {
            return _obstacles[ObstacleType.DamageCrate];
        }
    }
}
