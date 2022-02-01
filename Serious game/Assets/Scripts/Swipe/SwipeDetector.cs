using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minDistanceForSwipe = 20f;
    public Text test;
    public Text objectiveText;
    public GameObject button;
    public Light lampLight;

    private int count = 5;

    SwipeDirection swipeDirection;
    SwipeDirection nextSwipeDirection;
    int val = 0;
    void start()
    {
        nextSwipeDirection = SwipeDirection.Up;
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipeDirection();

                // If the swipe was up, turn the light on
                if(swipeDirection == SwipeDirection.Up)
                {
                    lampLight.gameObject.SetActive(true);
                }
                else if(swipeDirection == SwipeDirection.Down)
                {
                    // If the swipe was down, turn the light off
                    lampLight.gameObject.SetActive(false);
                }

                if (swipeDirection == nextSwipeDirection)
                {
                    // If the player swiped up after finishing the game
                    if(count == 0)
                    {
                        // Reset the game
                        count = 5;
                        objectiveText.text = "Turn the light on and off " + count + " times!";
                        button.SetActive(false);
                    }

                    val++;
                    if (val > 1)
                    {
                        val = 0;
                        count--;
                        objectiveText.text = "Turn the light on and off " + count + " times!";
                    }
                    test.text = "SWIPE " + Enum.GetName(typeof(SwipeDirection), val) + " !";
                    nextSwipeDirection = (SwipeDirection)val;
                }
            }
        }

        if(count == 0)
        {
            test.text = "STOP!";
            button.SetActive(true);
        }

        // DEBUG CODE: Shouldn't affect the mobile version though
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState.taskList[GameState.currentCollisionKey].onMinigameComplete();
        }
    }

    public void LoadMainScene()
    {
        // Execute the callback for this minigame being complete
        GameState.taskList[GameState.currentCollisionKey].onMinigameComplete();
        //SceneManager.LoadScene(sceneName: "Movement");
    }

    private void DetectSwipeDirection()
    {
        if (SwipeDistanceCheckMet())
        {
            if (IsVerticalSwipe())
            {
                swipeDirection = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;

            }
            else
            {
                swipeDirection = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;

            }
            fingerUpPosition = fingerDownPosition;

        }


    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }
}
    
public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}