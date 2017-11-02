using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvelineAnimations : MonoBehaviour {

    public Animator evelineAnimation;
    public PlayerController pc;
    public CharacterController cc;

    public bool isFirstPerson;
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public SkinnedMeshRenderer[] evelineMesh;

    public bool isJumping;
    public float direction;
    public float runSpeed;

    void Start()
    {
        pc = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {

        //Setup First Person and Third Person switch

        if(Input.GetKeyDown(KeyCode.V)) //Change with InputManager in final build
        {
            isFirstPerson = !isFirstPerson;
        }

        if (isFirstPerson)
        {
            firstPersonCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
            pc.m_Camera = firstPersonCamera;
            for (int i = 0; i< evelineMesh.Length; i++)
            {
                evelineMesh[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
        } else
        {
            firstPersonCamera.gameObject.SetActive(false);
            thirdPersonCamera.gameObject.SetActive(true);
            pc.m_Camera = thirdPersonCamera;
            for (int i = 0; i < evelineMesh.Length; i++)
            {
                evelineMesh[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }

        isJumping = pc.m_Jumping;
        runSpeed = cc.velocity.magnitude / cc.velocity.magnitude;

        evelineAnimation.SetBool("isJumping", isJumping);
        evelineAnimation.SetFloat("RunSpeed", runSpeed);
        evelineAnimation.SetFloat("Direction", direction);

    }

}
