using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalOre : MonoBehaviour, IAttackable
{
    [field: SerializeField] public int Health { get; set; }

    [SerializeField] DropItem dropItem;
    [SerializeField] List<Sprite> oreStateSprites;
    [SerializeField] List<Collider2D> colliders;
    public AttackedType type { get; set; }

    int maxHealth;
    SpriteRenderer spriteRenderer;

    public event Action OnFinished;
    void Awake()
    {
        type = AttackedType.Ore;
        maxHealth = Health;
        SetCollider(0);
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void OnEnable()
    {
        OnFinished += OnOreBroken;
    }
    void OnDisable()
    {
        OnFinished -= OnOreBroken;
    }
    public void OnAttack(int damage)
    {
        Health -= damage;
        CheckState();
        if (Health <= 0)
            OnFinished.Invoke();
    }

    void OnOreBroken()
    {
        for (int i = 0; i < dropItem.num; i++)
        { Instantiate(dropItem.objectToDrop, this.transform.position, Quaternion.identity); }
        Destroy(gameObject);
    }

    void CheckState()
    {
        if (Health <= maxHealth * 2 / 3)
        {
            spriteRenderer.sprite = oreStateSprites[0];
            SetCollider(1);
        }
        if (Health <= maxHealth / 3)
        {
            spriteRenderer.sprite = oreStateSprites[1];
            SetCollider(2);
        }
    }

    void SetCollider(int index)
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i].enabled = false;
        }
        colliders[index].enabled = true;
    }
}
