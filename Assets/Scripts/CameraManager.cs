using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera main;
    [SerializeField] private CinemachineVirtualCamera top;
    [SerializeField] private GameManager gameManager;
    
    private bool canSwitch = true;
    private void Awake()
    {
        main.m_Priority = 1;
        top.m_Priority = 0;
    }

    public void OnSwitchCam(InputAction.CallbackContext ctx)
    {
        if (!canSwitch || !ctx.performed) return;
        if (top.m_Priority==0)
        {
            top.m_Priority = 2;
            gameManager.DisablePlayerMovement();
        }
        else
        {
            top.m_Priority = 0;
            gameManager.EnablePlayerMovement();
        }
        canSwitch = false;
        StartCoroutine(SwitchCamCooldown());
    }

    private IEnumerator SwitchCamCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canSwitch = true;
    }
}
