using UnityEngine;
using System.Collections;

public class Arrow : Ammo {
	public float fadeTime = 0;
	float fadeLeft;


	protected override void Fly(float t) {
		float t1 = (flightLeft + Time.deltaTime) / flightTime;
		base.Fly(t);
		transform.LookAt(GetPositionAt(t1));
	}
	protected override void Die() {
		Color currentC = sprite.color;
		if (currentC.a > 0) {
			fadeLeft += Time.deltaTime;
			currentC.a = Mathf.Max (0, 1 - (fadeLeft/fadeTime));
			sprite.color = currentC;
		} else {
			enabled = false;
			Destroy (gameObject);
		}
	}
	protected override bool ApplyDamage() {
		if (transform.parent == target.transform) return false;
		
		transform.parent = target.transform;
		target.TakeDamage(damage);
		return true;
	}
}
