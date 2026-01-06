using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class L_Digger : MonoBehaviour
{
    [SerializeField]protected int attack;
    public abstract void Dig();
}
