using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaypointCreator : MonoBehaviour
{

    public Tilemap tilemap;
    public TileBase starterTileBase;
    public TileBase endTileBase;
    public TileBase pathTileBase;

    private TileBase startTile = null;
    private TileBase endTile = null;
    private Vector3Int startTilePos = new Vector3Int(0,0,0);
    private Vector3Int endTilePos = new Vector3Int(0,0,0);

    public List<GameObject> wayPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createWaypoints()
    {
        ResetWayPoints();
        Debug.Log("Creating waypoints...");

        Debug.Log("Checking requirements...");

        int checkX = tilemap.size.x;
        int checkY = tilemap.size.y;

        Vector3Int position = new Vector3Int(0, 0, 0);

        for(int i = -checkX; i < checkX; i++)
        {
            for(int j = -checkY; j < checkY; j++)
            {
                position.x = i;
                position.y = j;
                TileBase tile = tilemap.GetTile(position);
                if (tile != null)
                {
                    if (tile.name == starterTileBase.name)
                    {
                        if (startTile == null)
                        {
                            startTile = tile;
                            startTilePos = position;
                            Debug.Log("Start pos: " + startTilePos);
                        }
                        else
                        {
                            ResetWayPoints();
                            Debug.LogError("More than 1 starting tile found");
                            return;
                        }
                    }
                    if (tile.name == endTileBase.name)
                    {
                        if (endTile == null)
                        {
                            endTile = tile;
                            endTilePos = position;
                        }
                        else
                        {
                            ResetWayPoints();
                            Debug.LogError("More than 1 end tile found");
                            return;
                        }
                    }
                }
            }
        }
        Debug.Log("Finished checking requirements...");
        Debug.Log("Generate path...");
        currentPosition = startTilePos;
        GeneratePath();
        Debug.Log("Finished generating path...");


    }

    private Vector3Int currentPosition = new Vector3Int(0,0,0);
    private Vector3Int previousDirection = new Vector3Int(0,0,0);

    private void GeneratePath()
    {
        if(currentPosition != endTilePos)
        {
            if((tilemap.GetTile(currentPosition + previousDirection) == pathTileBase || tilemap.GetTile(currentPosition + previousDirection) == endTileBase) && previousDirection != new Vector3Int(0,0,0))
            {
            }
            else if(previousDirection != Vector3Int.down && (tilemap.GetTile(currentPosition + Vector3Int.up) == pathTileBase || tilemap.GetTile(currentPosition + Vector3Int.up) == endTileBase))
            {
                createWaypoint();
                previousDirection = Vector3Int.up;
            }
            else if (previousDirection != Vector3Int.left && (tilemap.GetTile(currentPosition + Vector3Int.right) == pathTileBase || tilemap.GetTile(currentPosition + Vector3Int.right) == endTileBase))
            {
                createWaypoint();
                previousDirection = Vector3Int.right;
            }
            else if (previousDirection != Vector3Int.up && (tilemap.GetTile(currentPosition + Vector3Int.down) == pathTileBase || tilemap.GetTile(currentPosition + Vector3Int.down) == endTileBase))
            {
                createWaypoint();
                previousDirection = Vector3Int.down;
            }
            else if (previousDirection != Vector3Int.right && (tilemap.GetTile(currentPosition + Vector3Int.left) == pathTileBase || tilemap.GetTile(currentPosition + Vector3Int.left) == endTileBase))
            {
                createWaypoint();
                previousDirection = Vector3Int.left;
            }
            else
            {
                Debug.LogError("Could not find path.");
                return;
            }
            currentPosition += previousDirection;
            GeneratePath();
        }
        else
        {
            createWaypoint();
        }
    }
    private void createWaypoint()
    {
        GameObject newPoint = new GameObject();
        newPoint.name = "Waypoint";
        Vector3 pos = tilemap.CellToWorld(currentPosition);
        newPoint.transform.position = pos;
        wayPoints.Add(newPoint);
    }

    private void destroyWayPoints()
    {
        foreach(GameObject point in wayPoints)
        {
            DestroyImmediate(point);
        }
        wayPoints.Clear();
    }

    private void ResetWayPoints()
    {
        startTile = null;
        endTile = null;
        destroyWayPoints();
    }

}
