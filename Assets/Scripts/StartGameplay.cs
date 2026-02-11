using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartGameplay : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Gameplay";
    
    public void OnPlay()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}