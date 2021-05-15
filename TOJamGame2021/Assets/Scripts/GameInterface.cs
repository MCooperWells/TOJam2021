using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + GameManager.Singleton.GetGameScore();
    }
    public void UpdateUI()
    {
        scoreText.text = "Score: " + GameManager.Singleton.GetGameScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
