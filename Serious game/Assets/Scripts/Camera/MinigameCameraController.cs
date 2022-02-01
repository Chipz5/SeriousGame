using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameState gameState = GameState.instance;
        
        if(GameState.currentCollisionKey == "Stove")
        {
            transform.position = gameState.stoveCameraTransformPosition;
            transform.rotation = gameState.stoveCameraTransformRotation;
        }
        else if (GameState.currentCollisionKey == "Lamp")
        {
            transform.position = gameState.lampCameraTransformPosition;
            transform.rotation = gameState.lampCameraTransformRotation;
        }
        else if (GameState.currentCollisionKey == "Door")
        {
            transform.position = gameState.doorCameraTransformPosition;
            transform.rotation = gameState.doorCameraTransformRotation;
        }
        else if (GameState.currentCollisionKey == "Blocks")
        {
            transform.position = gameState.blocksCameraTransformPosition;
            transform.rotation = gameState.blocksCameraTransformRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
