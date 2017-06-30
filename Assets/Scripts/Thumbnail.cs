using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class Thumbnail : MonoBehaviour {
    public Text sunlight;
    public Text resources2;
    public Text resources3;
    public Text resources4;
    private Collider2D collide;
    public Vector3 originalPos;
    public bool selected = false;
    bool pc = true;
    Touch touch;
    LineRenderer lineRenderer;
    static bool another = false;
	GameObject connectedObject;
	public Material lineMaterial;
	bool planting=false;
	float i=0;

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

	void Update ()
	{


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
