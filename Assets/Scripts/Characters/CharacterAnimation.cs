using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterAnimation : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private CharacterSettings.AnimationSettings _animation;
		private CharacterBase _character;
		private Animator _animator;

		[Header("Ids")]
		private int _locomotionId;
		private int _jumpId;
		private int _isFalling;

		[Header("Values")]
		private float _locomotionValue;

		private void OnEnable() {
			_character.InputHandler.MovementEvent += HandleLocomotion;
			_character.Gravity.FallingEvent += HandleFalling;
			_character.Locomotion.JumpEvent += HandleJump;
		}

		private void OnDisable() {
			_character.InputHandler.MovementEvent -= HandleLocomotion;
			_character.Gravity.FallingEvent -= HandleFalling;
			_character.Locomotion.JumpEvent -= HandleJump;
		}

		private void Awake() {
			_character = GetComponent<CharacterBase>();
			_animator = _character.Animator;

			_locomotionId = Animator.StringToHash("locomotion");
			_jumpId = Animator.StringToHash("jump");
			_isFalling = Animator.StringToHash("isFalling");
		}

		private void Update() {
			HandleLocomotion();
		}

		private void HandleLocomotion(Vector2 moveInput) {
			_locomotionValue = Equals(moveInput, Vector2.zero) ? 0f : .5f;
		}

		private void HandleLocomotion() {
			_animator.SetFloat(_locomotionId, _locomotionValue, _animation.LocomotionDampTime, Time.deltaTime);
		}

		private void HandleJump() => _animator.SetTrigger(_jumpId);
	
		private void HandleFalling(bool value) {
			_animator.SetBool(_isFalling, value);
		}
	}
}