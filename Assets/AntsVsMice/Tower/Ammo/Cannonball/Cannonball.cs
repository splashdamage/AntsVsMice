using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannonball : Ammo {
	float explodeTime = 0.3f;
	float explodeElapsed = 0;
	public float range = 250;
	protected bool didDamage = false;
	bool exploding = false;
	public override Vector3 end {
		get { return _targetPosAtFiring; }
	}

	public virtual List<Enemy> FindTargets () {
		Collider[] inRange=Physics.OverlapSphere(transform.position,range);
		List<Enemy> enemies = new List<Enemy>();
		foreach(Collider c in inRange){
			Enemy e = c.GetComponent<Enemy>();
			if (e !=null && e.health > 0){
				enemies.Add (e);
			}
		}
		return enemies;
	}
	protected override void Die() {
		enabled = false;
		Destroy(gameObject);
	}
	
	protected override bool ApplyDamage() {
		if (!exploding) {
			sprite.transform.localScale = Vector3.zero;
			((tk2dAnimatedSprite)sprite).Play("boom");
			exploding = true;
			return true;
		} else if (explodeElapsed < explodeTime) {
			explodeElapsed += Time.deltaTime;
			float t = explodeTime / explodeElapsed;
			if (!didDamage && t > 0.33f) {
				foreach(Enemy e in FindTargets()) {
					e.TakeDamage(damage);
				}
				didDamage = true;
			}
			sprite.transform.localScale = Vector3.one * t;
			return true;
		}
		return false;
	}
}
