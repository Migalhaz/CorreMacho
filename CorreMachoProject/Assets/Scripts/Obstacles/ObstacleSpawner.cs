using Game.Player;
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
        [SerializeField, Min(1)] float m_pointsToMaxTime;
        [SerializeField] FloatRange m_spawnTime;
        float m_currentSpawnTime;

        [SerializeField] IntRange m_spawnCountRange;

        private void Start()
        {
            m_currentSpawnTime = GetTimeToSpawn();
        }

        private void Update()
        {
            m_currentSpawnTime -= Time.deltaTime;
            if (m_currentSpawnTime <= 0)
            {
                m_currentSpawnTime = GetTimeToSpawn();
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

        float GetTimeToSpawn()
        {
            PlayerManager player = PlayerManager.ProvideInstance();
            float percent = 1 - (player.m_PlayerPoints.Points / m_pointsToMaxTime);
            return m_spawnTime.Lerp(percent);
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
