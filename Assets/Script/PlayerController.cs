using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの状態リスト
    public enum EPlayerState
    {
        Stand,     //立ち
        Walk,      //歩き
        Dash,      //走り
        Jump,      //ジャンプ
        Desu,      //死亡
    }
    public EPlayerState currentState = EPlayerState.Stand;  //最初は立ち

    //移動速度関連の変数
    public float moveSpeed;             //プレイヤーの移動速度
    public float walkSpeed;             //プレイヤーの歩く速度

    //ダッシュ関連の変数
    public Dashing dash;
    public bool dashing;
    public float dashSpeed;             //プレイヤーのダッシュ速度

    //ジャンプ関連の変数
    bool isGround;                      //地面に接地しているかどうか
    public float jumpForce;             //プレイヤーのジャンプ力


    Rigidbody rb;
    private Vector2 moveInput;          //移動のための入力値(Vector2)を保持する変数
    private bool Right = true;          //最初は右向き

    public TextMeshProUGUI textState;   //Stateを表示するテキスト
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        isGround = false;

        textState.text = "State:";
    }
    void Update()
    {
        //移動処理を実行する
        Move();

        //現在のStateを表示する
        PlayerStateChange();
        textState.text = "State:" + currentState.ToString();

        PlayerAction();

        //アニメーション処理
        anim.SetBool("isWalk", moveInput.x != 0);
        anim.SetBool("isDash", dashing);
    }

    void PlayerStateChange()
    {
        if (dashing)
        {
            currentState = EPlayerState.Dash;
        }
        else if (moveInput.x != 0)
        {
            currentState = EPlayerState.Walk;
        }
        else if(!isGround)
        {
            currentState = EPlayerState.Jump;
        }
        else
        {
            currentState = EPlayerState.Stand;
        }
    }

    void PlayerAction()
    {
        if (currentState == EPlayerState.Stand)
        {
            PlayerStand();
        }
        if (currentState == EPlayerState.Walk)
        {
            PlayerWalk();
        }
        if (currentState == EPlayerState.Dash)
        {
            PlayerDush();
        }
        if (currentState == EPlayerState.Jump)
        {
            PlayerJump();
        }
    }

    void PlayerStand()
    {
        anim.Play("PlayerIdle");
    }
    void PlayerWalk()
    {
        moveSpeed = walkSpeed;
        anim.Play("PlayerWalk");
    }

    void PlayerDush()
    {
        anim.Play("PlayerDush");
    }
    void PlayerJump()
    {
        anim.Play("PlayerJump");
    }

    private void Move()
    {
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
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
        PlayerWalk();
        moveInput = context.ReadValue<Vector2>();

        //入力値のｘが０ではないときだけ向きをチェック
        if (moveInput.x < 0 && Right)
        {
            Flip();
        }
        else if (moveInput.x > 0 && !Right)
        {
            Flip();
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
        if(isGround)
        {
            isGround = false;
            rb.AddForce(transform.up * jumpForce);
        }
    }
}
