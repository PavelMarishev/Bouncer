using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowScript : MonoBehaviour
{

    public List<GameObject> items;
    public GameObject itemPrefab;
    public GameObject boxPrefab;
    float gameOver = -7f;
    bool isMoving;
    Vector3 startPos;
    Vector3 endPos;
    float lerpTime = 0.15f;
    float currLerpTime;
    void Start()
    {
        int spawnItem = Random.Range(0, 100);
        if (spawnItem > 50)
        {
            int itemPlace = Random.Range(0, 6);
            Instantiate(itemPrefab, items[itemPlace].transform.position, items[itemPlace].transform.rotation, transform);
            Destroy(items[itemPlace]);
            items.RemoveAt(itemPlace);
        }
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                int switcher = Random.Range(0, 2);
                switch (switcher)
                {

                    case 1:
                        Instantiate(boxPrefab, item.transform.position, item.transform.rotation, transform);
                        break;
                }
                Destroy(item);
            }
        }
    }
    public void moveRow()
    {
        currLerpTime = 0;
        startPos = transform.position;
        endPos = startPos + new Vector3(0, -1, 0);
        isMoving = true;

    }
    private void Update()
    {
       
        if (isMoving)
        {
            if (transform.position == endPos) isMoving = false;
            currLerpTime += Time.deltaTime;
            if (currLerpTime >= lerpTime) currLerpTime = lerpTime;
            float Perc = currLerpTime / lerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, Perc);
            if (transform.localPosition.y <= gameOver) GameControllerScript.gameOver();
        }
    }


}
