using UnityEngine;
using System.Collections;

public class Arrows : BulletShooter {

	public Enemy target;
	float lastFired = 0;
	
	// Update is called once per frame
	void Update () {
		if (target == null || target.lifeLeft == 0 || Vector3.Distance(transform.position, target.transform.position) > range) {
			target = FindTarget();
		} else {
			//transform.LookAt(target.transform);
			if (Time.time - lastFired >= rateOfFire) {
				Debug.Log ("arrows");
				
				target.TakeDamage(attack);
				lastFired = Time.time;
			}
		}
	}
}