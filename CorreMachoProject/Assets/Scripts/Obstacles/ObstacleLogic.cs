using MigalhaSystem.Extensions;
using MigalhaSystem.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Obstacles
{
    public class ObstacleLogic : MonoBehaviour
    {
        [Header("LifeTime Settings")]
        [SerializeField] PoolData m_obstaclePool;
        [SerializeField] Timer m_lifeTime;

        [Header("Move Settings")]
        [SerializeField] FloatRange m_speed;
        Vector2 m_moveVector;

        private void OnEnable()
        {
            m_lifeTime.StartTimer();
            m_moveVector = new Vector2(-1 * Time.deltaTime * m_speed.GetRandomValue(), 0);
        }

        private void Update()
        {
            Move();
            LifeTime();
        }

        void Move()
        {
            transform.Translate(m_moveVector, Space.World);
        }

        void LifeTime()
        {
            if (m_lifeTime.TimerElapse(Time.deltaTime))
            {
                m_obstaclePool.PushObject(gameObject);
            }
        }
    }
}
