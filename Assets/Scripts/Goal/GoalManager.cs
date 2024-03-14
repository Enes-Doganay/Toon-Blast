using System;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : Singleton<GoalManager>
{
    [SerializeField] private GoalObject GoalPrefab;
    [SerializeField] private Transform goalsParent;
    private List<GoalObject> goalObjects = new List<GoalObject>();

    public Action OnGoalsCompleted;
    private bool allGoalsCompleted = false;
    public void Init(List<LevelGoal> goals)
    {
        foreach(LevelGoal goal in goals)
        {
            GoalObject goalObject = Instantiate(GoalPrefab, goalsParent);
            goalObject.Prepare(goal);
            goalObjects.Add(goalObject);
        }
    }

    public void UpdateLevelGoal(ItemType itemType)
    {
        if (allGoalsCompleted) return;

        var goalObject = goalObjects.Find(goal => goal.LevelGoal.ItemType.Equals(itemType));

        if (goalObject == null) return;

        goalObject.DecraseCount();

        CheckAllGoalsCompleted();
    }

    public bool CheckAllGoalsCompleted()
    {
        foreach(GoalObject goal in goalObjects)
        {
            if (!goal.IsCompleted())
                return false;
        }

        //Won
        allGoalsCompleted = true;
        OnGoalsCompleted?.Invoke();
        return true;
    }
}