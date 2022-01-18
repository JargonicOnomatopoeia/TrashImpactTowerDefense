using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {
	public List<int> weaknessType;
	[HideInInspector] public float speed;
	//Slow Down Effect
	[HideInInspector] public float slowTime;
	[HideInInspector] public float slowPercent;
	[Header("MOnster Attribute")]
	public float origSpeed = 10f;
	public float health = 10;
	public Image healthBar;
	private float startHealth;
	public int moneyDrop = 5;
	private Transform targetWp;
	private int indexWp = 0;

	void Start(){
		targetWp = GameFloorSpawner.wp[indexWp].transform;
		speed = origSpeed;
		startHealth = health;
	}

	void Update(){
		Vector3 dir = targetWp.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

		if(Vector3.Distance(transform.position, targetWp.position ) <= 0.2f){
			nextWp();
		}

		slowDownOnEffect();

	}

	void nextWp(){

		if(indexWp != GameFloorSpawner.wp.Count-1){
			indexWp++;
			targetWp = GameFloorSpawner.wp[indexWp].transform;
		}else{
			Stats.subLives(1);
			Destroy(gameObject);
		}
	}

	public void TakeDamage(int value){
		health -= value;
		healthBar.fillAmount = health / startHealth;
		
		if(health <= 0){
			Stats.moneyManip(moneyDrop, true);
			Destroy(gameObject);
		}
	}

	public bool checkWeak(int type){
		bool check = false;
		int x;

		for(x=0; x!= weaknessType.Count && type != weaknessType[x]; x++){}

		if(x != weaknessType.Count){
			check = true;
		}

		return check;
	}

	public void slowDown(float sdt, float prcnt){
		slowTime = sdt;
		slowPercent = prcnt;
	}

// Effect
	void slowDownOnEffect(){
		if(slowTime > 0){
			speed =  origSpeed * ((100f - slowPercent)/100f);
			slowTime -= Time.deltaTime;
		}else{
			speed = origSpeed;
		}
	}
}
