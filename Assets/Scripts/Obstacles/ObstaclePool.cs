using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoGD.Obstacles
{
    public class ObstaclePool
    {
        private int _maxSize;
        private List<Obstacle> _pool;
        private Obstacle _latest;
        private Func<Obstacle> _createFunc;

        public int Count
        {
            get;
            private set;
        }

        public int ActiveCount
        { get => Count - InactiveCount; }
        public int InactiveCount
        { get => _pool.Count + (_latest == null ? 1 : 0); }

        public ObstaclePool(int size, Func<Obstacle> createFunc)
        {
            _maxSize = size;
            _pool = new List<Obstacle>(size);
            _createFunc = createFunc;
        }

        /// <summary>
        /// Take an obstacle from the pool
        /// </summary>
        /// <returns></returns>
        public Obstacle Take()
        {
            Obstacle obstacle = null;
            if (_latest != null)
            {
                obstacle = _latest;
                _latest = null;
            }
            else if (_pool.Count == 0)
            {
                obstacle = _createFunc();
                Count++;
            }
            else
            {
                int index = _pool.Count - 1;
                obstacle = _pool[index];
                _pool.RemoveAt(index);
            }

            obstacle.gameObject.SetActive(true);
            return obstacle;
        }

        /// <summary>
        /// Put an obstacle back in the pool
        /// </summary>
        /// <param name="obstacle">The obstacle to be put back</param>
        /// <exception cref="InvalidOperationException">Trying to release an already released obstacle</exception>
        public void Release(Obstacle obstacle)
        {
            if (_pool.Count > 0 || _latest != null)
            {
                if (obstacle == _latest)
                    throw new InvalidOperationException("Trying to release an already released obstacle");

                for (int i = 0; i < _pool.Count; i++)
                {
                    if (_pool[i] == obstacle)
                        throw new InvalidOperationException("Trying to release an already released obstacle");
                }
            }

            obstacle.gameObject.SetActive(false);
            if (_latest == null)
            {
                _latest = obstacle;
                return;
            }
            if (InactiveCount < _maxSize)
            {
                _pool.Add(obstacle);
                return;
            }

            Count--;
            GameObject.Destroy(obstacle);
        }
    }
}
