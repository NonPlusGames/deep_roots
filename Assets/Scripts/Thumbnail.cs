using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Thumbnail : MonoBehaviour {
    public Text sunlight;
    public Text resources2;
    public Text resources3;
    public Text resources4;
	public Vector3 originalPos;
	public Material lineMaterial;
	public GameObject elementManager;
    public bool selected = false;
	private Collider2D collide;
	bool planting=false;
	bool pc = true;
	float i=0;
	static bool another = false;
	LineRenderer lineRenderer;
	GameObject connectedObject;
	GameObject hitConnectedObject;
	Touch touch;

    void Start()
    {
        originalPos = transform.position; 
        selected = false;
        another = false;
        lineRenderer = GetComponent<LineRenderer>();
    }

	void FixedUpdate () {
        checkMove();
	}

	void highLightElements(){
		if (gameObject.tag == "shrub") {
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().waterList)
			{
				item.GetComponent<Element> ().highlighted = true;
			}
		}
		if (gameObject.tag == "grass") {
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().nitrogenList)
			{
				item.GetComponent<Element> ().highlighted = true;
			}
		}
		if (gameObject.tag == "tree") {
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().nitrogenList)
			{
				item.GetComponent<Element> ().highlighted = true;
			}
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().waterList)
			{
				item.GetComponent<Element> ().highlighted = true;
			}
		}
	}
		
	void deHighLightElements(){
		if (gameObject.tag == "shrub") {
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().waterList)
			{
				item.GetComponent<Element> ().highlighted = false;
			}
		}
		if (gameObject.tag == "grass") {
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().nitrogenList)
			{
				item.GetComponent<Element> ().highlighted = false;
			}
		}
		if (gameObject.tag == "tree") {
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().nitrogenList)
			{
				item.GetComponent<Element> ().highlighted = false;
			}
			foreach (GameObject item in elementManager.GetComponent <ElementManager> ().waterList)
			{
				item.GetComponent<Element> ().highlighted = false;
			}
		}
	}	
    void checkMove()
    {

		if ((Input.touchCount > 0 || pc == true)) {

			Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit1 = new RaycastHit();
			if (Input.touchCount > 0)
			{
				touch = Input.GetTouch (0);
				ray1 = Camera.main.ScreenPointToRay (touch.position);
			}
			if (Physics.Raycast (ray1, out rayHit1, 1000f)) {
				
				if ((Input.GetMouseButtonDown (0) || (touch.phase==TouchPhase.Began && Input.touchCount > 0)) && (rayHit1.collider.tag==gameObject.tag)&& another == false) {
					selected = true;
				}
			}
            if (selected == true)
            {
                another = true;
				highLightElements ();
				if (touch.phase == TouchPhase.Moved)
				{
					transform.position = new Vector2(Camera.main.ScreenToWorldPoint(touch.position).x, Camera.main.ScreenToWorldPoint(touch.position).y);
				}

				if (Input.GetMouseButton(0))
				{
					transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
				}

                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.position.x, -1000), 1000);
                Debug.DrawLine(transform.position, new Vector2(transform.position.x, -1000), Color.cyan);
				if (hit)
                {
					hitConnectedObject = hit.collider.gameObject;
                    if (hit.collider.GetComponent<Collider2D>().tag == "grid" && hit.collider.GetComponent<Collider2D>().GetComponent<GroundGridManager>().isConnected == 0)
					{
                        lineRenderer.SetPosition(0, gameObject.transform.position);
                        lineRenderer.SetPosition(1, hit.collider.transform.position);
						connectedObject=hit.collider.GetComponent<Collider2D> ().gameObject;

						lineRenderer.material.mainTextureScale = new Vector2((int)Vector2.Distance(gameObject.transform.position,  hit.collider.transform.position), 1);
						i+=-.05f;
						lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(i, 0));
						lineRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;

						connectedObject.GetComponent<GroundGridManager> ().setMouseOver ();
						if ((touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp (0)) && gameObject.tag == "grass" && connectedObject.GetComponent<GroundGridManager> ().hasPlant == 0) {  
							lineRenderer.SetPosition (0, Vector2.zero);
							lineRenderer.SetPosition (1, Vector2.zero);
							if (int.Parse ("" + resources2.text) >= 1) {
								Instantiate (gameObject, originalPos, Quaternion.identity);

								resources2.text = "" + (int.Parse ("" + resources2.text) - 1);
								connectedObject.GetComponent<GroundGridManager> ().createGrass ();

								another = false;
								planting=false;
								Destroy (gameObject);
								deHighLightElements ();
							}
						}
						if ((touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp (0)) && gameObject.tag == "shrub" && connectedObject.GetComponent<GroundGridManager> ().hasPlant == 0) {
							lineRenderer.SetPosition (0, Vector2.zero);
							lineRenderer.SetPosition (1, Vector2.zero);
							if (int.Parse ("" + resources2.text) >= 1 && int.Parse ("" + resources3.text) >= 1) {
								Instantiate (gameObject, originalPos, Quaternion.identity);

								resources2.text = "" + (int.Parse ("" + resources2.text) - 1);
								resources3.text = "" + (int.Parse ("" + resources3.text) - 1);
								connectedObject.GetComponent<GroundGridManager> ().createShrub ();
								another = false;
								planting=false;
								Destroy (gameObject);
								deHighLightElements ();
							}
						}
						if ((touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp (0)) && gameObject.tag == "tree" && connectedObject.GetComponent<GroundGridManager> ().hasPlant == 0) {
							lineRenderer.SetPosition (0, Vector2.zero);
							lineRenderer.SetPosition (1, Vector2.zero);
							if (int.Parse ("" + resources4.text) >= 1 && int.Parse ("" + resources3.text) >= 1 && int.Parse ("" + resources2.text) >= 1) {
								Instantiate (gameObject, originalPos, Quaternion.identity);

								resources3.text = "" + (int.Parse ("" + resources3.text) - 1);
								resources4.text = "" + (int.Parse ("" + resources4.text) - 1);
								resources2.text = "" + (int.Parse ("" + resources2.text) - 1);
								connectedObject.GetComponent<Collider2D> ().GetComponent<GroundGridManager> ().createTree ();
								another = false;
								planting=false;
								Destroy (gameObject);
								deHighLightElements ();
							}
						}
                    }
                }

                if (touch.phase == TouchPhase.Ended || Input.GetMouseButtonUp(0))
                {
                    another = false;
                    selected = false;
                    transform.position = originalPos;
                    lineRenderer.SetPosition(0, Vector2.zero);
                    lineRenderer.SetPosition(1, Vector2.zero);
                }
				if (selected == false) {
					another = false;
					transform.position = originalPos;
					lineRenderer.SetPosition(0, Vector2.zero);
					lineRenderer.SetPosition(1, Vector2.zero);
				}
            }
        }
    }
}
