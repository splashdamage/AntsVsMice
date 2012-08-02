using UnityEngine;
using System.Collections;

public class TroopTower : Tower {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override Enemy FindTarget() {
		Collider [] inRange = Physics.OverlapSphere (transform.position, range); 
		foreach(Collider c in inRange) {
			Enemy e = c.GetComponent<Enemy>();
			if (e !=null && e.health > 0){
				return e;
			}
		}
		return null;
	}
}
