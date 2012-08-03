using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Walker : MonoBehaviour {
	public float health;
	public float damageReceived;
	public bool flying = false;	
	public Damage attackDamage;
	public float attackInterval = 1;
	public Walker target;
	
	public virtual void Stop() {}
	
	public float lifeLeft {
		get {
			if (damageReceived == 0) return health;
			if (damageReceived > health) return 0;
			return damageReceived / health;
		}
	}
	
	public float speed = 1;
	public float speedModifier = 1;
	
	public float speedCurrent {
		get {
			return speed * speedModifier;
		}
	}
	
	public List<Resistance> resistances = new List<Resistance>();
	
	public virtual bool Attack() {
		if (target == null) return false;
		target.TakeDamage(attackDamage);
		return true;
	}
	
	public float TakeDamage(Damage damage) {
		float damageTaken = 0;
		bool didResist = false;
		foreach (Resistance resist in resistances) {
			if (resist.type == damage.type) {
				damageTaken = damage.amount * resist.percent;
				didResist = true;
				break;
			}
		}
		if (!didResist) {
			damageTaken = damage.amount;
		}
		damageReceived += damageTaken;
		return damageTaken;
	}
	public virtual void StartAttack(Walker newTarget) {
		target = newTarget;
		StartCoroutine(DoAttack(target));
	}
	public IEnumerator DoAttack(Walker currentTarget) {
		while (currentTarget != null && currentTarget == target) {
			Attack();
			yield return new WaitForSeconds(attackInterval);
		}
	}
}
