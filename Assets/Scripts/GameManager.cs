using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance = null;

    public GameObject gameoverText;
    private bool isGameover = false;
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public int Gold = 0;
    public int GigDamLvl = 0;
    public int GigRangeLvl = 0;
    public int HpLvl = 0;
    

    public AudioSource _audioSource;
    public AudioClip _introLobbyAudioClip;
    public AudioClip _inGameClip;

    
    // Start is called before the first frame update
    void Start()
    {
        isGameover = false;
        //PlayLobbyMusic();
    }

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
    /*
     void Update() {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    */

    public void playeronDeath(){

        //죽었을 때 1000G 이상 가지고 있으면 -1000G
        if (Gold >= 1000){
            Gold -= 1000;
        }
        //1000G 미만이면 0으로
        else{
            Gold = 0;
        }
        isGameover = true;
        gameoverUI.SetActive(true);
    }
}
