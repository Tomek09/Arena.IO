using UnityEngine;

namespace Assets.Scripts.Lobby {
	public class LobbyGUI : MonoBehaviour {

		private void OnGUI() {
			Utilities.GUIDrawer.DrawLabel(0, 0, "[Lobby]", TextAnchor.MiddleCenter);
			Utilities.GUIDrawer.DrawButton(0, 1, "Ready", Players.PlayersManager.Instance.SetReadyStatus);
			Utilities.GUIDrawer.DrawButton(0, 2, "Disconnect", LobbyManager.Instance.Disconnect);

			if (!LobbyManager.Instance.IsServer) {
				return;
			}

			Utilities.GUIDrawer.DrawLabel(1, 0, "[Server]", TextAnchor.MiddleCenter);
			Utilities.GUIDrawer.DrawButton(1, 1, "Start", () => LobbyManager.Instance.StartGame(false));
			Utilities.GUIDrawer.DrawButton(1, 2, "Fast Start", () => LobbyManager.Instance.StartGame(true));
		}
	}
}