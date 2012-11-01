using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {
	public enum Phase { none=-1, began=0, moved=1, stationary=2, ended=3, cancelled=4 }
	public Phase currentPhase = Phase.none;
	public Tower inDrag;
	public Vector3 startScreenPos;
	public Vector3 lastScreenPos;
	public Vector3 startDragPos;
	public float towerDepth;
	public float moveSlopSquared = 25;
	public LayerMask layer;
	Camera cam;
	void Start() {
		cam = Camera.mainCamera;
	}
	public void Click() {}
	public void Update() {
		 HandleInput();
	}
	public void HandleInput() {
		if (OOB()) return;
		if (Input.GetMouseButtonDown(0)) {
			if (currentPhase != Phase.none) {
				currentPhase = Phase.cancelled;
			} else {
				RaycastHit[] hits;
				Ray ray = cam.ScreenPointToRay(Input.mousePosition);
				hits = Physics.RaycastAll(ray, Mathf.Infinity, layer.value);
				if (hits.Length > 0) {
					inDrag = hits[0].collider.transform.root.GetComponent<Tower>();
					// can't drag placed towers...
					if (inDrag.placed) {
						inDrag = null;
						return;
					}
					towerDepth = cam.transform.position.y - hits[0].distance;
					startDragPos = inDrag.transform.position;
					startScreenPos = Input.mousePosition;
					currentPhase = Phase.began;
				}
			}
		} else if (Input.GetMouseButton(0)) {
			switch (currentPhase) {
				case Phase.none:
				    // there was no hit.
					return;
				case Phase.began:
					if ((Input.mousePosition - startScreenPos).sqrMagnitude > moveSlopSquared ) {
						currentPhase = Phase.moved;
					}
					break;
				case Phase.moved:
					if ((Input.mousePosition - lastScreenPos).sqrMagnitude == 0) {
						currentPhase = Phase.stationary;
					}
					Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
					newPos.y = towerDepth;
				    inDrag.transform.position = newPos;
					Debug.Log (newPos);
					break;
				case Phase.stationary:
					if ((Input.mousePosition - lastScreenPos).sqrMagnitude > 0) {
						currentPhase = Phase.moved;
					} else {
						// check for valid position...
					}
					break;
			}
		}
		if (inDrag != null && Input.GetMouseButtonUp(0)) {
			Drop();
		}
		lastScreenPos = Input.mousePosition;
		if (currentPhase == Phase.cancelled) {
			CancelDrag();
		} else if (currentPhase == Phase.ended) {
			currentPhase = Phase.none;
		}
	}
	bool OOB() {
		if(Input.mousePosition.x < 0 || Input.mousePosition.y < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height) {
			CancelDrag();
			return true;
		}
		return false;
	}
	void StartDrag() {}
	void Drop() {
		// check for valid position...
		inDrag.placed = true;
		inDrag.enabled = true;
		inDrag = null;
		currentPhase = Phase.ended;
	}
	void CancelDrag() {
		currentPhase = Phase.none;
		if (inDrag != null) {
			inDrag.transform.position = startDragPos;
			inDrag.placed = false;
			inDrag.enabled = false;
		}
	}
	bool Active() {
		return Input.GetMouseButton(0);
	}
}
