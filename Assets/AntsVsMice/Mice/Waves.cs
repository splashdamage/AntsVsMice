using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Waves : MonoBehaviour {
	public float timeBetweenWaves = 5;
	public float timeFactorBetweenMice = 1;
	public List<Wave> waves = new List<Wave>();
	[System.Serializable]
	public class Wave {
		public iTweenPath path;
		public List<WaveItem> mice = new List<WaveItem>();
	}
	[System.Serializable]
	public class WaveItem {
		public GameObject mousePrefab;
		public int quantity = 1;
	}
	public IEnumerator Start() {
		foreach (Wave wave in waves) {
			yield return new WaitForSeconds(timeBetweenWaves);
			foreach (WaveItem item in wave.mice) {
				while (item.quantity > 0) {
					GameObject obj = (GameObject) Instantiate(item.mousePrefab, wave.path.nodes.First(), item.mousePrefab.transform.rotation);
					Enemy enemy = obj.GetComponent<Enemy>();
					enemy.March(wave.path);
					item.quantity--;
					yield return new WaitForSeconds(timeFactorBetweenMice);
				}
			}
		}
	}
}
