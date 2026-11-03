using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void MenuExit()
    {
        SceneManager.LoadScene("Menu");
    }
}
