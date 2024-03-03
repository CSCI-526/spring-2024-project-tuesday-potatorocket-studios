using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnim : MonoBehaviour
{
    public Animator anim;

    private float timer;
    

    public GameObject sword;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("swordanimation"))
        {
            timer += Time.deltaTime;
            sword.SetActive(false);
        }

        if (timer >= 1)
        {
            sword.SetActive(true);
            anim.Play("swordanimation", -1, 0f);
            timer = 0f;
        }
        
    }
    
    bool AnimatorIsPlaying(){
        return anim.GetCurrentAnimatorStateInfo(0).length >
               anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
