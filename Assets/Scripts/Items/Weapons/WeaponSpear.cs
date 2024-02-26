using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Items.Weapons {
	public class WeaponSpear : WeaponItem {

		public override void OnPrimaryUse(bool value) {
			if (!value) {
				return;
			}

			_owner.Animator.SetTrigger(GetAttackId());
		}

		public override void PrimaryUseRpc() { }

		public override void OnSecondaryUse(bool value) { }

		public override void SecondaryUseRpc() { }
	}
}