using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    [Header("Settings for controlling the player and camera")]
    [Tooltip("The speed at which the player will move")]
    public float moveSpeed;
    [Tooltip("The speed at which the camera will rotate up and down")]
    public float lookSpeedVertical;
    [Tooltip("The speed at which the camera/character will rotate left and right")]
    public float lookSpeedHorizontal;
    [Tooltip("How far down the camera can look")]
    public float minRotationX;
    [Tooltip("How far up the camera can look")]
    public float maxRotationX;
    [Tooltip("Whether the vertical camera controls are inverted")]
    public bool invertCameraVertical;
    [Tooltip("Whether the horizontal camera controls are inverted")]
    public bool invertCameraHorizontal;

    [Header("Camera snapping options")]
    [Tooltip("Whether or not the camera will snap back to the center after looking up or down")]
    public bool cameraSnapsBackToCenter;
    [Tooltip("How much the camera will move up or down when moving the right stick (if cameraSnapsBackToCenter is true)")]
    public float cameraTiltFactor;

    [Header("Objects needed for controlling the player and camera")]
    [Tooltip("The joystick that will control movement")]
    public FixedJoystick fixedJoystickMovement;
    [Tooltip("The joystick that will control the camera")]
    public FixedJoystick fixedJoystickCamera;
    [Tooltip("The object representing the character (must have a Rigidbody attached)")]
    public Rigidbody playerRB;
    [Tooltip("The camera representing the player's view (should be a child of the player)")]
    public Camera playerCamera;


    private float rotationX;
    private float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        rotationX = 0;
        rotationY = 0;


    }

    // Update is called once per frame
    void Update()
    {
        // Get the Vector3 direction indicated by the movement joystick
        Vector3 direction = playerRB.transform.forward * fixedJoystickMovement.Vertical +
                            playerRB.transform.right * fixedJoystickMovement.Horizontal;
        // Add the force to the player to make it move
        playerRB.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);

        // If we want the camera to snap back to the center when you let go of the right joystick
        if (cameraSnapsBackToCenter)
        {
            // If we want up the camera to look up when we drag down
            if (invertCameraVertical)
            {
                // Calculate how much to rotate the camera around the X axis (up and down)
                rotationX = fixedJoystickCamera.Vertical * lookSpeedVertical * cameraTiltFactor;
            }
            else
            {
                rotationX = -fixedJoystickCamera.Vertical * lookSpeedVertical * cameraTiltFactor;
            }
        }
        else
        {
            if (invertCameraVertical)
            {
                rotationX += fixedJoystickCamera.Vertical * lookSpeedVertical * Time.deltaTime;
            }
            else
            {
                rotationX += -fixedJoystickCamera.Vertical * lookSpeedVertical * Time.deltaTime;
            }

        }
        // Calculate how much to rotate the player around the Y axis (left and right)
        rotationY = fixedJoystickCamera.Horizontal * lookSpeedHorizontal * Time.deltaTime;
        // Make sure the camera never looks too high up or down
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        // Rotate the camera up and down
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        // Spin the player around left and right, to look left and right
        if(invertCameraHorizontal)
        {
            playerRB.AddTorque(transform.up * -rotationY);
        }
        else
        {
            playerRB.AddTorque(transform.up * rotationY);
        }
    }
}

