using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes {
	public class ScenesManager : Utilities.SingletonPersistent<ScenesManager> {

		public static event System.Action<ulong, string> OnClientSceneLoadCompleted = delegate { };

		public void InitializeNetworkCallbacks() {
			NetworkManager.Singleton.SceneManager.OnLoadComplete -= OnLoadComplete;
			NetworkManager.Singleton.SceneManager.OnLoadComplete += OnLoadComplete;
		}

		public void LoadScene(string sceneToLoad, float delay = 0f, bool isNetworkSessionActive = true) {
			StartCoroutine(ChangeScene(sceneToLoad, delay, isNetworkSessionActive));
		}

		private IEnumerator ChangeScene(string sceneToLoad, float delay, bool isNetworkSessionActive) {
			yield return new WaitForSeconds(delay);

			if (isNetworkSessionActive && NetworkManager.Singleton.IsServer) {
				LoadSceneNetwork(sceneToLoad);
			} else {
				LoadSceneLocal(sceneToLoad);
			}
		}

		private void LoadSceneLocal(string sceneToLoad) {
			SceneManager.LoadScene(sceneToLoad);
		}

		private void LoadSceneNetwork(string sceneToLoad) {
			NetworkManager.Singleton.SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
		}

		private void OnLoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode) {
			if (!NetworkManager.Singleton.IsServer)
				return;

			OnClientSceneLoadCompleted?.Invoke(clientId, sceneName);
		}
	}
}