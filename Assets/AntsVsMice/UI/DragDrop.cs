using UnityEngine;
using System.Collections;

public class DragDrop : MonoBehaviour {
	public enum Phase { none=-1, began=0, moved=1, stationary=2, ended=3, cancelled=4 }
	public Phase currentPhase = Phase.none;
	Tower maybeClicked;
	public Tower inDrag;
	public Vector3 startScreenPos;
	public Vector3 lastScreenPos;
	public Vector3 startDragPos;
	public float towerDepth;
	public float moveSlopSquared = 25;
	public LayerMask layer;
	Score score;
	Camera cam;
	void Start() {
		cam = Camera.mainCamera;
		score = GetComponent<Score>();
	}
	public void Click(Tower t) {
		
	}
	public void Update() {
		 HandleInput();
	}
	public Tower GetTowerHere() {
		RaycastHit[] hits;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll(ray, Mathf.Infinity, layer.value);
		if (hits.Length > 0) {
			towerDepth = cam.transform.position.y - hits[0].distance;
			return hits[0].collider.transform.root.GetComponent<Tower>();
		}
		return null;
	}
	
	public void HandleInput() {
		if (OOB()) return;
		if (Input.GetMouseButtonDown(0)) {
			if (currentPhase != Phase.none) {
				currentPhase = Phase.cancelled;
			} else {
				Tower t = GetTowerHere();
				if (t == null) return;
				if (t.placed || t.currentLevel.cost > score.money) {
					if (t.placed) {
						maybeClicked = t;
					}
					return;
				}
				inDrag = t;
				startDragPos = inDrag.transform.position;
				startScreenPos = Input.mousePosition;
				currentPhase = Phase.began;
			}
		} else if (Input.GetMouseButton(0)) {
			switch (currentPhase) {
				case Phase.none:
					break;
				case Phase.began:
					if ((Input.mousePosition - startScreenPos).sqrMagnitude > moveSlopSquared ) {
						currentPhase = Phase.moved;
						inDrag = (Tower) ((GameObject)Instantiate(inDrag.gameObject, inDrag.transform.position, inDrag.transform.rotation)).GetComponent<Tower>();
						inDrag.radiusSprite.gameObject.active = true;
					}
					break;
				case Phase.moved:
					if ((Input.mousePosition - lastScreenPos).sqrMagnitude == 0) {
						currentPhase = Phase.stationary;
					}
					Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
					newPos.y = towerDepth;
				    inDrag.transform.position = newPos;
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
		if (Input.GetMouseButtonUp(0)) {
			if (inDrag != null) {
				Drop();
			}
			if (maybeClicked != null) {
				Tower.inFocus = maybeClicked;
				Tower.inFocus.radiusSprite.gameObject.active = true;
				maybeClicked = null;
			}
			if (Tower.inFocus != null && GetTowerHere() != Tower.inFocus) {
				Tower.inFocus.radiusSprite.gameObject.active = false;
				Tower.inFocus = null;
			} 
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
		if (inDrag.collisions.Count > 0) {
			currentPhase = Phase.cancelled;
			return;
		}
		inDrag.placed = true;
		inDrag.enabled = true;
		inDrag.collider.isTrigger = true;
		inDrag.radiusSprite.gameObject.active = false;
		score.money -= inDrag.currentLevel.cost;
		inDrag = null;
		currentPhase = Phase.ended;
	}
	void CancelDrag() {
		currentPhase = Phase.none;
		if (inDrag != null) {
			Destroy (inDrag.gameObject);
			inDrag = null;
		}
	}
	bool Active() {
		return Input.GetMouseButton(0);
	}
}
