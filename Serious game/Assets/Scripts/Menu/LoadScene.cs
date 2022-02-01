using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void Scene1()
    {
        GameState.afterSceneTransitionToMovement = () => { GameState.instance.InitializeGame(); };
        SceneManager.LoadScene("Movement");
    }
}
