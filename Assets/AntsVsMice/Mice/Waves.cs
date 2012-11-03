using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Waves : MonoBehaviour {
	public static bool over = false;
	public float timeBetweenWaves = 5;
	public List<Wave> waves = new List<Wave>();
	[System.Serializable]
	public class Wave {
		public MousePath path;
		public GameObject mousePrefab;
		public int quantity = 1;
		public float healthMultiplier = 1;
		public float timeBetweenMice;
		public Wave() {
			healthMultiplier = 1;
			timeBetweenMice = 1;
		}
	}
	public IEnumerator Start() {
		foreach (Wave wave in waves) {
			yield return new WaitForSeconds(timeBetweenWaves);
			while (wave.quantity > 0) {
				GameObject obj = (GameObject) Instantiate(wave.mousePrefab, wave.path.transform.position, wave.mousePrefab.transform.rotation);
				Enemy enemy = obj.GetComponent<Enemy>();
				enemy.March(wave.path.points);
				wave.quantity--;
				yield return new WaitForSeconds(wave.timeBetweenMice);
			}
		}
		over = true;
	}
}
