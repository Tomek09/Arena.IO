using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Scripts.Main_Menu {
	public class MainMenuGUI : MonoBehaviour {

		private IEnumerator Start() {
			yield return new WaitUntil(() => NetworkManager.Singleton.SceneManager != null);

			Scenes.ScenesManager.Instance.InitializeNetworkCallbacks();
		}

		private void OnGUI() {
			Utilities.GUIDrawer.DrawLabel(0, 0, "[LOCALHOST]", TextAnchor.MiddleCenter);
			Utilities.GUIDrawer.DrawButton(0, 1, "Host", LocalHost);
			Utilities.GUIDrawer.DrawButton(0, 2, "Client", LocalClient);
		}

		private void LocalHost() {
			NetworkManager.Singleton.StartHost();
			Scenes.ScenesManager.Instance.LoadScene("Lobby");
		}

		private void LocalClient() {
			NetworkManager.Singleton.StartClient();
		}
	}
}