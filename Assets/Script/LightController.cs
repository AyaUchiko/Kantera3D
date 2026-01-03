using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LightController : MonoBehaviour
{
    [Header("RangeSetting")]
    public float changeRange;      //変化する範囲
    public float maxRange;         //最大範囲
    public float miniRange;        //最小範囲

    float range;                   //現在の値を保持
    bool lightUp;
    bool lightDown;

    Light lt;
    public TextMeshProUGUI textRange;   //Rangeを表示するテキスト

    void Start()
    {
        lt = GetComponent<Light>();
        range = lt.range;

        textRange.text = "Light.Range:";
    }

    void Update()
    {
        if(lightUp)
        {
            range += changeRange * Time.deltaTime;
            if (range >= maxRange)
            {
                range = maxRange;
            }
        }
        if(lightDown)
        {
            range -= changeRange * Time.deltaTime;
            if(range<=miniRange)
            {
                range = miniRange;
            }
        }

        lt.range = range;

        textRange.text="Light.Range:" + range.ToString();
    }

    public void OnLightUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            lightUp = true;
        }

        if (context.canceled)
        {
            lightUp = false;
        }
    }
    public void OnLightDown(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            lightDown = true;
        }

        if(context.canceled)
        {
            lightDown = false;
        }
    }
}
