using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed, rotSpeed, height;
    Vector3 pos;

    //Level changes
    public bool ExistsInLevel1;
    public bool ExistsInLevel2;
    public bool ExistsInLevel3;

    // Start is called before the first frame update
    void Start()
    {
        SetGameLevel(GameManager.gameLevel);

        pos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        float newY = Mathf.Sin(Time.time * speed) *height +pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        transform.Rotate(0, rotSpeed, 0);
    }
    void SetGameLevel(int level)
    {
        switch (level)
        {
            case 1:
                if (!ExistsInLevel1)
                {
                    Destroy(this.gameObject);
                }

                break;
            case 2:
                if (!ExistsInLevel2)
                {
                    Destroy(this.gameObject);
                }

                break;
            case 3:
                if (!ExistsInLevel3)
                {
                    Destroy(this.gameObject);
                }

                break;
            default:

                break;
        }
    }
}
