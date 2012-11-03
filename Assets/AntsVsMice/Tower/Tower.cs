using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Tower : MonoBehaviour {
	public static Tower inFocus;
	public tk2dAnimatedSprite anim;
	public Ammo currentAmmo;
	public Transform launchFrom;
	public bool firing = true;
	public bool placed = false;
	public LayerMask targetLayer;
	public Enemy target;
	public Level[] levels;
	public Level currentLevel;
	float lastFired = 0;
	public tk2dSprite radiusSprite;
	
	[System.Serializable]
	public class Level {
		public float rateOfFire;
		public GameObject ammoPrefab;
		public int cost;
		public float range;
	}
	public int curIdx {
		get {
			return System.Array.IndexOf(levels, currentLevel);
		}
	}
	public Level next {
		get {
			if (curIdx == levels.Length -1) return null;
			return levels[curIdx+1];
		}
	}
	
	public void Awake() {
		currentLevel = levels[0];
		
		anim.animationCompleteDelegate = FireCompleteDelegate;
		anim.animationEventDelegate = Launch;
	}
	
	
	// Update is called once per frame
	public virtual Enemy FindTarget () {
		Vector3 fireCenter = transform.position;
		Collider[] inRange=Physics.OverlapSphere(fireCenter, currentLevel.range, targetLayer.value);
		System.Array.Sort (inRange, (x, y) => (x.transform.position - transform.position).sqrMagnitude.CompareTo((y.transform.position - transform.position).sqrMagnitude) );
		//Debug.Log ("Colliders: "+inRange.Length);
		foreach(Collider c in inRange){
			Enemy e = c.transform.root.GetComponent<Enemy>();
			if (e !=null && e.lifeLeft > 0){
				return e;
			}
		}
		return null;
	}
	public virtual void Reload(Ammo newAmmo) {
		if (currentAmmo == null) {
			if (newAmmo == null) {
				newAmmo = ((GameObject) GameObject.Instantiate(currentLevel.ammoPrefab, launchFrom.position, currentLevel.ammoPrefab.transform.rotation)).GetComponent<Ammo>();
			}
			newAmmo.flightTime = currentLevel.rateOfFire;
			newAmmo.launchFrom = launchFrom;
			newAmmo.transform.parent = launchFrom;
			newAmmo.transform.localPosition = Vector3.zero;
			newAmmo.transform.localRotation = Quaternion.identity;
			currentAmmo = newAmmo;
		}
	}
	
	public void Launch(tk2dAnimatedSprite sprite, tk2dSpriteAnimationClip clip, tk2dSpriteAnimationFrame frame, int frameNum) {
		currentAmmo.LaunchAt(target);
		currentAmmo = null;
	}
	
	public virtual void Fire() {
		if (firing) return;
		Reload(null);
		lastFired = Time.time;
		anim.Play("fire");
		firing = true;
	}
	
	public void FireCompleteDelegate(tk2dAnimatedSprite sprite, int clipId) {
		firing = false;
		Reload(null);
	}
	public void Update() {
		if (placed) {
			if (target == null || target.lifeLeft == 0) {
				target = FindTarget();
			} else if (Vector3.Distance(transform.position, target.transform.position) > currentLevel.range) {
				target = null;
			} else if (lastFired + currentLevel.rateOfFire <= Time.time) {
				Fire();
			}
		} else {
			Dragging();
		}
	}
	public virtual void Dragging() {
		
	}
	public virtual void OnGUI() {
		Vector3 center = Camera.main.WorldToScreenPoint(transform.position);
		GUI.skin = Info.mouseSkin;
		if (curIdx > 0) {
			Debug.Log ("here "+ curIdx);
			string plus = (curIdx == 1) ? "+" : "++";
			GUI.Label(new Rect(center.x, Screen.height - center.y, 20, 40), plus);
		}
		if (inFocus != this) return;
		if (GUI.Button(new Rect(center.x - 90, Screen.height - center.y - 20, 80, 30), "Sell: $"+(currentLevel.cost * 0.8f))) {
			Score.instance.money += (int) (currentLevel.cost * 0.8f);
			Destroy(gameObject);
		}
		if (next != null && next.cost < Score.instance.money) {
			if (GUI.Button(new Rect(center.x + 10, Screen.height - center.y - 20, 80, 30), "Up: $"+next.cost)) {
				currentLevel = next;
				Score.instance.money -= currentLevel.cost; 
			}
		}
	}
	public virtual void OnDrawGizmos() {
		if (placed) {
			Gizmos.DrawWireSphere(transform.position, currentLevel.range);
		}
	}
}
