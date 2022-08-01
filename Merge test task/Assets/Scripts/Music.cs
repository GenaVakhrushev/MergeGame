using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("music").Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
