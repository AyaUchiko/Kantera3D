using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの状態リスト
    public enum EPlayerState
    {
        Stand,
        Walk,      
        Dash,      
        Jump,      
        Dead,      
    }
    public EPlayerState currentState = EPlayerState.Stand;  

    //移動速度関連の変数
    public float moveSpeed;             
    public float walkSpeed;            

    //ダッシュ関連の変数
    public Dashing dash;
    public bool dashing;
    public float dashSpeed;        

    //ジャンプ関連の変数
    bool isGround;                    
    public float jumpForce;
    private int groundCount = 0;


    Rigidbody rb;
    public Vector2 moveInput;         
    private bool Right = true;
    private PushBox currentPushBox;
    private bool pushAction = false;
    private bool dead = false;
    private bool chargeAction = false;
    public bool ChargeAction => chargeAction;
    public float pushSpeed = 2f;

    public TextMeshProUGUI textState;
    PlayerAnimator playerAnimator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<PlayerAnimator>();

        dead = false;
        currentState = EPlayerState.Stand;
        isGround = true;
    }

    void FixedUpdate()
    {
        Move();

        if (currentPushBox != null && pushAction == true)
        {
            currentPushBox.MoveBox(moveInput.x, pushSpeed);
        }
    }
    void Update()
    {
        PlayerStateChange();
        SendAnimState();

        textState.text = "State:" + currentState.ToString();
    }

    void PlayerStateChange()
    {
        if (currentState == EPlayerState.Dead) return;

        if (!isGround)
        {
            currentState = EPlayerState.Jump;
            return;
        }

        if (dashing)
        {
            currentState = EPlayerState.Dash;
            return;
        }

        if (moveInput.x != 0)
        {
            currentState = EPlayerState.Walk;
            if (pushAction == true)
            {
                moveSpeed = pushSpeed;
            }
            else
            {
                moveSpeed = walkSpeed;
            }
            return;
        }

        currentState = EPlayerState.Stand;
    }

    public IEnumerator DeadProcess()
    {
        if (dead) yield break;
        dead = true;
        currentState = EPlayerState.Dead;
        rb.linearVelocity = Vector3.zero;

        playerAnimator.ChangeState(PlayerAnimator.EPlayerAnimState.PlayerDead);

        if (FadeManager.Instance != null)
        {
            yield return StartCoroutine(FadeManager.Instance.FadeOut());
        }
        else
        {
            yield return new WaitForSeconds(2.0f);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Move()
    {
        if (dashing) return;
        rb.linearVelocity = new Vector3(moveInput.x * moveSpeed, rb.linearVelocity.y, 0f);
    }

    private void Flip()
    {
        //反転処理
        Right = !Right;     //trueの場合falseが入る、falseの場合 trueが入る

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        gameObject.transform.localScale = localScale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (pushAction == false)
        {
            if (moveInput.x < 0 && Right)
            {
                Flip();
            }
            else if (moveInput.x > 0 && !Right)
            {
                Flip();
            }
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            dash.Dash();
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started&&isGround)
        {
            isGround = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }    
    }
    public void OnPush(InputAction.CallbackContext context)
    {
        if (currentPushBox == null)
        {
            pushAction = false;
            return;
        }

        if (context.started)
        {
            pushAction = true;
            currentPushBox.SetPush(true);
        }

        if (context.canceled)
        {
            pushAction = false;
            currentPushBox.SetPush(false);
        }
    }
    public void OnCharge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            chargeAction = true;
        }
        else if (context.canceled)
        {
            chargeAction = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            currentPushBox = other.GetComponent<PushBox>();
        }

        if (other.CompareTag("EnemyAttack") && !dead)
        {
            StartCoroutine(DeadProcess());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            currentPushBox = null;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            groundCount++;
            isGround = true;
        }

        if (col.gameObject.CompareTag("Enemy") && !dead)
        {
            StartCoroutine(DeadProcess());
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            groundCount--;
            if (groundCount <= 0)
            {
                isGround = false;
                groundCount = 0;
            }
        }
    }
    void SendAnimState()
    {
        switch (currentState)
        {
            case EPlayerState.Stand:
                playerAnimator.ChangeState(PlayerAnimator.EPlayerAnimState.PlayerIdle);
                break;
            case EPlayerState.Walk:
                playerAnimator.ChangeState(PlayerAnimator.EPlayerAnimState.PlayerWalk);
                break;
            case EPlayerState.Dash:
                playerAnimator.ChangeState(PlayerAnimator.EPlayerAnimState.PlayerDash);
                break;
            case EPlayerState.Jump:
                playerAnimator.ChangeState(PlayerAnimator.EPlayerAnimState.PlayerJump);
                break;
            case EPlayerState.Dead:
                playerAnimator.ChangeState(PlayerAnimator.EPlayerAnimState.PlayerDead);
                break;
        }
    }
}
