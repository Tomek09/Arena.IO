using UnityEngine;

namespace Assets.Scripts.Utilities {
	public class Knockback : MonoBehaviour {

		private CharacterController _controller;
		private float _force;
		private float _duration;
		private float _startTime;
		private Vector3 _direction;
		private bool _isActive = false;

		private void Update() {
			if (!_isActive) {
				return;
			}

			_controller.Move(_direction * _force * Time.deltaTime);

			if (Time.time - _startTime >= _duration) {
				_isActive = false;
			}
		}

		public void Initialize(CharacterController controller, Vector3 direction, float force, float duration) {
			_controller = controller;
			_direction = direction.normalized;
			_force = force;
			_duration = duration;

			_startTime = Time.time;
			_isActive = true;
		}
	}
}