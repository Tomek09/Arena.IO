using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Utilities {
	public abstract class ToolWindow : EditorWindow {

		protected const string MENU_ITEM_LABEL = "Game Tools/";

		protected GUIContent GetTitleContent(string label) {
			Texture icon = EditorGUIUtility.IconContent("HeadZoomSilhouette").image;
			return new GUIContent(label, icon);
		}
	}
}