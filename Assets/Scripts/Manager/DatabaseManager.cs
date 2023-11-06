using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.PackageManager;
using Unity.IO.LowLevel.Unsafe;

[System.Serializable]
public class SaveData {

    public int Gold = 0;
    public int GigDamLvl = 0;
    public int GigRangeLvl = 0;
    public int HpLvl = 0;
    public Item[] item;

}

public class DatabaseManager : MonoBehaviour
{

    private static DatabaseManager instance = null;

    public GameObject player;

    #region singleton
    void Awake()
    {
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
        path = Path.Combine(Application.dataPath + "/Data/database.json");
    }

    public static DatabaseManager Instance
    {
        get
        {
            if (null == DatabaseManager.instance)
            {
                return null;
            }
            return DatabaseManager.instance;
        }
    }
    #endregion

    public string path;
    private string savefilepathpath;
    private Animator animator;
    public Vector3 toplayerpos;

    SaveData saveData = new SaveData();

    [ContextMenu("From Json Data")]
    public void JsonLoad() {
        // SaveData saveData = new SaveData();

        if (!File.Exists(path)) {
            JsonSave();
        } 
        else 
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null) {
                GameManager.Instance.Gold = saveData.Gold;
                GameManager.Instance.GigDamLvl = saveData.GigDamLvl;
                GameManager.Instance.GigRangeLvl = saveData.GigRangeLvl;
                GameManager.Instance.HpLvl = saveData.HpLvl;
            }
            else {
                Debug.Log("ERROR:NOSAVEDATAEXIST");
            }
        }
    }
    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void JsonSave() {
        Time.timeScale = 0f;

        saveData.Gold = GameManager.Instance.Gold;
        saveData.GigDamLvl = GameManager.Instance.GigDamLvl;
        saveData.GigRangeLvl = GameManager.Instance.GigRangeLvl;
        saveData.HpLvl = GameManager.Instance.HpLvl;
        GameObject player = GameObject.FindWithTag("Player");
        for (int i = 0; i < 8; ++i) {
            saveData.item[i] = player.GetComponent<Inventory>().slots[i].item;
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
        StartCoroutine(Loading());
        Time.timeScale = 1f;
    }

    IEnumerator Loading() {
        yield return null;
        Time.timeScale = 0f;
        Debug.Log("enter");
        if (File.Exists(path)) {
            Time.timeScale = 1f;
            StopCoroutine(Loading());
        }
        else {
            StartCoroutine(Loading());
        }
    }
}
