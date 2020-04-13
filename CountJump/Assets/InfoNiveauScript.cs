using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoNiveauScript : MonoBehaviour
{
    [SerializeField] private int nbSauts;
    [SerializeField] GameObject depart;
    [SerializeField] PlayerControleurScript joueur;
    [SerializeField] List<GameObject> bonus;

    public void StartLevel()
    {
        gameObject.SetActive(true);
        joueur.ReplacePlayer(depart.transform.position + new Vector3(0f, 2f, 0f));
        joueur.SetJumpLimit(nbSauts);
        joueur.ResetCompteurs();

        ActiverBonus();
    }

    public void EndLevel()
    {
        gameObject.SetActive(false);
    }

    private void ActiverBonus()
    {
        foreach(var b in bonus)
        {
            b.SetActive(true);
        }
    }
}
