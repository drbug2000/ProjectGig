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
    public Item item;

}

public class DatabaseManager : MonoBehaviour
{
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

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);

        StartCoroutine(Loading());
    }

    public void playerpos() {
        saveData.playerpos = player.gameObject.transform.position;
    }

    IEnumerator Loading() {
        yield return null;
        Time.timeScale = 0f;
        if (File.Exists(path)) {
            Time.timeScale = 1f;
            StopAllCoroutines();
        }
        else {
            Loading();
        }
    }
}
