using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    private RoomByGridSize roomByGridSize;

    private int worldScale;
    private float prefabHeight, prefabWidth;
    private List<GameObject> roomList = new List<GameObject>();

    Vector3 worldLowerLeftZero, initialVector;
    bool placeRooms;
    GameObject worldBase;
    Dictionary<string, GameObject> roomMap = new Dictionary<string, GameObject>();
    Dictionary<string, int> roomValuesMap = new Dictionary<string, int>();
    List<string> roomMapKeys = new List<string>();

    int nameX, nameY;

    public bool recreateLayoutSwitch;
    private bool switchPreviousValue;


    // Start is called before the first frame update
    void Start()
    {
        levelGenerator = GetComponent<LevelGenerator>();
        roomByGridSize = GetComponent<RoomByGridSize>();
        worldScale = levelGenerator.worldScale;
        prefabHeight = roomByGridSize.prefabHeight;
        prefabWidth = roomByGridSize.prefabWidth;
        roomValuesMap = levelGenerator.roomValuesMap;
        switchPreviousValue = recreateLayoutSwitch;

        CreateBase();
        CreateGrid();


    }
    void ResetLayout()
    {
        worldScale = levelGenerator.worldScale;
        prefabHeight = roomByGridSize.prefabHeight;
        prefabWidth = roomByGridSize.prefabWidth;
        roomValuesMap = levelGenerator.roomValuesMap;
        switchPreviousValue = recreateLayoutSwitch;
        ResetRooms();
        placeRooms = false;


        Destroy(worldBase);
        CreateBase();

        roomMap.Clear();
        roomMapKeys.Clear();

        CreateGrid();
    }

    void ResetRooms()
    {
        foreach (GameObject room in levelGenerator.roomList)
        {
            Destroy(room); //destroy all rooms
        }

        levelGenerator.roomList.Clear(); //clear list for reuse
        levelGenerator.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelGenerator.roomList.Count == levelGenerator.roomCount &! placeRooms) 
        {
            if (worldBase != null)
            {
                if(worldLowerLeftZero != Vector3.zero)
                {
                    placeRooms = true;
                    PlaceRooms();
                }
            }
;
        }

        if (recreateLayoutSwitch != switchPreviousValue)
        {
            recreateLayoutSwitch = switchPreviousValue;
            ResetLayout();
        }

    }
    void CreateBase()
    {
        //create the base of the world
        worldBase = GameObject.CreatePrimitive(PrimitiveType.Plane);
        worldBase.transform.localScale *= worldScale;
        worldBase.transform.parent = this.transform;

        float x, y, z;
        x = worldBase.transform.localPosition.x - (10 * worldScale / 2); //plane scale is 10. always.
        z = worldBase.transform.localPosition.z - (10 * worldScale / 2);
        y = 0;
        worldLowerLeftZero = new Vector3(x, y, z); //UPPERLEFTZERO SHOULD BE USED TO LINE UP ROOMS INSIDE THE PLANE

        initialVector = Vector3.zero;
        Debug.Log(worldLowerLeftZero);

    }
    void CreateGrid()
    {
        for (int x = 0; x < (10 / prefabWidth * worldScale); x++) //10 is the plane scale, divided by single tilePrefabWidth and height
        {
            for (int y = 0; y < (10 / prefabHeight * worldScale); y++)
            {
                roomMap.Add($"{x},{y}", null);
                roomMapKeys.Add($"{x},{y}"); //also add them to the list
            }
        }

    }
    void PlaceRooms()
    {
        roomList = levelGenerator.roomList;
        int worldX = 0;
        int worldZ = 0;
        string nextCoordKey;
        string[] split;
        int failCount = 0;

        //Vector3 roomLowerLeftZero = new Vector3(0, 0, 0);
        Vector3 newZeroPosition = Vector3.zero;

        for (int currentRoomNumber = 0; currentRoomNumber < roomList.Count;)
        {
            GameObject room = roomList[currentRoomNumber];
            room.transform.SetParent(worldBase.transform, true); //makes the plane the parent, so it can all be moved
            room.transform.position = newZeroPosition + worldLowerLeftZero; //subtract lowerleftzero from any position to align properly to the grid
            bool checkFailed = false;

            //check values before placing
            for (int x = 0; x < (roomValuesMap[room.name + "width"]); x++)
            {
                for (int y = 0; y < (roomValuesMap[room.name + "depth"]); y++)
                {
                    string keyToRemove = $"{x + worldX},{y + worldZ}";
                    if (!roomMapKeys.Contains(keyToRemove))
                    {
                        checkFailed = true;
                        roomMapKeys.Remove(keyToRemove); //remove the key from the list as well
                        failCount++;
                        break;
                    }
                }
            }

            if (!checkFailed)
            {
                //this commits the placement, needs a 'is this okay to place' check first
                for (int x = 0; x < (roomValuesMap[room.name + "width"]); x++)
                {
                    for (int y = 0; y < (roomValuesMap[room.name + "depth"]); y++)
                    {
                        string keyToRemove = $"{x + worldX},{y + worldZ}";
                        roomMap[keyToRemove] = room;
                        roomMapKeys.Remove(keyToRemove); //remove the key from the list as well
                    }
                }
                currentRoomNumber++;
            }

            //pick a random start spot from the available remaining start spots
            nextCoordKey = roomMapKeys[Random.Range(0, roomMapKeys.Count)];
            split = nextCoordKey.Split(',');
            worldX = int.Parse(split[0]);
            worldZ = int.Parse(split[1]);
            newZeroPosition = new Vector3(worldX * prefabWidth, 0, worldZ * prefabHeight);
            
            if (failCount > 9500)
            {
                Debug.Log("FAILED OUT MAH BOY");
                return;
            }
        }
    }
}
