using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{

	//[SerializeField] Dropdown resolutionDropDown;
	[SerializeField] Text qualityText;
	[SerializeField] Toggle bloomToggle;
	//[SerializeField] Slider sensitivitySlider;
	//[SerializeField] Toggle fullscreenToggle;



    void Start()
    {
        // apply settings to game world
		qualityText.text = GetQualityAsString(Settings.currentSettings.quality);
		bloomToggle.SetIsOnWithoutNotify(Settings.currentSettings.bloom);
		//sensitivitySlider.SetValueWithoutNotify(Settings.currentSettings.sensitivity);
		//fullscreenToggle.SetIsOnWithoutNotify(Settings.currentSettings.fullscreen);

		// get resolutions to populate drop down
		var text = Screen.currentResolution.width + " x " + Screen.currentResolution.height;
		//resolutionDropDown.AddOptions(new List<Dropdown.OptionData> {new Dropdown.OptionData(text)});
		// set resolution drop down
		//resolutionDropDown.SetValueWithoutNotify(0);
    }



	public void ChangeSettings()
	{
		// gather new settings from UI
		Settings.currentSettings.quality = QualitySettings.GetQualityLevel();
		Settings.currentSettings.bloom = bloomToggle.isOn;
		//Settings.currentSettings.sensitivity = sensitivitySlider.value;
		//Settings.currentSettings.fullscreen = fullscreenToggle.isOn;

		Settings.SaveSettings();
	}



	string GetQualityAsString(int quality)
	{
		return QualitySettings.names[quality];
	}



	public void IncreaseQuality()
	{
		// increase quality
		QualitySettings.IncreaseLevel();
		// update label
		qualityText.text = GetQualityAsString(QualitySettings.GetQualityLevel());
		// save
		ChangeSettings();
	}



	public void DecreaseQuality()
	{
		// decrease quality
		QualitySettings.DecreaseLevel();
		// update label
		qualityText.text = GetQualityAsString(QualitySettings.GetQualityLevel());
		// save
		ChangeSettings();
	}  
}
