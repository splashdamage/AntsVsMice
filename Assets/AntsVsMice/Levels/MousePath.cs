using UnityEngine;
using System.Collections.Generic;

public class MousePath : MonoBehaviour{
	public static Dictionary<string, MousePath> paths = new Dictionary<string, MousePath>();
	public static Vector3[] Get(string pathName) {
		return paths[pathName].points;
	}
	
	List<Vector3> _points;
	public Vector3[] points {
		get {
			return _points.ToArray();
		}
	}
	void Awake() {
		_points = new List<Vector3>(transform.childCount);
		foreach (Transform child in transform) {
			_points.Add(child.position);
		}
		paths[name] = this;
	}
}
