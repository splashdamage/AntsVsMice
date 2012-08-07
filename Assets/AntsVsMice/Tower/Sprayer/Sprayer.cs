using UnityEngine;
using System.Collections;

public class Sprayer: BulletShooter {
	public bool gasBullet;
	public float gasRange;
	public GameObject gasEffect;
	public Enemy target;
	float lastFired = 0;
	public void Explode (Vector3 position) {
		Collider[] inRange=Physics.OverlapSphere(position,gasRange);
		Damage fullDamage = new Damage(attack);
		fullDamage.amount /= 1;
		foreach(Collider c in inRange){
			Enemy e = c.GetComponent<Enemy>();
			if (e !=null && e.health > 0){
				e.TakeDamage(fullDamage);
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (target == null || target.lifeLeft == 0 || Vector3.Distance(transform.position, target.transform.position) > range) {
			target = FindTarget();
		} else {
			//transform.LookAt(target.transform);
			if (Time.time - lastFired >= rateOfFire) {
				Debug.Log ("bang");
				Damage fullDamage = new Damage(attack);
				fullDamage.amount /= 1;
				target.TakeDamage(fullDamage);
				Explode(target.transform.position);
				lastFired = Time.time;
			}
		}
	}
}
