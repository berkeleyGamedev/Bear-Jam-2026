using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneUI : MonoBehaviour
{
    public List<Transform> children;
    private int index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
        for(int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }
        foreach(Transform child in children)
        {
            child.gameObject.SetActive(false);
        }
        children[0].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(index >= children.Count)
            {
                gameObject.SetActive(false);
                return;
            }
            children[index].gameObject.SetActive(false);
            index++;
            children[index].gameObject.SetActive(true);
        }
    }
}
