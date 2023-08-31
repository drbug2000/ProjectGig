using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public GameObject smallfish;
    public GameObject middlefish;
    public GameObject bigish;
    public GameObject shark;

    public int smallFishCounter;
    public int middleFishCounter;
    public int bigFishCounter;
    public int sharkCounter;



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < smallFishCounter; i++)
        {
            spawnFish(smallfish, RandomStartPos(20,5));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnFish(GameObject fishPrefab,Vector2 Pos)
    {
        
            GameObject spawnedFish = Instantiate(fishPrefab, Pos, Quaternion.identity);
        
    }

    Vector2 RandomStartPos(int row, int col)
    {
        //(-row/2,0)(row,-col) 사각형 테두리에서 랜덤하게 생성
        Vector2 StartPos;
        int i = Random.Range(0, row + 2*col);

        if (i < col)
        {
            StartPos = new Vector2(-row/2, -i);
        }else if(i < row+col)
        {
            StartPos = new Vector2(-i+row, -col);
        }
        else
        {
            StartPos = new Vector2(row/2, -i+row+col);
        }

        return StartPos;
    }

}
