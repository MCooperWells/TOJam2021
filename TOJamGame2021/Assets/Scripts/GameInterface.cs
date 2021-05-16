using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public Text scoreText;

    public Text nextLevlText;

    private int gameLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameLevel = GameManager.gameLevel;
        UpdateNextLevelText(gameLevel);
        scoreText.text = "Coins: " + GameManager.Singleton.GetGameScore() + "\nLevel: " + gameLevel;
    }
    public void UpdateUI()
    {
        scoreText.text = "Coins: " + GameManager.Singleton.GetGameScore() + "\nLevel: " + gameLevel;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateNextLevelText(int level)
    {
        switch (level)
        {
            case 1:
                nextLevlText.text = "Once More!";
                break;
            case 2:
                nextLevlText.text = "ONCE MORE,\nWITH FEELING!";
                break;
            case 3:
                nextLevlText.text = "ONCE MORE,\nWITH FEELING!";
                break;
            default:
                nextLevlText.text = "Once More";
                break;
        }

    }
}
