using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterSkin : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private Renderer[] _renderers;
		private CharacterBase _character;

		private void Awake() {
			_character = GetComponent<CharacterBase>();
		}

		private void Start() {
			ulong clientId = _character.OwnerClientId;
			Players.PlayerInstance playerInstance = Players.PlayersManager.Instance.GetPlayerInstance(clientId);
			Material mainMaterial = Teams.TeamsManager.Instance.GetTeamPalette(playerInstance.TeamId).MainMaterial;

			for (int i = 0; i < _renderers.Length; i++) {
				Renderer renderer = _renderers[i];
				renderer.material = mainMaterial;
			}
		}
	}
}