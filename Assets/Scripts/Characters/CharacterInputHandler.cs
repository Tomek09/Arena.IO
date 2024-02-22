using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Characters {
	public class CharacterInputHandler : MonoBehaviour, Inputs.GameControls.ICharacterActions {

		[Header("Components")]
		private Inputs.GameControls _inputsActions;

		public event UnityAction<Vector2> MoveInputEvent = delegate { };

		private void Start() {
			_inputsActions = new Inputs.GameControls();
			_inputsActions.Character.AddCallbacks(this);

			EnableInputs(_inputsActions.Character);
		}

		private void EnableInputs(InputActionMap actionMap) => actionMap.Enable();

		private void DisableInputs(InputActionMap actionMap) => actionMap.Disable();


		public void OnMoveInput(InputAction.CallbackContext context) {
			Vector2 moveInput = context.ReadValue<Vector2>();
			MoveInputEvent?.Invoke(moveInput);
		}
	}
}