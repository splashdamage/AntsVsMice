using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Info : MonoBehaviour {
	public static GUISkin mouseSkin;
	public GUISkin skin;
	Camera cam;
	Score score;
	public List<TowerOnScreen> towers = new List<TowerOnScreen>();
	[System.Serializable]
	public class TowerOnScreen {
		public Tower tower;
		public Rect priceRect;
		public TowerOnScreen(Tower t) {
			tower = t;
		}
	}
	void Awake() {
		score = GetComponent<Score>();
		mouseSkin = skin;
	}
	void Start() {
		cam = Camera.mainCamera;
		foreach (Tower t in FindObjectsOfType(typeof(Tower)) as Tower[]) {
			TowerOnScreen nT = new TowerOnScreen(t);
			Vector3 tPos = cam.WorldToScreenPoint(t.transform.position);
			nT.priceRect = new Rect(tPos.x - 25, Screen.height - tPos.y, 50, 20);
			towers.Add(nT);
		}
	}
	void OnGUI() {
		GUI.skin = skin;
		GUI.Label(new Rect(10,0,100,60),"$"+score.money, "Money" );
		GUI.Label(new Rect(Screen.width - 160,0,150,60),"Cheese: "+score.cheese, "Cheese");
		
		foreach (TowerOnScreen t in towers) {
			GUI.Label(t.priceRect, "$"+t.tower.currentLevel.cost, "Price");
		}
	}
}
