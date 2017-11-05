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
	//The 5 element array = [WallUp, WallRight, WallDown, WallLeft, trapType] where 1 represents true and 0 represents false

	public GameObject floor;
	public GameObject wall;

	void putMazeInUnity(){
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < cols; c++) {
				GameObject newFloor = Instantiate (floor, new Vector3 (r*scaleOfEachCell, 0, c*scaleOfEachCell), Quaternion.identity) as GameObject; 
				if (maze [r,c,0] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 180, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r*scaleOfEachCell - scaleOfEachCell / 2, heightOfWall/2, c*scaleOfEachCell), q) as GameObject;
				}
				if (maze [r,c,1] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 270, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r*scaleOfEachCell, heightOfWall/2, c*scaleOfEachCell + scaleOfEachCell / 2), q) as GameObject;
				}
				if (maze [r,c,2] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 0, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r*scaleOfEachCell + scaleOfEachCell / 2, heightOfWall/2, c*scaleOfEachCell), q) as GameObject;
				}
				if (maze [r,c,3] == 1) {
					Quaternion q = Quaternion.identity;
					q.eulerAngles = new Vector3 (0, 90, 90);
					GameObject gameWall = Instantiate (wall, new Vector3 (r*scaleOfEachCell, heightOfWall/2, c*scaleOfEachCell - scaleOfEachCell / 2), q) as GameObject;
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

	int[,,] generateMazeWithRecursiveDivision(int[,,] sectionedMaze, int startRowIndex, int endRowIndex, int startColIndex, int endColIndex){
		//Recursive division
		//Random row and column for a wall
		//Put a hole in the wall for each and call function again on the smaller chambers
		if (startRowIndex + 2 >= endRowIndex || startColIndex + 2 >= endColIndex) {
			return sectionedMaze;
		}
		int randColWall = Random.Range (startColIndex, endColIndex - 1); //Put wall right of this column
//		print("randColWall " + randColWall); 
		int randRowWall = Random.Range (startRowIndex, endRowIndex - 1); //Put wall below this column
//		print("randRowWall " + randRowWall); 

		for (int i = startColIndex; i < endColIndex + 1; i++) {
			sectionedMaze [randRowWall, i, 2] = 1;
			sectionedMaze [randRowWall + 1, i, 0] = 1;
		}
		for (int i = startRowIndex; i < endRowIndex + 1; i++) {
			sectionedMaze [i, randColWall, 1] = 1;
			sectionedMaze [i, randColWall + 1, 3] = 1;
		}
		//1st slit
		int randColSlit = Random.Range (startColIndex, endColIndex);
		while (randColSlit == randRowWall) {
			randColSlit = Random.Range (startColIndex, endColIndex);
		}
		sectionedMaze [randRowWall, randColSlit, 2] = 0;
		sectionedMaze [randRowWall + 1, randColSlit, 0] = 0;

		//2nd slit
		int randRowSlit = Random.Range (startRowIndex, endRowIndex);
		while (randRowSlit == randColWall) {
			randRowSlit = Random.Range (startRowIndex, endRowIndex);
		}
		sectionedMaze [randRowSlit, randColWall, 1] = 0;
		sectionedMaze [randRowSlit, randColWall + 1, 3] = 0;

		//Getting 3rd slit
		if (Random.Range (0, 1) == 0) {
			if (randColSlit > randColWall) {
				sectionedMaze [randRowWall, Random.Range (startColIndex, randColWall - 1), 2] = 0;
				sectionedMaze [randRowWall + 1, Random.Range (startColIndex, randColWall - 1), 0] = 0;
			} else {
				sectionedMaze [randRowWall, Random.Range (randColWall + 1, endColIndex + 1), 2] = 0;
				sectionedMaze [randRowWall + 1, Random.Range (randColWall + 1, endColIndex + 1), 0] = 0;
			}
			
		} else {
			if (randRowSlit > randRowWall) {
				sectionedMaze [Random.Range (startRowIndex, randRowWall - 1), randColWall, 1] = 0;
				sectionedMaze [Random.Range (startRowIndex, randRowWall - 1), randColWall + 1, 3] = 0;
			} else {
				sectionedMaze [Random.Range (randRowWall + 1, endRowIndex + 1), randColWall, 1] = 0;
				sectionedMaze [Random.Range (randRowWall + 1, endRowIndex + 1), randColWall + 1, 3] = 0;
			}
		}
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, startRowIndex, randRowWall, startColIndex, randColWall);
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, randRowWall, endRowIndex, startColIndex, randColWall);
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, startRowIndex, randRowWall, randColWall, endColIndex);
		sectionedMaze = generateMazeWithRecursiveDivision (sectionedMaze, randRowWall, endRowIndex, randColWall, endColIndex);
		return sectionedMaze;
	}

	void Start(){
		initializeMaze ();
		maze = generateMazeWithRecursiveDivision (maze, 0, rows - 1, 0, cols - 1);
		putMazeInUnity ();
		GlobalVariables.maze = maze;
	}

}