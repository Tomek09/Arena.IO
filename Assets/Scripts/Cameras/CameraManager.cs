using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Cameras {
	public class CameraManager : Utilities.Singleton<CameraManager> {

		[Header("Components")]
		[SerializeField] private Transform _body;
		[SerializeField] private Transform _rig;
		[SerializeField] private Transform _lens;

		[Header("Presets")]
		[SerializeField] private CameraPreset[] _presets;
		private CameraPreset _currentPreset;

		private Transform _followTarget;

		private void Start() {
			SetPreset(_presets[0]);
		}

		private void Update() {
			HandleRig();
			HandleZoom();
		}

		private void LateUpdate() {
			HandleBody();
			HandleFollow();
		}

		public void SetTarget(Transform target) {
			_followTarget = target;
		}


		private void HandleBody() {
			if (_followTarget) {
				return;
			}

			Vector3 targetValue = _currentPreset.Body.Value;
			_body.position = Vector3.Lerp(_body.position, targetValue, _currentPreset.Body.UpdateTime * Time.deltaTime);
		}

		private void HandleRig() {
			Vector3 targetValue = _currentPreset.Rig.Value;
			_rig.localEulerAngles = Vector3.Lerp(_rig.localEulerAngles, targetValue, _currentPreset.Rig.UpdateTime * Time.deltaTime);
		}

		private void HandleZoom() {
			Vector3 targetValue = Vector3.forward * _currentPreset.Lens.Value;
			_lens.localPosition = Vector3.Lerp(_lens.localPosition, targetValue, _currentPreset.Lens.UpdateTime * Time.deltaTime);
		}

		private void HandleFollow() {
			if (!_followTarget) {
				return;
			}

			Vector3 currentPosition = _body.position;
			Vector3 desinationPosition = _followTarget.position + _currentPreset.Body.Value;
			if (_currentPreset.FollowFreezeY) {
				desinationPosition.y = _currentPreset.Body.Value.y;
			}

			_body.position = Vector3.Lerp(currentPosition, desinationPosition, _currentPreset.FollowSpeed * Time.deltaTime);
		}


		private void SetPreset(CameraPreset cameraPreset) {
			_currentPreset = cameraPreset;
			_body.DOMove(_currentPreset.Body.Value, _currentPreset.Body.InitDuration);
			_rig.DOLocalRotate(_currentPreset.Rig.Value, _currentPreset.Rig.InitDuration);
			_lens.DOLocalMove(Vector3.forward * _currentPreset.Lens.Value, _currentPreset.Lens.InitDuration);
		}
	}
}