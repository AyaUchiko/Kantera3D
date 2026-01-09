using UnityEngine;
using UnityEngine.UI;       //UIを使うために必要

public class GaugeController : MonoBehaviour
{
    [SerializeField] LightController lightController;
    float lRange;
    float lIntensity;

    Image gaugeImage;
    public float maxEnergy=100;

    float timeCount=0;

    int count;//iRangeとiIntensityから決まるエネルギー消費の速さ

    void Start()
    {
        gaugeImage = GetComponent<Image>();
    }

    void Update()
    {
        lRange = lightController.range;
        lIntensity = lightController.intensity;

        Energy();

        maxEnergy -= count * Time.deltaTime;
    }

    void Energy()
    {
        if (lRange < 2 && lIntensity < 10)
        {
            count = 5;
        }
        else if (lRange < 3 && lIntensity < 20)
        {
            count = 4;
        }
        else if (lRange < 4 && lIntensity < 30)
        {
            count = 3;
        }
        else if(lRange < 5 && lIntensity < 40)
        {
            count = 2;
        }
        else
        {
            count = 1;
        }
    }

    void Count()
    {
        gaugeImage.fillAmount -= 0.1f;
    }
}
