using Assets.Scripts.Characters;
using UnityEngine;

namespace Assets.Scripts.Pickupable {
	public abstract class PickupableBase : MonoBehaviour, IPickupable {

		public abstract void Pickup(CharacterBase character);
	}
}