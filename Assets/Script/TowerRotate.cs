using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotate : MonoBehaviour {


	[HideInInspector] public Transform target;
	[HideInInspector] public int price;
	public int MaxUpgrade = 3;
	[HideInInspector] public int currentUpgrade = 0;
	[Header ("Tower Attributes")]
	public int type;
	public float range = 15f;
	public float fireRate = 1f;
	public int normalDamage = 2;
	public int effectiveDamage = 5;
	private float fireCD = 0f;

	[Header("Upgrades Attr")]
	public int upgradeCost = 5;
	public int priceIncrease = 10;
	public int additionalDamage = 5;
	public int additionalEffectiveDamage = 5;
	public float additionalfireRate = -5f;
	public float rangeIncrease = 5f;
	[Header ("Unity Setup")]
	public GameObject RotationPoint;
	public Color WireSphereColor = Color.green;
	public string enemyTag = "Enemy";
	[Header ("Bullet Setup")]
	public GameObject bullet;
	public Transform firePoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shrtDistance = Mathf.Infinity;
		GameObject nearEn = null; 

		foreach(GameObject enemy in enemies){
			float dist2Enemy = Vector3.Distance(transform.position, enemy.transform.position);
			if(dist2Enemy < shrtDistance){
				shrtDistance = dist2Enemy;
				nearEn = enemy;
			}
		}

		if(nearEn != null && shrtDistance <= range){
			target = nearEn.transform;
		}else{
			target = null;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null){
			return;
		}

		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = lookRotation.eulerAngles;
		RotationPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotation.z);
		

		if(fireCD <= 0f){
			Shoot();
			fireCD = 1f / fireRate;
		}

		fireCD -= Time.deltaTime;
	}

	void Shoot(){
		GameObject tempbull = (GameObject)Instantiate(bullet, firePoint.position, firePoint.transform.rotation);
		Bullet bul = tempbull.GetComponent<Bullet>();

		if(bul != null){
			bul.Seek(this);
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = WireSphereColor;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
