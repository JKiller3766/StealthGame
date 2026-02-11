using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    public void OnExitGame()
    {
		Application.Quit();
		Debug.Log("Salir del juego...");
    }
}