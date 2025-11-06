using UnityEngine;

public class ScoreCollectible : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.Instance.ScoreSystem.AddScore(scoreValue);
            gameObject.SetActive(false);
        }
    }
}