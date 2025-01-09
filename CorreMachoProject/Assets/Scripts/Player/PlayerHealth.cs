using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField, Min(1)] int m_healthPoints = 1;
        int m_currentHealth;
        [SerializeField] Trigger.System2D.BoxTrigger2D m_hitBox;

        private void OnEnable()
        {
            m_hitBox.m_TriggerEvents.OnTriggerEnterAddListner(Hit);
        }

        private void OnDisable()
        {
            m_hitBox.m_TriggerEvents.OnTriggerEnterRemoveListner(Hit);
        }

        private void Awake()
        {
            m_currentHealth = m_healthPoints;
        }

        private void Update()
        {
            m_hitBox.InTrigger(transform.position);
        }

        void Hit()
        {
            m_currentHealth--;
            PlayerHealthObserver.PlayerHit();
            if (m_currentHealth <= 0)
            {
                Death();
            }
        }

        void Death()
        {
            //TEMP DEATH
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.buildIndex);

            PlayerHealthObserver.PlayerDie();
        }

        private void OnDrawGizmosSelected()
        {
            m_hitBox.DrawTrigger(transform.position);
        }
    }
}
