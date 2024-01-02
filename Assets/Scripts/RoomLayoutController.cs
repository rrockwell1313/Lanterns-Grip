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
    /// 3. plane base size is 10 compared to 1 for any other 3d object
    /// </summary>
    /// 

    //Room types needed, for now startRoom, endRoom, interestRoom, itemRoom, puzzleRoom, hallwayRoom, hiddenRoom
    public GameObject plane;
    public GameObject test;
    public float roomDepth, roomHeight, roomWidth, planarScaleFactor, tileScaleFactor;
    private float origRoomDepth, origRoomHeight, origRoomWidth;
    private GameObject room;
    private Vector3 position = new Vector3(0, 0, 0);
    private Vector3 rotation = new Vector3(0, 0, 0);
    private Vector3 scale = new Vector3(1, 1, 1);
    private List<string> planeNamesList = new List<string>();
    private List<Vector3> planePositions = new List<Vector3>();
    private List<Vector3> planeScales = new List<Vector3>();
    private List<Vector3> planeRotations = new List<Vector3>();
    private List<GameObject> planesList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        CreateRoom();
        GenerateLists();
        CreatePlanes();
        //GenerateWalls();
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
        //storing original variables


        //Generate a room out of planes, a simple box
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
        planeNamesList = new List<string>
        {
            "floor",
            "northWall",
            "southWall",
            "eastWall",
            "westWall",
            "ceiling"
        };
        Debug.Log(planeNamesList.Count);

        planePositions = new List<Vector3> //positions should be based on the size of the room
        {
            new Vector3(0, 0, 0), //floor
            new Vector3(0, roomHeight, roomDepth), //northWall
            new Vector3(0, roomHeight, -roomDepth), //southWall
            new Vector3(roomWidth, roomHeight, 0), //eastWall
            new Vector3(-roomWidth, roomHeight, 0), //westWall
            new Vector3(0, roomHeight * 2, 0) //ceiling
        };
        planeScales = new List<Vector3>
        {
            new Vector3(roomWidth, 1, roomDepth), //floor
            new Vector3(roomWidth, 1, roomHeight), //northWall
            new Vector3(roomWidth, 1, roomHeight), //southWall
            new Vector3(roomDepth, 1, roomHeight), //eastWall
            new Vector3(roomDepth, 1, roomHeight), //westWall
            new Vector3(roomWidth, 1, roomDepth) //ceiling
        };

        planeRotations = new List<Vector3>
        {
            new Vector3(0, 0, 0), //floor
            new Vector3(-90, 0, 0), //northWall
            new Vector3(-90, 180, 0), //southWall
            new Vector3(-90, 0, 90), //eastWall
            new Vector3(-90, 180, 90), //westWall
            new Vector3(0, 0, 180) //ceiling
        };
    }

    void CreatePlanes()
    {
        Debug.Log("Creating Planes");
        //create walls, floor, and ceiling, that will typically be 6 walls for a box, adding in hallways and turns and things can come later
        int count = 0;
        foreach (string planeName in planeNamesList)
        {
            position = planePositions[count] * planarScaleFactor;
            rotation = planeRotations[count];
            scale = planeScales[count];
            GameObject currentPlane = Instantiate(plane, position, Quaternion.Euler(rotation), room.transform); //creates the plane, sets its position, rotation, and makes it a child of room
            planesList.Add(currentPlane);
            currentPlane.name = planeName;
            currentPlane.transform.localScale = scale;
            count++;
        }
    }

    void RescaleRoom()
    {
        //rescale
        planePositions = new List<Vector3>();
        planeRotations = new List<Vector3>();
        planeScales = new List<Vector3>();
        GenerateLists();

        int count = 0;
        foreach (GameObject currentPlane in planesList)
        {
            position = planePositions[count] * planarScaleFactor;
            rotation = planeRotations[count];
            scale = planeScales[count];
            currentPlane.transform.position = position;
            currentPlane.transform.eulerAngles = rotation;
            currentPlane.transform.localScale = scale;
            count++;
        }
    }

    //void GenerateWalls() // Each wall tile is 5 to 1 of a normal 3D object
    //{
    //    int count = 0;
    //    foreach (GameObject currentPlane in planesList)
    //    {
    //        string wallName = currentPlane.name;

    //        if (wallName == "floor" || wallName == "ceiling") // Skip if not a wall
    //        {
    //            count++;
    //            continue;
    //        }

    //        // The pivot of the planes is the center, the pivot of the prefabs/tiles is the bottom right corner
    //        float planeHalfWidth = currentPlane.transform.localScale.x * 5; // 10 units width
    //        float planeHalfHeight = currentPlane.transform.localScale.z * 5; // 10 units depth

    //        // Calculate upper left corner of the plane
    //        Vector3 upperLeftCorner = currentPlane.transform.position + currentPlane.transform.right * (-planeHalfWidth) + currentPlane.transform.forward * planeHalfHeight;


    //        //upperleftcorner + offsets will be tiling

    //        Quaternion rotation = Quaternion.LookRotation(-currentPlane.transform.up, Vector3.up); // up equals the correct direction, as planes visible face

    //        GameObject wall = Instantiate(test, upperLeftCorner, rotation);

    //        Debug.Log(wall.GetComponent<Renderer>().bounds.size.x);


            //// Zero of prefab is set to zero of plane
            //wall.transform.rotation = rotation; // Faces the plane's direction
            //wall.transform.SetParent(currentPlane.transform); // Assigns the child to the parent object
            //wall.transform.localPosition = Vector3.zero; // Sets the 0 of the object (bottom right) to the 0 of the plane (center)

            //// Adjust wall position - Align its pivot (bottom right) with the plane's upper left
            //float wallWidth = wall.GetComponent<Renderer>().bounds.size.x; // Assuming this is the width of the wall prefab
            //float wallHeight = wall.GetComponent<Renderer>().bounds.size.y; // Assuming this is the depth of the wall prefab

            //// Adjust position to move the pivot of the wall to the upper left of the plane
            //Vector3 adjustedPosition = upperLeftCorner +
            //                           wall.transform.right * wallWidth +
            //                           wall.transform.forward * (-wallHeight);

            //adjustedPosition.x = Mathf.RoundToInt(adjustedPosition.x);
            //adjustedPosition.y = Mathf.RoundToInt(adjustedPosition.y);
            //adjustedPosition.z = Mathf.RoundToInt(adjustedPosition.z);

            //wall.transform.position = adjustedPosition;

            ////adjust the local position to allign all tiles
            //adjustedPosition = wall.transform.localPosition;
            //adjustedPosition.y = 0f;
            //wall.transform.localPosition = adjustedPosition;

            //adjustedPosition = wall.transform.position;

    //        count++;

    //        //I want to now generate a wall of tiles from the starting tile. 
    //    }
    //}

}
