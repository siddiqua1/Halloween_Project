using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRandomCollectablesRandomly : MonoBehaviour {

	//This script will place the random collectables randomly

	void placeRandomly(int item){
		int x = Random.Range (0, GlobalVariables.maze.GetLength (0));
		int y = Random.Range (0, GlobalVariables.maze.GetLength (1));
		//Spawn at maze[x,y]
	}
		
	//For every 30s, an item spawns
	void Update () {
		if (GlobalVariables.time % 30 == 0) {
			placeRandomly (Random.Range (0, 10));
		}
	}
}
