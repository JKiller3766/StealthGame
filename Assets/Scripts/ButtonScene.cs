using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScene : MonoBehaviour
{
    public static void ChangeScene()
    {
        SceneManager.LoadScene("Gameplay");
        WinTrigger.Win = false;
        Timer.TimePast = 0;
        WinTrigger.CurrentTime = 0;
        PlayerMove.Distance = 0;
    }
}
