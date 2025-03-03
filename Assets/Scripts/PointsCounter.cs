using UnityEngine;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] Goal _goal;
    [SerializeField] BallControl _ballControl;

    public int reward, punish, BASE_POINTS;
    
    int points;
    int Points
    {
        get 
        { 
            if (points < 0)
            {
                UnityEditor.EditorApplication.ExitPlaymode();
            }
            return points; 
        }
        set
        {
            points = value;
        }
    }
    int lastBounces = 0;


    void Start()
    {
        Points = BASE_POINTS;
        UpdateScoreUI();

        _ballControl.ballBounced.AddListener(ChangeScoreOnCollision);
        _goal.goalReached.AddListener(ChangeScoreOnGoal);
    }

    void ChangeScoreOnGoal()
    {
        int finalReward = Mathf.Max(reward, reward * lastBounces);
        Points += finalReward;
        //lastBounces = 0;
        UpdateScoreUI();
    }

    void ChangeScoreOnCollision(int bounces)
    {
        lastBounces = bounces;

        Points -= Mathf.Min(punish, punish * lastBounces);
        UpdateScoreUI();
        
    }

    void UpdateScoreUI()
    {
        scoreUI.text = "Points: " + Points.ToString();
    }
}
