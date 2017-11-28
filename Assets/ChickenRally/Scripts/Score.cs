using UnityEngine;

public class Score : MonoBehaviour
{
    int totalScore;

    public int TotalScore {
        get { return totalScore; }
        set {
            totalScore = value;

            UIManager.instance.UpdateScore(totalScore);
        }
    }
}
