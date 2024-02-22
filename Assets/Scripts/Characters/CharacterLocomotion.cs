using UnityEditor.Rendering;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterLocomotion : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private CharacterSettings.LocomotionSettings _locomotion;
		[SerializeField] private CharacterSettings.GravitySettings _gravity;
		[SerializeField] private Utilities.GroundCheck _groundCheck;
		private CharacterBase _character;
		private CharacterController _controller;
		private Camera _mainCamera;

		[Header("Inputs")]
		private Vector2 _moveInput;
		private Plane _mouseCheckPlane;

		[Header("Input Smooth")]
		private Vector2 _currentMoveInput;
		private Vector2 _moveInputVelocity;

		[Header("Velocity")]
		private float _currentGravity;

		private void OnDrawGizmos() {
			_groundCheck.Draw();
		}

		private void OnEnable() {
			_character.InputHandler.MoveInputEvent += SetMoveInput;
		}

		private void OnDisable() {
			_character.InputHandler.MoveInputEvent -= SetMoveInput;
		}

		private void Awake() {
			_character = GetComponent<CharacterBase>();
			_controller = _character.Controller;
			_mainCamera = Camera.main;

			_mouseCheckPlane = new Plane(Vector3.up, Vector3.zero);
		}

		private void Update() {
			HandleInputs();
			HandleGravity();
			HandleMovement();
			HandleRotation();
		}

		private void HandleInputs() {
			_currentMoveInput = Vector2.SmoothDamp(_currentMoveInput, _moveInput, ref _moveInputVelocity, _locomotion.InputSmoothSpeed);
		}

		private void HandleMovement() {
			Vector3 moveDirection = new Vector3(_currentMoveInput.x, 0f, _currentMoveInput.y) * _locomotion.MoveSpeed;
			moveDirection.y = _currentGravity;

			_controller.Move(moveDirection * Time.deltaTime);
		}

		private void HandleRotation() {
			Vector3 direction = GetLookDirection();

			if (Equals(direction, Vector3.zero)) {
				return;
			}
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _locomotion.RotateSpeed * Time.deltaTime);
		}

		private void HandleGravity() {
			if (_currentGravity > _gravity.MaxFallSpeed) {
				_currentGravity += _gravity.Gravity * Time.deltaTime;
			}

			if (_currentGravity < 0.1f && _groundCheck.IsGrounded()) {
				_currentGravity = 0f;
			}
		}

		private void SetMoveInput(Vector2 moveInput) => _moveInput = moveInput;

		private Vector3 GetLookPoint() {
			// Move it to Camera script?

			Vector3 point = Vector3.zero;
			Ray ray = _mainCamera.ScreenPointToRay(UnityEngine.InputSystem.Mouse.current.position.ReadValue());
			if (_mouseCheckPlane.Raycast(ray, out float enter)) {
				point = ray.GetPoint(enter);
			}

			return point;
		}

		private Vector3 GetLookDirection() {
			Vector3 direction = GetLookPoint() - transform.position;
			direction.y = 0f;

			return direction;
		}
	}
}