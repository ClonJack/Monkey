using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Body
{
    public List<PartMonkey> partMonkeys = new List<PartMonkey>();
    public List<Transform> sequence = new List<Transform>();
    public List<Transform> sequencePlyer = new List<Transform>();

    public int idPart;

    public float speedMovePart;
}
