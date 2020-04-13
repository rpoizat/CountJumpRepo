using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private List<InfoNiveauScript> listeNiveaux;

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
}
