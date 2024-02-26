using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterInventory : MonoBehaviour {

		[Header("Components")]
		private CharacterBase _character;
		private Items.ItemBase[] _characterItems;
		private Dictionary<Items.ItemCode, Items.ItemBase> _itemsByCode;

		private void Awake() {
			_character = GetComponent<CharacterBase>();
			_characterItems = gameObject.GetComponentsInChildren<Items.ItemBase>(true);
			System.Array.ForEach(_characterItems, x => x.Initialize(_character));
			_itemsByCode = _characterItems.ToDictionary((x) => x.ItemCode, (x) => x);
		}

		public Items.ItemBase GetItem(Items.ItemCode itemCode) {
			return _itemsByCode[itemCode];
		}
	}
}