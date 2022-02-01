using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NumberSequence : MonoBehaviour
{
    public GameObject num;
    public GameObject[] nums;

    public string[] names ;
    public Vector2[] positions;
    private int index = 0;
    public GameObject button;
    void Awake()
    {
        List<int> listNumbers = new List<int>();
        int randomVal = 0;
        names = new string[9] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        positions = new Vector2[9] { new Vector2(0,0), new Vector2(100, 0), new Vector2(0, 100), new Vector2(100, 100),
                                    new Vector2(-100, 100) , new Vector2(-100, 0), new Vector2(100, -100), new Vector2(0, -100),
                                    new Vector2(-100, -100)};
        index = 0;
        for (int i = 0; i < 9; i++)
        {
            button = GameObject.Find(names[i]);
            button.GetComponentInChildren<Text>().text = " ";
            randomVal = Random.Range(0, 9);
            do
            {
               randomVal = Random.Range(0, 9);
            } while (listNumbers.Contains(randomVal));
            listNumbers.Add(randomVal);
            button.transform.position = button.transform.TransformPoint(positions[randomVal]);
        }

    }

    private void Update()
    {
        // DEBUG CODE: Shouldn't affect the mobile version though
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameState.taskList[GameState.currentCollisionKey].onMinigameComplete();
        }
    }

    public void checkCorrectSequence()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        bool correctSeq = false;
        print(index);
        if(names[index] == buttonName)
        {
            correctSeq = true;
        } else
        {
            print("reloading scene ....");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        index++;
        if(index > 8)
        {
            if (correctSeq)
            {
                GameState.taskList[GameState.currentCollisionKey].onMinigameComplete();
            }
            index = 0;
        }
    }
}
 