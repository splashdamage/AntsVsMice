using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {
	public int quantity;
	public float timeBetweenSpawns;
	public GameObject prefab;
	
	public IEnumerator Start() {
		for (int i=0; i< quantity; i++) {
			GameObject.Instantiate(prefab);
			yield return new WaitForSeconds(timeBetweenSpawns);
		}
	}
}
