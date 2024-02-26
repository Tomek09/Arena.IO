using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Characters {
	public class CharacterEquipment : MonoBehaviour {

		[Header("Components")]
		private CharacterBase _character;
		public Items.ItemBase CurrentItem { get; private set; }

		public event UnityAction<Items.ItemBase> OnItemEquip = delegate { };
		public event UnityAction<Items.ItemBase> OnUnequip = delegate { };

		private void OnEnable() {
			_character.InputHandler.PrimaryMouseEvent += HandlePrimary;
			_character.InputHandler.SecondaryMouseEvent += HandleSecondary;
		}

		private void OnDisable() {
			_character.InputHandler.PrimaryMouseEvent -= HandlePrimary;
			_character.InputHandler.SecondaryMouseEvent -= HandleSecondary;
		}

		private void Awake() {
			_character = GetComponent<CharacterBase>();
		}


		private void HandlePrimary(bool value) => CurrentItem?.OnPrimaryUse(value);

		private void HandleSecondary(bool value) => CurrentItem?.OnSecondaryUse(value);


		public void EquipItem(Items.ItemCode itemCode) {
			if (CurrentItem != null) {
				UnequipItem();
			}

			Items.ItemBase item = _character.Inventory.GetItem(itemCode);
			CurrentItem = item;
			item.Equip();

			OnItemEquip?.Invoke(CurrentItem);
		}

		private void UnequipItem() {
			Items.ItemBase item = CurrentItem;
			CurrentItem = null;
			item.Unequip();

			OnUnequip?.Invoke(item);
		}
	}
}