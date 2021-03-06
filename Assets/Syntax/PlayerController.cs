﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Use this for initialization
    Animator anim; //animator dari player
    Rigidbody2D rigid; //rigidbody dari player
    public bool isGrounded = false; //untuk menyimpan state apakah karakter berada di ground
    public bool isFacingRight = true;
    public float jumpForce = 420; //besarnya gaya untuk mengangkat karakter ke atas
    public float walkForce = 5; //besarnya gaya untuk mendorong karakter ke samping
    public float maxSpeed = 10; //kecepatan maksimum dari karakter utama
                                //obyek peluru
    public GameObject projectile;
    //kecepatan peluru
    public Vector2 projectileVelocity = new Vector2(50, 0);
    //jarak posisi peluru dari posisi karakter
    public Vector2 projectileOffset = new Vector2(0.75f, -0.104f);
    //jeda waktu untuk menembak
    public float cooldown = 1f;
    bool canShoot = true;
    public AudioSource Temak;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    //digunakan untuk mengecek collision yang baru terjadi
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
            Debug.Log("IsGround");
        }
    }
    //digunakan untuk mengecek collision yang telah terjadi
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("IsGrounded", true);
            isGrounded = true;
            Debug.Log("IsGround");
        }
    }
    //digunakan untuk mengecek collision yang telah terjadi
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("IsGrounded", false);
            isGrounded = false;
            Debug.Log("NotGround");
        }
    }
    // Update is called once per frame
    void Update()
    {
        InputHandler();
        anim.SetInteger("Speed", (int)rigid.velocity.x);
    }
    void InputHandler()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        //inputan untuk menembak
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    void MoveLeft()
    {
        if (rigid.velocity.x * -1 < maxSpeed)
            rigid.AddForce(Vector2.left * walkForce);
        //membalik arah karakter apabila menghadap ke arah yang berlawanan dari seharusya
        if (isFacingRight)
        {
            Flip();
        }
    }
    void MoveRight()
    {
        if (rigid.velocity.x * 1 < maxSpeed)
            rigid.AddForce(Vector2.right * walkForce);
        //membalik arah karakter apabila menghadap ke arah yang berlawanan dari seharusya
        if (!isFacingRight)
        {
            Flip();
        }
    }
    void Jump()
    {
        rigid.AddForce(Vector2.up * jumpForce);
    }
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        isFacingRight = !isFacingRight;
    }
    void Fire()
    {
        //jika karakter dapat menembak
        if (canShoot)
        {
            anim.SetTrigger("Shoot");
            float scaleX = 1;
            if (!isFacingRight) scaleX = -1;
            //Membuat projectile baru
            GameObject bullet = (GameObject)Instantiate(projectile, (Vector2)transform.position + projectileOffset * scaleX, Quaternion.identity);
            //Mengatur kecepatan dari projectile.
            Vector2 velocity = new Vector2(projectileVelocity.x * transform.localScale.x, projectileVelocity.y);
            bullet.GetComponent<Rigidbody2D>().velocity = velocity;
            //Menyesuaikan scale dari projectile dengan scale karakter
            CanShoot();
            Temak.Play();
        }
    }
    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}