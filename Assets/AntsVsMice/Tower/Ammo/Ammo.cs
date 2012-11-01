using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {
	public float flightTime = 1;
	protected float flightLeft;
	public tk2dSprite sprite;
	public Damage damage;
	public Transform launchFrom;
	protected Vector3 _launchPosAtFiring;
	protected float altitude = 1000;
	protected Enemy _target;
	protected Vector3 _targetPosAtFiring;
	protected Vector3 _targetLastPos;
	
	public virtual Enemy target {
		get {
			return _target;
		}
		set {
			_targetPosAtFiring = value.damageLocation.position;
			_target = value;
		}
	}
	
	public virtual Vector3 end {
		get {
			if (target != null) {
				_targetLastPos = target.damageLocation.position;
			}
			return _targetLastPos;
		}
	}
	public virtual Vector3 start {
		get {
			return _launchPosAtFiring;
		}
	}

	
	protected virtual void Fly(float t) {
		transform.position = GetPositionAt(t);
	}
	public virtual void LaunchAt(Enemy target) {
		this.target = target;
		_launchPosAtFiring = launchFrom.transform.position;
		transform.parent = null;
		enabled = true;
	}
	
	protected virtual bool ApplyDamage() {
		if (target == null || target.lifeLeft == 0) return false;
		return (target.TakeDamage(damage) > 0) ? true : false;
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
	
	public virtual void Update() {
		flightLeft += Time.deltaTime;
		float t = flightLeft / flightTime;
		if (t <= 1) {
			Fly(t);
			return;
		}
		if (!ApplyDamage()) {
			Die();
		}
	}
}
