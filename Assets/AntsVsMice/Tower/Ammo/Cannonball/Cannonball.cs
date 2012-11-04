using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannonball : Ammo {
	public float range = 250;
	protected bool didDamage = false;
	protected override float altitude {
		get {
			return 1024;
		}
	}
	public override Vector3 end {
		get { return _targetPosAtFiring; }
	}
	public void Awake() {
		((tk2dAnimatedSprite)sprite).animationCompleteDelegate = DoneExploding;
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
	
	public void DoneExploding(tk2dAnimatedSprite anim, int idx) {
		Destroy(gameObject);
	}
	public void Update() {
		flightLeft += Time.deltaTime;
		float t = flightLeft / flightTime;
		if (t <= 1) {
			Fly(t);
			return;
		}
		Explode();
		enabled = false;
	}
	public void Explode() {
		((tk2dAnimatedSprite)sprite).Play("explosion");
		foreach(Enemy e in FindTargets()) {
			e.TakeDamage(damage);
		}
	}
}
