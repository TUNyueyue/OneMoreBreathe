using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlanetCharacter : PlanetEntity
{

    [Header("Jump")]
    [SerializeField] float groundCheckLength;
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded;

    [Header("Move")]
    [SerializeField] float moveSpeed;
    protected bool faceright = true;
    protected int faceDir = 1;


    [Header("Planet")]
    [SerializeField]protected Transform PlanetTrans;
    public Vector2 vecToPlanet;

    protected virtual void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, -this.transform.up, groundCheckLength, groundLayer);
        //检测是否接触地面
        FootToPlanet();
    }

    void FootToPlanet()
    {
        vecToPlanet = PlanetTrans.position - this.transform.position;
        Vector3 direction = -vecToPlanet;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    void OnDrawGizmos()
    {
        Vector3 groundCheckPoint = transform.position - this.transform.up * groundCheckLength;
        Gizmos.DrawLine(transform.position, groundCheckPoint);
    }
    //调试

    //XInput>0为向右走
    protected void Move(float XInput)
    {
        Vector2 tangentDir = new Vector2(-vecToPlanet.y, vecToPlanet.x).normalized;
        float currentSpeed = Vector2.Dot(rb.velocity, tangentDir);
        //点积单位向量算投影，即当前切向速度
        float targetSpeed = XInput * moveSpeed;

        float newSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, 10f * Time.fixedDeltaTime);
        float speedChange = newSpeed - currentSpeed;
        Vector2 velocityChange = tangentDir * speedChange;
        rb.velocity += velocityChange;
    }
    protected void Flip()
    {
        faceDir *= -1;
        faceright = !faceright;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    

    protected void Jump()
    {
        if (isGrounded)
            rb.AddForce(this.transform.up * jumpForce * 10f);
    }


}
