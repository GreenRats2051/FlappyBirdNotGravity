using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    [SerializeField] private Text scoreText;

    private void Awake()
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Score")
        {
            score++;
            scoreText.text = $"Score: {score}";
            Destroy(collision.gameObject);
        }
    }
}