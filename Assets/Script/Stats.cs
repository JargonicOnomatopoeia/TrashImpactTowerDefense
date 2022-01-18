using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	public static int Rounds;
	public static int Money;
	public int startingMoney = 500;
	public static int Lives;
	public int startingLives = 1;
	private Text liveString;
	void Start(){

		Rounds = 0;
		Lives = 0;
		Money = 0;

		moneyManip(startingMoney, true);
		addLives(startingLives);
	}

	public static void moneyManip(int value, bool check){
		Text priceString = (Text) GameObject.Find("Gold Counter/Counter").GetComponent<Text>();
		if(check == true){
			Money += value;
		}else{
			Money -= value;
		}
		priceString.text = Money.ToString();
	}

	public static void subLives(int value){
		Text liveString = (Text) GameObject.Find("Lives Counter/Counter").GetComponent<Text>();
		Lives -= value;

		liveString.text = Lives.ToString();
	}

	public static void addLives(int value){
		Text liveString = (Text) GameObject.Find("Lives Counter/Counter").GetComponent<Text>();
		Lives += value;

		liveString.text = Lives.ToString();		
	}

	public static void nextRound(){
		Text roundString = (Text) GameObject.Find("Round Counter/Counter").GetComponent<Text>();
		Rounds++;

		roundString.text = Rounds.ToString();		
	}

}
