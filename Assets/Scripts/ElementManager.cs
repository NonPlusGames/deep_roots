using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ElementManager : MonoBehaviour {
	public List<GameObject> nitrogenList;
	public List<GameObject> waterList;
	// Use this for initialization
	void Start () {
		nitrogenList=new List<GameObject>();
		waterList=new List<GameObject>();
		foreach(GameObject nitrogen in GameObject.FindGameObjectsWithTag("nitrogen")){
			nitrogenList.Add (nitrogen);
		}
		foreach(GameObject water in GameObject.FindGameObjectsWithTag("water")){
			waterList.Add (water);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
