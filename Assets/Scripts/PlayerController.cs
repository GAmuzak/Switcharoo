using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float maxSpeed = 0f;
    [SerializeField] private Collider playerCollider;
    
    public bool canMove = true;
    
    private Vector3 movementInputCorrected = Vector3.zero;
    private Rigidbody rb = null;
    private Vector3 moveForce = Vector3.zero;
    private float isWalking = 0f;
    private float appliedMaxSpeed = 0f;
    private bool restrainVelocity = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        CounterMovement();
    }
    
    private void Movement()
    {
        moveForce = movementInputCorrected*moveSpeed;
        rb.AddForce(moveForce);
        appliedMaxSpeed = isWalking>0 ? maxSpeed / 2 : maxSpeed;
        if(restrainVelocity) rb.velocity = Vector3.ClampMagnitude(rb.velocity, appliedMaxSpeed);
    }

    private void CounterMovement()
    {
        bool noInput = Mathf.Approximately(Vector3.SqrMagnitude(movementInputCorrected), 0);
        bool oppositeInput = Vector3.Dot(rb.velocity, movementInputCorrected) <= 0;
        if (noInput)
        {
            Vector3 counterForce = rb.velocity * (-1f * 0.99f);
            rb.AddForce(counterForce);
        }
        if (oppositeInput)
        {
            rb.AddForce(moveForce*3f);
        }
    }
    
    private void Dash()
    {
        playerCollider.enabled = false;
        rb.useGravity = false;
        canMove = false;
        restrainVelocity = false;
        StartCoroutine(ReEnableColliderAndGravity());
    }

    private IEnumerator ReEnableColliderAndGravity()
    {
        yield return new WaitForSeconds(0.5f);
        playerCollider.enabled = true;
        rb.useGravity = true;
        canMove = true;
        restrainVelocity = true;
    }

    public void OnMovementInput(InputAction.CallbackContext ctx)
    {
        if (!canMove) return;
        Vector2 movementInput = ctx.ReadValue<Vector2>();
        Vector3 movementInputCasted = new Vector3(movementInput.x, 0, movementInput.y);
        movementInputCorrected = Quaternion.Euler(0, 45f, 0) * movementInputCasted;
    }

    public void OnWalk(InputAction.CallbackContext ctx)
    {
        isWalking = ctx.ReadValue<float>();
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        Dash();
    }

    
}
