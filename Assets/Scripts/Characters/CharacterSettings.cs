
namespace Assets.Scripts.Characters {
	public class CharacterSettings {

		[System.Serializable]
		public class LocomotionSettings {
			public float MoveSpeed = 3f;
			public float RotateSpeed = 3f;
			public float InputSmoothSpeed = 0.05f;
		}

		[System.Serializable]
		public class GravitySettings {
			public float Gravity = -9.81f;
			public float MaxFallSpeed = -10f;
			public float JumpHeight;
			public float JumpFalloff;

		}

		[System.Serializable]
		public class AnimationSettings {
			public float LocomotionDampTime = 0.1f;
		}
	}
}