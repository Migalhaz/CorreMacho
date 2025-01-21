using MigalhaSystem.Singleton;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Game.GameSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : Singleton<InputManager>
    {
        [SerializeField] PlayerInput m_gameInputMap;

        public void OnJump(InputAction.CallbackContext context)
        {
            InputObserver.Jump(context);
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
            InputObserver.Escape(context);
        }
    }
}
