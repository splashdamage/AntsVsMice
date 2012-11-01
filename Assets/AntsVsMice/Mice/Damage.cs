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
	public float duration;
	
	public Damage(Damage d) {
		this.type = d.type;
		this.amount = d.amount;
	}
	public Damage(Damage.Type type, float amount) {
		this.type = type;
		this.amount = amount;
	}
	public Damage(Damage.Type type, float amount, float duration) : this(type, amount) {
		this.duration = duration;
	}
}
