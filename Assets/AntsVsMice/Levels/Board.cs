using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour {
	CamAnchors anchors;
	public Vector3[] myVertices;
	// Use this for initialization
	void Start () {
		anchors = Camera.mainCamera.GetComponent<CamAnchors>();
		transform.position = anchors.lowerLeft;
		//UpdateMesh ();
	}
}
