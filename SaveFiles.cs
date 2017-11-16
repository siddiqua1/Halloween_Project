using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SaveFiles : MonoBehaviour {

	[MenuItem("Tools/Write file")]
	void SaveToText(int saveNumber){
		string path = "Saves/save" + saveNumber + ".txt";
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine("Test");
		writer.Close();
	}

	[MenuItem("Tools/Read file")]
	void LoadFromText(int saveNumber){
		string path = "Saves/save" + saveNumber + ".txt";
		StreamReader reader = new StreamReader(path); 
		string a = reader.ReadToEnd();
		reader.Close();
	}
		
}
