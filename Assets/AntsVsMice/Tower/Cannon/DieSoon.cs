using UnityEngine;
using System.Collections;

public class DieSoon : MonoBehaviour {

	// Use this for initialization
	IEnumerable Start () {
		yield return new WaitForSeconds(0.4f);
		ParticleSystem ps = GetComponent<ParticleSystem>();
		ps.Stop();
		while (ps.IsAlive()) yield return null;
		Destroy(transform.root.gameObject);
	}
}
