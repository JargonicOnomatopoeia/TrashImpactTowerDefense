using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryAndNextWave : MonoBehaviour {
	public GameObject Story;
	public GameObject nextWaveBut;

	public void closeStory(){
		Time.timeScale = 1f;
		GameMan.gameEnd = false;
		Story.SetActive(false);
	}

	public void waveStart(){
		nextWaveBut.SetActive(false);
		GameObject[] spawner = GameObject.FindGameObjectsWithTag("Spawner");
		GameMan.noMonsters = false;
		foreach(GameObject tempSp in spawner){
			tempSp.GetComponent<WaveSpawner>().activateWave();
		}
	}
}
