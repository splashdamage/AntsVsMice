using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	public float reward;
	public float health;
	public float damageReceived;
	public bool flying = false;
	public iTweenPath myPath;
	int pathIdx = 1;
	float lastMove = 0;
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
	public void March(iTweenPath myPath) {
		this.myPath = myPath;
	}
	public void Update() {
		if (myPath == null) return;
		if (health == 0) {
			Destroy (gameObject);
			return;
		}
		if (Vector3.Distance(transform.position, myPath.nodes[pathIdx]) > lastMove) {
			Vector3 move = (myPath.nodes[pathIdx] - transform.position).normalized * speedCurrent * Time.deltaTime;
			transform.Translate(move);
			lastMove = move.magnitude;
		} else if (pathIdx < myPath.nodes.Count -1) {
			pathIdx++;
		} else {
			MarchOver ();
			Destroy (gameObject);
		}
	}
	public void MarchOver() {
		Debug.Log ("Eat cheese");
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
}
