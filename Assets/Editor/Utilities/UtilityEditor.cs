using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Utilities {
	public static class UtilityEditor {

		public static void Horizontal(System.Action action, params GUILayoutOption[] options) {
			GUILayout.BeginHorizontal(options);
			action?.Invoke();
			GUILayout.EndHorizontal();
		}

		public static void Horizontal(System.Action action, bool isDisabled, params GUILayoutOption[] options) {
			DisabledGroup(() => Horizontal(action), isDisabled);
		}

		public static void Horizontal(System.Action action, ref Vector2 scroll, params GUILayoutOption[] options) {
			scroll = GUILayout.BeginScrollView(scroll);
			Horizontal(action, options);
			GUILayout.EndScrollView();
		}

		public static void Horizontal(System.Action action, bool isDisabled, ref Vector2 scroll, params GUILayoutOption[] options) {
			scroll = GUILayout.BeginScrollView(scroll);
			Horizontal(action, isDisabled, options);
			GUILayout.EndScrollView();
		}


		public static void Vertical(System.Action action, params GUILayoutOption[] options) {
			GUILayout.BeginVertical(options);
			action?.Invoke();
			GUILayout.EndVertical();
		}

		public static void Vertical(System.Action action, bool isDisabled, params GUILayoutOption[] options) {
			DisabledGroup(() => Vertical(action), isDisabled);
		}

		public static void Vertical(System.Action action, ref Vector2 scroll, params GUILayoutOption[] options) {
			scroll = GUILayout.BeginScrollView(scroll);
			Vertical(action, options);
			GUILayout.EndScrollView();
		}

		public static void Vertical(System.Action action, bool isDisabled, ref Vector2 scroll, params GUILayoutOption[] options) {
			scroll = GUILayout.BeginScrollView(scroll);
			Vertical(action, isDisabled, options);
			GUILayout.EndScrollView();
		}


		public static void DisabledGroup(System.Action action, bool isDisabled) {
			EditorGUI.BeginDisabledGroup(isDisabled);
			action?.Invoke();
			EditorGUI.EndDisabledGroup();
		}


		public static void BoldLabel(GUIContent content, params GUILayoutOption[] options) {
			GUIStyle style = new GUIStyle(EditorStyles.boldLabel) {
				alignment = TextAnchor.MiddleCenter
			};

			GUILayout.Label(content, style, options);
		}

		public static void Label(GUIContent content, params GUILayoutOption[] options) {
			GUIStyle style = new GUIStyle(EditorStyles.label) {
				alignment = TextAnchor.MiddleRight
			};

			GUILayout.Label(content, style, options);
		}


		public static void HorizontalLine() {
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
		}
	}
}