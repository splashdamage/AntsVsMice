using UnityEngine;
using System.Collections;

public class Troop : Walker {
	public Tower homeBase;
	public float attackRange = 0.1f;
	
	public void Start() {
		StartCoroutine(LookForTarget());
	}
	
	public IEnumerator LookForTarget() {
		while (target == null) {
			Walker enemy = (Walker) homeBase.FindTarget();
			if (enemy == null) {
				yield return new WaitForSeconds(1);
			} else {
				enemy.Stop();
				while (Vector3.Distance(transform.position, enemy.transform.position) > attackRange) {
					MoveToward(enemy.transform);
					yield return null;
				}
				StartAttack(enemy);
			}
		}
	}
	public void MoveToward(Transform destination) {
				
	}
}
