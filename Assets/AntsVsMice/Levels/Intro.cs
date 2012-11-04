using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	public GUISkin skin;

	public void OnGUI() {
		GUI.skin = skin;
		GUILayout.BeginArea(new Rect(Screen.width *0.66f, 0, Screen.width * 0.33f, Screen.height));
		GUILayout.BeginVertical();
		GUILayout.Label ("DEFEND.",skin.GetStyle("CenteredHeader"));
		GUILayout.Label ("YOUR.",skin.GetStyle("CenteredHeader"));
		GUILayout.Label ("CHEESE.",skin.GetStyle("CenteredHeader"));
		if(GUILayout.Button("Oh noes! Not the cheese!")) {
			Application.LoadLevel (1);
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
