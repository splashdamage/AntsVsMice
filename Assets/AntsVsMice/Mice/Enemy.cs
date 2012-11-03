using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	public int reward;
	public int health;
	
	public int cheeseEat = 1;
	public float damageReceived;
	public bool flying = false;
	public Vector3[] myPath;
	public int pathIdx = 0;
	public Transform healthLocation;
	public Transform damageLocation;
	public int healthWidth = 40;
	public virtual void Stop() {}
	public float lifeLeft {
		get {
			if (damageReceived == 0) return health;
			if (damageReceived > health) return 0;
			return health - damageReceived;
		}
	}
	float baseSpeed = 40;
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
		myPath = path;
	}
	public void Die() {
		enabled = false;
		Destroy (gameObject);
	}
	public void Update() {
		if (myPath == null) return;
		if (lifeLeft == 0) {
			Score.instance.money += reward;
			Die ();
		}
		if (Vector3.Distance(transform.position, myPath[pathIdx]) > 1) {
			Vector3 move = (myPath[pathIdx] - transform.position).normalized * baseSpeed * speedCurrent * Time.deltaTime;
			transform.Translate(move);
		} else if (pathIdx < myPath.Length -1) {
			pathIdx++;
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
	public void OnGUI() {
		GUI.skin = Info.mouseSkin;
		if (damageReceived > 0 && lifeLeft > 0) {
			float healthPct = 1 / (health / lifeLeft);
			Vector3 screenPos = Camera.mainCamera.WorldToScreenPoint(healthLocation.position);
			//Debug.Log ("width: "+healthWidth*healthPct);
			GUI.Box (new Rect(screenPos.x - (healthWidth/2), Screen.height - screenPos.y, healthWidth * healthPct, 5), "", "health");
		}
	}
}
