using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Characters {
	public class CharacterLocomotion : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private CharacterSettings.LocomotionSettings _locomotion;
		[SerializeField] private CharacterSettings.GravitySettings _gravity;
		private CharacterBase _character;
		private CharacterController _controller;

		[Header("Inputs")]
		private Vector2 _moveInput;

		[Header("Input Smooth")]
		private Vector2 _currentMoveInput;
		private Vector2 _moveInputVelocity;

		[Header("Velocity")]
		private float _currentGravity;
		private Vector3 _jumpForce;
		private Vector3 _jumpForceVelocity;
		public Vector3 Velocity { get; private set; }

		public event UnityAction JumpEvent = delegate { };

		private void OnEnable() {
			_character.InputHandler.MovementEvent += SetMoveInput;
			_character.InputHandler.JumpEvent += HandleJump;
		}

		private void OnDisable() {
			_character.InputHandler.MovementEvent -= SetMoveInput;
			_character.InputHandler.JumpEvent -= HandleJump;
		}

		private void Awake() {
			_character = GetComponent<CharacterBase>();
			_controller = _character.Controller;
		}

		public void Tick() {
			HandleInputs();
			HandleGravity();
			HandleMovement();
			HandleRotation();
			HandleJump();
		}

		private void HandleInputs() {
			_currentMoveInput = Vector2.SmoothDamp(_currentMoveInput, _moveInput, ref _moveInputVelocity, _locomotion.InputSmoothSpeed);
		}

		private void HandleMovement() {
			Vector3 moveDirection = new Vector3(_currentMoveInput.x, 0f, _currentMoveInput.y);
			Vector3 velocity = _locomotion.MoveSpeed * Time.deltaTime * moveDirection;
			velocity.y = _currentGravity;
			velocity += _jumpForce * Time.deltaTime;

			Velocity = velocity;
			_controller.Move(Velocity);
		}

		private void HandleRotation() {
			Vector3 direction = _character.Camera.GetLookDirection();

			if (Equals(direction, Vector3.zero)) {
				return;
			}
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _locomotion.RotateSpeed * Time.deltaTime);
		}

		private void HandleGravity() {
			if (_currentGravity > _gravity.MaxFallSpeed && _jumpForce.y < 0.1f) {
				_currentGravity += _gravity.Gravity * Time.deltaTime;
			}

			if (_currentGravity < 0.1f && _character.Gravity.IsGrounded()) {
				_currentGravity = 0f;
			}

			if (_jumpForce.y > 0.1f) {
				_currentGravity = 0f;
			}
		}

		private void HandleJump() {
			_jumpForce = Vector3.SmoothDamp(_jumpForce, Vector3.zero, ref _jumpForceVelocity, _gravity.JumpFalloff);
		}

		private void SetMoveInput(Vector2 moveInput) => _moveInput = moveInput;

		private void HandleJump(bool value) {
			if (!value || !_character.Gravity.IsGrounded()) {
				return;
			}

			_jumpForce = Vector3.up * _gravity.JumpHeight;
			JumpEvent?.Invoke();
		}
	}
}