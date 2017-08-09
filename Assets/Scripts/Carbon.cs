using UnityEngine;
using System.Collections;

public class Carbon : MonoBehaviour {
	Rigidbody rbody;
	public float fallDragOverTime=.5f;
	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rbody.drag < 100) {
			rbody.drag += fallDragOverTime;
		}
	
	}
}
