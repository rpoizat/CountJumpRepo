using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManagerScript : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject interfaceEditor;
    [SerializeField] private GameObject content;

    [Header("Liste des items")]
    [SerializeField] private List<GameObject> listePrefabs;
    
    public void ActiverInterface()
    {
        interfaceEditor.SetActive(true);

        foreach(var g in listePrefabs)
        {
            GameObject item = Instantiate<GameObject>(g);
            ScrollItemScript itemInfo = item.GetComponent<ScrollItemScript>();
            item.transform.SetParent(content.transform);
        }
    }
}
