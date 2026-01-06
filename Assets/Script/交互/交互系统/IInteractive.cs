using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive
//强迫症福音，怎么改都不舒服
//人类一思考，上帝就发笑.jpg
{
    GameObject Tip { get; set; }
    void Interact(PlayerHand hand);
    //哈耶克的大手，饿啊！
    void EmergeTip(bool isEmerge)
    {
        if (this.Tip == null) return;
        this.Tip.SetActive(isEmerge);
    }
}
