using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Teams {
	public class TeamsManager : Utilities.SingletonPersistent<TeamsManager> {

		[Header("Components")]
		[SerializeField] private TeamPalette[] _teamPalettes;


		public TeamPalette GetTeamPalette(int teamId) {
			return _teamPalettes[teamId];
		}
	}
}