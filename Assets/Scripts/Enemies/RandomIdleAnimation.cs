using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Этот класс управляет случайной анимацией бездействия.
public class RandomIdleAnimation : MonoBehaviour
{
    private Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

//вызывается при старте игры. Запускает случайную анимацию бездействия.
    private void Start() {
        if (!myAnimator) { return; }

        AnimatorStateInfo state = myAnimator.GetCurrentAnimatorStateInfo(0);
        myAnimator.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
}
