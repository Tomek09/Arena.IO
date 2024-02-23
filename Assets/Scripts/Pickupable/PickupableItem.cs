using UnityEngine;

namespace Assets.Scripts.Pickupable {
	public class PickupableItem : PickupableBase {

		[Header("Settings")]
		[SerializeField] private Items.ItemCode _itemCode;

		public override void Pickup(Characters.CharacterBase character) {
			if (character.Equipment.CurrentItem != null) {
				character.Equipment.UnequipItem();
			}

			character.Equipment.EquipItem(_itemCode);

			Destroy(gameObject);
		}
	}
}