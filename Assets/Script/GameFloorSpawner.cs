using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameFloorSpawner : MonoBehaviour
{
	public float StartEndwaypointSpawnZ = 0.5f;
    public GameObject[] tiles;
    public GameObject[] waypoint;
    public string textMap = "test"; 
    public string wayPoint = "waypoints";
    private int rowNumber;
    private int columnNumber;
    public Dictionary<Coordinates,TileProperties> mapLocation{get; set;}
    public static List<GameObject> wp{get;set;}
	public GameObject Destroyer;
	public GameObject Spawner;
	
	public Vector3 IdentifySides{
		get{
			Vector3 imageSize = tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size;

			return new Vector3(imageSize.x/2f,3f,0.1f);
		}
	}

    // Use this for initialization//
    void Awake()
    {
    	Coordinates[] corners;
    	string[] mapData = ConvertTextToMap();
		TileMapSpawn(mapData);
		corners = fourCornerFound(mapData);
		cameraChangePosToMid(corners);
		StartEndwaypointSpawn();
    }

    void TileMapSpawn(string[] mapData)
    {

    	mapLocation = new Dictionary<Coordinates, TileProperties>();
		Vector3 pos = new Vector3(0,0,0);

		for (int y=0; y < mapData.Length ;y++){
            
        	pos.x = y*IdentifySides.x;
        	pos.z = -IdentifySides.z*y;
        	pos.y = -IdentifySides.y*y;

            string[] mapRow = mapData[y].Split(',');
            for (int x=0; x < mapRow.Length ;x++){
            	placeTile(x, y, pos, mapRow[x]);

				pos.y += IdentifySides.y;
				pos.x += IdentifySides.x;
				pos.z += IdentifySides.z;
            }
        }
    }

    void placeTile(int x, int y,Vector3 pos,string tileNumber){
    	TileProperties tileClone;
        int number;
    	if(Int32.TryParse(tileNumber,out number)){
	    	tileClone = Instantiate(tiles[number], pos, Quaternion.identity).GetComponent<TileProperties>();
            tileClone.SetCoordinates(new Coordinates(x, y), pos);
            mapLocation.Add(new Coordinates(x, y), tileClone);
            tileClone.transform.parent = transform;
        }
    }

    void cameraChangePosToMid(Coordinates[] corners){
    	Vector3 middle = new Vector3(0,0,-10);
    	
		middle.x = mapLocation[corners[0]].transform.position.x;
        middle.x = (middle.x + mapLocation[corners[1]].transform.position.x)/2;
		middle.y = mapLocation[corners[2]].transform.position.y;
		middle.y = (middle.y + mapLocation[corners[3]].transform.position.y)/2;

		Camera.main.transform.position = middle;
    }

    private string[] ConvertTextToMap(){
    	TextAsset text = Resources.Load(textMap) as TextAsset;

    	string field = text.text.Replace(Environment.NewLine, string.Empty);

    	return field.Split('-');
    }

    private Coordinates[] fourCornerFound(string[] map){
    	int y;
    	int longRow = 0;
    	int columnLongRow = 0;
		Coordinates[] corners = new Coordinates[4];
    	
    	for(y=map.Length-1;y > -1;y--){
    		if(longRow < map[y].Split(',').Length){
    			longRow = map[y].Split(',').Length;
    			columnLongRow = y;
    		}
    	}
    	corners[0] = new Coordinates(0,0);
    	corners[1] = new Coordinates(longRow-1,columnLongRow);
    	corners[2] = new Coordinates(0,map.Length-1);
    	corners[3] = new Coordinates(longRow-1,columnLongRow);

    	return corners;
    }

//-------------------------------------------------------------------------------------//
//									WAYPOINTS
//-------------------------------------------------------------------------------------//

    private string[] fileWaypointLocations(){
    	TextAsset data = Resources.Load("waypoints") as TextAsset;

    	string locations = data.text.Replace(Environment.NewLine, string.Empty);

    	return locations.Split('-');
    }
    Coordinates[] convertStringWaypointsToCoordinates(string[] coords){
    	
    	Coordinates[] waypoints = new Coordinates[coords.Length];
        int num1, num2;
    	int z = 0;
    	for(int y = 0; y < coords.Length;y++){
    		string[] coord = coords[y].Split(',');
            Int32.TryParse(coord[1], out num2);
            Int32.TryParse(coord[0], out num1);
            waypoints[z] = new Coordinates(num1,num2);
    		z++;
    	}

    	return waypoints;
    }


    void StartEndwaypointSpawn(){
    	int x;
    	string[] waypointData = fileWaypointLocations();
    	Coordinates[] waypoints = convertStringWaypointsToCoordinates(waypointData);
    	wp = new List<GameObject>();

		//Spawner
		Vector3 spawnerLoc = mapLocation[waypoints[0]].transform.position;
		spawnerLoc.z -= StartEndwaypointSpawnZ;
		GameObject s = Instantiate(Spawner,spawnerLoc,Quaternion.identity);

    	for(x = 1; x < waypoints.Length;x++){
    			Vector3 location = mapLocation[waypoints[x]].transform.position;
    			location.z -= StartEndwaypointSpawnZ;
    			GameObject waypointClone = Instantiate(waypoint[0],location,Quaternion.identity);
    			wp.Add(waypointClone);
    			waypointClone.transform.parent = GameObject.Find("Waypoints").transform;
    		}  
		//Destroyer
		Vector3 destroyerLoc = mapLocation[waypoints[x-1]].transform.position;
		destroyerLoc.z -= StartEndwaypointSpawnZ;
		GameObject d = Instantiate(Destroyer,destroyerLoc,Quaternion.identity);
		
		//Placement
		s.transform.parent = GameObject.Find("Spawner(s)&Destroyer(s)").transform;
		d.transform.parent = GameObject.Find("Spawner(s)&Destroyer(s)").transform;
    }
}