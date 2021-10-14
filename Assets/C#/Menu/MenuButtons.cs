using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    
	[SerializeField] Text qualityText;



	void Update()
	{
		// update quality text
		var targetQualityText = QualitySettings.names[Settings.currentSettings.quality];
		
		if(targetQualityText != qualityText.text)
		{
			qualityText.text = targetQualityText;
		}
	}



	public void Play()
	{
		// set player's starting inn location to the cellar
		InnDataManager.data.startAtDoor = false;
		InnDataManager.SaveData();

		SceneLoader.LoadLevel("Inn");
	}



	public void Quit()
	{
		Application.Quit();
	}



	public void ClearSavedData()
	{
		// clear bricks and health from save file
		PlayerDataManager.data = new PlayerDataManager.PlayerData();
		PlayerDataManager.SaveData();
	}
}
