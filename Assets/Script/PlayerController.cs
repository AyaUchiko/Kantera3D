using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //プレイヤーの行動リスト
    public enum EPlayerState
    {
        Stand,     //立ち
        Walk,      //歩き
        Run,       //走り
    }
    public EPlayerState currentState = EPlayerState.Stand;  //最初は立ち

    public float moveSpeed;             //プレイヤーの移動速度
    public float walkSpeed;             //プレイヤーの歩く速度
    public float runSpeed;              //プレイヤーの走る速度
    private Vector2 moveInput;          //移動のための入力値(Vector2)を保持する変数
    private bool Right = true;          //最初は右向き
    public TextMeshProUGUI textState;   //Stateを表示するテキスト
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

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
    }

    void PlayerStateChange()
    {
        if (moveInput.x != 0 && moveSpeed < runSpeed)
        {
            currentState = EPlayerState.Walk;
        }
        else if (moveInput.x != 0 && moveSpeed >= runSpeed )
        {
            currentState = EPlayerState.Run;
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
        if (currentState == EPlayerState.Run)
        {
            PlayerRun();
        }
    }

    void PlayerStand()
    {
        anim.Play("PlayerStand");
    }
    void PlayerWalk()
    {
        moveSpeed = walkSpeed;
        anim.Play("PlayerWalk");
    }

    void PlayerRun()
    {
        anim.Play("PlayerRun");
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
    public void OnJump(InputAction.CallbackContext context)
    {
        moveSpeed = runSpeed;
    }
}
