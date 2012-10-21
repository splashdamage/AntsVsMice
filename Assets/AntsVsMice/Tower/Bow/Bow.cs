using UnityEngine;
using System.Collections;

public class Bow : BulletShooter {
	public GameObject arrowPrefab;
	public Transform arrowLoc;
	public Arrow currentArrow;
	public Enemy target;
	float lastFired = 0;
	
	void Start() {

	}
	void Reload(Arrow newArrow) {
		if (currentArrow == null) {
			if (newArrow == null) {
				newArrow = ((GameObject) GameObject.Instantiate(arrowPrefab, arrowLoc.position, arrowPrefab.transform.rotation)).GetComponent<Arrow>();
			}
			newArrow.transform.parent = arrowLoc;
			newArrow.transform.localPosition = Vector3.zero;
			newArrow.transform.localRotation = Quaternion.identity;
			currentArrow = newArrow;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null || target.lifeLeft == 0 || Vector3.Distance(transform.position, target.transform.position) > range) {
			target = FindTarget();
		} else {
			//transform.LookAt(target.transform);
			if (Time.time - lastFired >= rateOfFire) {
				Debug.Log ("arrows");
				
				target.TakeDamage(attack);
				lastFired = Time.time;
			}
		}
	}
}