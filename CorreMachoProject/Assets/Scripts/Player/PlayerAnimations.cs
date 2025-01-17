using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] Animator m_playerAnimator;
        [SerializeField] string m_runAnimationName;
        [SerializeField] string m_jumpAnimationName;

        private void OnEnable()
        {
            PlayerMoveObserver.OnPlayerJump += PlayJumpAnimation;
            PlayerMoveObserver.OnPlayerGrounded += PlayRunAnimation;
        }

        private void OnDisable()
        {
            PlayerMoveObserver.OnPlayerJump -= PlayJumpAnimation;
            PlayerMoveObserver.OnPlayerGrounded -= PlayRunAnimation;

        }

        private void Awake()
        {
            m_playerAnimator ??= GetComponent<Animator>();
            PlayRunAnimation();
        }

        void PlayJumpAnimation()
        {
            m_playerAnimator.Play(m_jumpAnimationName);
        }

        void PlayRunAnimation()
        {
            m_playerAnimator.Play(m_runAnimationName);
        }
    }
}
