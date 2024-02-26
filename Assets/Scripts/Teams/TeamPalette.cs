using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Teams {
	[System.Serializable]
	public class TeamPalette {

		[HideInInspector] public string name;
		[field: Header("Settings")]
		[field: SerializeField] public string TeamName { get; private set; } = string.Empty;
		[field: SerializeField] public Color Color { get; private set; } = Color.white;
		[field: SerializeField] public Material MainMaterial { get; private set; } = null;


		public void OnValidate() {
			name = TeamName;
		}
	}
}