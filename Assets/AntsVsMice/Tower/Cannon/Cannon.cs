using UnityEngine;
using System.Collections;

public class Cannon : BulletShooter {
	public bool explosiveBullet;
	public float explosiveRange;
	public GameObject explosionEffect;
	public Enemy target;
	float lastFired = 0;
	public void Explode (Vector3 position) {
		Collider[] inRange=Physics.OverlapSphere(position,explosiveRange);
		Damage halfDamage = new Damage(attack);
		halfDamage.amount /= 2;
		foreach(Collider c in inRange){
			Enemy e = c.GetComponent<Enemy>();
			if (e !=null && e.health > 0){
				e.TakeDamage(halfDamage);
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
				Damage halfDamage = new Damage(attack);
				halfDamage.amount /= 2;
				target.TakeDamage(halfDamage);
				Explode(target.transform.position);
				lastFired = Time.time;
			}
		}
	}
}
