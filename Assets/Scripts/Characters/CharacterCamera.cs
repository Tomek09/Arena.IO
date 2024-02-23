using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterCamera : MonoBehaviour {

		[Header("Components")]
		private Camera _mainCamera;
		private Plane _mouseCheckPlane;

		private void Awake() {
			_mainCamera = Camera.main;
			_mouseCheckPlane = new Plane(Vector3.up, Vector3.zero);
		}

		public Vector3 GetLookPoint() {
			Vector3 point = Vector3.zero;
			Vector3 cursorPosition = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
			cursorPosition.y = -30;
			Ray ray = _mainCamera.ScreenPointToRay(cursorPosition);
			if (_mouseCheckPlane.Raycast(ray, out float enter)) {
				point = ray.GetPoint(enter);
			}

			return point;
		}

		public Vector3 GetLookDirection() {
			Vector3 direction = GetLookPoint() - transform.position;
			direction.y = 0f;

			return direction;
		}
	}
}