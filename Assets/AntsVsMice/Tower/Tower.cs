using UnityEngine;
using System.Collections;


public abstract class Tower : MonoBehaviour {
	
	public tk2dAnimatedSprite anim;
	public Ammo currentAmmo;
	public Transform launchFrom;
	public bool firing = true;
	public bool placed = false;
	public Enemy target;
	public Level[] levels;
	public Level currentLevel;
	
	float lastFired = 0;
	
	[System.Serializable]
	public class Level {
		public float rateOfFire;
		public GameObject ammoPrefab;
		public int cost;
		public float range;
	}
	
	public void Awake() {
		currentLevel = levels[0];
	}
	
	public int Upgrade() {
		if (currentLevel == levels[2]) return 0;
		currentLevel = levels[System.Array.IndexOf(levels,currentLevel) + 1];
		return currentLevel.cost;
	}
	
	
	// Update is called once per frame
	public virtual Enemy FindTarget () {
		Collider[] inRange=Physics.OverlapSphere(transform.position,currentLevel.range);
		foreach(Collider c in inRange){
			Enemy e = c.GetComponent<Enemy>();
			if (e !=null && e.health > 0){
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
			newAmmo.launchFrom = launchFrom;
			newAmmo.transform.parent = launchFrom;
			newAmmo.transform.localPosition = Vector3.zero;
			newAmmo.transform.localRotation = Quaternion.identity;
			currentAmmo = newAmmo;
		}
	}
	
	public virtual void Fire() {
		if (firing) return;
		Reload(null);
		anim.Play("fire");
		anim.animationCompleteDelegate = FireCompleteDelegate;
		firing = true;
		currentAmmo.LaunchAt(target);
	}
	
	public void FireCompleteDelegate(tk2dAnimatedSprite sprite, int clipId) {
		firing = false;
	}
	public void Update() {
		if (placed) {
			if (target == null) {
				target = FindTarget();
			} else if (lastFired + currentLevel.rateOfFire <= Time.time) {
				Fire();
			}
		} else {
			Dragging();
		}
	}
	public virtual void Dragging() {
		
	}
}
