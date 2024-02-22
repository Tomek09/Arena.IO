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

		[Header("Values")]
		private float _locomotionValue;

		private void OnEnable() {
			_character.InputHandler.MoveInputEvent += HandleLocomotion;
		}

		private void OnDisable() {
			_character.InputHandler.MoveInputEvent -= HandleLocomotion;
		}

		private void Awake() {
			_character = GetComponent<CharacterBase>();
			_animator = _character.Animator;

			_locomotionId = Animator.StringToHash("locomotion");
		}

		private void Update() {
			_animator.SetFloat(_locomotionId, _locomotionValue, _animation.LocomotionDampTime, Time.deltaTime);
		}

		private void HandleLocomotion(Vector2 moveInput) {
			_locomotionValue = Equals(moveInput, Vector2.zero) ? 0f : .5f;
		}
	}
}