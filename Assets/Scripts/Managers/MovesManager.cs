using System;
using TMPro;
using UnityEngine;

public class MovesManager : Singleton<MovesManager>
{
    [SerializeField] private TextMeshProUGUI movesText;
    private int moves;
    public int Moves => moves;
    public Action OnMovesFinished;
    public void Init(int moves)
    {
        this.moves = moves;
        UpdateMovesText();
    }

    public void DecraseMoves()
    {
        moves--;
        
        if (moves <= 0)
        {
            moves = 0;
            OnMovesFinished?.Invoke();
        }

        UpdateMovesText();
    }

    private void UpdateMovesText()
    {
        movesText.text = moves.ToString();
    }
}