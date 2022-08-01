using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    //предметы первый индекс - уровень, второй - тип
    List<GameObject>[] items = new List<GameObject>[5];
    int itemType = -1;
    int itemLevel;
    void Start()
    {
        for (int i = 0; i < 5; i++)
            items[i] = new List<GameObject>();

        GameObject[] allItems = GameObject.FindGameObjectsWithTag("item");

        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i].GetComponent<Item>().Level != 0)
            {
                items[allItems[i].GetComponent<Item>().Level - 1].Add(allItems[i]);
            }
        }
    }

    void Update()
    {
        itemType = -1;

        //кнопки отвечающие за тип
        if (Input.GetKey(KeyCode.Q))
            itemType = 0;
        if (Input.GetKey(KeyCode.W))
            itemType = 1;
        if (Input.GetKey(KeyCode.E))
            itemType = 2;
        if (Input.GetKey(KeyCode.R))
            itemType = 3;

        //кнопки отвечающие за уровень
        if (Input.GetKeyUp(KeyCode.Alpha1))
            itemLevel = 0;
        if (Input.GetKeyUp(KeyCode.Alpha2))
            itemLevel = 1;
        if (Input.GetKeyUp(KeyCode.Alpha3))
            itemLevel = 2;
        if (Input.GetKeyUp(KeyCode.Alpha4))
            itemLevel = 3;
        if (Input.GetKeyUp(KeyCode.Alpha5))
            itemLevel = 4;

        if (itemType > -1 && itemLevel > -1)
        {
            GameObject slot = Chest.NearestEmptySlot(Chest.slots, Vector2.zero);
            if (!slot)
            {
                itemLevel = -1;
                return;
            }
            Instantiate(items[itemLevel][itemType], slot.transform.position, Quaternion.identity);
            slot.GetComponent<Slot>().IsEmpty = false;
        }

        itemLevel = -1;
    }
}
