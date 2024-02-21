using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Utilities {
    public abstract class DatabaseSO : ScriptableObject {

		private const string DEFAULT_DATABASE_PATH_FORMAT = "Assets/Editor/Utilities/Databases/{0}.asset";

		public abstract string DatabaseName { get; }

		public string GetDatabasePath() {
			return string.Format(DEFAULT_DATABASE_PATH_FORMAT, DatabaseName);	
		}

		public bool IsValid(string path) {
			return AssetDatabase.LoadAssetAtPath(path, typeof(Object)) != null;
		}

		protected void MarkAsDirty() {
#if UNITY_EDITOR
			EditorUtility.SetDirty(this);
#endif
		}
	}
}