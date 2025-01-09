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
}