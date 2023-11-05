using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    void Awake()
    {
        resumeGame();
        // fishspawn = GameObject.Find("spawner").GetComponent<FishSpawn>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == GameManager.instance)
            {
                return null;
            }
            return GameManager.instance;
        }

    }
    #endregion

    public GameObject gameoverText;
    // 현재는 이 변수가 쓰이고 있지 않습니다.
    // private bool isGameover = false;
    public int Gold = 0;
    public int GigDamLvl = 0;
    public int GigRangeLvl = 0;
    public int HpLvl = 0;
    public ShopManager shopManager;
    public AssetManager theassetmanager;
    public GameObject Gunobject;

    // Start is called before the first frame update
    void Start()
    {
        Gunobject.SetActive(true);
        gameoverText.SetActive(false);
        //LivingEntity event subscribe
        LivingEntity deathEvent = new LivingEntity();
        deathEvent.onDeath += new System.Action(playeronDeath);
        StartCoroutine(RestartGame());
    }

    public void playeronDeath(){
        Gunobject.SetActive(false);
        gameoverText.SetActive(true);
        //죽었을 때 1000G 이상 가지고 있으면 -1000G
        if (Gold >= 1000){
            Gold -= 1000;
        }
        //1000G 미만이면 0으로
        else{
            Gold = 0;
        }
        theassetmanager.Sell();
        StartCoroutine(RestartGame());
    }

    // 게임 오버 상태일때 다시 실행하게 해주는 함수
    IEnumerator RestartGame()
    {
        yield return null;
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StopAllCoroutines();
        }

        StartCoroutine(RestartGame());
    }

    // Game을 멈추는 함수
    public void pauseGame() {
        Time.timeScale = 0f;
    }

    // Game을 멈춘 것을 재실행하는 함수
    public void resumeGame() {
        Time.timeScale = 1f;
    }
}
