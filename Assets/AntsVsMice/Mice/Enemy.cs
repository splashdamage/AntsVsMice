using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	public int reward;
	public int health;
	public int cheeseEat = 1;
	public float damageReceived;
	public bool flying = false;
	public Stack<Vector3> myPath;
	Vector3 next;
	public Transform damageLocation;
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
	protected float slowdownTimeLeft = 0;
	
	public float speedCurrent {
		get {
			return speed * speedModifier;
		}
	}
	
	public List<Resistance> resistances = new List<Resistance>();
	public void March(Vector3[] path) {
		myPath = new Stack<Vector3>(path);
	}
	public void Die() {
		enabled = false;
		Destroy (gameObject);
	}
	public void Update() {
		if (myPath == null) return;
		if (lifeLeft == 0) {
			Score.instance.money += reward;
			
		}
		if (Vector3.Distance(transform.position, next) > lastMove) {
			Vector3 move = (next - transform.position).normalized * speedCurrent * Time.deltaTime;
			transform.Translate(move);
			lastMove = move.magnitude;
		} else if (myPath.Count > 0) {
			next = myPath.Pop();
		} else {
			MarchOver();
		}
	}
	public void MarchOver() {
		Score.instance.EatCheese(cheeseEat);
		Die();
	}
	public float TakeDamage(Damage damage) {
		if (damage.type == Damage.Type.slow) {
			slowdownTimeLeft = damage.duration;
			if (damage.amount < speedModifier) {
				speedModifier = damage.amount;
				return speedModifier;
			}
			return 0;
		}
		float damageTaken = damage.amount;
		foreach (Resistance resist in resistances) {
			if (resist.type == damage.type) {
				damageTaken *= resist.percent;
				break;
			}
		}
		damageReceived += damageTaken;
		return damageTaken;
	}
	public void Start() {
		if (damageLocation == null) damageLocation = transform;
	}
}
