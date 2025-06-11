using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<ItemData> allItems;
    public Dictionary<int, ItemData> allItemDictionary = new Dictionary<int, ItemData>();

       
    public void Init()
    {
        foreach (var item in allItems)
        {
            allItemDictionary[item.itemCode] = item;
        }

        ItemInstance testWeaponData = new ItemInstance(allItems[0], 1, 100);
        DropItem(testWeaponData, new Vector3(0, 0.7f, -1f));

        ItemInstance testPotionData = new ItemInstance(allItems[1], 10, 100);
        DropItem(testPotionData, new Vector3(2, 0.5f, 1f));
    }

    

    public void DropItem(ItemInstance item, Vector3 position)
    {
        GameObject drop = Instantiate(item.itemData.dropItemPrefab, position, Quaternion.identity);
        drop.GetComponent<DropItem>().Init(item);
    }
}
