using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharactersManager : Utilities.SingletonNetwork<CharactersManager> {

		[Header("Prefabs")]
		[SerializeField] private CharacterBase _characterPrefab;
		private Dictionary<ulong, CharacterBase> _characterByClientId;


		public void InitializeCharacters() {
			_characterByClientId = new Dictionary<ulong, CharacterBase>();
			SpawnCharacters();
		}

		private void SpawnCharacters() {
			Players.PlayerInstance[] players = Players.PlayersManager.Instance.GetPlayerInstances();
			System.Array.ForEach(players, x => SpawnCharacter(x.ClientId));
		}

		public void SpawnCharacter(ulong clientId) {
			Utilities.SpawnInfo spawnInfo = new Utilities.SpawnInfo($"Character - {clientId}", Vector3.zero, Vector3.zero);
			CharacterBase character = Utilities.NetworkGameObjects.GOInstantiateAsPlayerObject(_characterPrefab, spawnInfo, clientId);
			AddCharcter(clientId, character);
		}

		public void AddCharcter(ulong clientId, CharacterBase character) {
			_characterByClientId.Add(clientId, character);	
		}

		public void RemoveCharcter(ulong clientId) {

		}
	}

}