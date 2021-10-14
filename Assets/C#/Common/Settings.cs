using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using UnityEngine.Rendering.PostProcessing;


public class Settings : MonoBehaviour
{
    
	public static PlayerSettings currentSettings;
	[SerializeField] PostProcessProfile ppp;
	static Bloom bloom;



	void Awake()
	{
		ppp.TryGetSettings(out bloom);

		LoadSettings();
	}



	public static void SaveSettings() 
	{
		ApplySettings();

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/settings.dwg");
		bf.Serialize(file, currentSettings);
		file.Close();
	}



	public static void LoadSettings() 
	{
		if(File.Exists(Application.persistentDataPath + "/settings.dwg")) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/settings.dwg", FileMode.Open);
			currentSettings = (PlayerSettings)bf.Deserialize(file);
			file.Close();
		}
		else
		{
			// no settings exist
			currentSettings = new PlayerSettings();
		}

		ApplySettings();
	}



	public static void ApplySettings()
	{
		QualitySettings.SetQualityLevel(Settings.currentSettings.quality);
		bloom.active = Settings.currentSettings.bloom;
	}



	[System.Serializable]
	public class PlayerSettings
	{
		public int quality = 5;
		public bool bloom = true;
	}
}
