using Game.GameSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMove : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Rigidbody2D m_rig;

        [Header("Jump Settings")]
        [SerializeField, Min(0)] float m_jumpForce;
        [SerializeField, Min(0)] float m_jumpTime;
        float m_currentJumpTime;
        bool m_onGround;

        [Header("Jump Buffering")]
        [SerializeField, Min(0)] float m_jumpBufferingTime;
        float m_jumpBufferingCurrentTime;


        [Header("Collision Settings")]
        [SerializeField] Trigger.System2D.CircleTrigger2D m_groundCheck;

        private void OnEnable()
        {
            InputObserver.OnJump += JumpInput;
            m_groundCheck.m_TriggerEvents.OnTriggerEnterAddListner(EnterGround);
        }

        private void OnDisable()
        {
            InputObserver.OnJump -= JumpInput;
            m_groundCheck.m_TriggerEvents.OnTriggerEnterRemoveListner(EnterGround);
        }


        void JumpInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StartJump();
            }
            if (context.canceled)
            {
                m_jumpBufferingCurrentTime = 0;
                CancelJump();
            }
        }

        private void Update()
        {
            Jump();
            JumpBuffering();
        }

        private void FixedUpdate()
        {
            m_onGround = m_groundCheck.InTrigger(transform.position);
        }

        void StartJump()
        {
            if (!m_onGround)
            {
                m_jumpBufferingCurrentTime = m_jumpBufferingTime;
                return;
            }
            DoJump();
        }

        void DoJump()
        {
            m_jumpBufferingCurrentTime = 0;
            PlayerMoveObserver.PlayerJump();
            m_currentJumpTime = m_jumpTime;
        }
        
        void CancelJump()
        {
            
            m_currentJumpTime = 0;
        }

        void EnterGround()
        {
            PlayerMoveObserver.PlayerGrounded();
            CancelJump();
        }

        void Jump()
        {
            if (m_currentJumpTime > 0)
            {
                m_rig.velocity = Vector3.up * m_jumpForce;
                m_currentJumpTime -= Time.deltaTime;
            }
        }

        void JumpBuffering()
        {
            if (m_jumpBufferingCurrentTime <= 0) return;
            m_jumpBufferingCurrentTime -= Time.deltaTime;
            if (m_jumpBufferingCurrentTime > 0 && m_onGround)
            {
                DoJump();
            }
        }

        private void OnDrawGizmosSelected()
        {
            m_groundCheck.DrawTrigger(transform.position);
        }

        private void Reset()
        {
            TryGetComponent(out m_rig);
        }
    }
}
