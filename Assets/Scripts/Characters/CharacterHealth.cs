using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterHealth : NetworkBehaviour, Damage.IDamagable {

		[Header("Settings")]
		[SerializeField] private int _maxHealth;
		private int _currentHealth;

		private void Start() {
			SetHealth(_maxHealth);
		}

		[Rpc(SendTo.Everyone)]
		public void DealDamageRpc(Damage.DamageInfo damageInfo) {
			SetHealth(_currentHealth - damageInfo.Damage);
		}

		private void SetHealth(int value) {
			_currentHealth = Mathf.Clamp(value, 0, _maxHealth);
		}
	}
}