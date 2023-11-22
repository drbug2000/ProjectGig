using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
// using UnityEditor.PackageManager;
// using Unity.IO.LowLevel.Unsafe;
// using JetBrains.Annotations;

[System.Serializable]
public class SaveData {

    public int Gold = 0;
    public int GigDamLvl = 0;
    public int GigRangeLvl = 0;
    public int HpLvl = 0;
    public List<string> nameofinv = new List<string>() {"littlefish", "middlefish", "bigfish", "shark"};
    public List<int> countofinv = new List<int>() {0, 0, 0, 0};

}

public class Todictionary {
        public Dictionary<string, int> dic = new Dictionary<string, int> {
        {"littlefish", 0},
        {"middlefish", 0},
        {"bigfish", 0},
        {"shark", 0}
    };
}

public class DatabaseManager : MonoBehaviour
{
    [SerializeField]
    private Item[] item;
    private static DatabaseManager instance = null;

    public ItemSlotUI[] slots;
    public StorageManager thestoragemanager;

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
        path = Path.Combine(Application.streamingAssetsPath + "database.json");
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

    SaveData saveData = new SaveData();
    Todictionary todic = new Todictionary();

    [ContextMenu("From Json Data")]
    public void JsonLoad() {
        // SaveData saveData = new SaveData();

        // if (!File.Exists(path)) {
        //     JsonSave();
        // } 
        // else 
        // {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null) {
                GameManager.Instance.Gold = saveData.Gold;
                GameManager.Instance.GigDamLvl = saveData.GigDamLvl;
                GameManager.Instance.GigRangeLvl = saveData.GigRangeLvl;
                GameManager.Instance.HpLvl = saveData.HpLvl;
                
                int i = 0;
                int isempty = 0;
                if (saveData.countofinv[isempty] != 0) {
                    slots[i].item = item[0];
                    slots[i].itemCount = saveData.countofinv[isempty];
                    i += 1;
                }
                isempty += 1;
                if (saveData.countofinv[isempty] != 0) {
                    slots[i].item = item[1];
                    slots[i].itemCount = saveData.countofinv[isempty];
                    i += 1;
                }
                isempty += 1;
                if (saveData.countofinv[isempty] != 0) {
                    slots[i].item = item[2];
                    slots[i].itemCount = saveData.countofinv[isempty];
                    i += 1;
                }
                isempty += 1;
                if (saveData.countofinv[isempty] != 0) {
                    slots[i].item = item[3];
                    slots[i].itemCount = saveData.countofinv[isempty];
                    Debug.Log(isempty);
                    Debug.Log(saveData.countofinv[isempty]);
                    Debug.Log(slots[i].itemCount);
                }

            }
            else {
                Debug.Log("ERROR:NOSAVEDATAEXIST");
            }
        // }
    }
    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void JsonSave() {
        // Time.timeScale = 0f;

        saveData.Gold = GameManager.Instance.Gold;
        saveData.GigDamLvl = GameManager.Instance.GigDamLvl;
        saveData.GigRangeLvl = GameManager.Instance.GigRangeLvl;
        saveData.HpLvl = GameManager.Instance.HpLvl;         
        todic.dic["littlefish"] = thestoragemanager.littleFishCount;
        todic.dic["middlefish"] = thestoragemanager.middleFishCount;
        todic.dic["bigfish"] = thestoragemanager.bigFishCount;
        todic.dic["shark"] = thestoragemanager.sharkCount;

        saveData.countofinv[0] = thestoragemanager.littleFishCount;
        saveData.countofinv[1] = thestoragemanager.middleFishCount;
        saveData.countofinv[2] = thestoragemanager.bigFishCount;
        saveData.countofinv[3] = thestoragemanager.sharkCount;
    
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
        StartCoroutine(Loading());
        // Time.timeScale = 1f;
    }

    IEnumerator Loading() {
        yield return null;
        while(!File.Exists(path)) {
            GameManager.Instance.pauseGame();
        }
        // GameManager.Instance.resumeGame();
    }

    public void ClearAllTheData() {
        GameManager.Instance.Gold = 0;
        GameManager.Instance.GigDamLvl = 0;
        GameManager.Instance.GigRangeLvl = 0;
        GameManager.Instance.HpLvl = 0;
        for (int i = 0; i < 4; ++i) {
            slots[i].item = null;
            slots[i].itemCount = 0;
        }
    }
}
