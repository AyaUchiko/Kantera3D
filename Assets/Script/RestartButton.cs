using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void TitleSceneButton()
    {
        SceneManager.LoadScene("Stage1");
    }
}
