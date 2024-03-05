using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharactersManager : Utilities.SingletonNetwork<CharactersManager> {

		[Header("Prefabs")]
		[SerializeField] private CharacterBase _characterPrefab;
		private Dictionary<ulong, CharacterBase> _characterByClientId;

		private void Start() {
			_characterByClientId = new Dictionary<ulong, CharacterBase>();
		}

		public void InitializeCharacters() {
			SpawnCharacters();
		}

		private void SpawnCharacters() {
			Players.PlayerInstance[] players = Players.PlayersManager.Instance.GetPlayerInstances();
			System.Array.ForEach(players, x => SpawnCharacter(x.ClientId));
		}

		public void SpawnCharacter(ulong clientId) {
			Utilities.SpawnInfo spawnInfo = new Utilities.SpawnInfo($"Character - {clientId}", Vector3.zero, Vector3.zero);
			CharacterBase character = Utilities.NetworkGameObjects.GOInstantiateAsPlayerObject(_characterPrefab, spawnInfo, clientId);
			AddCharcterRpc(character, clientId);
		}

		[Rpc(SendTo.Everyone)]
		private void AddCharcterRpc(NetworkBehaviourReference characterReference, ulong clientId) {
			if (characterReference.TryGet(out CharacterBase character)) {
				AddCharcter(clientId, character);
			}
		}


		private void AddCharcter(ulong clientId, CharacterBase character) {
			_characterByClientId.Add(clientId, character);
		}

		private void RemoveCharcter(ulong clientId) {

		}

		public CharacterBase GetCharacter(ulong clientId) => _characterByClientId[clientId];
	}

}