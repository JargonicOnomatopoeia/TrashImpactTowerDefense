using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour {
	public static bool noMonsters;
	public static bool gameEnd;
	public GameObject story;
	public GameObject gameOverUI;
	public GameObject victoryUI;
	public GameObject gameUI;
	public GameObject nextWaveButton;
	
	void Start(){
		gameEnd = true;
		InvokeRepeating("checkMonsters", 0f, 0.5f);
		Story();
	}
	// Update is called once per frame
	void Update () {
		if(gameEnd != false){
			return;
		}else{
			if(Stats.Lives <= 0){
				gameOver();
			}else if (Stats.Lives > 0 && noMonsters == true){
				if(WaveSpawner.waveIndex >= WaveSpawner.AllWaves){
					victory();
				}else{
					nextWaveButton.SetActive(true);
				}
			}
		}
	}

	void gameOver(){
		EndGame();
		gameOverUI.SetActive(true);
	}

	void victory(){
		EndGame();
		victoryUI.SetActive(true);
	}
	void EndGame(){
		PauseGame();
		gameUI.SetActive(false);
		TurretBuildManager.tbm.upgradeUI.SetActive(false);
	}

	void Story(){
		Time.timeScale = 0f;
		story.SetActive(true);
		nextWaveButton.SetActive(true);
	}

	void PauseGame(){
		gameEnd = true;
		Time.timeScale = 0f;	
	}

	void checkMonsters(){
		GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");

		noMonsters = (monsters.Length == 0)?true:false;
	}
}
