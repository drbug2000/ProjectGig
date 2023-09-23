using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName; // 아이템의 이름
    public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울)
    public GameObject itemPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)

    public int Cost;
}