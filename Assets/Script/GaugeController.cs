using UnityEngine;
using UnityEngine.UI;       //UIを使うために必要
using TMPro;

public class GaugeController : MonoBehaviour
{
    [SerializeField] LightController lightController;
    Image gaugeImage;

    public float maxEnergy = 100;
    float currentEnergy;
    float count;

    public TextMeshProUGUI textReset;   //何秒で消費するかを表示するテキスト

    void Start()
    {
        gaugeImage = GetComponent<Image>();
        currentEnergy = maxEnergy;
        ResetCount();
    }

    void Update()
    {
        count -= Time.deltaTime;

        if (count <= 0)
        {
            currentEnergy -= 1;
            gaugeImage.fillAmount = currentEnergy / 100;
            ResetCount();
        }

        textReset.text =count.ToString();
    }

    void ResetCount()
    {
        float lRange = lightController.range;
        float lIntensity = lightController.intensity;

        //RangeとIntensityの値を見て、何秒でエネルギーを減らすか決める
        if (lRange < 2 && lIntensity < 15)
        {
            count = 5;
        }
        else if (lRange < 3 && lIntensity < 20)
        {
            count = 3f;
        }
        else if (lRange < 4 && lIntensity < 30)
        {
            count = 2f;
        }
        else if (lRange < 5 && lIntensity < 40)
        {
            count = 1f;
        }
        else
        {
            count = 0.1f;
        }
    }
}