using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DrawLine : MonoBehaviour
{
    private bool isMousePressed;
    public bool isConnectedToElement = false;
	public bool deletingLine=false;
    public GameObject element;
    private GameObject newRoot;
    public GameObject rootPrefab;
    public List<GameObject> rootList = new List<GameObject>();
	public Text sunlight;
    bool pc = true;
    Touch touch;
	LineRenderer lineRenderer;
    public int hasPlant;
	public string platform = "pc";




    void Start()
    {
        rootList.Clear();
		#if UNITY_ANDROID
		platform="android";
		#endif
		#if UNITY_EDITOR
		platform="pc";
		#endif
		Debug.Log (platform);
    }

    void Update()
    {
		
		if (!(newRoot == null)&&rootList!=null) {
			foreach (GameObject rootObject in rootList)
			{
				int nextRoot = rootList.IndexOf (rootObject)+1;
				rootObject.GetComponent<Root>().lineRenderer.SetPosition(0, rootObject.transform.position);
				if((rootList.Count-1!=rootList.IndexOf(rootObject)))
					rootObject.GetComponent<Root>().lineRenderer.SetPosition(1, rootList[nextRoot].transform.position);
				else {
					rootObject.GetComponent<Root>().lineRenderer.SetPosition(1, rootObject.transform.position);
				}
				rootObject.GetComponent<Renderer>().material.color=new Color(1, 1, 1, 1.0f);
				rootObject.GetComponent<Renderer>().material.color=new Color(1, 1, 1, 1.0f);
				rootObject.GetComponent<Renderer> ().enabled = false;
			} 

		}

    }

    public void deleteLine()
    {
		
		deletingLine=true;
		isConnectedToElement = false;

		List<GameObject> itemsToBeRemoved = new List<GameObject>();

		foreach (GameObject item in rootList)
		{
			itemsToBeRemoved.Add (item);
		}

		rootList.RemoveRange (0,itemsToBeRemoved.Count);
		foreach (GameObject item in itemsToBeRemoved)
		{
			Destroy (item);
		}
		sunlight.text = "" + (int.Parse("" + sunlight.text) + 1);
		deletingLine=false;
    }

    public void drawLine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit = new RaycastHit();
        if (Input.touchCount > 0 || pc == true)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                ray = Camera.main.ScreenPointToRay(touch.position);
            }
            if (Physics.Raycast(ray, out rayHit, 1000f))
            {
				
				if(Input.GetMouseButtonDown(0)||(touch.phase== TouchPhase.Began&&platform=="android")){
					Debug.Log (touch.phase);
					deleteLine ();
				}
				if ((Input.GetMouseButton(0)||touch.phase== TouchPhase.Moved|| Input.GetMouseButtonUp(0) || touch.phase == TouchPhase.Ended) && (rayHit.collider.tag == "nitrogen"||rayHit.collider.tag == "water" || rayHit.collider.tag == "carbon")&&(deletingLine==false))
                {
					
					if ((hasPlant == 1 || hasPlant == 3) &&rayHit.collider.GetComponent<Element> ().elementType == "carbon" ) {
						isConnectedToElement = true;
						GetComponent<SoilGridMngr> ().isConnectedToElement = isConnectedToElement;
						element = rayHit.collider.gameObject;
						GetComponent<SoilGridMngr> ().element = element;
						element.GetComponent<Element> ().highlighted=true;
						GetComponent<SoilGridMngr> ().startDraw = false;
					} else if ((hasPlant == 2 || hasPlant == 3) && rayHit.collider.GetComponent<Element> ().elementType == "hydrogen") {
						isConnectedToElement = true;
						GetComponent<SoilGridMngr> ().isConnectedToElement = isConnectedToElement;
						element = rayHit.collider.gameObject;
						GetComponent<SoilGridMngr> ().element = element;
						element.GetComponent<Element> ().highlighted=true;
						GetComponent<SoilGridMngr> ().startDraw = false;
					} else {
						deleteLine ();
						Debug.Log ("deleted by element");
                        sunlight.text = "" + (int.Parse("" + sunlight.text) - 1);
                        GetComponent<SoilGridMngr>().startDraw = false;
					}
                }

				if ((Input.GetMouseButton(0) || touch.phase == TouchPhase.Moved) && !(rayHit.collider.tag == "root")&&(deletingLine==false))
                {
                    newRoot = (GameObject)Instantiate(rootPrefab, rayHit.point, Quaternion.identity);
					newRoot.GetComponent<Renderer> ().enabled = false;
                    rootList.Add(newRoot);
                }
				else if ((!(rayHit.collider.gameObject == newRoot) && !isConnectedToElement))
                {
					Debug.Log ("deleted by self");
                    deleteLine();
                    GetComponent<SoilGridMngr>().startDraw = false;
                }
            }
        }
    }
}
