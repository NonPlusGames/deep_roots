using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class SoilGridMngr : MonoBehaviour {

	public int hasPlant;
    public Text carbon;
    public Text hydrogen;
	public Text sunlight;
    public GameObject newLine;
    public GameObject element;
	public bool startDraw = false;
	public bool isConnectedToElement = false;
	public bool hasDepletedElement = false;
	public bool sentResource = false;
	public bool mouseOver = false;
	bool pc = true; 
	SpriteRenderer rend;
	Touch touch;

    void Start() {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.touchCount > 0 || pc == true)
        {

			if (Input.touchCount > 0)
				touch = Input.GetTouch (0);

			if (((touch.phase == TouchPhase.Began && Input.touchCount > 0) || Input.GetMouseButtonDown(0)) && !isConnectedToElement && hasPlant > 0 && (Vector2.Distance(Camera.main.ScreenToWorldPoint(touch.position), transform.position) <= .5f || Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) <= .5f))
            {
               
                rend.color = new Color(1.0f, .92f, .016f, 1.0f);
				if ((int.Parse("" + sunlight.text) >= 1) && hasDepletedElement == false)
                {
                    sunlight.text = "" + (int.Parse("" + sunlight.text) - 1);
                    startDraw=true;
                }
            }


            if (startDraw)
            {
                GetComponent<DrawLine>().drawLine();
            }
            if (!startDraw)
            {
                rend.color = new Color(206.0f / 255.0f, 141.0f / 255.0f, 0.0f / 255.0f, 0f);
            }
        }
    }

	public void elementGain()
	{
		sentResource = false;
		if (isConnectedToElement&&!sentResource)
		{
			if (element.GetComponent<Element>().elementType == "carbon" && (GetComponent<DrawLine>().hasPlant == 1 || GetComponent<DrawLine>().hasPlant == 3))
			{
				carbon.text = "" + (int.Parse("" + carbon.text) + 1);
				StartCoroutine( shrink (element.transform.localScale,element.transform.localScale/2, 1));
				element.GetComponent<Element> ().health--;
				if (element.GetComponent<Element> ().health <= 0) {

					hasDepletedElement = true;
					element.GetComponent<Element> ().destroySelf ();
					isConnectedToElement = false;
				}
				sentResource = true;
			}

			if (element.GetComponent<Element>().elementType == "hydrogen" && (GetComponent<DrawLine>().hasPlant == 2 || GetComponent<DrawLine>().hasPlant == 3))
			{
				hydrogen.text = "" + (int.Parse("" + hydrogen.text) + 1);
				StartCoroutine( shrink (element.transform.localScale,element.transform.localScale/2, 1));
				element.GetComponent<Element> ().health--;
				if (element.GetComponent<Element> ().health <= 0) {

					element.GetComponent<Element> ().destroySelf ();
					isConnectedToElement = false;
				}
				sentResource = true;
			}
		}
	}
	IEnumerator shrink(Vector3 oldPosition, Vector3 newPosition, float time){
		float elapsedTime = 0;
		Vector3 startPos = oldPosition;
		while (elapsedTime < time && !(element==null)) {
			element.transform.localScale = Vector3.Lerp (startPos, newPosition, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

}
