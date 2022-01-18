using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class set{
	public GameObject[] enemies;
}

public class WaveSpawner : MonoBehaviour {
	public float wfs = 0.5f; 
	public static int waveIndex;
	public static int AllWaves;
	public List<set> enemySet;

	void Start(){
		waveIndex = 0;
		AllWaves = enemySet.Count;
	}

	public void activateWave(){
		StartCoroutine("SpawnWave");
		waveIndex++;
	}

	IEnumerator SpawnWave(){
		GameObject[] temp = enemySet[waveIndex].enemies;
		Stats.nextRound();
		foreach(GameObject enemy in temp){
			if(enemy != null){
				SpawnEnemy(enemy);
			}
			yield return new WaitForSeconds(wfs);
		}
	}

	void SpawnEnemy(GameObject enemy){
		GameObject e = Instantiate(enemy, transform.position, Quaternion.identity);
		e.transform.parent = GameObject.Find("Monsters").transform;
	}
}
