using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons {
	public class WeaponSword : WeaponItem {

		[Header("Values")]
		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _distance;
		[SerializeField] private float _radius;

		private void OnDrawGizmos() {
			Vector3 position = _ownerTransform == null ? Vector3.zero : GetTriggerPosition();
			Gizmos.DrawSphere(position, _radius);
		}

		public override void OnPrimaryUse(bool value) {
			if (!value) {
				return;
			}

			_owner.Animator.SetTrigger(GetAttackId());

			PrimaryUseRpc();
		}

		[Rpc(SendTo.Server, Delivery = RpcDelivery.Unreliable)]
		public override void PrimaryUseRpc() {
			Damage.DamageInfo damageInfo = new Damage.DamageInfo(_damage);
			Damage.IDamagable[] damagables = Damage.DamageController.GetDamagables(GetTriggerPosition(), _radius);
			System.Array.ForEach(damagables, x => x.DealDamageRpc(damageInfo));
		}

		public override void OnSecondaryUse(bool value) { }

		public override void SecondaryUseRpc() { }

		private Vector3 GetTriggerPosition() {
			return _ownerTransform.position + _offset + _ownerTransform.forward * _distance;
		}
	}
}