using MigalhaSystem.Extensions;
using MigalhaSystem.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] List<ObstacleSpawnerSettings> m_spawners;
        [SerializeField] Timer m_timeToSpawn;
        [SerializeField] IntRange m_spawnCountRange;

        private void Awake()
        {
            m_timeToSpawn.StartTimer();
        }

        private void Update()
        {
            if (m_timeToSpawn.TimerElapse(Time.deltaTime))
            {
                SpawnLogic();
            }
        }

        void SpawnLogic()
        {
            if (m_spawners.IsNullOrEmpty()) return;

            int spawnCount = m_spawnCountRange.GetRandomValue(true);
            int objectCount = Mathf.Clamp(spawnCount, 1, m_spawners.Count);
            List<ObstacleSpawnerSettings> spawners = new(m_spawners);
            for (int i = 0; i < objectCount; i++)
            {
                ObstacleSpawnerSettings spawnToUse = spawners.GetRandom();
                spawnToUse.DoSpawn();
                spawners.Remove(spawnToUse);
            }
        }
    }

    [System.Serializable]
    public class ObstacleSpawnerSettings
    {
        [SerializeField] Transform m_spawnPoint;
        [SerializeField] List<PoolData> m_availableObstacles;
        public void DoSpawn()
        {
            if (m_availableObstacles.IsNullOrEmpty()) return;
            PoolData obstacle = m_availableObstacles.GetRandom();
            PoolManager.ProvideInstance().PullObject(obstacle, m_spawnPoint.position, Quaternion.identity);
        }
    }
}
