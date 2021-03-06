using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMode : MonoBehaviour
{
    public GameObject playerReference;
    public GameObject[] coinReferences;
    public GameObject[] obstacleReferences;

    public float playerSpeedIncrease;
    public float obstacleSpeedIncrease;

    public GameObject onceMoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        onceMoreText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseDifficulty()
    {
        PlayerController player = playerReference.GetComponent<PlayerController>();
        player.IncreaseDifficulty(playerSpeedIncrease);


        for (int i = 0; i < coinReferences.Length; i++)
        {
            coinReferences[i].SetActive(true);
        }

        for (int i = 0; i < obstacleReferences.Length; i++)
        {
            MovingObstacles mo = obstacleReferences[i].GetComponent<MovingObstacles>();
            mo.IncreaseDifficulty(obstacleSpeedIncrease);
        }

        onceMoreText.SetActive(true);

        Debug.Log("ENDLESS MODE START START");
    }
}
