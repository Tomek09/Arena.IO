using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterBase : NetworkBehaviour {

		[field: Header("Components")]
		[field: SerializeField] public CharacterController Controller { get; private set; }
		[field: SerializeField] public Animator Animator { get; private set; }

		[field: Header("Components")]
		[field: SerializeField] public CharacterInputHandler InputHandler { get; private set; }
		[field: SerializeField] public CharacterCamera Camera { get; private set; }
		[field: SerializeField] public CharacterLocomotion Locomotion { get; private set; }
		[field: SerializeField] public CharacterAnimation Animation { get; private set; }
		[field: SerializeField] public CharacterGravity Gravity { get; private set; }
		[field: SerializeField] public CharacterEquipment Equipment { get; private set; }
		[field: SerializeField] public CharacterInventory Inventory { get; private set; }
		[field: SerializeField] public CharacterHealth Health { get; private set; }

		private void Start() {
			if (!IsLocalPlayer) {
				return;
			}

			Cameras.CameraManager.Instance.SetPreset(Cameras.CameraType.Character);
			Cameras.CameraManager.Instance.SetTarget(transform);
		}

		private void Update() {
			if (!IsLocalPlayer) {
				return;
			}

			Locomotion.Tick();
			Animation.Tick();
			Gravity.Tick();
		}

	}
}