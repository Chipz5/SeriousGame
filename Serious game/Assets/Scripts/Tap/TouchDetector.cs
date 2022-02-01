using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using System.Collections.Specialized;

public class TouchDetector : MonoBehaviour
{
    public Text test;
    public Text objectiveText;
    int count = 5;
    public GameObject button;
    void Awake()
    {
        test.text = "Tap on the screen 5 times exactly!";
        button = GameObject.Find("Button");
        button.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if( count == 6)
            {
                test.text = "You tapped too many times.... START AGAIN!!!!";
                count--;
                objectiveText.text = "Lock and unlock the door " + count + " times!";
            }
            else if (count == 0)
            {
                test.text = "DO NOT TAP ANYMORE!!!!";
                objectiveText.text = "Lock and unlock the door " + count + " times!";
                count = 6;
                button.SetActive(true);

            }
            else
            {
                button.SetActive(false);
                test.text = "Tap " + count + " more times!";
                objectiveText.text = "Lock and unlock the door " + count + " times!";
                count--;
            }
            //print("Tap detected...");


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




}
