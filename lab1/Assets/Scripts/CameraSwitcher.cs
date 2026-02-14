using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private int currentCameraIndex = 0;

    [SerializeField] private List<GameObject> cameras; 
    private List<Camera> cameraComponents;

    private void Start()
    {
        cameraComponents = new List<Camera>();
        foreach (var go in cameras)
            cameraComponents.Add(go.GetComponent<Camera>());

        if (cameras.Count == 0) return;

        foreach (var cam in cameras)
            cam.gameObject.SetActive(false);

        cameras[0].gameObject.SetActive(true);
        currentCameraIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            cameras[currentCameraIndex].gameObject.SetActive(false);

            currentCameraIndex++;
            if (currentCameraIndex >= cameras.Count)
                currentCameraIndex = 0;

            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}

