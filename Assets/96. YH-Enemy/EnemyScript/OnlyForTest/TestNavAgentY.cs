using UnityEngine;
using UnityEngine.AI;

public class TestNavAgentY : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpDuration = 1f;

    private NavMeshAgent navMeshAgent;
    private bool isJumping;
    private float jumpTimer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        isJumping = false;
        jumpTimer = 0f;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            Jump();
        }

        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDuration)
            {
                FinishJump();
            }
        }
    }

    private void Jump()
    {
        int x = Random.Range(-5, 5);
        Vector3 r = new Vector3(x, 1, 0);
        Vector3 jumpForceVector = r * jumpForce;
        navMeshAgent.enabled = false; // 네비메시 에이전트 비활성화
        GetComponent<Rigidbody>().AddForce(jumpForceVector, ForceMode.VelocityChange);
        isJumping = true;
        jumpTimer = 0f;
    }

    private void FinishJump()
    {
        isJumping = false;
        navMeshAgent.enabled = true; // 네비메시 에이전트 다시 활성화
    }
}
