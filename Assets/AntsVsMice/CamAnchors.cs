using UnityEngine;
using System.Collections;

public class CamAnchors : MonoBehaviour {
	public Vector3 lowerLeft;
	public Vector3 lowerRight;
	public Vector3 upperLeft;
	public Vector3 upperRight;
	public float offset = -1;
	// Use this for initialization
	void Awake () {
		float depth = camera.farClipPlane + offset;
		lowerLeft = camera.ScreenToWorldPoint(new Vector3 (0,0,depth));
		lowerRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0, depth));
		upperLeft = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, depth));
		upperRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, depth));
	}
}
