using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GroundGrid : MonoBehaviour {

	public GameObject[,] gridObject;
	public List<GameObject> gridGroup;
	public GameObject groundGridManager;
	public GameObject soilGridManager;
	public int rows;
	public int cols;
	int xpos;
	int ypos;

	void Start()
	{
		rows=1;
		cols=25;
		gridObject=new GameObject[rows,cols];
		float distx=0;
		float disty=0;
		for (int i=0; i < rows; i++) {
			for (int j=0; j<cols; j++){
				gridObject[i,j] = Instantiate (groundGridManager, new Vector2(-7+distx,.6f+disty), Quaternion.identity) as GameObject;
				gridObject[i,j].GetComponent<GroundGridManager>().connectedObject=(Instantiate (soilGridManager, new Vector2(-7+distx,.7f+disty-1), Quaternion.identity) as GameObject);
				distx+=.9f;
			}
			disty+=.7f;
			distx=0;
		}
	}

	public GameObject[,] getGrid()
	{
		return gridObject;
	}


}
