using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Pickupable {
	public abstract class PickupableBase : NetworkBehaviour {

		[Header("Settings")]
		[SerializeField] private bool _isServerOnly = true;


		private void OnTriggerEnter(Collider other) {
			if (_isServerOnly && !IsServer) {
				return;
			}

			if (!TryGetCharacter(other, out Characters.CharacterBase character)) {
				return;
			}

			OnCharacterEnter(character);
		}

		private void OnTriggerExit(Collider other) {
			if (_isServerOnly && !IsServer) {
				return;
			}

			if (!TryGetCharacter(other, out Characters.CharacterBase character)) {
				return;
			}

			OnCharacterExit(character);
		}


		public abstract void OnCharacterEnter(Characters.CharacterBase character);
		public abstract void OnCharacterExit(Characters.CharacterBase character);
  
		private bool TryGetCharacter(Collider other, out Characters.CharacterBase character) => other.TryGetComponent(out character);
	}
}