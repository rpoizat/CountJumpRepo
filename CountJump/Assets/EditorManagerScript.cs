using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

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

    [SerializeField] private GameObject arrivePrefab;
    [SerializeField] private GameObject departPrefab;
    [SerializeField] private GameObject plateformePrefab;
    [SerializeField] private GameObject bonusSpeedPrefab;
    [SerializeField] private GameObject bonusPwJumpPrefab;
    [SerializeField] private GameObject bonusNbJumpPrefab;
    [SerializeField] private GameObject playerPrefab;


    //fonction de sauvegarde du menu custom
    public void SaveCustomLevel()
    {
        //création du fichier de sauvegarde dans le dossier Asset
        var writer = File.CreateText(Application.dataPath.ToString() + "/saveCustom.txt");

        //boucle sur chaque objet fils du gameobject racine
        for (int i = 0; i < rootGO.transform.childCount; i++)
        {
            GameObject go = rootGO.transform.GetChild(i).gameObject;

            //condition pour chaque composant du jeu on stock son nom:sa_position:son_scale(ou sa valeur dans le cas d'un bonus)
            if(go.name.Contains("Depart"))
            {
                writer.WriteLine("start:" + go.transform.position.x + ":" + go.transform.position.y + ":" + go.transform.position.z
                                + ":" + go.transform.localScale.x + ":" + go.transform.localScale.y + ":" + go.transform.localScale.z);
            }
            else
            {
                if(go.name.Contains("Arrivee"))
                {
                    writer.WriteLine("end:" + go.transform.position.x + ":" + go.transform.position.y + ":" + go.transform.position.z
                                + ":" + go.transform.localScale.x + ":" + go.transform.localScale.y + ":" + go.transform.localScale.z);
                }
                else
                {
                    if(go.name.Contains("Plateforme"))
                    {
                        writer.WriteLine("plateforme:" + go.transform.position.x + ":" + go.transform.position.y + ":" + go.transform.position.z
                                + ":" + go.transform.localScale.x + ":" + go.transform.localScale.y + ":" + go.transform.localScale.z);
                    }
                    else
                    {
                        if(go.name.Contains("Bonus5"))
                        {
                            writer.WriteLine("bonus5:" + go.transform.position.x + ":" + go.transform.position.y + ":" + go.transform.position.z
                                + ":" + go.GetComponent<BonusSaut>().getValueBonus());
                        }
                        else
                        {
                            if(go.name.Contains("BonusJ"))
                            {
                                writer.WriteLine("BonusJ:" + go.transform.position.x + ":" + go.transform.position.y + ":" + go.transform.position.z
                                + ":" + go.GetComponent<BonusJump>().getValueBonus());
                            }
                            else
                            {
                                if(go.name.Contains("BonusS"))
                                {
                                    writer.WriteLine("BonusS:" + go.transform.position.x + ":" + go.transform.position.y + ":" + go.transform.position.z
                                    + ":" + go.GetComponent<BonusSpeed>().getValueBonus());
                                }
                            }
                        }
                    }
                }
            }
        }

        writer.WriteLine("player:" + tweakNbSaut.transform.position.x + ":" + tweakNbSaut.transform.position.y + ":" + tweakNbSaut.transform.position.z);
        writer.WriteLine("limiteNbSaut:" + tweakNbSaut.getlimiteSaut());

        writer.Close();
    }

    public void loadCustomLevel()
    {
        string path = Application.dataPath.ToString() + "/saveCustom.txt";

        //Read the text from directly from the test.txt file
      
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
            // Read and display lines from the file until the end of 
            // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    
                    List<string> gameObjectInfo = line.Split(new char[] { ':' }).ToList();

                    switch (gameObjectInfo[0])
                    {
                        case "plateforme":

                            GameObject plateforme = Instantiate<GameObject>(plateformePrefab);
                            plateforme.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            plateforme.transform.localScale = new Vector3(float.Parse(gameObjectInfo[4]), float.Parse(gameObjectInfo[5]), float.Parse(gameObjectInfo[6]));
                            plateforme.transform.SetParent(rootGO.transform);
                            RewindTester rtester = plateforme.GetComponent<RewindTester>();
                            rtester.SetRewindComponent(RewindComponent);
                            break;

                        case "start":

                            GameObject start = Instantiate<GameObject>(departPrefab);
                            start.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            start.transform.localScale = new Vector3(float.Parse(gameObjectInfo[4]), float.Parse(gameObjectInfo[5]), float.Parse(gameObjectInfo[6]));
                            start.transform.SetParent(rootGO.transform);
                            RewindTester rtesterStart = start.GetComponent<RewindTester>();
                            rtesterStart.SetRewindComponent(RewindComponent);
                            break;

                        case "end":

                            GameObject end = Instantiate<GameObject>(arrivePrefab);
                            end.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            end.transform.localScale = new Vector3(float.Parse(gameObjectInfo[4]), float.Parse(gameObjectInfo[5]), float.Parse(gameObjectInfo[6]));
                            end.transform.SetParent(rootGO.transform);
                            RewindTester rtesterEnd = end.GetComponent<RewindTester>();
                            rtesterEnd.SetRewindComponent(RewindComponent);
                            break;

                        case "player":

                            
                            playerPrefab.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            break;

                        case "bonus5":

                            GameObject Bonus5 = Instantiate<GameObject>(bonusNbJumpPrefab);
                            Bonus5.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            Bonus5.GetComponent<BonusSaut>().instantiateValueBonus(int.Parse(gameObjectInfo[4]));
                            Debug.Log(gameObjectInfo[4]);
                            Bonus5.transform.SetParent(rootGO.transform);
                            break;

                        case "BonusJ":

                            GameObject BonusJ = Instantiate<GameObject>(bonusPwJumpPrefab);
                            BonusJ.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            BonusJ.GetComponent<BonusJump>().instantiateValueBonus(int.Parse(gameObjectInfo[4]));
                            BonusJ.transform.SetParent(rootGO.transform);
                            BonusJ.GetComponent<BonusJump>().SetPlayer(playerPrefab.GetComponent<PlayerControleurScript>());
                            break;

                        case "BonusS":

                            GameObject BonusS = Instantiate<GameObject>(bonusSpeedPrefab);
                            BonusS.transform.position = new Vector3(float.Parse(gameObjectInfo[1]), float.Parse(gameObjectInfo[2]), float.Parse(gameObjectInfo[3]));
                            BonusS.GetComponent<BonusSpeed>().instantiateValueBonus(int.Parse(gameObjectInfo[4]));
                            BonusS.transform.SetParent(rootGO.transform);
                            break;

                        case "limiteNbSaut":

                        tweakNbSaut.SetJumpLimit(int.Parse(gameObjectInfo[1]));
                        break;

                    }
                }
            }

                    




    }
    
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
                    tweakNbSaut.setNbSaut(0);

                    //boucle sur chaque objet fils du gameobject racine pour réactiver les bonus ramassés
                    for (int i = 0; i < rootGO.transform.childCount; i++)
                    {
                        GameObject go = rootGO.transform.GetChild(i).gameObject;

                        if (go.activeSelf == false) go.SetActive(true);
                    }
                }
                break;

            default :

                if (Input.GetKeyDown(KeyCode.Space) && etat == EtatEditor.NOSELECT && rootGO != null && etat == EtatEditor.NOSELECT)
                {
                    tweakNbSaut.EditMode = false;
                    tweakNbSaut.getRigidBody().isKinematic = false;
                    etat = EtatEditor.TESTING;
                    tweakNbSaut.setNbSaut(0);
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
