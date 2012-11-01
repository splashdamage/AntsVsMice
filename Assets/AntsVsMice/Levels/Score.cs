using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public static Score instance;
	int startMoney = 10;
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
			GameOver();
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
	public void GameOver() {
		Debug.Log ("GameOver");
	}
}
