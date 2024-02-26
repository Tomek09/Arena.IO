using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Pickupable {
	public class PickupableItem : PickupableBase {

		[Header("Settings")]
		[SerializeField] private Items.ItemCode _itemCode;

		public override void OnCharacterEnter(Characters.CharacterBase character) {
			CharacterTriggerRpc(character);
		}

		public override void OnCharacterExit(Characters.CharacterBase character) { }

		[Rpc(SendTo.Server)]
		private void CharacterTriggerRpc(NetworkBehaviourReference characterReference) {
			if (!characterReference.TryGet(out Characters.CharacterBase _)) {
				return;
			}

			CharacterEquipRpc(characterReference);
			NetworkObject.Despawn();
		}

		[Rpc(SendTo.Everyone)]
		private void CharacterEquipRpc(NetworkBehaviourReference characterReference) {
			if (!characterReference.TryGet(out Characters.CharacterBase character)) {
				return;
			}

			character.Equipment.EquipItem(_itemCode);
		}
	}
}