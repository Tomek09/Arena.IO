using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Characters {
	public class CharacterGravity : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private Utilities.GroundCheck _groundCheck;
		private CharacterLocomotion _locomotion;

		private bool _isFalling;
		private bool _wasFalling = true;

		public event UnityAction<bool> FallingEvent = delegate { };

		private void OnDrawGizmos() {
			_groundCheck.Draw();
		}

		private void Awake() {
			CharacterBase character = GetComponent<CharacterBase>();
			_locomotion = character.Locomotion;
		}

		public void Tick() {
			_wasFalling = _isFalling;
			_isFalling = IsFalling();
			if(_isFalling && !_wasFalling) {
				FallingEvent?.Invoke(true);
			} else if (!_isFalling && _wasFalling) {
				FallingEvent?.Invoke(false);
			}
		}

		public bool IsGrounded() => _groundCheck.IsGrounded();

		public bool IsFalling() => !IsGrounded() && _locomotion.Velocity.y < 0f;
	}
}