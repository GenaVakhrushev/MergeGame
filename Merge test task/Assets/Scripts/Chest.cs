using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //предметы 1 уровня
    public GameObject[] Items;

    public static GameObject[] slots;

    private void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("slot");    
    }

    private void OnMouseUpAsButton()
    {
        GameObject slot = NearestEmptySlot();

        if (!slot)
        {
            return;
        }

        FX.singleton.Play(FX.singleton.Pop);

        GameObject poof = Instantiate(FX.singleton.IceShatter, transform.position, Quaternion.identity);
        Destroy(poof, 1);

        GetComponent<Animator>().Play("Opening");
        GameObject item = Instantiate(Items[Random.Range(0, Items.Length)], transform.position, Quaternion.identity);
        item.GetComponent<Item>().SetDestination(slot.transform.position);
        slot.GetComponent<Slot>().IsEmpty = false;
    }

    //ближайший пустой слот
    GameObject NearestEmptySlot()
    {
        return NearestEmptySlot(slots, transform.position);
    }

    public static GameObject NearestEmptySlot(GameObject[] slots, Vector2 pos)
    {
        GameObject slot = null;
        float minDist = float.MaxValue;

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].GetComponent<Slot>().IsEmpty)
                continue;
            float dist = (pos - (Vector2)slots[i].transform.position).magnitude;
            if (dist < minDist)
            {
                slot = slots[i];
                minDist = dist;
            }
        }

        return slot;
    }
}
