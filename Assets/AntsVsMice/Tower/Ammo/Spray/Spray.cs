using UnityEngine;
using System.Collections;

public class Spray : Ammo {
	protected float fadeTime = 0.25f;
	float fadeLeft;
	bool didDamage = false;
	protected override float altitude {
		get {
			return 128;
		}
	}
	protected override void Fly(float t) {
		float t1 = (flightLeft + Time.deltaTime) / flightTime;
		base.Fly(t);
		transform.LookAt(GetPositionAt(t1));
	}
	protected void Die() {
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
	public virtual void Update() {
		flightLeft += Time.deltaTime;
		float t = flightLeft / flightTime;
		if (t <= 1) {
			Fly(t);
			return;
		}
		if (target == null) {
			Destroy (gameObject);
			return;
		}
		if (!didDamage) {
			target.TakeDamage(damage);
			didDamage = true;
		} else {
			Die();
		}
	}

}
