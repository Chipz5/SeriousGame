using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [Tooltip("The time it takes (in seconds) to transition to or from a specific spotlight")]
    public float tweenTime;

    [Tooltip("The transform representing the rotation and position of the stove camera object")]
    public Transform stove;
    [Tooltip("The transform representing the rotation and position of the lamp camera object")]
    public Transform lamp;
    [Tooltip("The transform representing the rotation and position of the door camera object")]
    public Transform door;
    [Tooltip("The transform representing the rotation and position of the blocks camera object")]
    public Transform blocks;
    [Tooltip("The transform representing the rotation and position of the finish camera object")]
    public Transform finish;

    // Start is called before the first frame update
    void Start()
    {
        GameState gameState = GameState.instance;
        gameState.cameraController = gameObject.GetComponent<CameraController>();
        gameState.stoveCameraTransformPosition = stove.position;
        gameState.stoveCameraTransformRotation = stove.rotation;
        gameState.lampCameraTransformPosition = lamp.position;
        gameState.lampCameraTransformRotation = lamp.rotation;
        gameState.doorCameraTransformPosition = door.position;
        gameState.doorCameraTransformRotation = door.rotation;
        gameState.blocksCameraTransformPosition = blocks.position;
        gameState.blocksCameraTransformRotation = blocks.rotation;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator TweenToStove(Action onComplete)
    {
        float t = 0.0f;

        Vector3 fromPosition = GameState.instance.playerCamera.transform.position;
        Quaternion fromRotation = GameState.instance.playerCamera.transform.rotation;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(fromPosition, stove.position, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(fromRotation, stove.rotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = stove.position;
        GameState.instance.playerCamera.transform.rotation = stove.rotation;

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromStove(Action onComplete)
    {
        float t = 0.0f;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(stove.position, GameState.preTweenCameraPosition, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(stove.rotation, GameState.preTweenCameraRotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = GameState.preTweenCameraPosition;
        GameState.instance.playerCamera.transform.rotation = GameState.preTweenCameraRotation;

        // The preTweenCameraPosition is off for some reason, so set the localPosition so it doesn't end up off
        GameState.instance.playerCamera.transform.localPosition = new Vector3(0, 0.73f, 0);

        onComplete();
        yield return null;
    }

    public IEnumerator TweenToLamp(Action onComplete)
    {
        float t = 0.0f;

        Vector3 fromPosition = GameState.instance.playerCamera.transform.position;
        Quaternion fromRotation = GameState.instance.playerCamera.transform.rotation;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(fromPosition, lamp.position, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(fromRotation, lamp.rotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = lamp.position;
        GameState.instance.playerCamera.transform.rotation = lamp.rotation;

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromLamp(Action onComplete)
    {
        float t = 0.0f;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(lamp.position, GameState.preTweenCameraPosition, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(lamp.rotation, GameState.preTweenCameraRotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = GameState.preTweenCameraPosition;
        GameState.instance.playerCamera.transform.rotation = GameState.preTweenCameraRotation;

        // The preTweenCameraPosition is off for some reason, so set the localPosition so it doesn't end up off
        GameState.instance.playerCamera.transform.localPosition = new Vector3(0, 0.73f, 0);

        onComplete();
        yield return null;
    }

    public IEnumerator TweenToDoor(Action onComplete)
    {
        float t = 0.0f;

        Vector3 fromPosition = GameState.instance.playerCamera.transform.position;
        Quaternion fromRotation = GameState.instance.playerCamera.transform.rotation;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(fromPosition, door.position, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(fromRotation, door.rotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = door.position;
        GameState.instance.playerCamera.transform.rotation = door.rotation;

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromDoor(Action onComplete)
    {
        float t = 0.0f;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(door.position, GameState.preTweenCameraPosition, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(door.rotation, GameState.preTweenCameraRotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = GameState.preTweenCameraPosition;
        GameState.instance.playerCamera.transform.rotation = GameState.preTweenCameraRotation;

        // The preTweenCameraPosition is off for some reason, so set the localPosition so it doesn't end up off
        GameState.instance.playerCamera.transform.localPosition = new Vector3(0, 0.73f, 0);

        onComplete();
        yield return null;
    }

    public IEnumerator TweenToBlocks(Action onComplete)
    {
        float t = 0.0f;

        Vector3 fromPosition = GameState.instance.playerCamera.transform.position;
        Quaternion fromRotation = GameState.instance.playerCamera.transform.rotation;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(fromPosition, blocks.position, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(fromRotation, blocks.rotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = blocks.position;
        GameState.instance.playerCamera.transform.rotation = blocks.rotation;

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromBlocks(Action onComplete)
    {
        float t = 0.0f;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(blocks.position, GameState.preTweenCameraPosition, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(blocks.rotation, GameState.preTweenCameraRotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = GameState.preTweenCameraPosition;
        GameState.instance.playerCamera.transform.rotation = GameState.preTweenCameraRotation;

        // The preTweenCameraPosition is off for some reason, so set the localPosition so it doesn't end up off
        GameState.instance.playerCamera.transform.localPosition = new Vector3(0, 0.73f, 0);

        onComplete();
        yield return null;
    }

    public IEnumerator TweenToFinish(Action onComplete)
    {
        float t = 0.0f;

        Vector3 fromPosition = GameState.instance.playerCamera.transform.position;
        Quaternion fromRotation = GameState.instance.playerCamera.transform.rotation;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            GameState.instance.playerCamera.transform.position = Vector3.Lerp(fromPosition, finish.position, t);
            GameState.instance.playerCamera.transform.rotation = Quaternion.Lerp(fromRotation, finish.rotation, t);
            yield return null;
        }

        GameState.instance.playerCamera.transform.position = finish.position;
        GameState.instance.playerCamera.transform.rotation = finish.rotation;

        onComplete();
        yield return null;
    }

}
