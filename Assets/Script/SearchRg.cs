using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRg : MonoBehaviour
{
    [SerializeField] float maxAttractDistance = 5f; // 最大吸引距离
    [SerializeField] float maxForce = 10f; // 最大吸力
    [SerializeField] LayerMask targetLayer; // 仅吸引指定图层的物体（优化性能）
    private Rigidbody2D magnetRb;

    void Awake()
    {
        magnetRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // 查找指定图层中所有带 Rigidbody2D 的物体（减少性能消耗）
        Rigidbody2D[] targetRbs = FindObjectsOfType<Rigidbody2D>();
        foreach (var targetRb in targetRbs)
        {
            // 跳过磁铁自身 + 非目标图层的物体
            if (targetRb == magnetRb || !IsInTargetLayer(targetRb.gameObject))
                continue;

            // 关键修改：将磁铁的 Vector3 坐标转为 Vector2（忽略 z 轴）
            Vector2 magnetPos = transform.position; 
            Vector2 targetPos = targetRb.position;

            // 计算距离（统一为 Vector2 运算）
            float distance = Vector2.Distance(magnetPos, targetPos);
            if (distance > maxAttractDistance)
                continue;

            // 计算吸力方向（从目标指向磁铁）
            Vector2 forceDir = (magnetPos - targetPos).normalized;
            // 吸力随距离衰减（远弱近强）
            float force = maxForce * (1 - distance / maxAttractDistance);
            // 施加吸力（FixedUpdate 中用 ForceMode2D.Force 更稳定）
            targetRb.AddForce(forceDir * force, ForceMode2D.Force);
        }
    }
    // 辅助方法：判断物体是否在目标图层
    private bool IsInTargetLayer(GameObject obj)
    {
        return (targetLayer.value & (1 << obj.layer)) != 0;
    }
}
