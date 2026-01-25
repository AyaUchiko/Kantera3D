using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PlayerAnimator : MonoBehaviour
{
    public enum EPlayerAnimState
    { 
         PlayerIdle,
         PlayerWalk,
         PlayerDash,
         PlayerJump,
         PlayerDead,
         None
    }

     [SerializeField]private Animator animator;

    private EPlayerAnimState currentAnimState=EPlayerAnimState.None;
    private string currentAnimName;
    private bool isEnter;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        Debug.Log("Animator is" + (animator != null));
        ChangeState(EPlayerAnimState.PlayerIdle);
    }

    public void ChangeState(EPlayerAnimState nextState, float fadeTime =0.1f)
    {
        if (currentAnimState == nextState) return;

        currentAnimState = nextState;
        isEnter = false;

        string animName = GetAnimName(nextState);
        ChangeAnimation(animName, fadeTime);
    }

    private void ChangeAnimation(string animName,float fadeTime)
    {
        if (currentAnimName == animName) return;

        currentAnimName = animName;
        animator.CrossFade(animName, fadeTime);
    }

    private string GetAnimName(EPlayerAnimState state)
    {
        switch(state)
        {
            case EPlayerAnimState.PlayerIdle:
                return "PlayerIdle";
            case EPlayerAnimState.PlayerWalk:
                return "PlayerWalk";
            case EPlayerAnimState.PlayerDash:
                return "PlayerDash";
            case EPlayerAnimState.PlayerJump:
                return "PlayerJump";
            case EPlayerAnimState.PlayerDead:
                return "PlayerDead";
            default: return "";
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(!isEnter)
        {
            OnEnterState(currentAnimState);
            isEnter = true;
        }
    }

    private void OnEnterState(EPlayerAnimState state)
    {
        switch(state)
        {
            //‰¼
        }
    }
}
