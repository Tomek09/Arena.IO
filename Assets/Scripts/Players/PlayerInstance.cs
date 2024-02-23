using UnityEngine;

namespace Assets.Scripts.Players {
	[System.Serializable]
	public class PlayerInstance {

		public ulong ClientId { get; private set; }
		public bool IsReady { get; set; } = false;
		public int TeamId { get; private set; } = 0;

		public PlayerInstance(ulong clientId) {
			ClientId = clientId;
			TeamId = (int)clientId;
		}
	}
}