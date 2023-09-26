using UnityEngine;
using UnityEngine.UI;

public class SellNotice : MonoBehaviour
{
    //컴포넌트 선언
    NoticeUI _notice;
    //public Inventory inventory;

    // Start is called before the first frame update
    void Awake()
    {
        //inventory = GetComponent<Inventory>();
        _notice = FindObjectOfType<NoticeUI>();
    }

    public void SellAll(){
        //_notice.SUB(inventory.allcost);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
