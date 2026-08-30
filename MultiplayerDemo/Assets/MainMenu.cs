using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager m_mainMenu;
    private bool m_isHost;
    public void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }
    public void StartGame(bool shouldHost)
    {
        m_isHost = shouldHost;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void OnSceneChanged(Scene current, Scene next)
    {
        if (next.buildIndex == 1)
        {
            if (m_isHost)
            {
                m_mainMenu.StartHost();
            }
            else
            {
                m_mainMenu.StartClient();
            }
        }
    }
}
