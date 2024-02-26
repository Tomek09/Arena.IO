using UnityEngine;

namespace Assets.Scripts.Items {
	public abstract class ItemBase : MonoBehaviour {

		[field: Header("Item Settings")]
		[field: SerializeField] public ItemCode ItemCode { get; private set; } = ItemCode.None;
		protected Characters.CharacterBase _owner;
		protected Transform _ownerTransform;

		public virtual void Initialize(Characters.CharacterBase character) {
			_owner = character;
			_ownerTransform = character.transform;
			gameObject.SetActive(false);
		}

		public virtual void Equip() {
			gameObject.SetActive(true);
		}

		public virtual void Unequip() {
			gameObject.SetActive(false);
		}

		public abstract void OnPrimaryUse(bool value);
		public abstract void OnSecondaryUse(bool value);

		public abstract void PrimaryUseRpc();
		public abstract void SecondaryUseRpc();
	}
}