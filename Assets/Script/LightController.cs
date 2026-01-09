using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class LightController : MonoBehaviour
{
    [Header("RangeSetting")]
    public float changeRange;      //変更値
    public float maxRange;         //最大範囲
    public float miniRange;        //最小範囲

    [Header("IntensitySetting")]
    public float changeIntensity;  //変更値
    public float maxIntensity;     //最大範囲
    public float miniIntensity;    //最小範囲

    public float range;            //現在の値を保持
    public float intensity;        //現在の値を保持
    bool lightUp;
    bool lightDown;

    Light lt;
    public TextMeshProUGUI textRange;       //Rangeの値を表示するテキスト
    public TextMeshProUGUI textIntensity;   //Intensityの値を表示するテキスト

    void Start()
    {
        lt = GetComponent<Light>();
        range = lt.range;
        intensity = lt.intensity;

        textRange.text = "Light.Range:";
        textIntensity.text = "Light.Intensity:";
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

            intensity += changeIntensity * Time.deltaTime;
            if(intensity>=maxIntensity)
            {
                intensity = maxIntensity;
            }
        }

        if(lightDown)
        {
            range -= changeRange * Time.deltaTime;
            if(range<=miniRange)
            {
                range = miniRange;
            }

            intensity -= changeIntensity * Time.deltaTime;
            if (intensity <= miniIntensity)
            {
                intensity = miniIntensity;
            }
        }

        lt.range = range;
        lt.intensity = intensity;

        textRange.text= "Light.Range:" + range.ToString();
        textIntensity.text = "Light.intensity:" + intensity.ToString();
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
