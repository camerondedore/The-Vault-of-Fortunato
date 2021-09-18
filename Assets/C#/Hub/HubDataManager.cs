using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class HubDataManager : MonoBehaviour
{
    
	public static HubData data = new HubData();



	public static void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/hub-data.dwg");
		bf.Serialize(file, data);
		file.Close();
	}



	public static void LoadData()
	{
		if(File.Exists(Application.persistentDataPath + "/hub-data.dwg")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/hub-data.dwg", FileMode.Open);
			data = (HubData)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			// no data exist
			data = new HubData();
		}
	}



	[System.Serializable]
	public class HubData
	{
		public string startingDoor = "inn";
	}
}
