using UnityEngine;
using System.Collections;

public class ResourceGain : MonoBehaviour {
	public GameObject destination;
	// Use this for initialization
	void Start () {
		//iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath("Cloud"), "time", 5));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTween(string resource){
		if (resource == "oxygen") {
			Vector3[] positions=new Vector3[3];
			positions [0] = transform.position;
			positions[1]=transform.position+ new Vector3(0,2f,0);
			positions [2] = destination.transform.position;
			iTween.MoveTo(gameObject, iTween.Hash("path", positions, "time", 8.0f));
		}
	}
}
