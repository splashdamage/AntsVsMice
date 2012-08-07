using UnityEngine;
using System.Collections;


public abstract class Tower : MonoBehaviour {
	
	public int size;
	public float range;
	public float cost;
	public Damage attack;
	public abstract Enemy FindTarget();
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
