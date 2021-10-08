using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MapBrickCountUI : MonoBehaviour
{

    [SerializeField]
	Text innCounter,
		//hubCounter,
		cemeteryCounter,
		manorCounter,
		hedgeCounter;



	void Start()
	{
		// load counters
		innCounter.text = $"{PlayerDataManager.data.brickIds.Where(id => id.Contains("inn")).Count()}/1";
		//hubCounter.text = $"{PlayerDataManager.data.brickIds.Where(id => id.Contains("hub")).Count()}/2";
		cemeteryCounter.text = $"{PlayerDataManager.data.brickIds.Where(id => id.Contains("cemetery")).Count()}/5";
		manorCounter.text = $"{PlayerDataManager.data.brickIds.Where(id => id.Contains("manor")).Count()}/5";
		hedgeCounter.text = $"{PlayerDataManager.data.brickIds.Where(id => id.Contains("hedge")).Count()}/5";
	}
}
