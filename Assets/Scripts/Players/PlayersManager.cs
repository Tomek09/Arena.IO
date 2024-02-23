using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

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

				UpdateReadyStatusRPC(id, false);
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
				UpdateReadyStatusRPC(item.Key, value);
			}
		}

		[Rpc(SendTo.SpecifiedInParams)]
		private void AddClientRPC(ulong clientId, RpcParams rpcParams) => AddPlayer(clientId);

		[Rpc(SendTo.SpecifiedInParams)]
		private void RemoveClientRPC(ulong clientId, RpcParams rpcParams) => RemovePlayer(clientId);

		private void ReciveReadyStatus(ulong clientId, bool value) {
			UpdateReadyStatusRPC(clientId, value);
		}

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


		public void SetReadyStatus(bool value) => SendReadyStatusRequestRPC(LocalPlayerInstance.ClientId, value);

		public void SetReadyStatus() => SetReadyStatus(!LocalPlayerInstance.IsReady);


		public PlayerInstance GetPlayerInstance(ulong clientId) => _currentPlayers[clientId];

		public PlayerInstance[] GetPlayerInstances() => _currentPlayers.Values.ToArray();

		public bool IsPlayersReady() => _currentPlayers.All(item => item.Value.IsReady);


		[Rpc(SendTo.Server)]
		public void SendReadyStatusRequestRPC(ulong clientId, bool value) {
			ReciveReadyStatus(clientId, value);
		}


		[Rpc(SendTo.Everyone)]
		private void UpdateReadyStatusRPC(ulong clientId, bool value) {
			_currentPlayers[clientId].IsReady = value;
			OnReadyStatusPlayer?.Invoke(clientId, value);
		}
	}
}