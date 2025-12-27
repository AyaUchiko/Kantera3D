using Unity.Profiling;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerController pc;

    [Header("Dashing")]
    public float dashForce;                 //ダッシュ時に加える力
    public float dashDuration;              //ダッシュの持続時間
    private Vector3 delayedForceToApply;

    [Header("CoolDown")]
    public float dashCd;        //ダッシュのクールダウン時間
    private float dashCdTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }
    }

    //ダッシュ
    public void Dash()
    {
        //ダッシュのクールダウン中ならダッシュをしない
        if (dashCdTimer > 0)
        {
            return;
        }
        else
        {
            dashCdTimer = dashCd;
        }

        pc.dashing = true;

        Vector3 direction = DashDirection();

        Vector3 forceToApply = direction * dashForce;

        rb.useGravity = false;

        delayedForceToApply = forceToApply;

        Invoke(nameof(DelayedDashForce), 0.025f);//ステートが完全にdashingに切り替わるのを待つ

        Invoke(nameof(ResetDash), dashDuration);//ダッシュを終了
    }

    //ステートが完全にdashingに切り替わるのを待つ
    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    //ダッシュが終了したときの処理
    private void ResetDash()
    {
        pc.dashing = false;

        rb.useGravity = true;
    }

    //ダッシュをする方向を決める
    private Vector3 DashDirection()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction;

        //どの方向でもダッシュができるかをチェック
        direction = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //どの方向にも入力がなければ前方にダッシュ
        if (horizontalInput == 0 && verticalInput == 0)
            direction = orientation.forward;

        return direction.normalized;
    }
}
