using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindList : MonoBehaviour
{
    private Stack<GameObject> pilePlateform = new Stack<GameObject>();
    [SerializeField]
    private int nbPlateform;

    public void Update()
    {
        nbPlateform = pilePlateform.Count;
    }

    public Stack<GameObject> getRewindList()
    {
        return pilePlateform;
    }
    
}
