using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private CameraManager cameraManager;
    
    private void Start()
    {
        // Cursor.visible = false;
        // Cursor.lockState=CursorLockMode.Locked;
    }

    public void DisablePlayerMovement()
    {
        playerController.canMove = false;
    }

    public void EnablePlayerMovement()
    {
        playerController.canMove = true;
    }
}
