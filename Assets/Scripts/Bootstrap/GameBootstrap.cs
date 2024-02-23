using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Bootstrap {
	public class GameBootstrap : MonoBehaviour {

		private IEnumerator Start() {
			//yield return new WaitUntil(() => Managers.ServicesManager.Instance != null);
			//yield return new WaitUntil(() => Managers.ServicesManager.Instance.IsSinged);
			yield return new WaitUntil(() => Scenes.ScenesManager.Instance != null);

			Scenes.ScenesManager.Instance.LoadScene("Menu", 0.2f, false);
		}
	}
}