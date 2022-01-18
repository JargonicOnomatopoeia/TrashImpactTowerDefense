using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour {

	public float speed;
	private float zoom = 25;
	public bool borderEnabler;

	// Update is called once per frame
	void Update () {
		if(GameMan.gameEnd == true){
			return;
		}

		ScreenDrag();
		orthographicChange();
	}

	void ScreenDrag(){
		Vector3 pos = transform.position;

		if(!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0)){
			pos.x -= (Input.GetAxis("Mouse X") * speed * Time.deltaTime);
			pos.y -= (Input.GetAxis("Mouse Y") * speed * Time.deltaTime);
		}

		if(Input.GetKey("a")){
			pos.x--;
		}
		if(Input.GetKey("d")){
			pos.x++;
		}
		if(Input.GetKey("w")){
			pos.y++;
		}
		if(Input.GetKey("s")){
			pos.y--;
		}

		transform.position = pos;
	}

	void orthographicChange(){
		if(Camera.main.orthographic){
			if(Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetKeyDown(KeyCode.Equals)){
				if(zoom < 100){
					zoom++;
					speed++;
				}
				
			}
			if(Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetKeyDown(KeyCode.Minus)){
				if(zoom > 1){
					zoom--;
					speed--;
				}
			}
			GetComponent<Camera>().orthographicSize = zoom;
		}
	}

}
