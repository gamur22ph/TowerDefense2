using TMPro;
using UnityEngine;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerManager.instance.OnLifeChanged += Player_OnLifeChanged;
    }

    private void OnDestroy()
    {
        PlayerManager.instance.OnLifeChanged -= Player_OnLifeChanged;
    }

    private void Player_OnLifeChanged(float currentLives)
    {
        livesText.text = "Lives: " + currentLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
