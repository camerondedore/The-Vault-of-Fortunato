using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{

	[SerializeField] Toggle bloomToggle;



    void Start()
    {
        // apply settings to game world		
		bloomToggle.SetIsOnWithoutNotify(Settings.currentSettings.bloom);
    }



	public void ChangeSettings()
	{
		// gather new settings from UI
		Settings.currentSettings.quality = QualitySettings.GetQualityLevel();
		Settings.currentSettings.bloom = bloomToggle.isOn;

		Settings.SaveSettings();
	}



	public void IncreaseQuality()
	{
		// increase quality
		QualitySettings.IncreaseLevel();
		// save
		ChangeSettings();
	}



	public void DecreaseQuality()
	{
		// decrease quality
		QualitySettings.DecreaseLevel();
		// save
		ChangeSettings();
	}  
}
