using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    //позиция мыши
    public static Vector2 mousePos { get { return Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)); } }

    //позиция объекта при начале перетаскивания
    Vector2 startPos;

    //выделеный предмет, который на данный момент переносится
    GameObject selectedItem;
    //слот выделенного объекта
    Slot selectedSlot;

    //переносится ли сейчас какой-либо предмет
    bool isDragActive = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        //если какой-то объект переносится, то ждём когда правая кнопка отпустится, иначе ждём, когда её нажмут
        if (isDragActive)
        {
            Drag();
            //проверяем клик правой кнопкой
            if (Input.GetMouseButtonUp(1))
            {
                //конец перетаскивания
                EndDrag();
                return;
            } 
        }
        else
        {
            //проверяем клик правой кнопкой
            if (Input.GetMouseButtonDown(1))
            {
                //проверяем наличие предмета на месте мыши
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100, LayerMask.GetMask("item"));

                //если на месте мыши предмет, то берём его
                if (hit.collider)
                {
                    RaycastHit2D hitSlot = Physics2D.Raycast(mousePos, Vector2.zero, 100, LayerMask.GetMask("slot"));
                    if (hitSlot.collider)
                        selectedSlot = hitSlot.transform.gameObject.GetComponent<Slot>();

                    selectedItem = hit.transform.gameObject;
                    StartDrag();
                }
            }
        }
    }

    //начинаем перетаскивать
    void StartDrag()
    {
        isDragActive = true;
        startPos = selectedItem.transform.position;
        selectedItem.layer = 0;
    }

    //перетаскиваем
    void Drag()
    {
        selectedItem.transform.position = mousePos;
    }

    //заканчиваем перетаскивать
    void EndDrag()
    {
        isDragActive = false;

        //проверяем наличие слота на месте мыши
        RaycastHit2D hitSlot = Physics2D.Raycast(mousePos, Vector2.zero, 100, LayerMask.GetMask("slot"));

        //если на месте мыши пустой слот, то перемещаем туда предмет и помечаем слот как занятый, иначе сравниваем уровни предметов
        if (hitSlot.collider)
        {
            Slot slot = hitSlot.transform.gameObject.GetComponent<Slot>();
            if (slot.IsEmpty || slot == selectedSlot)
            {
                selectedItem.GetComponent<Item>().SetDestination(hitSlot.transform.position);
                selectedSlot.IsEmpty = true;
                slot.IsEmpty = false;
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100, LayerMask.GetMask("item"));

                Item item = hit.transform.gameObject.GetComponent<Item>();
                Item selItem = selectedItem.GetComponent<Item>();

                //если предметы одного уровня(не максимального) и типа, то объединяем их в новый следующего уровня, иначе возвращаем перетаскиваемый предмет обрано
                if (item.Level != 5 && item.Level == selItem.Level && item.NextItem == selItem.NextItem)
                {
                    FX.singleton.Play(FX.singleton.Splash);

                    Instantiate(item.NextItem, item.transform.position, Quaternion.identity);
                    Destroy(item.gameObject);
                    Destroy(selectedItem);
                    selectedSlot.IsEmpty = true;
                }
                else
                    selectedItem.GetComponent<Item>().SetDestination(startPos);
            }
        }
        else
            selectedItem.GetComponent<Item>().SetDestination(startPos);

        selectedItem.layer = LayerMask.NameToLayer("item");
    }
}
