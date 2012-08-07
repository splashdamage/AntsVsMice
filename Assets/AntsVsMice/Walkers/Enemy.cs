using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Walker {
	public float reward;
	public void LateUpdate() {
		if (lifeLeft == 0) Destroy(gameObject);
	}
}
