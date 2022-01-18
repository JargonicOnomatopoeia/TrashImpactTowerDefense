using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBuildManager : MonoBehaviour {

	[Header("Sell Attributes")]
	public bool sell = false;
	public GameObject upgradeUI;

	[Header("Buy Attributes")]
	public bool buy = false;
	public static TurretBuildManager tbm;
	public int type {get; set;}
	public GameObject turretToBuild {get; set;}
	public int price {get; set;}

	void Awake(){
		tbm = this;
	}
}
