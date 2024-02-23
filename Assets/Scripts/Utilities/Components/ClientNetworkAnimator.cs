using Unity.Netcode.Components;
using UnityEngine;

namespace Assets.Scripts.Utilities.Components {
    [DisallowMultipleComponent]
    public class ClientNetworkAnimator : NetworkAnimator {

		protected override bool OnIsServerAuthoritative() {
			return false;
		}
	}
} 