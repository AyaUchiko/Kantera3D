using UnityEngine;

public class PushBox : MonoBehaviour
{
    private Rigidbody boxRb;
    private bool isPush;

    void Start()
    {
        boxRb = GetComponent<Rigidbody>();
    }

    public void SetPush(bool push)
    {
        isPush = push;
        boxRb.isKinematic = !push;

        if (!push)
        {
            boxRb.linearVelocity = Vector3.zero;
        }
    }

    public void MoveBox(float inputX, float speed)
    {
        if (!isPush) return;

        if (Mathf.Abs(inputX) < 0.1f)
        {
            boxRb.linearVelocity = new Vector3(0, boxRb.linearVelocity.y, 0);
            return;
        }
        boxRb.linearVelocity = new Vector3(inputX * speed, boxRb.linearVelocity.y, 0f);
    }
}