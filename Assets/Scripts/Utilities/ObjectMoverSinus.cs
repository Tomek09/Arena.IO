using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities {
	public class ObjectMoverSinus : MonoBehaviour {

		[Header("Values")]
		[SerializeField] private Transform _target;
		[SerializeField] private float _speed;
		[SerializeField] private float _distance;
		private Vector3 _startPosition;

		private void Start() {
			if (_target == null) {
				_target = transform;
			}

			_startPosition = _target.position;
		}

		private void Update() {
			float newPoint = _startPosition.y + Mathf.Sin(Time.time * _speed) * _distance;
			_target.position = new Vector3(_target.position.x, newPoint, _target.position.z);
		}
	}
}