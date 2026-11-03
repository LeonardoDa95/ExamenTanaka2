using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    [SerializeField] private float comboTime = 5f;

    private int streak = 0;
    private float lastKillTime = 0f;

    private int previous = 0;
    private int current = 1;

    public void AddPoints(int points)
    {
        if (streak > 0 && Time.time - lastKillTime > comboTime)
        {
            ResetStreak();
        }

        streak++;

        int bonus = current * 10;

        if (bonus > 500)
        {
            bonus = 500;
        }

        score = score + points + bonus;

        int next = previous + current;
        previous = current;
        current = next;

        lastKillTime = Time.time;
    }

    public void ResetStreak()
    {
        streak = 0;
        previous = 0;
        current = 1;

    }
    public int GetStreak()
    {
        return streak;
    }
}