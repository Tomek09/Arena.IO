using Unity.Netcode;

namespace Assets.Scripts.Lobby {
	public class LobbyManager : Utilities.SingletonNetwork<LobbyManager> {


		private void OnEnable() {
			if (IsServer) {
				return;
			}

			Scenes.ScenesManager.OnClientSceneLoadCompleted += OnClientConnected;
			NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
		}

		private void OnDisable() {
			if (!IsServer) {
				return;
			}

			Scenes.ScenesManager.OnClientSceneLoadCompleted -= OnClientConnected;
			NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
		}

		public void Disconnect() {
			NetworkManager.Singleton.Shutdown();
			Scenes.ScenesManager.Instance.LoadScene("Menu");
		}

		public void StartGame() {
			if (!Players.PlayersManager.Instance.IsPlayersReady()) {
				return;
			}
			Players.PlayersManager.Instance.SetPlayersReadyStatus(false);
			Scenes.ScenesManager.Instance.LoadScene("Game Scene");
		}

		private void OnClientConnected(ulong clientId, string _) {
			if (!IsServer) {
				return;
			}

			Players.PlayersManager.Instance.ReciveNewPlayer(clientId);
		}

		private void OnClientDisconnect(ulong clientId) {
			if (!IsServer) {
				return;
			}

			Players.PlayersManager.Instance.ReciveDisconnect(clientId);
		}
	}
}