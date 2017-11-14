using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomCollectablesRandomly : MonoBehaviour {

	public float scaleOfEachCell;
	public GameObject[] items;

	//This script will place the random collectables randomly

	void placeRandomly(int item){
		int r = Random.Range (0, GlobalVariables.maze.GetLength (0));
		int c = Random.Range (0, GlobalVariables.maze.GetLength (1));
		GameObject newItem = Instantiate (items[item], new Vector3 (r*scaleOfEachCell, 1, c*scaleOfEachCell), Quaternion.identity) as GameObject; 
	}
		
	//For every 30s, an item spawns
	void Update () {
		if (GlobalVariables.time % 30 == 0) {
			placeRandomly (Random.Range (0, items.Length));
		}
	}
}
