using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	public int[,] scoreMaze;
	private int levelTracker;

	// Use this for initialization
	void Start () {
		scoreMaze = new int[GlobalVariables.maze.GetLength(0), GlobalVariables.maze.GetLength(1)];
		levelTracker = GlobalVariables.level;
	}

	void ScoreBoardRecursively(int[] currentPos){
		if (scoreMaze[currentPos[0],currentPos[1]] != 0 && scoreMaze[currentPos[0] + 1, currentPos[1]] != 0 && scoreMaze[currentPos[0], currentPos[1] + 1] != 0 && scoreMaze[currentPos[0] - 1, currentPos[1]] != 0 && scoreMaze[currentPos[0], currentPos[1] - 1] != 0 ){
			return;
		}
		if (scoreMaze [currentPos [0], currentPos [1]] == 0) {
			scoreMaze [currentPos [0], currentPos [1]] = 1;
		}

		int[] upPos = new int[2];
		int[] rightPos = new int[2];
		int[] downPos = new int[2];
		int[] leftPos = new int[2];

		upPos [0] = currentPos [0] + 1; 
		upPos [1] = currentPos [1];
		rightPos [0] = currentPos [0]; 
		rightPos [1] = currentPos [1] + 1;
		downPos [0] = currentPos [0] - 1; 
		downPos [1] = currentPos [1];
		leftPos [0] = currentPos [0]; 
		leftPos [1] = currentPos [1] - 1;

		if (GlobalVariables.maze[currentPos[0], currentPos[1], 0] == 0 && (scoreMaze [upPos[0], upPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [upPos[0], upPos[1]] == 0)) {
			scoreMaze [upPos[0], upPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}
		if (GlobalVariables.maze[currentPos[0], currentPos[1], 1] == 0 && (scoreMaze [rightPos[0], rightPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [rightPos[0], rightPos[1]] == 0)) {
			scoreMaze [rightPos[0], rightPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}
		if (GlobalVariables.maze[currentPos[0], currentPos[1], 2] == 0 && (scoreMaze [downPos[0], downPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [downPos[0], downPos[1]] == 0)) {
			scoreMaze [downPos[0], downPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}
		if (GlobalVariables.maze[currentPos[0], currentPos[1], 3] == 0 && (scoreMaze [leftPos[0], leftPos[1]] > scoreMaze [currentPos [0], currentPos [1]] + 10 || scoreMaze [leftPos[0], leftPos[1]] == 0)) {
			scoreMaze [leftPos[0], leftPos[1]] = scoreMaze [currentPos [0], currentPos [1]] + 10;
		}

		ScoreBoardRecursively (upPos);
		ScoreBoardRecursively (rightPos);
		ScoreBoardRecursively (downPos);
		ScoreBoardRecursively (leftPos);
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

	Stack getPathRecursively(int[] startingPos, int[] endingPos, Stack p){
		Stack path = p;
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
			if (GlobalVariables.maze[endingPos[0], endingPos[1], 0] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[upPos[0], upPos[1]]){
				path.Push (Vector2.down);
				getPathRecursively (startingPos, upPos, path);
			}
			if (GlobalVariables.maze[endingPos[0], endingPos[1], 1] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[rightPos[0], rightPos[1]]){
				path.Push (Vector2.left);
				getPathRecursively (startingPos, rightPos, path);
			}
            if (GlobalVariables.maze[endingPos[0], endingPos[1], 2] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[downPos[0], downPos[1]])
            {
                path.Push(Vector2.up);
                getPathRecursively(startingPos, upPos, path);
            }
            if (GlobalVariables.maze[endingPos[0], endingPos[1], 3] == 0 && scoreMaze[endingPos[0], endingPos[1]] - 10 == scoreMaze[leftPos[0], leftPos[1]])
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
	}
}
