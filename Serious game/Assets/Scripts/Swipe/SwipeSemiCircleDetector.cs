using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SwipeSemiCircleDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minDistanceForSwipe = 60f;
    private List<Vector2> touchPositions = new List<Vector2>();
    int val = 1;
    void start()
    {
    }

    private void Update()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Moved)
            {
                touchPositions.Add(touch.position);
            }
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                if (isCircularMotion())
                {
                    print("Semi circle detected...");
                    SceneManager.LoadScene(sceneName: "Movement");
                }
                
                touchPositions = new List<Vector2>();
                
            }
        }
    }

    private bool isCircularMotion()
    {
        int count = touchPositions.Count;
        Vector2 mid = touchPositions[count / 2];

        if (fingerDownPosition.y > fingerUpPosition.y - 30 && fingerDownPosition.y < fingerUpPosition.y + 30 && fingerDownPosition.x > fingerUpPosition.x)
        {
            if (mid.y > fingerUpPosition.y && mid.y > fingerDownPosition.y && mid.x > fingerUpPosition.x && mid.y < fingerDownPosition.x && mid.y > minDistanceForSwipe)
                return true;
        }
        
        return false;
    }

}