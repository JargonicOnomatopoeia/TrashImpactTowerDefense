using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Text rounds;

	void OnEnable(){
		rounds.text = Stats.Rounds.ToString();
	}

	public void Retry(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu(){
		Debug.Log("Pretty Good");
	}
}
