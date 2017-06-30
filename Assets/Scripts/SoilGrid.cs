using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SoilGrid : MonoBehaviour {

	public List<GameObject> gridGroup;
	public GameObject box;
	public int numOfRootPaths=25;

	void Start()
	{
		float dist=0;
		for (float i=0; i < numOfRootPaths; i++) {
			gridGroup.Add(Instantiate (box, new Vector2(-12f+i-dist,-1), Quaternion.identity) as GameObject);
			dist+=.08f;
		}

	}
	
}