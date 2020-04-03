using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeScript : MonoBehaviour
{
    [SerializeField] private Transform PFtransform;

    //changer les dimensions de la plateforme
    public void RescalePlateforme(float scaleX, float scaleY)
    {
        PFtransform.localScale = new Vector3(scaleX, scaleY, 2);
    }

    //placer la plateforme
    public void Reposition(Vector3 position)
    {
        PFtransform.position = position;
    }
}
