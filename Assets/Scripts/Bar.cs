using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Bar : MonoBehaviour {
    public float fill;
    public Text resource;

	void Start(){
		//GetComponent<Renderer> ().material.SetTextureScale("_MainTex",new Vector2(GetComponent<Renderer> ().material.mainTextureScale.x*2,GetComponent<Renderer> ().material.mainTextureScale.y));
	}
    void Update()
    {
        fill = int.Parse("" + resource.text) * .1f;
        Image image = GetComponent<Image>();
        image.fillAmount=Mathf.MoveTowards(image.fillAmount, fill, Time.deltaTime*.5f);


		 
    }
}
