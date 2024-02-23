using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;

namespace Assets.Scripts.Players {
	public class PlayersManager : Utilities.SingletonNetworkPersistent<PlayersManager> {

		private readonly Dictionary<ulong, PlayerInstance> _currentPlayers = new Dictionary<ulong, PlayerInstance>();
		public PlayerInstance LocalPlayerInstance { get; private set; }

		public static event System.Action<ulong> OnAddPlayer = delegate { };
		public static event System.Action<ulong> OnRemovePlayer = delegate { };
		public static event System.Action<ulong, bool> OnReadyStatusPlayer = delegate { };
		public static event System.Action OnClearPlayers = delegate { };

		public override void OnNetworkDespawn() {
			base.OnNetworkDespawn();
			ClearPlayers();
		}

		// Server

		public void ReciveNewPlayer(ulong clientId) {
			// Get current Current
			List<ulong> currentPlayersIds = _currentPlayers.Keys.ToList();

			// Update current clients about new client && new client about current clients.
			foreach (ulong id in currentPlayersIds) {
				SendNewClient(id, clientId);    // Add new client to current client
				SendNewClient(clientId, id);    // Add current client to new client

				UpdateReadyStatus(id, false);
			}

			// Send new client to himself
			SendNewClient(clientId, clientId);
		}

		public void ReciveDisconnect(ulong clientId) {
			RpcParams rpcParams = RpcTarget.Not(clientId, RpcTargetUse.Temp);
			RemoveClientRPC(clientId, rpcParams);
		}

		private void SendNewClient(ulong clientId, params ulong[] clientIds) {
			AddClientRPC(clientId, RpcTarget.Group(clientIds, RpcTargetUse.Temp));
		}

		public void SetPlayersReadyStatus(bool value) {
			foreach (KeyValuePair<ulong, PlayerInstance> item in _currentPlayers) {
				SetClientReadyStatusRPC(item.Key, value);
			}
		}

		[Rpc(SendTo.SpecifiedInParams)]
		private void AddClientRPC(ulong clientId, RpcParams rpcParams) => AddPlayer(clientId);

		[Rpc(SendTo.SpecifiedInParams)]
		private void RemoveClientRPC(ulong clientId, RpcParams rpcParams) => RemovePlayer(clientId);

		// Clients

		private void AddPlayer(ulong clientId) {
			PlayerInstance playerInstance = new PlayerInstance(clientId);
			_currentPlayers.Add(clientId, playerInstance);

			OnAddPlayer?.Invoke(clientId);

			if (LocalPlayerInstance == null && Equals(clientId, NetworkManager.Singleton.LocalClientId)) {
				LocalPlayerInstance = playerInstance;
			}
		}

		private void RemovePlayer(ulong clientId) {
			_currentPlayers.Remove(clientId);
			OnRemovePlayer?.Invoke(clientId);
		}

		private void ClearPlayers() {
			_currentPlayers.Clear();
			OnClearPlayers?.Invoke();
		}

		private void UpdateReadyStatus(ulong clientId, bool value) {
			_currentPlayers[clientId].IsReady = value;
			OnReadyStatusPlayer?.Invoke(clientId, value);
		}

		public void ChangeReadyStatus() {
			bool value = LocalPlayerInstance.IsReady;
			RequestReadyStatus(!value);
		}

		public void RequestReadyStatus(bool value) {
			RequestReadyStatus(LocalPlayerInstance.ClientId, value);
		}

		public void RequestReadyStatus(ulong clientId, bool value) {
			SetClientReadyStatusRPC(clientId, value);
		}

		public PlayerInstance GetPlayerInstance(ulong clientId) {
			return _currentPlayers[clientId];
		}

		public PlayerInstance[] GetPlayerInstances() {
			return _currentPlayers.Values.ToArray();
		}

		public bool IsPlayersReady() {
			return _currentPlayers.All(item => item.Value.IsReady);
		}

		[Rpc(SendTo.Server)]
		public void SetClientReadyStatusRPC(ulong clientId, bool value) {
			UpdateReadyStatus(clientId, value);
		}
	}
}