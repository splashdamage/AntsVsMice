using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {
	public tk2dSprite sprite;
	public float damage = 10;
	public Transform launchFrom;
	protected float altitude = 1000;
	protected Enemy _target;
	protected Vector3 _targetPosAtFiring;
	public virtual Enemy target {
		get {
			return _target;
		}
		set {
			_targetPosAtFiring = value.transform.position;
			_target = value;
		}
	}
	
	
	public virtual Vector3 end {
		get {
			return target.transform.position;
		}
	}
	public virtual Vector3 start {
		get {
			return launchFrom.transform.position;
		}
	}

	
	protected virtual void Fly(float t) {
		transform.position = GetPositionAt(t);
	}
	protected virtual bool ApplyDamage() {
		return false;
	}
	protected virtual void Die() {}
	
	protected Vector3 GetPositionAt(float t) {
		Vector3 center = (end - start) * 0.5f;
		Vector3 control = start + center + (Vector3.forward * altitude);
		return Bezier2(start, end, control, t);	
	}
	
	public Vector3 Bezier2(Vector3 start, Vector3 end, Vector3 control, float t)
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
		if (t <= 1) {
			Fly(t);
		}
		if (!ApplyDamage()) {
			Die();
		}
	}
}
