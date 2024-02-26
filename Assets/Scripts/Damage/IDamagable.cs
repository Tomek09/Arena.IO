using UnityEngine;

namespace Assets.Scripts.Damage {
	public interface IDamagable {

		public void DealDamageRpc(DamageInfo damageInfo);
	}
}