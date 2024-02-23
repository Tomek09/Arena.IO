using Unity.Netcode.Components;
using UnityEngine;

namespace Assets.Scripts.Utilities.Components {
	[DisallowMultipleComponent]
	public class ClientNetworkTransform : NetworkTransform {

		protected override bool OnIsServerAuthoritative() {
			return false;
		}
	}

}