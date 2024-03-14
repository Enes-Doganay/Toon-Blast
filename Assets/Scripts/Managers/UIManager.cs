using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject levelCompletedPanel;
    [SerializeField] private Button nextButton;

    [SerializeField] private GameObject levelLostPanel;
    [SerializeField] private Button retryButton;

    [SerializeField] private Image characterImage;

    private void OnEnable()
    {
        MovesManager.Instance.OnMovesFinished += CheckGoals;
    }
    private void OnDisable()
    {
        if(MovesManager.Instance != null) 
            MovesManager.Instance.OnMovesFinished -= CheckGoals;
    }

    private void CheckGoals()
    {
        if (!GoalManager.Instance.CheckAllGoalsCompleted())
        {
            SetLostPanel();
        }
    }

    public void SetLevelCompletedPanel()
    {
        levelCompletedPanel.SetActive(true);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() => GameManager.Instance.NextLevel());
        characterImage.transform.DOScale(2f, 1f);
        characterImage.transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360);
    }

    public void SetLostPanel()
    {
        levelLostPanel.SetActive(true);
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(() => GameManager.Instance.LoadLevelScene());
    }
}