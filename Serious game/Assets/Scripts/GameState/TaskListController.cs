using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskListController : MonoBehaviour
{
    public Text taskListText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        taskListText.text = "";
        foreach (var taskPair in GameState.taskList)
        {
            taskListText.text += taskPair.Value.overworldTaskListString + "\n";
        }

        if(GameState.taskList.Count == 0)
        {
            // TODO: Open the door here?
        }
    }
}
