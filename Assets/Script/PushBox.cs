using UnityEngine;

public class PushBox : MonoBehaviour
{
    private Rigidbody boxRb;
    private bool isPushing;

    void Start()
    {
        boxRb = GetComponent<Rigidbody>();

        boxRb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        boxRb.constraints |= RigidbodyConstraints.FreezePositionX;
    }

    public void SetPush(bool push)
    {
        isPushing = push;

        if (isPushing)
        {
            boxRb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        }
        else
        {
            boxRb.linearVelocity = new Vector3(0, boxRb.linearVelocity.y, 0);
            boxRb.constraints |= RigidbodyConstraints.FreezePositionX;
        }
    }

    public void MoveBox(float inputX, float speed)
    {
        if (!isPushing) return;

        if (Mathf.Abs(inputX) < 0.1f)
        {
            boxRb.linearVelocity = new Vector3(0, boxRb.linearVelocity.y, 0);
            return;
        }

        boxRb.linearVelocity = new Vector3(inputX * speed, boxRb.linearVelocity.y, 0f);
    }
}