using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Editor.Utilities {
	public class SceneWindow : ToolWindow {

		// Database
		private ScenesDatabase _database;

		// Toolbar
		private readonly string[] _toolDisplayNames = new string[2] { "Scenes", "Constructor" };
		private int _currentToolId;
		private Dictionary<int, System.Action> _toolDrawActions;

		// Builder
		private string _displayName;
		private string _sceneName;
		private string _scenePath;

		// GUI
		private Vector2 _scrollView;

		[MenuItem(MENU_ITEM_LABEL + "Scenes")]
		public static void ShowWindow() {
			SceneWindow window = GetWindow(typeof(SceneWindow)) as SceneWindow;
			window.titleContent = window.GetTitleContent("Scenes");
		}

		private void OnEnable() {
			_database = EditorDatabases.GetOrCreateDatabase<ScenesDatabase>();
			_toolDrawActions = new Dictionary<int, System.Action>() {
				{0, DrawScenes },
				{1, DrawBuilder }
			};

			BuilderDataClear();
		}

		private void OnGUI() {
			DrawTool();
		}


		private void DrawTool() {
			UtilityEditor.HorizontalLine();

			UtilityEditor.BoldLabel(new GUIContent("Scenes Window"), GUILayout.ExpandWidth(true));

			UtilityEditor.HorizontalLine();

			_currentToolId = GUILayout.SelectionGrid(_currentToolId, _toolDisplayNames, _toolDisplayNames.Length);
			UtilityEditor.Vertical(_toolDrawActions[_currentToolId].Invoke, ref _scrollView);
		}

		private void DrawScenes() {
			UtilityEditor.BoldLabel(new GUIContent("Current Scenes"), GUILayout.ExpandWidth(true));
			for (int i = 0; i < _database.ScenesInfo.Count; i++) {
				DrawScene(_database.ScenesInfo[i]);
			}
		}

		private void DrawBuilder() {
			UtilityEditor.BoldLabel(new GUIContent("Builder"), GUILayout.ExpandWidth(true));

			UtilityEditor.Vertical(() => {
				DrawTextField("Display Name", ref _displayName);
				DrawTextField("Scene Name", ref _sceneName);
				DrawTextField("Path", ref _scenePath);
			});

			UtilityEditor.HorizontalLine();

			UtilityEditor.Vertical(() => {
				DrawButton("Clear", BuilderDataClear, 25);
				DrawButton("Add", BuilderDataAdd, 25);
			});

		}


		private void DrawScene(ScenesDatabase.SceneInfo sceneInfo) {
			bool isCurrentScene = Equals(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, sceneInfo.SceneName);

			UtilityEditor.Horizontal(() => {
				UtilityEditor.DisabledGroup(() => {
					if (GUILayout.Button(sceneInfo.DisplayName, GUILayout.Height(25))) {
						if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
							EditorSceneManager.OpenScene(sceneInfo.Path);
						}
					}
				}, isCurrentScene);

				if (GUILayout.Button("X", GUILayout.Width(25), GUILayout.Height(25))) {
					BuilderDataRemove(sceneInfo);
				}

				if (GUILayout.Button("/\\", GUILayout.Width(25), GUILayout.Height(25))) {
					BuilderDataMove(sceneInfo, 1);
				}

				if (GUILayout.Button("\\/", GUILayout.Width(25), GUILayout.Height(25))) {
					BuilderDataMove(sceneInfo, -1);
				}
			});
		}

		private void DrawTextField(string label, ref string output) {
			GUILayout.BeginHorizontal();

			UtilityEditor.Label(new GUIContent(label), GUILayout.Width(80));
			output = GUILayout.TextField(output);

			GUILayout.EndHorizontal();
		}

		private void DrawButton(string label, System.Action action, float height) {
			if (GUILayout.Button(label, GUILayout.ExpandWidth(true), GUILayout.Height(height))) {
				action?.Invoke();
			}
		}


		private void BuilderDataClear() {
			_displayName = "Display Name";
			_sceneName = "Scene Name";
			_scenePath = "Assets/Scenes/PATH.unity";
		}

		private void BuilderDataAdd() {
			ScenesDatabase.SceneInfo sceneInfo = new ScenesDatabase.SceneInfo(_displayName, _sceneName, _scenePath);
			if (!_database.IsValid(_scenePath)) {
				EditorUtility.DisplayDialog("Error", "There is no scene with such a path", "ok");
				return;
			}

			EditorUtility.DisplayDialog("Success", "Success", "Success");
			_database.AddScene(sceneInfo);
		}

		private void BuilderDataRemove(ScenesDatabase.SceneInfo sceneInfo) {
			_database.RemoveScene(sceneInfo);
		}
	
		private void BuilderDataMove(ScenesDatabase.SceneInfo sceneInfo, int direction) {
			_database.MoveScene(sceneInfo, direction);
		}
	}
}