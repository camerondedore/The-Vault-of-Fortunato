using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{

	[SerializeField]
	Toggle bloomToggle;
	[SerializeField]
	Text qualityText;
	[SerializeField]
	Button qualityUpButton,
		qualityDownButton;
	bool qualityChanged = true;



    void Start()
    {
        // apply settings to game world		
		bloomToggle.SetIsOnWithoutNotify(Settings.currentSettings.bloom);
    }



	void Update()
	{
		if(qualityChanged)
		{
			qualityChanged = false;

			// update quality text
			qualityText.text = QualitySettings.names[Settings.currentSettings.quality];

			if(Settings.currentSettings.quality == QualitySettings.names.Length - 1)
			{
				// disable up button
				qualityUpButton.interactable = false;
			}
			else if(Settings.currentSettings.quality == 0)
			{
				// disable down button
				qualityDownButton.interactable = false;
			}
			else
			{
				// enable up and down buttons
				qualityUpButton.interactable = true;
				qualityDownButton.interactable = true;
			}
		}
	}



	public void ChangeSettings()
	{
		// gather new settings from UI
		//Settings.currentSettings.quality = QualitySettings.GetQualityLevel();
		Settings.currentSettings.bloom = bloomToggle.isOn;

		// save
		Settings.SaveSettings();
	}



	public void IncreaseQuality()
	{
		var oldQuality = Settings.currentSettings.quality;

		// increase quality
		Settings.currentSettings.quality = Mathf.Clamp(Settings.currentSettings.quality + 1, 0, QualitySettings.names.Length - 1);
		QualitySettings.SetQualityLevel(Settings.currentSettings.quality);

		// changed check
		if(oldQuality != Settings.currentSettings.quality)
		{
			qualityChanged = true;
		}	

		// save
		Settings.SaveSettings();
	}



	public void DecreaseQuality()
	{
		var oldQuality = Settings.currentSettings.quality;

		// decrease quality
		Settings.currentSettings.quality = Mathf.Clamp(Settings.currentSettings.quality - 1, 0, QualitySettings.names.Length - 1);
		QualitySettings.SetQualityLevel(Settings.currentSettings.quality);

		// changed check
		if(oldQuality != Settings.currentSettings.quality)
		{
			qualityChanged = true;
		}	
		
		// save
		Settings.SaveSettings();
	}  
}
