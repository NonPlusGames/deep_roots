using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Element : MonoBehaviour {
	public string elementType;
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

	// Update is called once per frame
	void Start() {
		if (elementType == "carbon") {
			spawnedElement = (GameObject)Instantiate (nitrogenSprite, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			spawnedElement.transform.parent = gameObject.transform;
		}
		if (elementType == "hydrogen") {
			spawnedElement = (GameObject)Instantiate (waterSprite, new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity);
			spawnedElement.transform.parent = gameObject.transform;
		}
		
	}

	public void spawnHighlight() {
		if (highlighted == false) {
			spawnedHighlight = (GameObject)Instantiate (highlight, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1), Quaternion.identity);
			gameObject.transform.parent = spawnedHighlight.transform;
			highlighted = true;
		}
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
