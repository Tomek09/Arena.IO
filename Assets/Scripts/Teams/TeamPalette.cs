using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Teams {
	[CreateAssetMenu(fileName = "Team Palette - ", menuName = "SO/Teams/Palette")]
	public class TeamPalette : ScriptableObject {

		[field: Header("Settings")]
		[field: SerializeField] public Color Color { get; private set; } = Color.white;
	}
}