using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Utilities {
	public static class EditorDatabases {

		public static bool TryGetDatabase<T>(out T database) where T : DatabaseSO {
			string path = ScriptableObject.CreateInstance<T>().GetDatabasePath();
			database = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
			return database != null;
		}

		public static T CreateDatabase<T>() where T : DatabaseSO {
			T database = ScriptableObject.CreateInstance<T>();
			EditorUtility.SetDirty(database);
			AssetDatabase.CreateAsset(database, database.GetDatabasePath());
			return database;
		}

		public static T GetOrCreateDatabase<T>() where T : DatabaseSO {
			return TryGetDatabase(out T database) ? database : CreateDatabase<T>();
		}

	}
}