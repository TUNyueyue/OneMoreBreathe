using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCamera : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] PlanetPlayer player;
    [SerializeField] float playerCameraSize = 2f;
    //[SerializeField] BaseCraft craft;
    //[SerializeField] float craftCameraSize = 3f;
    //备用
    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    
    void Update()
    {
        FollowPlayer();
        
    }
    //牢代码打赢复活赛了
    void FollowPlayer()
    {
        mainCamera.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        mainCamera.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, -player.vecToPlanet.normalized);
        mainCamera.orthographicSize = playerCameraSize;
    }
    //void FollowCraft()
    //{
    //    mainCamera.gameObject.transform.position = new Vector3(craft.transform.position.x, craft.transform.position.y, -10f);
    //    mainCamera.gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, -craft.vecToPlanet.normalized);
    //    mainCamera.orthographicSize = craftCameraSize;
    //}
}
