using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EtatEditor
{
    PLATEFORMESELECTED,
    BONUSNBJUMPSELECTED,
    BONUSPWJUMPSELECTED,
    BONUSSPEEDSELECTED,
    TWEAKBONUSNBJUMP,
    TWEAKBONUSPWJUMP,
    TWEAKBONUSSPEED,
    TWEAKNBSAUT,
    TWEAKPLAYERPOSITION,
    NOSELECT,
    SCALING,
}

public class EditorManagerScript : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject interfaceEditor;
    [SerializeField] private GameObject content;
    [SerializeField] private RewindList RewindComponent;

    [Header("Liste des items")]
    [SerializeField] private List<GameObject> listePrefabs;

    [SerializeField]
    private PlayerControleurScript tweakNbSaut;

    [SerializeField] private Transform playerTransform;

    private EtatEditor etat = EtatEditor.NOSELECT;
    private GameObject currentItem = null;
    private GameObject rootGO;
    private GameObject scalingGO;
    private GameObject bonusTweakGO;
    
    
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
        if(t == TypeItem.BONUSNBJUMP)
        {
            currentItem = g;
            etat = EtatEditor.BONUSNBJUMPSELECTED;
        }

        if (t == TypeItem.BONUSPWJUMP)
        {
            currentItem = g;
            etat = EtatEditor.BONUSPWJUMPSELECTED;
        }


        if (t == TypeItem.BONUSSPEED)
        {
            currentItem = g;
            etat = EtatEditor.BONUSSPEEDSELECTED;
        }

        if (t == TypeItem.PLATEFORME)
        {
                currentItem = g;
                etat = EtatEditor.PLATEFORMESELECTED;
        }
    }

    //update, gérer les clics en fonction de l'état
    private void Update()
    {

        switch(etat)
        {
            case EtatEditor.PLATEFORMESELECTED:
                
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
                break;

            case EtatEditor.BONUSNBJUMPSELECTED:
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
                        currentItem = null;
                        bonusTweakGO = item;
                        etat = EtatEditor.TWEAKBONUSNBJUMP;
                        Debug.Log("On passe en teawkBonus !" + etat);
                    }
                }
                break;

            case EtatEditor.BONUSPWJUMPSELECTED:
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
                        currentItem = null;
                        bonusTweakGO = item;
                        etat = EtatEditor.TWEAKBONUSPWJUMP;
                        Debug.Log("On passe en teawkBonus !" + etat);


                    }
                }
                break;

            case EtatEditor.BONUSSPEEDSELECTED:
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
                        currentItem = null;
                        bonusTweakGO = item;
                        etat = EtatEditor.TWEAKBONUSSPEED;
                        Debug.Log("On passe en teawkBonus !" + etat);

                    }
                }
                break;


            case EtatEditor.SCALING:

                if (Input.GetKey(KeyCode.LeftArrow) && scalingGO.transform.localScale.x > 0.5f)
                {
                    scalingGO.transform.localScale = new Vector3(scalingGO.transform.localScale.x - 0.1f, scalingGO.transform.localScale.y, scalingGO.transform.localScale.z);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    scalingGO.transform.localScale = new Vector3(scalingGO.transform.localScale.x + 0.1f, scalingGO.transform.localScale.y, scalingGO.transform.localScale.z);
                }

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    scalingGO.transform.localScale = new Vector3(scalingGO.transform.localScale.x, scalingGO.transform.localScale.y + 0.1f, scalingGO.transform.localScale.z);
                    scalingGO.transform.position = new Vector3(scalingGO.transform.position.x, scalingGO.transform.position.y + 0.05f, scalingGO.transform.position.z);
                }
                if (Input.GetKey(KeyCode.DownArrow) && scalingGO.transform.localScale.y > 0.5f)
                {
                    scalingGO.transform.localScale = new Vector3(scalingGO.transform.localScale.x, scalingGO.transform.localScale.y - 0.1f, scalingGO.transform.localScale.z);
                    scalingGO.transform.position = new Vector3(scalingGO.transform.position.x, scalingGO.transform.position.y - 0.05f, scalingGO.transform.position.z);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Destroy(scalingGO);
                }
                break;

            case EtatEditor.TWEAKBONUSNBJUMP:

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    bonusTweakGO.GetComponent<BonusSaut>().setValueBonus(1);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    bonusTweakGO.GetComponent<BonusSaut>().setValueBonus(-1);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;
                    bonusTweakGO = null;
                }
                break;

            case EtatEditor.TWEAKBONUSPWJUMP:

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    bonusTweakGO.GetComponent<BonusJump>().setValueBonus(1);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    bonusTweakGO.GetComponent<BonusJump>().setValueBonus(-1);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;
                    bonusTweakGO = null;
                }
                break;

            case EtatEditor.TWEAKBONUSSPEED:

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    bonusTweakGO.GetComponent<BonusSpeed>().setValueBonus(1);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    bonusTweakGO.GetComponent<BonusSpeed>().setValueBonus(-1);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;
                    bonusTweakGO = null;
                }
                break;

            case EtatEditor.TWEAKNBSAUT:

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    tweakNbSaut.AddJumps(1);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) && tweakNbSaut.getlimiteSaut() > 0)
                {
                    tweakNbSaut.AddJumps(-1);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;
                    
                }

                break;

            case EtatEditor.TWEAKPLAYERPOSITION:

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + 0.1f, playerTransform.position.z);
                }

                if (Input.GetKey(KeyCode.DownArrow) && tweakNbSaut.getlimiteSaut() > 0)
                {
                    playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.1f, playerTransform.position.z);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerTransform.position = new Vector3(playerTransform.position.x - 0.1f, playerTransform.position.y, playerTransform.position.z);
                }

                if (Input.GetKey(KeyCode.DownArrow) && tweakNbSaut.getlimiteSaut() > 0)
                {
                    playerTransform.position = new Vector3(playerTransform.position.x + 0.1f, playerTransform.position.y, playerTransform.position.z);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;

                }
                break;
        }

        if ((Input.GetKeyDown(KeyCode.V) && rootGO!= null && etat == EtatEditor.NOSELECT)) {

            etat = EtatEditor.TWEAKNBSAUT;

        }

        if ((Input.GetKeyDown(KeyCode.P) && rootGO != null && etat == EtatEditor.NOSELECT))
        {

            etat = EtatEditor.TWEAKPLAYERPOSITION;

        }

        if (Input.GetKeyDown(KeyCode.Space) && etat == EtatEditor.NOSELECT && rootGO != null && etat == EtatEditor.NOSELECT)
        {
            tweakNbSaut.getRigidBody().isKinematic = false;
            Debug.Log("test du niveau édité enclanché !");
        }

        Debug.Log(etat);


    }
    
}
