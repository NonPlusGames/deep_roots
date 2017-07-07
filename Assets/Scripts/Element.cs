using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Element : MonoBehaviour {
	public string elementType;
	public GameObject elementManager;
	public GameObject highlight;
	public GameObject nitrogenSprite;
	public GameObject waterSprite;
	private GameObject spawnedElement;
	private GameObject spawnedHighlight;
    public bool highlighted = false;
    public bool isConnectedToRoot = false;
	public int numOfPlants = 0;
	public int health = 3;
	public Text score;
	bool highlightDestroyed=false;
	bool added=false;
	// Update is called once per frame
	void Start() {
		if (elementType == "carbon") {
			spawnedElement = (GameObject)Instantiate (nitrogenSprite, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			spawnedElement.transform.parent = gameObject.transform;
			elementManager.GetComponent <ElementManager> ().nitrogenList.Add (gameObject);
		}
		if (elementType == "hydrogen") {
			spawnedElement = (GameObject)Instantiate (waterSprite, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			spawnedElement.transform.parent = gameObject.transform;
			elementManager.GetComponent <ElementManager> ().waterList.Add (gameObject);
		}
		spawnedHighlight = (GameObject)Instantiate (highlight, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
		spawnedHighlight.transform.parent = gameObject.transform;
	}

	void Update(){
		
		if (highlighted == true) {
			spawnHighlight ();
		}
		if (highlighted == false) {
			spawnedHighlight.SetActive (false);
		}
	}

	public void spawnHighlight() {
		//spawnedHighlight = (GameObject)Instantiate (highlight, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
		spawnedHighlight.SetActive (true);
	}
	public void destroyHighlight(){
		spawnedHighlight.SetActive (false);
		highlightDestroyed=true;
	}
	public void destroySelf(){
		score.text = "" + (int.Parse("" + score.text) + 1);
		destroyHighlight ();
		if (highlightDestroyed == true)
			gameObject.SetActive (false);
	}
}
