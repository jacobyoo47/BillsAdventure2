using UnityEngine;
using UnityEngine.UI;

public class cherry_counter : MonoBehaviour
{

    [SerializeField]
    private Text scoreText;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = "x " + score;
    }
}
