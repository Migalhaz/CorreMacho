using Game.Player;
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
        [SerializeField, Min(0)] float m_speed;
        [SerializeField] FloatRange m_speedClamp = new FloatRange(6f, 25f);
        PlayerPoints m_playerPoints;

        private void OnEnable()
        {
            m_lifeTime.StartTimer();
        }

        float GetMoveSpeed()
        {
            m_playerPoints = m_playerPoints != null ? m_playerPoints : PlayerManager.ProvideInstance().m_PlayerPoints;
            return m_playerPoints.GetFinalSpeed(m_speed, m_speedClamp.minValue, m_speedClamp.maxValue);
        }

        private void Update()
        {
            Move();
            LifeTime();
        }

        void Move()
        {
            float moveSpeed = GetMoveSpeed();
            float finalMoveSpeed = moveSpeed * Time.deltaTime;
            Vector3 finalMoveVector = finalMoveSpeed * Vector3.left;
            transform.Translate(finalMoveVector, Space.World);
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
