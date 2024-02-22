using UnityEngine;

namespace Assets.Scripts.Cameras {
	[CreateAssetMenu(fileName = "Camera Preset", menuName = "SO/Cameras/Preset")]
	public class CameraPreset : ScriptableObject {

		[System.Serializable]
		public class Settings<T> {
			public T Value;
			public float InitDuration;
			public float UpdateTime;

			public Settings(T value, float initDuration, float updateTime) {
				Value = value;
				InitDuration = initDuration;
				UpdateTime = updateTime;
			}
		}

		[field: Header("Values")]
		[field: SerializeField] public Settings<Vector3> Body { get; private set; } = new Settings<Vector3>(Vector3.zero, .1f, 1f);
		[field: SerializeField] public Settings<Vector3> Rig { get; private set; } = new Settings<Vector3>(Vector3.right * 60f, .1f, 1f);
		[field: SerializeField] public Settings<float> Lens { get; private set; } = new Settings<float>(-10f, .1f, 1f);

		[field: Header("Follow")]
		[field: SerializeField] public float FollowSpeed { get; private set; } = 2f;
		[field: SerializeField] public bool FollowFreezeY { get; private set; } = false;

	}
}