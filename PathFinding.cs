using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour {

	public string playerName;
	public int[,] scoreMaze;
	private int levelTracker;

	void Start () {
		scoreMaze = new int[GlobalVariables.maze.GetLength(0), GlobalVariables.maze.GetLength(1)];
		levelTracker = GlobalVariables.level;
	}

	void ScoreBoardRecursively(int[] currentPos){



		int[] upPos = new int[2];
		int[] rightPos = new int[2];
		int[] downPos = new int[2];
		int[] leftPos = new int[2];

		upPos [0] = currentPos [0] - 1; 
		upPos [1] = currentPos [1];

		rightPos [0] = currentPos [0]; 
		rightPos [1] = currentPos [1] + 1;

		downPos [0] = currentPos [0] + 1; 
		downPos [1] = currentPos [1];

		leftPos [0] = currentPos [0]; 
		leftPos [1] = currentPos [1] - 1;

		bool canMoveUp = upPos[0] >= 0 && GlobalVariables.maze[currentPos[0], currentPos[1], 0] == 0;
		bool canMoveRight = rightPos[0] < GlobalVariables.col && GlobalVariables.maze[currentPos[0], currentPos[1], 1] == 0;
		bool canMoveDown = downPos[0] < GlobalVariables.col && GlobalVariables.maze [currentPos [0], currentPos [1], 2] == 0;
		bool canMoveLeft = leftPos[0] >= 0 && GlobalVariables.maze[currentPos[0], currentPos[1], 3] == 0;

		//print (GlobalVariables.col - 1);
		//print (GlobalVariables.row - 1);

		print (currentPos [0]);
		print (currentPos [1]);

		if (currentPos [0] == 0 && currentPos [1] == 0) {
			return;
		}
		if (currentPos [0] == 0 && currentPos [1] == GlobalVariables.col - 1 ) {
			return;
		}
		if (currentPos [0] == GlobalVariables.row - 1 && currentPos [1] == 0) {
			return;
		}
		if (currentPos [0] == GlobalVariables.row - 1 && currentPos [1] == GlobalVariables.col - 1 ) {
			return;
		}

		if (currentPos [1] == 0) {
			if (canMoveRight && scoreMaze[rightPos[0], rightPos[1]] != 0) {
				if (canMoveUp) {
					if (scoreMaze [upPos [0], upPos [1]] != 0) {
						return;
					}
				}
				if (canMoveDown) {
					if (scoreMaze [downPos [0], downPos [1]] != 0) {
						return;
					}
				}
			} else {
				if (canMoveUp) {
					if (scoreMaze [upPos [0], upPos [1]] != 0) {
						return;
					}
				}
				if (canMoveDown) {
					if (scoreMaze [downPos [0], downPos [1]] != 0) {
						return;
					}
				}
			}
		} 

		if (currentPos [1] == GlobalVariables.col - 1) {
			if (canMoveLeft && scoreMaze[leftPos[0], leftPos[1]] != 0) {
				if (canMoveUp) {
					if (scoreMaze [upPos [0], upPos [1]] != 0) {
						return;
					}
				}
				if (canMoveDown) {
					if (scoreMaze [downPos [0], downPos [1]] != 0) {
						return;
					}
				}
			} else {
				if (canMoveUp) {
					if (scoreMaze [upPos [0], upPos [1]] != 0) {
						return;
					}
				}
				if (canMoveDown) {
					if (scoreMaze [downPos [0], downPos [1]] != 0) {
						return;
					}
				}
			}
		} 

		if (currentPos [0] == 0) {
			if (canMoveDown && scoreMaze[downPos[0], downPos[1]] != 0) {
				if (canMoveLeft) {
					if (scoreMaze [leftPos [0], leftPos [1]] != 0) {
						return;
					}
				}
				if (canMoveRight) {
					if (scoreMaze [rightPos [0], rightPos [1]] != 0) {
						return;
					}
				}
			} else {
				if (canMoveLeft) {
					if (scoreMaze [leftPos [0], leftPos [1]] != 0) {
						return;
					}
				}
				if (canMoveRight) {
					if (scoreMaze [rightPos [0], rightPos [1]] != 0) {
						return;
					}
				}
			}
		} 

		if (currentPos [0] == GlobalVariables.row - 1) {
			if (canMoveUp && scoreMaze[upPos[0], upPos[1]] != 0) {
				if (canMoveLeft) {
					if (scoreMaze [leftPos [0], leftPos [1]] != 0) {
						return;
					}
				}
				if (canMoveRight) {
					if (scoreMaze [rightPos [0], rightPos [1]] != 0) {
						return;
					}
				}
			} else {
				if (canMoveLeft) {
					if (scoreMaze [leftPos [0], leftPos [1]] != 0) {
						return;
					}
				}
				if (canMoveRight) {
					if (scoreMaze [rightPos [0], rightPos [1]] != 0) {
						return;
					}
				}
			}
		} 



		//Base Case: Everything else


		
		/*
		catch (Exception e){
			print (upPos [0]);
			print (upPos [1]);
			print (rightPos [0]);
			print (rightPos [1]);
			print (downPos [0]);
			print (downPos [1]);
			print (leftPos [0]);
			print (leftPos [1]);
		}
		*/

		if (scoreMaze [currentPos [0], currentPos [1]] == 0) {
			scoreMaze [currentPos [0], currentPos [1]] = 1;
		}






		if (upPos[0] >= 0 && GlobalVariables.maze[currentPos[0], currentPos[1], 0] == 0 && (scoreMaze [upPos[0], upPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [upPos[0], upPos[1]] == 0)) {
			scoreMaze [upPos[0], upPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}
		if (rightPos[0] < GlobalVariables.col && GlobalVariables.maze[currentPos[0], currentPos[1], 1] == 0 && (scoreMaze [rightPos[0], rightPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [rightPos[0], rightPos[1]] == 0)) {
			scoreMaze [rightPos[0], rightPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}
		if (downPos[0] < GlobalVariables.row && GlobalVariables.maze[currentPos[0], currentPos[1], 2] == 0 && (scoreMaze [downPos[0], downPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [downPos[0], downPos[1]] == 0)) {
			scoreMaze [downPos[0], downPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}
		if (leftPos[0] >= 0 && GlobalVariables.maze[currentPos[0], currentPos[1], 3] == 0 && (scoreMaze [leftPos[0], leftPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [leftPos[0], leftPos[1]] == 0)) {
			scoreMaze [leftPos[0], leftPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}

		if (upPos [0] >= 0) {
			ScoreBoardRecursively (upPos);
		}
		if (rightPos [0] < GlobalVariables.col) {
			ScoreBoardRecursively (rightPos);
		}
		if (downPos [0] < GlobalVariables.row) {
			ScoreBoardRecursively (downPos);
		}
		if (leftPos [0] >= 0) {
			ScoreBoardRecursively (leftPos);
		}





	}

	bool boardIsScored(){
		for (int i = 0; i < scoreMaze.GetLength (0); i++) {
			for (int j = 0; j < scoreMaze.GetLength (1); j++) {
				if (scoreMaze [i, j] != 0) {
					return false;
				}
			}
		}
		return true;
	}

	Stack<Vector2> getPathRecursively(int[] startingPos, int[] endingPos, Stack<Vector2> p){
		Stack<Vector2> path = p;
		if (!boardIsScored ()) {
			return null;
		}
		if (startingPos [0] == endingPos [0] && startingPos [1] == endingPos [1]) {
			return path;
		} else {
			int[] upPos = new int[2];
			int[] rightPos = new int[2];
			int[] downPos = new int[2];
			int[] leftPos = new int[2];

			upPos [0] = endingPos [0] + 1; 
			upPos [1] = endingPos [1];
			rightPos [0] = endingPos [0]; 
			rightPos [1] = endingPos [1] + 1;
			downPos [0] = endingPos [0] - 1; 
			downPos [1] = endingPos [1];
			leftPos [0] = endingPos [0]; 
			leftPos [1] = endingPos [1] - 1;
			if (upPos[0] >= 0 && GlobalVariables.maze[endingPos[0], endingPos[1], 0] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[upPos[0], upPos[1]]){
				path.Push (Vector2.down);
				getPathRecursively (startingPos, upPos, path);
			}
			if (rightPos[0] < GlobalVariables.col && GlobalVariables.maze[endingPos[0], endingPos[1], 1] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[rightPos[0], rightPos[1]]){
				path.Push (Vector2.left);
				getPathRecursively (startingPos, rightPos, path);
			}
			if (downPos[0] < GlobalVariables.row && GlobalVariables.maze[endingPos[0], endingPos[1], 2] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[downPos[0], downPos[1]])
			{
				path.Push(Vector2.up);
				getPathRecursively(startingPos, downPos, path);
			}
			if (leftPos[0] >= 0 && GlobalVariables.maze[endingPos[0], endingPos[1], 3] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[leftPos[0], leftPos[1]])
			{
				path.Push(Vector2.right);
				getPathRecursively(startingPos, leftPos, path);
			}
		}
		return path;
	}


	// Update is called once per frame
	void Update () {
		if (levelTracker != GlobalVariables.level) {
			levelTracker = GlobalVariables.level;
			scoreMaze = new int[GlobalVariables.maze.GetLength(0), GlobalVariables.maze.GetLength(1)];
		}
		Vector3 position = this.gameObject.transform.position;
		int[] element = new int[2];
		element [0] = Mathf.RoundToInt (position.x / 2);
		element [1] = Mathf.RoundToInt (position.z / 2);
		Vector3 playerPos = GameObject.Find (playerName).transform.position;
		int[] playerPosition = new int[2];
		playerPosition [0] = Mathf.RoundToInt (playerPos.x / 2);
		playerPosition [1] = Mathf.RoundToInt (playerPos.z / 2);
		Stack<Vector2> p = new Stack<Vector2> ();
		ScoreBoardRecursively (element);
		Stack<Vector2> path = getPathRecursively (element, playerPosition, p);
		Vector2 bestPath = path.Pop ();
		Vector3 bestPath3 = new Vector3 (bestPath.x, 0, bestPath.y);
		this.gameObject.transform.Translate (bestPath3 * Time.deltaTime);
	}
}
