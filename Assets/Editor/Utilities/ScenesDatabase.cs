using System.Collections.Generic;

namespace Assets.Editor.Utilities {
	public class ScenesDatabase : DatabaseSO {

		[System.Serializable]
		public class SceneInfo {
			public string DisplayName;
			public string SceneName;
			public string Path;

			public SceneInfo(string displayName, string sceneName, string path) {
				DisplayName = displayName;
				SceneName = sceneName;
				Path = path;
			}
		}

		public List<SceneInfo> ScenesInfo = new List<SceneInfo>();

		public override string DatabaseName => "Scenes";

		public void AddScene(SceneInfo scene) {
			ScenesInfo.Add(scene);
			MarkAsDirty();
		}

		public void RemoveScene(SceneInfo scene) {
			ScenesInfo.Remove(scene);
			MarkAsDirty();
		}

		public void MoveScene(SceneInfo scene, int direction) {
			int currentIndex = ScenesInfo.IndexOf(scene);
			int newIndex = currentIndex - direction;
			bool isValid = direction == 1
				? currentIndex > 0 && currentIndex < ScenesInfo.Count
				: direction == -1 && currentIndex >= 0 && currentIndex < ScenesInfo.Count - 1;


			if (!isValid) {
				return;
			}

			(ScenesInfo[newIndex], ScenesInfo[currentIndex]) = (ScenesInfo[currentIndex], ScenesInfo[newIndex]);
		}
	}
}