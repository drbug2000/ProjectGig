using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData {

    public int Gold = 0;
    public int GigDamLvl = 0;
    public int GigRangeLvl = 0;
    public int HpLvl = 0;

    public Vector3 playerpos;
}

public class DatabaseManager : MonoBehaviour
{
    string path;
    #region singleton
    private static DatabaseManager instance = null;

    public GameObject player;

    void Awake()
    {
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

    SaveData saveData = new SaveData();
    // Start is called before the first frame update
    void Start()
    {
        string path = JsonUtility.ToJson(saveData);
        SaveData save1 = JsonUtility.FromJson<SaveData>(path);
        JsonLoad();
    }

    [ContextMenu("From Json Data")]
    public void JsonLoad() {
        // SaveData saveData = new SaveData();

        if (!File.Exists(path)) {
            GameManager.Instance.Gold = 100;
            GameManager.Instance.GigDamLvl = 4;
            GameManager.Instance.GigRangeLvl = 4;
            GameManager.Instance.HpLvl = 4;
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
        }
    }
    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void JsonSave() {
        
        // SaveData saveData = new SaveData();

        saveData.Gold = GameManager.Instance.Gold;
        saveData.GigDamLvl = GameManager.Instance.GigDamLvl;
        saveData.GigRangeLvl = GameManager.Instance.GigRangeLvl;
        saveData.HpLvl = GameManager.Instance.HpLvl;
        playerpos();
        Debug.Log(saveData.playerpos);

        string json = JsonUtility.ToJson(saveData, true);
        path = Path.Combine(Application.dataPath + "/Data/", "database.json");
        File.WriteAllText(path, json);
    }

    public void playerpos() {
        saveData.playerpos = player.gameObject.transform.position;
    }
}
