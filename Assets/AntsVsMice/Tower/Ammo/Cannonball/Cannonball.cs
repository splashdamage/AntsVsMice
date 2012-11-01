using UnityEngine;
using System.Collections;

public class Cannonball : Ammo {
	protected bool didDamage = false;
	public override Vector3 end {
		get { return _targetPosAtFiring; }
	}
	// Update is called once per frame
	protected override bool ApplyDamage() {
		if (didDamage) return false;
		Damage halfDamage = new Damage(attack);
		halfDamage.amount /= 2;
		// Spherecast and apply halfdamage...
		didDamage = true;
		return didDamage;
	}
}
