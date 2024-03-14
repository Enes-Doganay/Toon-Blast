using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalObject : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Image markImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private ParticleSystem effect;
    private int count;
    private LevelGoal levelGoal;
    public LevelGoal LevelGoal => levelGoal;
    public void Prepare(LevelGoal goal)
    {
        levelGoal = goal;
        var goalSprite = ItemImageLibrary.Instance.GetSpriteForItemType(levelGoal.ItemType);
        image.sprite = goalSprite;

        count = levelGoal.Count;
        countText.text = count.ToString();
    }

    public void DecraseCount()
    {
        count--;

        if (count <= 0)
        {
            count = 0;
            countText.gameObject.SetActive(false);
            markImage.gameObject.SetActive(true);
            return;
        }

        countText.text = count.ToString();

        DecraseEffectPlay();
    }
    private void DecraseEffectPlay()
    {
        effect.Play();
    }

    public bool IsCompleted()
    {
        return count <= 0;
    }
}