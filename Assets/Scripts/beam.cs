using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class beam : MonoBehaviour {
	public GameObject sun;
	public Text day;
	bool dayEnd=false;
	public GameObject end;
	public GameObject sky;
	public Color skyColor;
	public Color skyDay;
	SpriteRenderer rend;
	float duration = 2; // This will be your time in seconds.
	float smoothness = 0.02f; // This will determine the smoothness of the lerp. Smaller values are smoother. Really it's the time between updates.
	Color currentColor = Color.white; // This is the state of the color in the current interpolation.
	bool run=false;
	// Use this for initialization
	void Start () {
		rend=sky.GetComponent<SpriteRenderer>();
		currentColor = rend.color;
	}
	
	// Update is called once per frame
	void Update () {
		//rend.color=Color.Lerp(Color.white,Color.black, Mathf.PingPong(Time.time*1f, 1.0f));
		Vector3 sunPos=new Vector3(sun.transform.position.x, sun.transform.position.y, 0f);
		transform.position=new Vector3(sunPos.x, sunPos.y-16.3f, transform.position.z);
		if (!dayEnd&&transform.position.x>11) {
			dayCount ();

		}
		if (transform.position.x > -11 && transform.position.x < -10) {
			dayEnd = false;
			if (run == false) {
				StartCoroutine ("LerpColor");
				run = true;
			}
			//rend.color=new Color(1f,1f,1f,1f);
			//rend.color=Color.Lerp(new Color(1f,1f,1f,1f),new Color(.5f,.5f,1f,1f), Time.deltaTime*1);
		}
	}

	void dayCount()
	{
		day.text = "" + (int.Parse("" + day.text) - 1);
		dayEnd = true;
		StartCoroutine ("LerpColor");
		//rend.color=Color.Lerp(new Color(.5f,.5f,1f,1f),new Color(1f,1f,1f,1f), Time.deltaTime*1);
	}
		
	IEnumerator LerpColor()
	{
		float progress = 0; //This float will serve as the 3rd parameter of the lerp function.
		float increment = smoothness/duration; //The amount of change to apply.
		while(progress < 1)
		{
			if(dayEnd)
				rend.color = Color.Lerp(new Color(1f,1f,1f), new Color(.2f,.2f,.2f), progress);
			else
				rend.color = Color.Lerp(new Color(.2f,.2f,.2f), new Color(1f,1f,1f), progress);
			progress += increment;
			yield return new WaitForSeconds(smoothness);
		}
		run = false;
		return true;
	}

}
