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
    TESTING
}

public class EditorManagerScript : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GameObject interfaceEditor;
    [SerializeField] private GameObject content;
    [SerializeField] private RewindList RewindComponent;
    [SerializeField] private Text infoEtat;

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
        tweakNbSaut.EditMode = true;
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

                        if(item.GetComponent<ArriveeScript>() != null)
                        {
                            item.tag = "custom";
                        }

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

                bonusTweakGO.GetComponent<BonusJump>().SetPlayer(tweakNbSaut);

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

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.1f, playerTransform.position.z);
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    playerTransform.position = new Vector3(playerTransform.position.x - 0.1f, playerTransform.position.y, playerTransform.position.z);
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    playerTransform.position = new Vector3(playerTransform.position.x + 0.1f, playerTransform.position.y, playerTransform.position.z);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    etat = EtatEditor.NOSELECT;

                }
                break;

            case EtatEditor.TESTING :

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    etat = EtatEditor.NOSELECT;
                    tweakNbSaut.EditMode = true;
                    tweakNbSaut.getRigidBody().isKinematic = true;
                }
                break;

            default :

                if (Input.GetKeyDown(KeyCode.Space) && etat == EtatEditor.NOSELECT && rootGO != null && etat == EtatEditor.NOSELECT)
                {
                    tweakNbSaut.EditMode = false;
                    tweakNbSaut.getRigidBody().isKinematic = false;
                    etat = EtatEditor.TESTING;
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

        UpdateInfoEtat();
    }

    private void UpdateInfoEtat()
    {
        switch(etat)
        {
            case EtatEditor.PLATEFORMESELECTED:
                infoEtat.text = "Placement de plateforme";
                infoEtat.color = Color.grey;
                break;

            case EtatEditor.BONUSNBJUMPSELECTED:
                infoEtat.text = "Placement d'un bonus de nombre de saut";
                infoEtat.color = Color.green;
                break;

            case EtatEditor.BONUSPWJUMPSELECTED:
                infoEtat.text = "Placement d'un bonus de puissance de saut";
                infoEtat.color = Color.magenta;
                break;

            case EtatEditor.BONUSSPEEDSELECTED:
                infoEtat.text = "Placement d'un bonus de vitesse de déplacement";
                infoEtat.color = Color.yellow;
                break;

            case EtatEditor.NOSELECT:
                infoEtat.text = "En attente";
                infoEtat.color = Color.white;
                break;

            case EtatEditor.SCALING:
                infoEtat.text = "Redimensionnement";
                infoEtat.color = Color.red;
                break;

            case EtatEditor.TESTING:
                infoEtat.text = "Test en cours";
                infoEtat.color = Color.blue;
                break;

            case EtatEditor.TWEAKPLAYERPOSITION:
                infoEtat.text = "Paramétrage de la position de départ";
                infoEtat.color = Color.black;
                break;

            case EtatEditor.TWEAKNBSAUT:
                infoEtat.text = "Paramétrage de la limite de saut";
                infoEtat.color = Color.black;
                break;

            case EtatEditor.TWEAKBONUSNBJUMP:
                infoEtat.text = "Paramétrage de la valeur du bonus";
                infoEtat.color = Color.green;
                break;

            case EtatEditor.TWEAKBONUSPWJUMP:
                infoEtat.text = "Paramétrage de la valeur du bonus";
                infoEtat.color = Color.magenta;
                break;

            case EtatEditor.TWEAKBONUSSPEED:
                infoEtat.text = "Paramétrage de la valeur du bonus";
                infoEtat.color = Color.yellow;
                break;

            default:
                break;
        }
    }
}
