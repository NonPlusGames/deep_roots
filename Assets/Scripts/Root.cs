using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour {
	public LineRenderer lineRenderer;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    void OnTriggerEnter(Collider activator)
    {
        //Debug.Log("hey");
    }
}
