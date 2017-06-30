using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class GroundGridManager : MonoBehaviour {
	public Text resources;
	public Text resources2;
	public int sunlight=0;
	public int oxygen=0;
	public GameObject beam;
	public int health=3;
	public int day=0;
	public int hasPlant=0;
	public GameObject soilGrid;
	static int score=0;
	public AudioClip shimmer;
	public AudioClip pageturn;
	public AudioClip rocks;
	public AudioClip air;
	Animator anim;
	bool mouseOver=false;
	GameObject root;
	bool safe=false;
	SpriteRenderer rend;
	bool soundPlay=false;
	string plant="grow";
	string idle = "BG";
	public int isConnected=0;
	public GameObject connectedObject;
	GameObject rootObject = null;
    public GameObject rootStub;
	public GameObject rootStub2;
	public GameObject oxygenSprite;
	public GameObject spawnedOxygen;
	bool grow=true;

	// Update is called once per frame

	void Start(){
		rend=GetComponent<SpriteRenderer>();
		anim=GetComponent<Animator>();
		GameObject[] objs = GameObject.FindGameObjectsWithTag("gridSoil");
		foreach (GameObject go in objs) {
			if (go.transform.position.x == transform.position.x) {
				rootObject = go;
				break;
			}
		}
		rend.color=new Color(rend.color.r, rend.color.g, rend.color.b, .2f);
	}
	void Update () {
		
        if (connectedObject.GetComponent<SoilGridMngr>().isConnectedToElement)
		{
			isConnected=1;
			safe=true;
		}else{
			safe=false;
		}
		
		if(mouseOver==true) 
		{
			rend.color=new Color(rend.color.r, rend.color.g, rend.color.b, 1.0f);
			if(hasPlant==0)
				anim.SetBool(idle, true);
			
			mouseOver=false;
		}else{
			if(hasPlant==0)
			{
				day=0;
				soundPlay=false;
				anim.SetBool(plant, false);
				anim.SetBool(idle, false);
				rend.color=new Color(rend.color.r, rend.color.g, rend.color.b, .2f);
				transform.localScale = new Vector3(1f, 1f, 0f);
				GetComponent<BoxCollider2D>().size=new Vector2(1f, 1);
				health=2;
			}else if(!(hasPlant==1)){
				anim.SetBool(idle, false);
				transform.localScale = new Vector3(1f, 1f, 0f);
				GetComponent<BoxCollider2D>().size=new Vector2(1f, 1);
			}
		}

		if(hasPlant>=1)
		{
			anim.SetBool(idle, true);
			transform.localScale = new Vector3(2f, 2f, 0f);
			GetComponent<BoxCollider2D>().size=new Vector2(.5f, 1);
			if(day==1)
			{
				if(grow==true)
					anim.SetBool(plant, true);
				if(soundPlay==false)
				{
						GetComponent<AudioSource>().clip=pageturn;
						GetComponent<AudioSource>().PlayDelayed(1f);
					soundPlay=true;
				}
			}
			checkPlant();

		}
	}

	public void createGrass()
	{
		anim.SetBool(idle, false);
		plant="grow";
		idle="BG";
		hasPlant=1;
        Instantiate(rootStub2, transform.position+new Vector3(0, -1, 0), Quaternion.identity);
        rootObject.GetComponent<DrawLine>().hasPlant = hasPlant;
        rootObject.GetComponent<SoilGridMngr>().hasPlant = hasPlant;


    }
	public void createTree()
	{
		anim.SetBool(idle, false);
		plant="TreeGrow";
		idle="BGIdle";
		hasPlant=3;
		score++;
        Instantiate(rootStub2, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        rootObject.GetComponent<DrawLine>().hasPlant = hasPlant;
        rootObject.GetComponent<SoilGridMngr>().hasPlant = hasPlant;
    }
	public void createShrub()
	{
		anim.SetBool(idle, false);
		plant="ShrubGrow";
		idle="BGshrub";
		hasPlant=2;
        Instantiate(rootStub, transform.position + new Vector3(0, -1, 0), Quaternion.identity);
        rootObject.GetComponent<DrawLine>().hasPlant = hasPlant;
        rootObject.GetComponent<SoilGridMngr>().hasPlant = hasPlant;

    }
	public void setMouseOver()
	{
		mouseOver=true;
	}

	void checkPlant()
	{

		if(safe)
		{
			health=3;
		}
		Vector2 targetPos=beam.transform.position;
		targetPos.y=transform.position.y;
		if(beam.transform.position.y>transform.position.y)
		{
			if(hasPlant>=1&&rootObject.GetComponent<SoilGridMngr>().isConnectedToElement==true)
			{
				if(Vector2.Distance(targetPos,transform.position)<.1f&&oxygen==0)
				{
					
					resources2.text=""+(int.Parse(""+resources2.text)+1);
					if (hasPlant == 3) {
						resources2.text=""+(int.Parse(""+resources2.text)+1);
					}
					sendOxygen ();

					oxygen=1;
					GetComponent<AudioSource>().clip=air;
					GetComponent<AudioSource>().PlayDelayed(.5f);
					day++;
					if(rootObject.GetComponent<SoilGridMngr>().isConnectedToElement)
					{
						rootObject.GetComponent<SoilGridMngr> ().elementGain ();
					}
				}
				if(Vector2.Distance(targetPos,transform.position)>.1f&&oxygen==1)
				{
					oxygen=0;
				}
			}
		}
		if(health<0)
			hasPlant=0;
	}
	public void sendOxygen(){
		spawnedOxygen= (GameObject)Instantiate (oxygenSprite, transform.position, Quaternion.identity);
		if (hasPlant == 3) {
			spawnedOxygen.transform.localScale = oxygenSprite.transform.localScale * 2;
		}
		spawnedOxygen.GetComponent<ResourceGain> ().setTween ("oxygen");
	}
	public void startIdle(){
		grow = false;
		anim.SetBool (plant, false);
	}
}
