using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Characters {
	public class CharacterInputHandler : MonoBehaviour, Inputs.GameControls.ICharacterActions {

		[Header("Components")]
		private Inputs.GameControls _inputsActions;

		public event UnityAction<Vector2> MovementEvent = delegate { };
		public event UnityAction<bool> JumpEvent = delegate { };
		public event UnityAction<bool> PrimaryMouseEvent = delegate { };
		public event UnityAction<bool> SecondaryMouseEvent = delegate { };

		private void Start() {
			_inputsActions = new Inputs.GameControls();
			_inputsActions.Character.AddCallbacks(this);

			EnableInputs(_inputsActions.Character);
		}

		private void EnableInputs(InputActionMap actionMap) => actionMap.Enable();

		private void DisableInputs(InputActionMap actionMap) => actionMap.Disable();


		public void OnMovement(InputAction.CallbackContext context) {
			Vector2 moveInput = context.ReadValue<Vector2>();
			MovementEvent?.Invoke(moveInput);
		}

		public void OnJump(InputAction.CallbackContext context) {
			if (context.started || context.canceled) {
				float value = context.ReadValue<float>();
				JumpEvent?.Invoke(value > 0f);
			}
		}

		public void OnPrimaryMouseButton(InputAction.CallbackContext context) {
			if (context.started || context.canceled) {
				float value = context.ReadValue<float>();
				PrimaryMouseEvent?.Invoke(value > 0f);
			}
		}

		public void OnSecondaryMouseButton(InputAction.CallbackContext context) {
			if (context.started || context.canceled) {
				float value = context.ReadValue<float>();
				SecondaryMouseEvent?.Invoke(value > 0f);
			}
		}
	}
}