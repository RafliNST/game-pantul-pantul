using UnityEngine;
using TMPro;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] Goal _goal;
    [SerializeField] BallControl _ballControl;

    public int reward, punish, BASE_POINTS;
    int points;


    void Start()
    {
        points = BASE_POINTS;
        scoreUI.text = "Score: " + points.ToString();

        _ballControl.ballBounced.AddListener(ChangeScore);
    }

    void ChangeScore(int bounces)
    {
        points += (_ballControl.IsMoving() ? -punish : Mathf.Max(reward, bounces * reward));
        scoreUI.text = "Score: " + points.ToString();
    }
}
