using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour {

	public Coordinates Crd {get; set;}
	public Vector3 location {get;set;}
	public bool available;

	public void SetCoordinates(Coordinates crd, Vector3 tilePos){
		transform.position = tilePos;
		location = tilePos;
		Crd = crd;
	}
}
