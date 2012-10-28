using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	public float flightTime = 1;
	public float fadeTime = 0;
	public float damage = 10;
	public tk2dSprite sprite;
	float flightLeft;
	float fadeLeft;
	public Bow bow;
	Enemy target;
	float altitude = 1000;
	
	
	
	public void Fire(Enemy target) {
		flightLeft = 0;
		this.target = target;
		bow.currentArrow = null;
		enabled = true;
	}
	Vector3 Bezier2(Vector3 start, Vector3 end, Vector3 control, float t)
	{
	    return (((1-t)*(1-t)) * start) + (2 * t * (1 - t) * control) + ((t * t) * end);
	}

	public void Update() {
		if (target == null) {
			enabled = false;
			Destroy (gameObject);
		}
		flightLeft += Time.deltaTime;
		float t = flightLeft / flightTime;
		float t1 = (flightLeft + Time.deltaTime) / flightTime;
		// at the midpoint we should be at max altitude and 0.5 (way to target
		if (flightLeft <= flightTime ) {
			Vector3 dirVec = (target.transform.position - bow.arrowLoc.transform.position);
			Vector3 center = dirVec * 0.5f;
			Vector3 control = bow.arrowLoc.transform.position + center + (Vector3.forward * altitude);
			Vector3 start = bow.arrowLoc.transform.position;
			Vector3 end = target.transform.position;
			
			transform.position = Bezier2(start, end, control, t);
			
			Vector3 ahead = Bezier2(start, end, control, t1);
			transform.LookAt(ahead);
			return;
		} else if (transform.parent != target.transform) {
			transform.parent = target.transform;
			target.TakeDamage(new Damage(Damage.Type.direct, damage));
			return;
		}
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
}
