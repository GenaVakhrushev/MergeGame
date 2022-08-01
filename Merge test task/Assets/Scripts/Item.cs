using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int Level;

    //предмет следующего уровня, который получется при объединении
    public GameObject NextItem;

    //куда нужно плавно передвинуть предмет
    Vector2 destination;

    float speed = 0.1f;
    float delta = 0.001f;

    //нужно ли перемещать предмет
    bool moving = false;

    private void Start()
    {
        if (GetComponent<Chest>())
            SetDestination(new Vector2(0, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(DragController.mousePos, Vector2.zero, 100, LayerMask.GetMask("item"));
            if(hit.collider)
            if (Level == 5 && hit.collider.gameObject == gameObject)
            {
                FX.singleton.Play(FX.singleton.Destr);

                GameObject cast = Instantiate(FX.singleton.LightCast, transform.position, Quaternion.identity);
                Destroy(cast, 0.5f);

                Destroy(gameObject);

                Text score = GameObject.FindGameObjectWithTag("score").GetComponent<Text>();
                score.text = (int.Parse(score.text) + 1).ToString();
                RaycastHit2D hitSlot = Physics2D.Raycast(DragController.mousePos, Vector2.zero, 100, LayerMask.GetMask("slot"));
                hitSlot.transform.gameObject.GetComponent<Slot>().IsEmpty = true;
            }
        }
    }

    //для плавного перемещения
    private void FixedUpdate()
    {
        if (moving)
        {
            Vector2 dir = destination - (Vector2)transform.position;
            transform.Translate(dir * speed);
            if (dir.magnitude < delta)
                moving = false;
        }
    }

    public void SetDestination(Vector2 newDestination)
    {
        destination = newDestination;
        moving = true;
    }
}
