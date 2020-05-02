using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EtatEditor
{
    PLATEFORMESELECTED,
    BONUSSELECTED,
    NOSELECT,
    SCALING
}

public class EditorManagerScript : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject interfaceEditor;
    [SerializeField] private GameObject content;
    [SerializeField] private RewindList RewindComponent;

    [Header("Liste des items")]
    [SerializeField] private List<GameObject> listePrefabs;

    private EtatEditor etat = EtatEditor.NOSELECT;
    private GameObject currentItem = null;
    private GameObject rootGO;
    private GameObject scalingGO;
    
    //lancement du mode éditeur
    public void ActiverInterface()
    {
        interfaceEditor.SetActive(true);

        foreach(var g in listePrefabs)
        {
            GameObject item = Instantiate<GameObject>(g);
            ScrollItemScript itemInfo = item.GetComponent<ScrollItemScript>();

            itemInfo.SetEditor(this);
            Button itemButton = item.GetComponent<Button>();
            itemButton.interactable = true;
            item.transform.SetParent(content.transform);
        }

        rootGO = new GameObject();
        rootGO.transform.position = new Vector3(0f, 0f, 30f);
        rootGO.name = "CustomLevel";
    }

    //définir l'item qui vient d'être sélectionné par l'utilisateur
    public void SetCurrentItem(GameObject g, TypeItem t)
    {
        if(t == TypeItem.BONUS)
        {
            currentItem = g;
            etat = EtatEditor.BONUSSELECTED;
        }
        else
        {
            if(t == TypeItem.PLATEFORME)
            {
                currentItem = g;
                etat = EtatEditor.PLATEFORMESELECTED;
            }
        }
    }

    //update, gérer les clics en fonction de l'état
    private void Update()
    {
        //si l'utilisateur a déjà selectionné un item à placer
        if(etat == EtatEditor.BONUSSELECTED)
        {
            //clic droit pour déselectionner l'item
            if(Input.GetMouseButtonDown(1))
            {
                currentItem = null;
                etat = EtatEditor.NOSELECT;
            }
            else
            {
                //clic gauche pour le placer
                if(Input.GetMouseButtonDown(0))
                {
                    //placer l'item
                    Vector3 p = Input.mousePosition;
                    p.z = 30f;
                    Vector3 pos = Camera.main.ScreenToWorldPoint(p);
                    var item = Instantiate<GameObject>(currentItem);
                    item.transform.position = new Vector3(pos.x, pos.y, 30f);
                    item.transform.SetParent(rootGO.transform);
                    currentItem = null;
                    etat = EtatEditor.NOSELECT;
                }
            }
        }
        else
        {
            if(etat == EtatEditor.PLATEFORMESELECTED)
            {
                //clic droit pour déselectionner l'item
                if (Input.GetMouseButtonDown(1))
                {
                    currentItem = null;
                    etat = EtatEditor.NOSELECT;
                }
                else
                {
                    //clic gauche pour le placer
                    if (Input.GetMouseButtonDown(0))
                    {
                        //placer l'item
                        Vector3 p = Input.mousePosition;
                        p.z = 30f;
                        Vector3 pos = Camera.main.ScreenToWorldPoint(p);

                        var item = Instantiate<GameObject>(currentItem);
                        item.transform.position = new Vector3(pos.x, pos.y, 30f);
                        item.transform.SetParent(rootGO.transform);

                        RewindTester rtester = item.GetComponent<RewindTester>();
                        rtester.SetRewindComponent(RewindComponent);

                        currentItem = null;
                        etat = EtatEditor.SCALING;
                        scalingGO = item;
                    }
                }
            }
            else
            {
                //on vient de placer une plateforme que l'on souhaite scale
                if(etat == EtatEditor.SCALING)
                {
                    if(Input.GetKey(KeyCode.LeftArrow))
                    {
                        scalingGO.transform.localScale = new Vector3(scalingGO.transform.localScale.x - 0.1f, scalingGO.transform.localScale.y, scalingGO.transform.localScale.z);
                    }
                    if(Input.GetKey(KeyCode.RightArrow))
                    {
                        scalingGO.transform.localScale = new Vector3(scalingGO.transform.localScale.x + 0.1f, scalingGO.transform.localScale.y, scalingGO.transform.localScale.z);
                    }
                    if(Input.GetMouseButtonDown(0))
                    {
                        etat = EtatEditor.NOSELECT;
                    }
                    if(Input.GetMouseButtonDown(1))
                    {
                        Destroy(scalingGO);
                    }
                }
            }
        }
    }
}
