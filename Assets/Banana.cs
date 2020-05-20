using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    [SerializeField] private float speedRotateBanana;
    [SerializeField] private Transform spinnerBanana;

    public void RotateBonana() => spinnerBanana.transform.Rotate(new Vector3(0, 0, 10) * Time.deltaTime * speedRotateBanana);

}
