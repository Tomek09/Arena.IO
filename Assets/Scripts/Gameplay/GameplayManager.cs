using UnityEngine;

namespace Assets.Scripts.Gameplay {
	public class GameplayManager : Utilities.SingletonNetwork<GameplayManager> {

		public override void OnNetworkSpawn() {
			if (!IsServer) {
				return;
			}

			Players.PlayersManager.OnReadyStatusPlayer += OnPlayerReadyChange;
		}

		public override void OnNetworkDespawn() {
			if (!IsServer) {
				return;
			}

			Players.PlayersManager.OnReadyStatusPlayer += OnPlayerReadyChange;
		}

		private void Start() {
			Players.PlayersManager.Instance.RequestReadyStatus(true);
		}

		private void OnPlayerReadyChange(ulong _, bool __) {
			if (!Players.PlayersManager.Instance.IsPlayersReady()) {
				return;
			}

			Debug.Log("Start Game!");
		}
	}
}