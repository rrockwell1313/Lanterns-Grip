using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomLayoutController : MonoBehaviour
{
    /// <summary>
    /// The purpose of this script is to generate room layouts to help create some rooms for our dungeons to be used later in our random layout generator
    /// 1. Determine the type of room needed
    /// 2. make a room based on the type needed
    /// 3. prefab base size is 10 compared to 1 for any other 3d object
    /// </summary>
    /// 

    //Room types needed, for now startRoom, endRoom, interestRoom, itemRoom, puzzleRoom, hallwayRoom, hiddenRoom
    public GameObject prefab;
    public int roomDepth, roomHeight, roomWidth;
    private int origRoomDepth, origRoomHeight, origRoomWidth;
    private GameObject room;
    private Vector3 position = new Vector3(0, 0, 0);
    private Vector3 rotation = new Vector3(0, 0, 0);
    private Vector3 scale = new Vector3(1, 1, 1);
    private List<string> prefabNamesList = new List<string>();
    private List<Vector3> prefabPositions = new List<Vector3>();
    private List<Vector3> prefabScales = new List<Vector3>();
    private List<Vector3> prefabRotations = new List<Vector3>();
    private List<GameObject> prefabsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateRoom();
        GenerateLists();
        CreatePrefabs();
    }
    // Update is called once per frame
    void Update()
    {
        if (roomWidth != origRoomWidth || roomDepth != origRoomDepth || roomHeight != origRoomHeight)
        {
            RescaleRoom();
        }
    }

    void CreateRoom()
    {
        Debug.Log("Creating Room");
        room = new GameObject(); //creates the gameobject that will hold the room
        room.transform.position = new Vector3(0,0,0); //creates room at 0
        room.name = "Room";  //names room
    }

    void GenerateLists()
    {
        Debug.Log("Creating Lists");
        origRoomWidth = roomWidth;
        origRoomDepth = roomDepth;
        origRoomHeight = roomHeight;

        //create a list to use for naming the planes
        prefabNamesList = new List<string>
        {
            "floor",
            "northWall",
            "southWall",
            "eastWall",
            "westWall",
            "ceiling"
        };
        Debug.Log(prefabNamesList.Count);

        prefabPositions = new List<Vector3> //positions should be based on the size of the room
        {
            new Vector3(0, 0, 0), //floor
            new Vector3(0, roomHeight, roomDepth), //northWall
            new Vector3(0, roomHeight, -roomDepth), //southWall
            new Vector3(roomWidth, roomHeight, 0), //eastWall
            new Vector3(-roomWidth, roomHeight, 0), //westWall
            new Vector3(0, roomHeight * 2, 0) //ceiling
        };
        prefabScales = new List<Vector3>
        {
            new Vector3(roomWidth, 1, roomDepth), //floor
            new Vector3(roomWidth, 1, roomHeight), //northWall
            new Vector3(roomWidth, 1, roomHeight), //southWall
            new Vector3(roomDepth, 1, roomHeight), //eastWall
            new Vector3(roomDepth, 1, roomHeight), //westWall
            new Vector3(roomWidth, 1, roomDepth) //ceiling
        };

        prefabRotations = new List<Vector3>
        {
            new Vector3(0, 0, 0), //floor
            new Vector3(-90, 0, 0), //northWall
            new Vector3(-90, 180, 0), //southWall
            new Vector3(-90, 0, 90), //eastWall
            new Vector3(-90, 180, 90), //westWall
            new Vector3(0, 0, 180) //ceiling
        };
    }

    void CreatePrefabs()
    {
        Debug.Log("Creating Planes");
        //create walls, floor, and ceiling, that will typically be 6 walls for a box, adding in hallways and turns and things can come later
        int count = 0;
        foreach (string prefabName in prefabNamesList)
        {
            position = prefabPositions[count];
            rotation = prefabRotations[count];
            scale = prefabScales[count];
            GameObject currentPrefab = Instantiate(prefab, position, Quaternion.Euler(rotation), room.transform); //creates the prefab, sets its position, rotation, and makes it a child of room
            prefabsList.Add(currentPrefab);
            currentPrefab.name = prefabName;
            currentPrefab.transform.localScale = scale;
            count++;
        }
    }

    void RescaleRoom()
    {
        //rescale
        prefabPositions = new List<Vector3>();
        prefabRotations = new List<Vector3>();
        prefabScales = new List<Vector3>();
        GenerateLists();

        int count = 0;
        foreach (GameObject currentPrefab in prefabsList)
        {
            position = prefabPositions[count];
            rotation = prefabRotations[count];
            scale = prefabScales[count];
            currentPrefab.transform.position = position;
            currentPrefab.transform.eulerAngles = rotation;
            currentPrefab.transform.localScale = scale;
            count++;
        }
    }
}
