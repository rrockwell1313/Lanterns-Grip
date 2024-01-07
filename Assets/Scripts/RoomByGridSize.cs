using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomByGridSize : MonoBehaviour
{
    private GameObject wallTilePrefab, floorTilePrefab;
    public LevelGenerator levelGenerator;
    [HideInInspector] public GameObject room;
    public int roomWidth, roomHeight, roomDepth, doorways;
    private float calcRoomWidth, calcRoomHeight, calcRoomDepth;
    public float prefabWidth, prefabHeight;
    public List<GameObject> wallPrefabList;
    public List<GameObject> floorPrefabList;

    private void Start()
    {
        if (!levelGenerator.isActiveAndEnabled)
        {
            CreateRoom();
        }
    }
    public void CreateRoom()
    {
        calcRoomDepth = roomDepth;
        calcRoomHeight = roomHeight;
        calcRoomWidth = roomWidth;
        room = new GameObject("Room"); //create a new game object called Room
        ChooseRandomPrefab();
        GenerateWalls();
        DefineNewZero();
    }
    void ChooseRandomPrefab()
    {
        //choose a random prefab from a prebuilt list of prefabs
        wallTilePrefab = wallPrefabList[Random.Range(0, wallPrefabList.Count)];
        floorTilePrefab = floorPrefabList[Random.Range(0, floorPrefabList.Count)];

    }
    void GenerateWalls()
    {
        //each wall will have its own orientation, its own offset
        NorthWall();
        SouthWall();
        EastWall();
        WestWall();
        Floor();
    }

    void NorthWall()
    {
        float xOffset = 0;
        float yOffset = 0;

        for (int x = 0; x < calcRoomWidth; x++)
        {
            for (int y = 0; y < calcRoomHeight; y++)
            {
                Vector3 position = new Vector3((x * prefabWidth) + xOffset, (y * prefabHeight) + yOffset, calcRoomDepth / 2 * prefabWidth);
                GameObject currentPrefab = Instantiate(wallTilePrefab, position, Quaternion.Euler(0, 180, 0), room.transform); //spawns the prefab facing inward
                currentPrefab.name = $"Prefab_NorthWall{x}_{y}";
                wallTilePrefab = wallPrefabList[Random.Range(0, wallPrefabList.Count)];
            }
        }
    }

    void SouthWall()
    {
        //for south wall, no rotation needed, xoffset is 1 width because pivot is bottom right
        float xOffset = prefabWidth;
        float yOffset = 0;
        for (int x = 0; x < calcRoomWidth; x++)
        {
            for (int y = 0; y < calcRoomHeight; y++)
            {
                Vector3 position = new Vector3((x * prefabWidth) + xOffset, (y * prefabHeight) + yOffset, -calcRoomDepth / 2 * prefabWidth); //room depth to the rear is negative
                GameObject currentPrefab = Instantiate(wallTilePrefab, position, Quaternion.Euler(0, 0, 0), room.transform); //spawns the prefab facing inward
                currentPrefab.name = $"Prefab_SouthWall{x}_{y}";
                wallTilePrefab = wallPrefabList[Random.Range(0, wallPrefabList.Count)];
            }
        }
    }

    void EastWall()
    {
        //for east wall, rotation of -90 needed on the y, and the z is equal to the width instead of the x
        float xOffset = -((calcRoomDepth * prefabWidth) / 2) + prefabWidth;
        float yOffset = 0;
        for (int x = 0; x < calcRoomDepth; x++)
        {
            for (int y = 0; y < calcRoomHeight; y++)
            {
                Vector3 position = new Vector3(calcRoomWidth * prefabWidth, (y * prefabHeight) + yOffset, (x * prefabWidth) + xOffset); //room depth to the rear is negative
                GameObject currentPrefab = Instantiate(wallTilePrefab, position, Quaternion.Euler(0, -90, 0), room.transform); //spawns the prefab facing inward
                currentPrefab.name = $"Prefab_EastWall{x}_{y}";
                wallTilePrefab = wallPrefabList[Random.Range(0, wallPrefabList.Count)];
            }
        }
    }

    void WestWall()
    {
        //for west wall, rotation of +90 on the y axis, z is same
        float xOffset = -((calcRoomDepth * prefabWidth) / 2) + prefabWidth;
        float yOffset = 0;
        for (int x = 0; x < calcRoomDepth; x++)
        {
            for (int y = 0; y < calcRoomHeight; y++)
            {
                Vector3 position = new Vector3(0, (y * prefabHeight) + yOffset, -((x * prefabWidth) + xOffset)); //room depth to the rear is negative
                GameObject currentPrefab = Instantiate(wallTilePrefab, position, Quaternion.Euler(0, 90, 0), room.transform); //spawns the prefab facing inward
                currentPrefab.name = $"Prefab_WestWall{x}_{y}";
                wallTilePrefab = wallPrefabList[Random.Range(0, wallPrefabList.Count)];
            }
        }
    }
    void Floor()
    {
        float xOffset = 0;
        float yOffset = (calcRoomDepth / 2) * prefabHeight;

        for (int x = 0; x < calcRoomWidth; x++)
        {
            for (int y = 0; y < calcRoomDepth; y++)
            {
                Vector3 position = new Vector3((x * prefabWidth) + xOffset, 0, (y * -prefabHeight) + yOffset);
                GameObject currentPrefab = Instantiate(floorTilePrefab, position, Quaternion.Euler(0, 180, 0), room.transform); //spawns the prefab facing inward
                currentPrefab.name = $"Prefab_Floor{x}_{y}";
                floorTilePrefab = floorPrefabList[Random.Range(0, floorPrefabList.Count)];
            }
        }
    }

    void DefineNewZero()
    {
        //should define the new 0,0,0 of the whole room from the center to a pivot corner for grid use
        // Move each child by the adjustment amount
        foreach (Transform child in room.transform)
        {
            Vector3 bottomLeft = new Vector3(0, 0, -roomDepth * prefabHeight / 2);
            Vector3 adjustment = room.transform.position - bottomLeft;
            child.position += adjustment;
        }
    }

    void AdjustRoomSize()
    {
        //for now, just rebuild the whole room by destroying and rebuilding. this is clunky, will adjust to a list and dictionary based method that only adds the next or removes the previous layers
        Destroy(room);
        if (!levelGenerator.isActiveAndEnabled)
        {
            CreateRoom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (calcRoomDepth != roomDepth || calcRoomHeight != roomHeight || calcRoomWidth != roomWidth)
        {
            //one of the dimensions has changed in the inspector
            if (!levelGenerator.isActiveAndEnabled)
            {
                AdjustRoomSize();
            }
        }
    }
}
