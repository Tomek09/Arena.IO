using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities {
	public static class GUIDrawer {

		private static readonly float _offsetX = 5f;
		private static readonly float _offsetY = 5f;

		private static readonly float _buttonWidth = 120f;
		private static readonly float _buttonHeight = 20f;

		public static void DrawButton(int x, int y, string text, System.Action action) {
			if (GUI.Button(GetRect(x, y), text)) {
				action?.Invoke();
			}
		}

		public static void DrawLabel(int x, int y, string text) {
			DrawLabel(x, y, text, TextAnchor.MiddleLeft);
		}

		public static void DrawLabel(int x, int y, string text, TextAnchor textAnchor) {
			GUIStyle labelStyle = new GUIStyle("label") {
				alignment = textAnchor
			};

			GUI.Label(GetRect(x, y), text, labelStyle);
		}

		public static void DrawTextField(int x, int y, ref string text) {
			text = GUI.TextField(GetRect(x, y), text);
		}

		private static Rect GetRect(int x, int y) {
			return new Rect(
				_offsetX + (x * _buttonWidth) + (x * _offsetX),
				_offsetY + (y * _buttonHeight) + (y * _offsetY),
				_buttonWidth,
				_buttonHeight
			);
		}
	}
}