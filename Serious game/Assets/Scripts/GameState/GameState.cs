using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [Header("The state of the player in the Movement scene")]
    [Tooltip("The position of the player in the Movement scene")]
    public Vector3 playerPosition;
    [Tooltip("The rotation of the player in the Movement scene")]
    public Quaternion playerRotation;
    [Tooltip("The rotation of the camera in the Movement scene")]
    public Quaternion cameraRotation;

    [Header("The player and camera objects controlled by the joysticks")]
    [Tooltip("The player representing the main character (must have a Rigidbody attached)")]
    public Rigidbody player;
    [Tooltip("The camera representing the main character's view (should be the child of player)")]
    public Camera playerCamera;

    [Header("The various control objects needed to control camera, lights, and controls")]
    [Tooltip("The object holding the LightController for this scene")]
    public LightController lightController;
    [Tooltip("The object holding the ControlsController for this scene")]
    public ControlsController controlsController;
    [Tooltip("The object holding the CameraController for this scene")]
    public CameraController cameraController;

    [Header("Camera transforms needed for positioning the cameras in the minigames")]
    [Tooltip("The transform representing the position of the stove camera object")]
    public Vector3 stoveCameraTransformPosition;
    [Tooltip("The transform representing the rotation of the stove camera object")]
    public Quaternion stoveCameraTransformRotation;
    [Tooltip("The transform representing the position of the lamp camera object")]
    public Vector3 lampCameraTransformPosition;
    [Tooltip("The transform representing the rotation of the lamp camera object")]
    public Quaternion lampCameraTransformRotation;
    [Tooltip("The transform representing the position of the door camera object")]
    public Vector3 doorCameraTransformPosition;
    [Tooltip("The transform representing the rotation of the door camera object")]
    public Quaternion doorCameraTransformRotation;
    [Tooltip("The transform representing the position of the blocks camera object")]
    public Vector3 blocksCameraTransformPosition;
    [Tooltip("The transform representing the rotation of the blocks camera object")]
    public Quaternion blocksCameraTransformRotation;

    [Header("Animated objects")]
    [Tooltip("The object needed to play the door opening animation")]
    public Animator doorAnimator;

    public static string currentCollisionKey;
    

    private static bool firstInitializationComplete = false;
    public static Action afterSceneTransitionToMovement = null;
    public static Vector3 preTweenCameraPosition;
    public static Quaternion preTweenCameraRotation;

    // Create a singleton of this class, so that only one can ever exist
    private static GameState _instance;
    public static GameState instance
    {
        get
        {
            if(_instance == null)
            {
                // Create this object in the scene only once
                GameObject gameState = new GameObject("GameState");
                _instance = gameState.AddComponent<GameState>();
                // And make sure it is never destroyed
                DontDestroyOnLoad(gameState);
            }
            return _instance;
        }
    }

    // A dictionary storing the name of the gameobject that should trigger the corresponding task callbacks
    public static Dictionary<string, Task> taskList = new Dictionary<string, Task>();

    // Start is called before the first frame update
    void Start()
    {
        if(!firstInitializationComplete) // If this is the first time loading this object
        {
            firstInitializationComplete = true;
            InitializeGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a player and camera in this scene
        if(player != null && playerCamera != null)
        {
            // Store their positions and rotations
            playerPosition = player.transform.position;
            playerRotation = player.transform.rotation;
            cameraRotation = playerCamera.transform.rotation;
        }

        if(SceneManager.GetActiveScene().name == "Movement" && afterSceneTransitionToMovement != null)
        {
            afterSceneTransitionToMovement();
            afterSceneTransitionToMovement = null;
        }
    }

    public void InitializeGame()
    {
        if (lightController == null)
        {
            lightController = GameObject.Find("Lights")?.GetComponent<LightController>();
        }
        if (controlsController == null)
        {
            controlsController = GameObject.Find("Controls")?.GetComponent<ControlsController>();
        }
        if (cameraController == null)
        {
            cameraController = GameObject.Find("CameraTransforms")?.GetComponent<CameraController>();
        }
        if (doorAnimator == null)
        {
            doorAnimator = GameObject.Find("animatedDoor")?.GetComponent<Animator>();
        }

        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        playerCamera.transform.localPosition = new Vector3(0, 0.73f, 0);
        playerCamera.transform.rotation = Quaternion.identity;

        taskList.Add("Door", new Task("Leave your home.",
            () =>
            {
                    // When the player contacts the door trying to leave, add new tasks for them to complete
                    taskList.Remove("Door");
                taskList.Add("Stove", new Task("Check the stove.",
                    () =>
                    {
                        currentCollisionKey = "Stove";
                            // When the player contacts the stove, highlight the stove then launch the minigame
                            // Disable controls until it gets back to Movement
                            controlsController.gameObject.SetActive(false);
                        TweenToStove(() => {
                            SceneManager.LoadScene(sceneName: "TapAtSpecificPoint");
                        });
                    },
                    () =>
                    {
                        taskList.Remove("Stove");
                        IfAllMinigamesAreComplete();
                            // Trigger the tween code after scene load
                            afterSceneTransitionToMovement = () =>
                        {
                                // Disable controls
                                controlsController.gameObject.SetActive(false);
                            TweenFromStove(() =>
                            {
                                    // Enable controls
                                    controlsController.gameObject.SetActive(true);
                            });
                        };
                        SceneManager.LoadScene(sceneName: "Movement");
                    },
                    "Turn the stove on and off.",
                    5));
                taskList.Add("Lamp", new Task("Check the lamp.",
                    () =>
                    {
                        currentCollisionKey = "Lamp";
                            // When the player contacts the stove, launch the minigame
                            // Disable controls until it gets back to Movement
                            controlsController.gameObject.SetActive(false);
                        TweenToLamp(() =>
                        {
                            SceneManager.LoadScene(sceneName: "SwipeScene");
                        });
                    },
                    () =>
                    {
                        taskList.Remove("Lamp");

                        taskList.Add("Blocks", new Task("Count the blocks.",
                            () =>
                            {
                                currentCollisionKey = "Blocks";
                                    // When the player contacts the stove, launch the minigame
                                    // Disable controls until it gets back to Movement
                                    controlsController.gameObject.SetActive(false);
                                TweenToBlocks(() =>
                                {
                                    SceneManager.LoadScene(sceneName: "NumberScene");
                                });
                            },
                            () =>
                            {
                                taskList.Remove("Blocks");
                                IfAllMinigamesAreComplete();
                                    // Trigger the tween code after scene load
                                    afterSceneTransitionToMovement = () =>
                                {
                                        // Disable controls
                                        controlsController.gameObject.SetActive(false);
                                    TweenFromBlocks(() =>
                                    {
                                            // Enable controls
                                            controlsController.gameObject.SetActive(true);
                                    });
                                };
                                SceneManager.LoadScene(sceneName: "Movement");
                            },
                            "Count the blocks",
                            5));

                        IfAllMinigamesAreComplete();
                            // Trigger the tween code after scene load
                            afterSceneTransitionToMovement = () =>
                        {
                                // Disable controls
                                controlsController.gameObject.SetActive(false);
                            TweenFromLamp(() =>
                            {
                                    // Enable controls
                                    controlsController.gameObject.SetActive(true);
                            });
                        };
                        SceneManager.LoadScene(sceneName: "Movement");
                    },
                    "Turn the lamp on and off.",
                    5));
                    // TODO: Add other tasks to the list here
                },
            () =>
            {
                return; // There is no minigame to complete, so this is not needed
                },
            "",
            0));
    }

    private void IfAllMinigamesAreComplete()
    {
        if(taskList.Count == 0)
        {
            taskList.Add("Door", new Task("Leave your home.",
                         () =>
                         {
                             currentCollisionKey = "Door";
                             // When the player contacts the door, transition to the last minigame
                             // Disable controls until it gets back to Movement
                             controlsController.gameObject.SetActive(false);
                             TweenToDoor(() =>
                             {
                                SceneManager.LoadScene(sceneName: "TapScene");
                             });
                         },
                         () =>
                         {
                             taskList.Remove("Door");
                             // Trigger the tween code after scene load
                             afterSceneTransitionToMovement = () =>
                             {
                                 // Disable controls
                                 controlsController.gameObject.SetActive(false);
                                 TweenFromDoor(() =>
                                 {
                                     // Enable controls
                                     //controlsController.gameObject.SetActive(true);
                                     TweenToFinish(() =>
                                     {
                                         SceneManager.LoadScene(sceneName: "WinScene");
                                     });
                                 });
                             };
                             SceneManager.LoadScene(sceneName: "Movement");
                         },
                         "Lock and unlock the door.",
                         5));
        }
    }

    private void TweenToStove(Action action)
    {
        preTweenCameraPosition = playerCamera.transform.position;
        preTweenCameraRotation = playerCamera.transform.rotation;

        // Tween camera to look at stove
        StartCoroutine(cameraController.TweenToStove(() =>
        {
            // Trigger the lights to focus on the stove, then trigger action
            StartCoroutine(lightController.TweenToStove(action));
        }));
    }

    private void TweenFromStove(Action action)
    {
        // Turn off all lights but the stove lights
        lightController.SnapToStove();
        // Tween camera back to original player location
        StartCoroutine(cameraController.TweenFromStove(() =>
        {
            // Trigger the lights to stop focusing on the stove, then trigger action
            StartCoroutine(lightController.TweenFromStove(action));
        }));
    }

    private void TweenToLamp(Action action)
    {
        preTweenCameraPosition = playerCamera.transform.position;
        preTweenCameraRotation = playerCamera.transform.rotation;

        // Tween camera to look at stove
        StartCoroutine(cameraController.TweenToLamp(() =>
        {
            // Trigger the lights to focus on the stove, then trigger action
            StartCoroutine(lightController.TweenToLamp(action));
        }));
    }

    private void TweenFromLamp(Action action)
    {
        // Turn off all lights but the stove lights
        lightController.SnapToLamp();
        // Tween camera back to original player location
        StartCoroutine(cameraController.TweenFromLamp(() =>
        {
            // Trigger the lights to stop focusing on the stove, then trigger action
            StartCoroutine(lightController.TweenFromLamp(action));
        }));
    }

    private void TweenToDoor(Action action)
    {
        preTweenCameraPosition = playerCamera.transform.position;
        preTweenCameraRotation = playerCamera.transform.rotation;

        // Tween camera to look at stove
        StartCoroutine(cameraController.TweenToDoor(() =>
        {
            // Trigger the lights to focus on the stove, then trigger action
            StartCoroutine(lightController.TweenToDoor(action));
        }));
    }

    private void TweenFromDoor(Action action)
    {
        // Turn off all lights but the stove lights
        lightController.SnapToDoor();
        // Tween camera back to original player location
        StartCoroutine(cameraController.TweenFromDoor(() =>
        {
            // Trigger the lights to stop focusing on the stove, then trigger action
            StartCoroutine(lightController.TweenFromDoor(action));
        }));
    }

    private void TweenToBlocks(Action action)
    {
        preTweenCameraPosition = playerCamera.transform.position;
        preTweenCameraRotation = playerCamera.transform.rotation;

        // Tween camera to look at stove
        StartCoroutine(cameraController.TweenToBlocks(() =>
        {
            // Trigger the lights to focus on the stove, then trigger action
            StartCoroutine(lightController.TweenToBlocks(action));
        }));
    }

    private void TweenFromBlocks(Action action)
    {
        // Turn off all lights but the stove lights
        lightController.SnapToBlocks();
        // Tween camera back to original player location
        StartCoroutine(cameraController.TweenFromBlocks(() =>
        {
            // Trigger the lights to stop focusing on the stove, then trigger action
            StartCoroutine(lightController.TweenFromBlocks(action));
        }));
    }

    private void TweenToFinish(Action action)
    {
        StartCoroutine(cameraController.TweenToFinish(() =>
        {
            StartCoroutine(OpenDoor(action));
        }));
    }

    private IEnumerator OpenDoor(Action action)
    {
        if (doorAnimator == null)
        {
            doorAnimator = GameObject.Find("animatedDoor").GetComponent<Animator>();
        }

        doorAnimator.SetBool("OpenDoor", true);
        yield return new WaitForSeconds(3);
        action();
        yield return null;
    }
}
