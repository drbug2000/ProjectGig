using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

    public int Gold = 0;
    public int GigDamLvl = 0;
    public int GigRangeLvl = 0;
    public int HpLvl = 0;
    
    public bool isdeadingamemanager;

    public AudioSource _audioSource;
    public AudioClip _introLobbyAudioClip;
    public AudioClip _inGameClip;

    public FishSpawn fishspawn ; //외부에서 접근 가능한 변수 추가
    public ShopManager shopManager;

    public AssetManager theassetmanager;
    public GameObject gameoverText;
    void Awake()
    {
        //theassetmanager = GameObject.Find("Asset Set").GetComponent<AssetManager>();
        shopManager = GameObject.Find("Player 1").GetComponent<ShopManager>();
        fishspawn = GameObject.Find("spawner").GetComponent<FishSpawn>();
        gameoverText = GameObject.Find("Canvas").transform.Find("Gameover Text").gameObject;
        gameoverText.SetActive(false);
        /*
        if(gameoverText.IsActive()==false){
            gameoverText.SetActive(true);
            
        } else{
            gameoverText = GameObject.Find("Gameover Text");
        }
        */
        resumeGame();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
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

    void OnEnable()
    {
    	  // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LivingEntity deathEvent = new LivingEntity();
        deathEvent.onDeath += new System.Action(playeronDeath);
        theassetmanager = GameObject.Find("Asset Set").GetComponent<AssetManager>();
        shopManager = GameObject.Find("Player 1").GetComponent<ShopManager>();
        fishspawn = GameObject.Find("spawner").GetComponent<FishSpawn>();
        gameoverText = GameObject.Find("Canvas").transform.Find("Gameover Text").gameObject;

    // if (GameObject.Find("player 1").GetComponent<PlayerHealth>() != null) {
        if (SceneManager.GetActiveScene().name == "Merge 2" && !isdeadingamemanager){
            resumeGame();
            DatabaseManager.Instance.JsonLoad();
        }
        isdeadingamemanager = false;
    // }
        resumeGame();
        
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    // 현재는 이 변수가 쓰이고 있지 않습니다.
    // private bool isGameover = false;
    //public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    

    /*
    public void PlayLobbyMusic()
    {
        if (_audioSource.clip == _introLobbyAudioClip)
            return;

        _audioSource.Stop();
        _audioSource.clip = _introLobbyAudioClip;
        _audioSource.Play();
    }

    public void PlayGameMusic()
    {
        if (_audioSource.clip == _inGameClip)
            return;

        _audioSource.Stop();
        _audioSource.clip = _inGameClip;
        _audioSource.Play();
    }
    */

    //GameManager의 playeronDeath 메서드 실행

    //you died 화면창에 띄우기

    public void playeronDeath(){

        //죽었을 때 1000G 이상 가지고 있으면 -1000G
        isdeadingamemanager = true;
        if (Gold >= 1000){
            Gold -= 1000;
        }
        //1000G 미만이면 0으로
        else{
            Gold = 0;
        }
        
        theassetmanager.Sell();
        StartCoroutine(RestartGame());
        gameoverText.SetActive(true);
    }

    // 게임 오버 상태일때 다시 실행하게 해주는 함수
    IEnumerator RestartGame()
    {
        yield return null;
        // while(true) {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                StopAllCoroutines();
            }
        // }
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
