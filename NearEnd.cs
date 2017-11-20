using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnd : MonoBehaviour {

	public string playerName;

	void Update () {
		Vector3 playerPos = GameObject.Find (playerName).transform.position;
		if ((playerPos - new Vector3 (GlobalVariables.scaleOfEachCell * (GlobalVariables.row - 1), 0, GlobalVariables.scaleOfEachCell * (GlobalVariables.col - 1))).sqrMagnitude < 0.5) {

		}
	}
}
