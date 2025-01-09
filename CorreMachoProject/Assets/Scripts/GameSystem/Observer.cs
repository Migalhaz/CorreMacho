using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
	public static class InputObserver
	{
		public static event Action<InputAction.CallbackContext> OnJump;
		public static void Jump(InputAction.CallbackContext context) => OnJump?.Invoke(context);
	}

	public static class PlayerMoveObserver
	{
		public static event Action OnPlayerJump;
		public static void PlayerJump() => OnPlayerJump?.Invoke();
	}

	public static class PlayerHealthObserver
	{
		public static event Action OnPlayerHit;
		public static event Action OnPlayerDie;
		public static void PlayerHit() => OnPlayerHit?.Invoke();
		public static void PlayerDie() => OnPlayerDie?.Invoke();
	}
}