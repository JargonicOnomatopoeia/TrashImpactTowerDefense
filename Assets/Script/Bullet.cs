using UnityEngine;

public class Bullet : MonoBehaviour {

	private Transform target;
	private EnemyMovement e;
	[Header("Normal Bullet Attributes")]
	private int damage;
	public float speed = 70f;
	[Header ("Slowdown Effect")]
	public bool slowBullet = false;
	public float slowTime = 5f;
	public float slowPrcnt = 50f;
	public void Seek(TowerRotate tower){
		bool check ;

		target = tower.target;
		e = target.GetComponent<EnemyMovement>();
		check = e.checkWeak(tower.type);
		if(check == true){
			damage = tower.effectiveDamage;
		}else{
			damage = tower.normalDamage;
		}
	}


	
	// Update is called once per frame
	void Update () {
		if(target != null){
			Vector3 dir = target.position - transform.position;
			float disThisFrame = speed * Time.deltaTime;
			if(dir.magnitude <= disThisFrame){
				EnemyMovement e = target.GetComponent<EnemyMovement>();
				e.TakeDamage(damage);
				if(slowBullet == true){
					e.slowDown(slowTime, slowPrcnt);
				}
				Destroy(gameObject);
			}else{
				transform.Translate(dir.normalized * disThisFrame, Space.World);
			}
		}else{
			Destroy(gameObject);
		}
	}

	/*
	GARBAGE
	void HitTarget(){
		Damage();
		Destroy(gameObject);
		
	}
	void Damage(){
		Destroy(target.gameObject);
	}
	*/
}
