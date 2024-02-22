using UnityEngine;

namespace Assets.Scripts.Utilities {
	public static class GameObjects {

		public static T GOInstantiate<T>(T prefab, string name, Vector3 position = default, Vector3 eulerAngles = default, Transform parent = null, bool localizeTransform = false) where T : Object {
			T gameObject = Object.Instantiate(
				prefab,
				parent && localizeTransform ? parent.position + parent.rotation * position : position,
				parent && localizeTransform ? parent.rotation * Quaternion.Euler(eulerAngles) : Quaternion.Euler(eulerAngles),
				parent
			);

			gameObject.name = name;
			return gameObject;
		}

		public static T GOInstantiate<T>(T prefab, string name, Transform parent = null, bool localizeTransform = false) where T : Object {
			return GOInstantiate(prefab, name, default, default, parent, localizeTransform);
		}

		public static T GOInstantiate<T>(T prefab, Vector3 position = default, Vector3 eulerAngles = default, Transform parent = null, bool localizeTransform = false) where T : Object {
			return GOInstantiate(prefab, prefab.name, position, eulerAngles, parent, localizeTransform);
		}

		public static T GOInstantiate<T>(T prefab, Transform parent = null, bool localizeTransform = false) where T : Object {
			return GOInstantiate(prefab, prefab.name, default, default, parent, localizeTransform);
		}

		public static T GOInstantiate<T>(T prefab) where T : Component {
			return GOInstantiate(prefab, prefab.name, default, default);
		}
	}
}