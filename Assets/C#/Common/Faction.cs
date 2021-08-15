using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    
	public enum factions {Player, Police};
	public static List<Faction> actors = new List<Faction>();
	public factions faction;



	void OnEnable()
	{
		actors.Add(this);
	}



	void OnDisable()
	{
		actors.Remove(this);
	}
}
