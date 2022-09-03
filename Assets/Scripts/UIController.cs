using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject gamePanel;

    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _gameController.OnWin += ShowWinPanel;
        ShowGamePanel();
    }

    private void OnDestroy()
    {
        _gameController.OnWin -= ShowWinPanel;
    }

    private void ShowWinPanel()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
    }
    
    public void ShowGamePanel()
    {
        gamePanel.SetActive(true);
        winPanel.SetActive(false);
    }
}
