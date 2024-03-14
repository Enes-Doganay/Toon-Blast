using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        int level = PlayerPrefs.GetInt("Level", 1);
        levelText.text = "Level " + level;
        levelButton.onClick.RemoveAllListeners();
        levelButton.onClick.AddListener(() => GameManager.Instance.LoadLevelScene());
    }
}
