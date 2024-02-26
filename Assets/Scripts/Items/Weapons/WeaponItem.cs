using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons {
	public abstract class WeaponItem : ItemBase {

		private enum AttackTrigger {
			Attack_Right = 0,
			Attack_Left = 1,
		}

		[Header("Weapon Settings")]
		[SerializeField] private AttackTrigger _attackType;
		private Dictionary<AttackTrigger, int> _attackTriggerByType;

		[Header("Values")]
		[SerializeField] protected int _damage;

		public override void Initialize(Characters.CharacterBase character) {
			base.Initialize(character);

			_attackTriggerByType = new Dictionary<AttackTrigger, int>() {
				{ AttackTrigger.Attack_Right, Animator.StringToHash("attack_right") },
				{ AttackTrigger.Attack_Left, Animator.StringToHash("attack_left") }
			};
		}

		public int GetAttackId() => _attackTriggerByType[_attackType];
	}
}