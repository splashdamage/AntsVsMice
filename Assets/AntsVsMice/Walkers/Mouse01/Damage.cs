using UnityEngine;
using System.Collections;

[System.Serializable]
public class Damage {
	public enum Type {
		none=0,
		direct=1,
		explosive=2,
		acid=3,
		slow=4
	}
	public Type type;
	
	public float amount;
	public Damage(Damage d) {
		this.type = d.type;
		this.amount = d.amount;
	}
}
