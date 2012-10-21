using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public float flightTime = 1;
	public float damage = 10;
	float left;
	public Bow bow;
	Enemy target;
	
	public void Fire(Enemy target) {
		left = 0;
		this.target = target;
		enabled = true;
	}
	

	public void Update() {
		left += Time.deltaTime;
		if (left < flightTime) {
			float t = flightTime / left;
			float t1 = flightTime / (left + Time.deltaTime);
			Vector3 front = Vector3.Slerp(bow.transform.position, target.transform.position, t1);
			transform.position = Vector3.Slerp(bow.transform.position, target.transform.position, t);
			transform.LookAt(front);
		} else {
			if (target == null) Destroy (gameObject);
			transform.parent = target.transform;
			target.TakeDamage(new Damage(Damage.Type.direct, damage));
			enabled = false;
		}
	}
}
