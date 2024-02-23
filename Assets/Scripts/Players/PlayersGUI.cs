using UnityEngine;

namespace Assets.Scripts.Players {
	public class PlayersGUI : MonoBehaviour {

		private void OnGUI() {
			Utilities.GUIDrawer.DrawLabel(2, 0, "[PLAYERS]", TextAnchor.MiddleCenter);

			PlayerInstance[] currentPlayers = PlayersManager.Instance.GetPlayerInstances();
			for (int i = 0; i < currentPlayers.Length; i++) {
				DrawPlayer(i, currentPlayers[i]);
			}
		}

		private void DrawPlayer(int index, PlayerInstance playerInstance) {
			string output = $"{playerInstance.ClientId} [{playerInstance.TeamId}] [{playerInstance.IsReady}]";
			Utilities.GUIDrawer.DrawLabel(2, index + 1, output, TextAnchor.MiddleLeft);
		}
	}
}