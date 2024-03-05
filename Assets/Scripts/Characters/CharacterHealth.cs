using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterHealth : NetworkBehaviour, Damage.IDamagable {

		[Header("Components")]
		[SerializeField] private Utilities.Knockback _knockback;
		private CharacterBase _character;

		[Header("Settings")]
		[SerializeField] private int _maxHealth;
		private int _currentHealth;

		[Header("Knockback")]
		[SerializeField] private float _force;
		[SerializeField] private float _duration;


		private void Awake() {
			_character = GetComponent<CharacterBase>();
		}

		private void Start() {
			SetHealth(_maxHealth);
		}

		[Rpc(SendTo.Everyone)]
		public void DealDamageRpc(Damage.DamageInfo damageInfo) {
			CharacterBase character = CharactersManager.Instance.GetCharacter(damageInfo.DamageDealer);
			if(character == _character) {
				return;
			}

			DealDamage(damageInfo.Damage);
			ApplyKnockback(character);
		}

		private void DealDamage(int value) {
			SetHealth(_currentHealth - value);
		}

		private void ApplyKnockback(CharacterBase character) {
			Vector3 direction = transform.position - character.transform.position;
			direction.y = 1f;
			_knockback.Initialize(_character.Controller, direction, _force, _duration);
		}

		private void SetHealth(int value) {
			_currentHealth = Mathf.Clamp(value, 0, _maxHealth);
		}
	}
}