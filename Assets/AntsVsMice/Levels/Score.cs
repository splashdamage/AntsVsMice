using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public static Score instance;
	int startMoney = 20;
	public int money;
	int startCheese = 20;
	public int cheese;
	void Start() {
		money = startMoney;
		cheese = startCheese;
	}
	public void EatCheese(int howMany) {
		int left = cheese - howMany;
		if (left <= 0) {
			cheese = 0;
			return;
		}
		cheese = left;
	}
	
	void OnEnable() {
		instance = this;
	}
	void OnDisable() {
		instance = null;
	}
	public void OnGUI() {
		if (cheese > 0 && !Waves.over) return;
		GUI.skin = Info.mouseSkin;
		Time.timeScale = 0;
		GUILayout.BeginArea(new Rect(Screen.width *0.25f, Screen.height * 0.15f, Screen.width * 0.5f, Screen.height * 0.7f));
		GUILayout.BeginVertical ();
		if (cheese == 0) {
			GUILayout.Label ("The Cheese Is",GUI.skin.GetStyle("CenteredHeader"));
			GUILayout.Label ("GONE.",GUI.skin.GetStyle("CenteredHeader"));
		} else if (Waves.over) {
			GUILayout.Label ("You Win",GUI.skin.GetStyle("CenteredHeader"));
		}
		if (GUILayout.Button("Play Again?")) {
			Application.LoadLevel (Application.loadedLevel);
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
