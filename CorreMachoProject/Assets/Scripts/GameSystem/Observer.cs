using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
	public static class InputObserver
	{
		public static event Action<InputAction.CallbackContext> OnJump = null;
		public static void Jump(InputAction.CallbackContext context) => OnJump?.Invoke(context);
	}

	public static class GameSettingsObserver
	{
		public static event Action OnAudioButtonClick = null;
		public static event Action OnPauseButtonClick = null;
		public static void AudioButtonClick() => OnAudioButtonClick?.Invoke();
		public static void PauseButtonClick() => OnPauseButtonClick?.Invoke();
	}

	public static class PlayerMoveObserver
	{
		public static event Action OnPlayerJump = null;
		public static event Action OnPlayerGrounded = null;
		public static void PlayerJump() => OnPlayerJump?.Invoke();
		public static void PlayerGrounded() => OnPlayerGrounded?.Invoke();
	}

	public static class PlayerHealthObserver
	{
		public static event Action OnPlayerHit = null;
		public static event Action OnPlayerDie = null;
		public static void PlayerHit() => OnPlayerHit?.Invoke();
		public static void PlayerDie() => OnPlayerDie?.Invoke();
	}

	public static class PlayerPointsObserver
	{
		public static event Action<int> OnUpdatePoints = null;
		public static void UpdatePoint(int newPointValue) => OnUpdatePoints?.Invoke(newPointValue);
	}
}