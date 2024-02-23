using UnityEngine;

namespace Assets.Scripts.Pickupable {
	public interface IPickupable {

		public void Pickup(Characters.CharacterBase character);
	}
}