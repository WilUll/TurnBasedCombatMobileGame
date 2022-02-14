using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerInput : MonoBehaviour
{
    public Tilemap worldTilemap;
    Movement movementScript;
    PlayerInfo playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerInfo>();
        movementScript = gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickedPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellCoords = worldTilemap.WorldToCell(clickedPos);
            Vector3 cursorPos = worldTilemap.GetCellCenterWorld(cellCoords);
            movementScript.Move(cursorPos);
        }
    }
}
