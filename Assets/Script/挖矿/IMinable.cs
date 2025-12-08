using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinable
{
   int Durability { get; }
    void Consume(int value);
}
