using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

    public GameObject rowPrefab;
    List<GameObject> rows = new List<GameObject>();
    void addRow()
    {
        rows.Add(Instantiate(rowPrefab, transform));
    }
    void moveRows()
    {
        if (rows !=null)
        {
            for(int i = 0;i<rows.Count;i++)
            {

                if (rows[i].transform.childCount == 0)
                {
                    Destroy(rows[i]);
                    rows.Remove(rows[i]);
                    i--;
                }
                else rows[i].GetComponent<RowScript>().moveRow();
            }
        }
    }
    public void nextLevel()
    {
        moveRows();
        Invoke("addRow", 0.16f);
    }
}
