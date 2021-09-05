using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
   
	public static PlayerData data;



	void Awake()
	{
		LoadData();
	}



	public static void SaveData()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/player-data.dwg");
		bf.Serialize(file, data);
		file.Close();
	}



	public static void LoadData()
	{
		if(File.Exists(Application.persistentDataPath + "/player-data.dwg")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/player-data.dwg", FileMode.Open);
			data = (PlayerData)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			// no data exist
			data = new PlayerData();
		}
	}



	[System.Serializable]
	public class PlayerData
	{
		public float hitPoints = 3;
		public int bricks = 0;
	}
}
