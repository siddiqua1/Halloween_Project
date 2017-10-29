using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {
	//Generates a maze as a 2D array

	public int rows;
	public int cols;
	public float scaleOfEachCell;
	public float heightOfWall;

	private int[,,] maze;
	//Maze is (rows x cols x 5) where the 5 element array for each cell represents
	//The 5 element array = [WallUp, WallRight, WallDown, WallLeft, isVisited] where 1 represents true and 0 represents false

	public GameObject floor;
	public GameObject wall;

	void putMazeInUnity(){
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				GameObject newFloor = Instantiate (floor, new Vector3 (r, 0, c), Quaternion.identity) as GameObject; 
				//newFloor.AddComponent<Rigidbody> ();
				if (maze [r,c,0] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 180, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r - scaleOfEachCell / 2, heightOfWall/2, c), q) as GameObject;
					//gameWall.AddComponent<Rigidbody> ();
				}
				if (maze [r,c,1] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 270, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r, heightOfWall/2, c + scaleOfEachCell / 2), q) as GameObject;
					//gameWall.AddComponent<Rigidbody> ();
				}
				if (maze [r,c,2] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 0, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r + scaleOfEachCell / 2, heightOfWall/2, c), q) as GameObject;
					//gameWall.AddComponent<Rigidbody> ();
				}
				if (maze [r,c,3] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 90, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r, heightOfWall/2, c - scaleOfEachCell / 2), q) as GameObject;
					//gameWall.AddComponent<Rigidbody> ();
				}				
			}
		}
	}

	void initializeMaze(){
		maze = new int[rows, cols, 5];
		for (int c = 0; c < cols; c++) {
			for (int r = 0; r < rows; r++) {
				if (c == 0)
					maze [r, 0, 3] = 1;
				else if (c == cols - 1)
					maze [r, c, 1] = 1;
				if (r == 0)
					maze [r, c, 0] = 1;
				else if (r == rows - 1)
					maze [r, c, 2] = 1;
			}
		}
	}

	int[,,] generateMazeWithRecursiveDivision(int[,,] sectionedMaze){
		//Recursive division
		//Random row and column for a wall
		//Put a hole in the wall for each and call function again on the smaller chambers
		if (sectionedMaze.Length == 1) {
			return sectionedMaze;
		}
		int randColWall = Random.Range (0, cols - 2); //Put wall right of this column
		int randRowWall = Random.Range (0, rows - 2); //Put wall below this column
		for (int i = 0; i < cols; i++) {
			sectionedMaze [randRowWall, i, 2] = 1;
		}
		for (int i = 0; i < rows; i++) {
			sectionedMaze [i, randColWall, 1] = 1;
		}
		//1st slit
		int randColSlit = Random.Range (0, cols - 1);
		sectionedMaze [randRowWall, randColSlit, 2] = 0;
		//2nd slit
		int randRowSlit = Random.Range (0, rows - 1);
		sectionedMaze [randRowSlit, randColWall, 1] = 0;

		//Getting 3rd slit
		if (Random.Range (0, 1) == 0) {
			if (randColSlit > randColWall) 
				sectionedMaze [randRowWall, Random.Range(0, randColWall - 1), 2] = 0;
			else
				sectionedMaze [randRowWall, Random.Range(randColWall + 1, cols), 2] = 0;
			
		} else {
			if (randRowSlit > randRowWall) 
				sectionedMaze [Random.Range(0, randRowWall - 1), randColWall, 1] = 0;
			else
				sectionedMaze [Random.Range(randRowWall + 1, rows), randColWall, 1] = 0;
		}

		return sectionedMaze;
	}

	void Start(){
		initializeMaze ();
		maze = generateMazeWithRecursiveDivision (maze);
		putMazeInUnity ();

	}

}