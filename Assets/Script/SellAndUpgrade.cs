using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SellAndUpgrade : MonoBehaviour {
	[HideInInspector] public GameObject tower;
	[Header("Text To Insert")]
	public Text Damage;
	public Text EffectiveDamage;
	public Text FireRate;
	public Text Range;

	[Header("Upgrade Text To Insert")]
	public Text UpgradeDamage;
	public Text UpgradeEffectiveDamage;
	public Text UpgradeFireRate;
	public Text UpgradeRange;

	[Header("Upgrade Cost Text")]
	public Text UpgradeCost;
	public Text SellValue;

	[Header("Upgrade Limit Displays")]
	public Text currentUpgrade;
	public Text maxUpgrade;
	public static SellAndUpgrade su;
	private TurretBuildManager tb;
	private TowerRotate t;
	private Bullet s;

	private int sellPrice;
	private int UpgradePrice;
	void Start(){
		tb = TurretBuildManager.tbm;
		su = this;
	}

	public void showTowerStatus(){
		t = tower.GetComponent<TowerRotate>();
		currentUpgrade.text = t.currentUpgrade.ToString();
		maxUpgrade.text = "/"+t.MaxUpgrade;

		Damage.text = t.normalDamage.ToString();
		EffectiveDamage.text = t.effectiveDamage.ToString();
		FireRate.text = t.fireRate.ToString();
		Range.text = t.range.ToString();

		UpgradeDamage.text = "+"+t.additionalDamage;
		UpgradeEffectiveDamage.text = "+"+t.additionalEffectiveDamage;
		bool temp = FloatCalcForZero(t.fireRate, t.additionalfireRate);
		UpgradeFireRate.text = (temp == true)?"0":t.additionalfireRate.ToString();
		UpgradeRange.text = "+"+t.rangeIncrease;


		UpgradePrice = t.price + t.upgradeCost;
		UpgradeCost.text = UpgradePrice.ToString();
		sellPrice = t.price/2;
		SellValue.text = sellPrice.ToString();
	}

	bool FloatCalcForZero(float val1, float val2){
		float val;
		val = val1 - Mathf.Abs(val2);
		return (val <= 0f)?true:false;
	}

	public void buttonUpgrade(){
		if(UpgradePrice <= Stats.Money && t.MaxUpgrade != t.currentUpgrade){
			t.currentUpgrade++;
			Stats.moneyManip(UpgradePrice, false);
			t.price += t.priceIncrease;
			t.normalDamage += t.additionalDamage;
			t.effectiveDamage += t.additionalEffectiveDamage;
			bool temp = FloatCalcForZero(t.fireRate, t.additionalfireRate);
			t.fireRate += (temp == true)? 0: t.additionalfireRate;
			t.range += t.rangeIncrease;
			showTowerStatus();
		}
	}

	public void sellTower(){
		Stats.moneyManip(sellPrice, true);
		Destroy(tower);
		closeWindow();
	}

	public void closeWindow(){
		tb.sell = false;
		tb.upgradeUI.SetActive(false);
	}	


}
