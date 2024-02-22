using UnityEngine;

namespace Assets.Scripts.Utilities {
	[System.Serializable]
	public class GroundCheck  {

		[Header("Settings")]
		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _radius;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private Transform _transform;
		

		public void Draw() {
			Gizmos.color = IsGrounded() ? Color.green : Color.red;
			Gizmos.DrawSphere(_transform.position + _offset, _radius);
		}

		public bool IsGrounded() {
			return Physics.CheckSphere(_transform.position + _offset, _radius, _layerMask);
		}
	}
}