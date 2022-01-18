using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEventOnTiles : MonoBehaviour {
	public GameObject tower;
	public float OnMouseDownYPlacement = 4f;
	public float OnMouseDownZPlacement = 0.5f;

	void OnMouseEnter(){
		Color choice;
		if(GetComponent<TileProperties>().available == true){
			choice = Color.green;
		}else{
			choice = Color.red;
		}
		GetComponent<SpriteRenderer>().color = choice;
		if(tower != null){
			SpriteRenderer[] sr = tower.GetComponentsInChildren<SpriteRenderer>();
			foreach(SpriteRenderer s in sr){
				s.color = choice;
			}
		}
	}

	void OnMouseExit(){
		GetComponent<SpriteRenderer>().color = Color.white;
		if(tower != null){
			SpriteRenderer[] sr = tower.GetComponentsInChildren<SpriteRenderer>();
			foreach(SpriteRenderer s in sr){
				s.color = Color.white;
			}
		}
	} 

	void OnMouseDown(){

		if(tower != null){
			TurretBuildManager.tbm.upgradeUI.SetActive(true);
			SellAndUpgrade.su.tower = tower;
			SellAndUpgrade.su.showTowerStatus();
			TurretBuildManager.tbm.buy = false;
			TurretBuildManager.tbm.sell = true;
			
		}

		if(tower == null && GetComponent<TileProperties>().available != false && TurretBuildManager.tbm.turretToBuild != null){
			Vector3 location = GetComponent<TileProperties>().location;
			GameObject turret = TurretBuildManager.tbm.turretToBuild;
			Stats.moneyManip(TurretBuildManager.tbm.price, false);
			TurretBuildManager.tbm.buy = false;
			TurretBuildManager.tbm.turretToBuild = null;
			location.y += OnMouseDownYPlacement;
			location.z -= OnMouseDownZPlacement;
			
			tower = (GameObject)Instantiate(turret,location, Quaternion.identity);
			tower.GetComponent<TowerRotate>().price = TurretBuildManager.tbm.price;
			tower.transform.parent = GameObject.Find("Towers").transform;
		}
	}
}
