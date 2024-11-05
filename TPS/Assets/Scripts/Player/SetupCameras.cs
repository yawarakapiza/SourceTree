using UnityEngine;

public class SetupCameras : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask foregroundLayer;

    void Start()
    {
        // Create a new camera for foreground objects
        GameObject foregroundCameraObj = new GameObject("BulletCamera");
        Camera foregroundCamera = foregroundCameraObj.AddComponent<Camera>();

        // Configure the foreground camera
        foregroundCamera.CopyFrom(mainCamera);
        foregroundCamera.depth = mainCamera.depth + 1; // Ensure it renders after the main camera
        foregroundCamera.cullingMask = foregroundLayer;

        // Set the main camera to exclude the foreground layer
        mainCamera.cullingMask &= ~foregroundLayer;
    }
}
