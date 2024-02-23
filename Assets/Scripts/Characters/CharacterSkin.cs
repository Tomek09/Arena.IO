using UnityEngine;

namespace Assets.Scripts.Characters {
	public class CharacterSkin : MonoBehaviour {

		[Header("Components")]
		[SerializeField] private Renderer[] _renderers;
		[SerializeField] private Material _defaultCharacterMaterial;
		private CharacterBase _character;
		private Material _teamMaterial;

		private void Awake() {
			_character = GetComponent<CharacterBase>();
		}

		private void Start() {
			ulong clientId = _character.OwnerClientId;
			Players.PlayerInstance playerInstance = Players.PlayersManager.Instance.GetPlayerInstance(clientId);
			Teams.TeamPalette palette = Teams.TeamsManager.Instance.GetTeamPalette(playerInstance.TeamId);
			Color color = palette.Color;
			_teamMaterial = new Material(_defaultCharacterMaterial);
			_teamMaterial.SetColor("_Color", color);

			for (int i = 0; i < _renderers.Length; i++) {
				Renderer renderer = _renderers[i];
				renderer.material = _teamMaterial;
			}
		}
	}
}