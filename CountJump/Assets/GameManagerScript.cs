using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private List<InfoNiveauScript> listeNiveaux;
    [SerializeField] private PlayerControleurScript joueur;
    [SerializeField] private EditorManagerScript editor;

    private InfoNiveauScript currentLevel;
    private int compteurLevel = 0;

    private void Start()
    {
        //lancer le premier niveau
        currentLevel = listeNiveaux[compteurLevel];
        currentLevel.StartLevel();
    }

    public void EndLevel()
    {
        currentLevel.EndLevel();

        //passer au lv suivant
        compteurLevel++;
        if(compteurLevel < listeNiveaux.Count)
        {
            currentLevel = listeNiveaux[compteurLevel];
            currentLevel.StartLevel();
        }
    }

    //si l'utilisateur presse la touche E
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OpenEditor();
        }
    }

    private void OpenEditor()
    {
        //désactiver tous les décors
        foreach(var lv in listeNiveaux)
        {
            lv.gameObject.SetActive(false);
        }

        //reset les compteurs de sauts et de rewind
        joueur.ResetCompteurs(this.transform);

        //passer le joueur en kinetic pour qu'il ne tombe pas
        joueur.getRigidBody().isKinematic = true;

        //afficher l'interface de l'éditeur
        editor.ActiverInterface();
    }
}
