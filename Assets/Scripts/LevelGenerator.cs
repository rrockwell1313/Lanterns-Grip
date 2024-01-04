using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    private RoomByGridSize roomByGridSize;
    public int roomCount, worldScale, heightVariance, widthVariance, depthVariance;
    private Plane worldBase;
    public bool usePrefabs;
    public List<GameObject> roomPrefabsList;
    private List<GameObject> roomList = new List<GameObject>();
    GameObject roomPrefab;
    Vector3 worldUpperLeftZero;

    // Start is called before the first frame update
    void Start()
    {
        CreateBase();

        if (usePrefabs)
        {
            for (int i = 0; i < roomCount; i++)
            {
                LoadInRooms();
            }
        }
        else
        {
            roomByGridSize = GetComponent<RoomByGridSize>();
            for (int i = 0; i < roomCount; i++)
            {
                GenerateRooms();
            }
        }

        NameRooms();
        PlaceRooms();
    }
    void CreateBase()
    {
        //create the base of the world
        GameObject worldBase = GameObject.CreatePrimitive(PrimitiveType.Plane);
        worldBase.transform.localScale *= worldScale;
        worldBase.transform.parent = this.transform;

        float x, y, z;
        x = worldBase.transform.localPosition.x - (10 * worldScale / 2); //plane scale is 10. always.
        z = worldBase.transform.localPosition.z - (10 * worldScale / 2);
        y = 0;
        worldUpperLeftZero = new Vector3(x,y,z); //UPPERLEFTZERO SHOULD BE USED TO LINE UP ROOMS INSIDE THE PLANE
    }

    void GenerateRooms()
    {
        roomByGridSize.roomDepth = Random.Range(1, depthVariance + 1);
        roomByGridSize.roomWidth = Random.Range(1, widthVariance + 1);
        roomByGridSize.roomHeight = Random.Range(1, heightVariance +1);
        roomByGridSize.CreateRoom();
        roomPrefab = roomByGridSize.room;
        roomPrefab.transform.parent = this.transform;
        roomList.Add(roomPrefab);
    }

    void LoadInRooms()
    {
        roomPrefab = roomPrefabsList[Random.Range(0, roomPrefabsList.Count)];
        Vector3 position = new Vector3(0,0,0);
        GameObject newRoomPrefab = Instantiate(roomPrefab, position, Quaternion.Euler(0, 0, 0), gameObject.transform);
        roomList.Add(newRoomPrefab);
    }

    void NameRooms()
    {
        int count = 0;
        foreach (GameObject currentRoom in roomList)
        {
            currentRoom.name = $"Room_{count}";
            count++;
        }
    }

    void PlaceRooms()
    {
        roomList[0].transform.position = worldUpperLeftZero;
        //Debug.Log("Placing " + roomList[0].name + " in UpperLeft");
    }
}
