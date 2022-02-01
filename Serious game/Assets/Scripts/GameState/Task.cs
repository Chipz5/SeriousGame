using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public string overworldTaskListString = "";
    public Action onColliderHit; // Function to be called when the named collider is hit
    public Action onMinigameComplete; // Function to be called at the end of the minigame
    public string minigameTaskString = "";
    public int minigameRepititions = 0;

    public Task(string i_overworldTaskListString, Action i_onColliderHit, Action i_onMinigameComplete, string i_minigameTaskString, int i_minigameRepititions)
    {
        overworldTaskListString = i_overworldTaskListString;
        onColliderHit = i_onColliderHit;
        onMinigameComplete = i_onMinigameComplete;
        minigameTaskString = i_minigameTaskString;
        minigameRepititions = i_minigameRepititions;
    }
}
