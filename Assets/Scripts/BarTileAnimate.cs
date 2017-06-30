using UnityEngine;
using System.Collections;

public class BarTileAnimate : MonoBehaviour {
	float i=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		i+=.005f;
		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(i, 0));
		GetComponent<Renderer> ().material.mainTexture.wrapMode = TextureWrapMode.Repeat;
	}
}
