using UnityEngine;
using System.Collections;

public class Bow : BulletShooter {
	public GameObject arrowPrefab;
	public Transform arrowLoc;
	public Arrow currentArrow;
	public Enemy target;
	float lastFired = 0;
	public bool shooting = true;
	public void Reload(Arrow newArrow) {
		if (currentArrow == null) {
			if (newArrow == null) {
				newArrow = ((GameObject) GameObject.Instantiate(arrowPrefab, arrowLoc.position, arrowPrefab.transform.rotation)).GetComponent<Arrow>();
			}
			newArrow.bow = this;
			newArrow.transform.parent = arrowLoc;
			newArrow.transform.localPosition = Vector3.zero;
			newArrow.transform.localRotation = Quaternion.identity;
			currentArrow = newArrow;
		}
	}
	public void ShotCompleteDelegate(tk2dAnimatedSprite sprite, int clipId) {
		shooting = false;
	}
	public void Fire() {
		Reload (null);
		anim.Play("shoot");
		anim.animationCompleteDelegate = ShotCompleteDelegate;
		shooting = true;
		currentArrow.Fire(target);
	}
//	// Update is called once per frame
//	void Update () {
//		if (target == null || target.lifeLeft == 0 || Vector3.Distance(transform.position, target.transform.position) > range) {
//			target = FindTarget();
//		} else {
//			//transform.LookAt(target.transform);
//			if (Time.time - lastFired >= rateOfFire) {
//				Debug.Log ("arrows");
//				
//				target.TakeDamage(attack);
//				lastFired = Time.time;
//			}
//		}
//	}
}