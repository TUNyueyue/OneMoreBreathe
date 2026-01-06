using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface L_IMinable
{
   int Durability { get; }
    void Consume(int value);
}
