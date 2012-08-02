using UnityEngine;
using System.Collections;

public class Shooter : Tower {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] inRange=Physics.OverlapSphere(transform.position,range);
	}
}
