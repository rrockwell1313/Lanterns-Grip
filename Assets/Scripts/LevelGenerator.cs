using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelGenerator : MonoBehaviour
{
    private RoomByGridSize roomByGridSize;
    private int roomWidth, roomHeight, roomDepth, count;
    public int roomCount, worldScale, heightVariance, widthVariance, depthVariance;
    public bool usePrefabs;
    public List<GameObject> roomPrefabsList;
    [HideInInspector] public List<GameObject> roomList = new List<GameObject>();
    GameObject roomPrefab;
    [HideInInspector] public Dictionary<string, int> roomValuesMap = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {

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

    }




    void GenerateRooms()
    {
        roomWidth = Random.Range(2, widthVariance + 1);
        roomHeight = Random.Range(2, heightVariance + 1);
        roomDepth = Random.Range(2, depthVariance + 1);
        roomByGridSize.roomDepth = roomDepth;
        roomByGridSize.roomWidth = roomWidth;
        roomByGridSize.roomHeight = roomHeight;
        roomByGridSize.CreateRoom();
        roomPrefab = roomByGridSize.room;
        roomPrefab.transform.parent = this.transform;
        roomList.Add(roomPrefab);
        NameRooms();

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
        roomPrefab.name = $"Room{count}_{roomWidth}_{roomDepth}_{roomHeight}";

        roomValuesMap.Add(roomPrefab.name + "width", roomWidth);
        roomValuesMap.Add(roomPrefab.name + "depth", roomDepth);
        roomValuesMap.Add(roomPrefab.name + "height", roomHeight);

        count++;
    }


}
