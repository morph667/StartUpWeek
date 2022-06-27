using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{

    // Scripts playerinventory
    PlayerInventory playerInv;

    //Animations du perso
    Animation animations;

    //vitesse de deplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    // variable attaque
    public float attackCooldown;
    private bool isAttacking;
    private float currentCooldown;
    public float attackRange;
    public GameObject rayHit;

    //inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;
    
    //mort du personnage ?
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        playerInv = gameObject.GetComponent<PlayerInventory>();
        rayHit = GameObject.Find("RayHit");
    }

    bool IsGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f, layerMask:3);
    }

    // Update is called once per frame
    void Update()
    {   
        if (!isDead) { 
            //avance
            if(Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift)){
                transform.Translate(0,0, walkSpeed*Time.deltaTime);

                if (!isAttacking)
                {
                    animations.Play("walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                
            }
            //courir
            if(Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift )){
                transform.Translate(0,0, runSpeed*Time.deltaTime);
                animations.Play("run");
            }

            //recul
            if(Input.GetKey(inputBack)){
                transform.Translate(0,0, -(walkSpeed/2) *Time.deltaTime);

                if (!isAttacking)
                {
                    animations.Play("walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

            //repos
            if(!Input.GetKey(inputFront) && !Input.GetKey(inputBack)){

                if (!isAttacking)
                {
                    animations.Play("idle");
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
            }

            //rotation a gauche
            if(Input.GetKey(inputLeft)){
                transform.Rotate(0, -turnSpeed*Time.deltaTime, 0);
            }

            //rotation a droite
            if(Input.GetKey(inputRight)){
                transform.Rotate(0, turnSpeed*Time.deltaTime, 0);
            }

            //si on saute
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                //preparation du saut (necessaire en c#)
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                //saut
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
            }
        }

        if (isAttacking)
        {
            currentCooldown -= Time.deltaTime;
        }

        if(currentCooldown <= 0)
        {
            currentCooldown = attackCooldown;
            isAttacking = false;
        }
    }

    // Fonction d'attaque
    public void Attack()
    {
        if (!isAttacking)
        {
            animations.Play("attack");

            RaycastHit hit;

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<enemyAi>().ApplyDammage(playerInv.currentDamage);
                }

            }
            isAttacking = true;
        }

    }

}
