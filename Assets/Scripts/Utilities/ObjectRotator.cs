using UnityEngine;

namespace Assets.Scripts.Utilities {
	public class ObjectRotator : MonoBehaviour {

		[Header("Values")]
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _direction;
		[SerializeField] private float _speed;

		private void OnValidate() {
			_direction.Normalize();
		}

		private void Start() {
			if (_target == null) {
				_target = transform;
			}
		}

		private void Update() {
			_target.eulerAngles += _speed * Time.deltaTime * _direction;
		}
	}
}