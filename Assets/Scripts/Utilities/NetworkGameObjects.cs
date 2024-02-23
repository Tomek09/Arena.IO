using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Utilities {
	public static class NetworkGameObjects {
		public static T GOInstantiate<T>(T prefab, SpawnInfo spawnInfo, bool destroyWithScene = true) where T : NetworkBehaviour {
			T newObject = GOInstantiate(prefab, spawnInfo);
			NetworkObject networkObject = newObject.GetComponent<NetworkObject>();
			networkObject.Spawn(destroyWithScene);

			return newObject;
		}

		public static T GOInstantiateOwnernship<T>(T prefab, SpawnInfo spawnInfo, ulong newClientOwnerId, bool destroyWithScene = true) where T : NetworkBehaviour {
			T newObject = GOInstantiate(prefab, spawnInfo);
			NetworkObject networkObject = newObject.GetComponent<NetworkObject>();
			networkObject.SpawnWithOwnership(newClientOwnerId, destroyWithScene);

			return newObject;
		}

		public static T GOInstantiateAsPlayerObject<T>(T prefab, SpawnInfo spawnInfo, ulong newClientOwnerId, bool destroyWithScene = true) where T : NetworkBehaviour {
			T newObject = GOInstantiate(prefab, spawnInfo);
			NetworkObject networkObject = newObject.GetComponent<NetworkObject>();
			networkObject.SpawnAsPlayerObject(newClientOwnerId, destroyWithScene);

			return newObject;
		}

		private static T GOInstantiate<T>(T prefab, SpawnInfo spawnInfo) where T : NetworkBehaviour {
#if UNITY_EDITOR
			if (!NetworkManager.Singleton.IsServer) {
				Debug.LogError("ERROR: Spawning not happening in the server!");
			}
#endif

			T newObject = Object.Instantiate(prefab, spawnInfo.Position, Quaternion.Euler(spawnInfo.Euler), null);
			return newObject;
		}
	}
}
