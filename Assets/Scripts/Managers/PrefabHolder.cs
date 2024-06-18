using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{ 
    public static PrefabHolder Prefabs { get; private set; }

    [SerializeField]
    private List<GameObject> list = new List<GameObject>();


    public GameObject GetByTag(string tag)
    {
        foreach(GameObject item in list)
            if (item.tag == tag) 
                return item;
        throw new System.Exception("Tag " + tag + " not found in prefab list");
    }


    private PrefabHolder() { }

    public void Awake()
    {
        if (Prefabs != null)
            return;

        Prefabs = this;
    }
}
