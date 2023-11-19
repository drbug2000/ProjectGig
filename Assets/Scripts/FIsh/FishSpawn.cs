using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    public GameObject smallfish;
    public GameObject middlefish;
    public GameObject bigfish;
    public GameObject shark;

    public int smallFishCounter;
    public int middleFishCounter;
    public int bigFishCounter;
    public int sharkCounter;

    public int spawnRangeX;
    public int spawnRangeY;

    public int respawnTime;
    private WaitForSeconds respawntime;


    // Start is called before the first frame update
    void Start()
    {
        /*
        for (int i = 0; i < smallFishCounter; i++)
        {
            spawnFish(smallfish, RandomStartPos(20,5));
        }

        for (int i = 0; i < middleFishCounter; i++)
        {
            spawnFish(middlefish, RandomStartPos(20, 5));
        }
        */

        spawnFishPrefab(smallfish, smallFishCounter);
        spawnFishPrefab(middlefish, middleFishCounter);
        spawnFishPrefab(bigfish, bigFishCounter);
        spawnFishPrefab(shark, sharkCounter);

        respawntime = new WaitForSeconds(this.respawnTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnFish(GameObject fishPrefab,Vector2 Pos)
    {
        
            GameObject spawnedFish = Instantiate(fishPrefab, Pos, Quaternion.identity);
        
    }

    void spawnFishPrefab(GameObject fishPrefab, int many)
    {
        for (int i = 0; i < many; i++)
        {
            GameObject spawnedFish = Instantiate(fishPrefab, SideRandomPos(spawnRangeX, spawnRangeY), Quaternion.identity);
        }


    }

    //(0,0)을 기준으로 좌우로 x만큼 떨어져 있고, 아래로 y만큼의 길이를 가진 두 평행선에서 물고기 생성 
    Vector2 SideRandomPos(int xInterval,int yInterval)
    {

        int i = (int)Random.Range(0, yInterval*2);


        int x = xInterval;
        int y = i;

        if (i < yInterval)
        {
            x *= -1;
        }
        else
        {
            y -= yInterval;
        }

        return new Vector2(x, -y);

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

    public void fishOnCaught(GameObject Caughtfish)
    {
        StartCoroutine("fishRespawn",Caughtfish);
    }


    IEnumerator fishRespawn(GameObject fish)
    {
        FishClass fishclass = fish.GetComponent<FishClass>();
        yield return respawntime;
        //fish.SetActive(true);
        Debug.Log("fish respawn"+fishclass);
        fishclass.respawn(SideRandomPos(spawnRangeX, spawnRangeY));
    }

}
