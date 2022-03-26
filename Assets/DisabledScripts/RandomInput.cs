using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RandomInput : MonoBehaviour
{
    public Tilemap worldTilemap;
    Movement movementScript;
    public bool move;

    private float latestDirectionChangeTime;
    private float directionChangeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = gameObject.GetComponent<Movement>();
        worldTilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();

        directionChangeTime += Random.Range(-5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            GetRandomPos();
        }
    }

    private void GetRandomPos()
    {
        Vector3 movePos = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y + Random.Range(-5, 5));
        Vector3Int cellCoords = worldTilemap.WorldToCell(movePos);
        Vector3 newPos = worldTilemap.GetCellCenterWorld(cellCoords);
        movementScript.Move(newPos);
        latestDirectionChangeTime = Time.time;
    }
}
