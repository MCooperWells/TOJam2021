using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : MonoBehaviour
{
    public float speed, height;
    public bool upDown, sideToSideX, sideToSideZ;
    private Vector3 pos;
    // Start is called before the first frame update

    //Level changes
    public bool ExistsInLevel1;
    public bool ExistsInLevel2;
    public bool ExistsInLevel3;

    public float speedLevel1;
    public float speedLevel2;
    public float speedLevel3;

    protected void Start()
    {
        SetGameLevel(GameManager.gameLevel);
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {


        if(upDown == true)
        {

            float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        if(sideToSideX == true)
        {
            float newX = Mathf.Sin(Time.time * speed) * height + pos.x;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        }

        if(sideToSideZ == true)
        {
            float newZ = Mathf.Sin(Time.time * speed) * height + pos.z;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
    }

    void SetGameLevel(int level)
    {
        switch (level)
        {
            case 1:
                if(!ExistsInLevel1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    speed = speedLevel1;
                }
                break;
            case 2:
                if (!ExistsInLevel2)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    speed = speedLevel2;
                }
                break;
            case 3:
                if (!ExistsInLevel3)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    speed = speedLevel3;
                }
                break;
            default:
                if (!ExistsInLevel1)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    speed = speedLevel1;
                }
                break;
        }
    }

    public void IncreaseDifficulty(float speedIncrease)
    {
        speed += speedIncrease;
    }
}
