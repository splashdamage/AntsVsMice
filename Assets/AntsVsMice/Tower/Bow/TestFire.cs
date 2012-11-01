using UnityEngine;
using System.Collections;

public class TestFire : MonoBehaviour {
	Bow bow;
	public Enemy target;
	// Use this for initialization
	void Start () {
		bow = (Bow) GetComponent<Bow>();
		bow.target = target;
		InvokeRepeating("Fire", 1, 1);
	}
	public void Fire() {
		bow.Fire();
	}

}
