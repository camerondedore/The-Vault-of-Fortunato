using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class InnDataManager : MonoBehaviour
{
    
	public static InnData data = new InnData();



	public static void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/inn-data.dwg");
		bf.Serialize(file, data);
		file.Close();
	}



	public static void LoadData()
	{
		if(File.Exists(Application.persistentDataPath + "/inn-data.dwg")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/inn-data.dwg", FileMode.Open);
			data = (InnData)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			// no data exist
			data = new InnData();
		}
	}



	[System.Serializable]
	public class InnData
	{
		public bool startAtDoor = false;
	}
}
