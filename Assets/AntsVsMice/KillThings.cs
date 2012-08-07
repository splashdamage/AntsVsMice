using UnityEngine;
using System.Collections;

public class KillThings : MonoBehaviour {
void OnTriggerExit(Collider other){
		Destroy(other.gameObject);
	}
}
