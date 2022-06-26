using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAnimator : MonoBehaviour
{
    
    [System.Serializable]
    public class Animation
    {
        public string name;
        public float fps;
        public bool loop;
        public Sprite[] sprites;
    }

    public SpriteRenderer spriteRenderer;
    public Animation[] animations;
    public string startAnimation;

    private int frameLength;
    private int currentFrame;
    private string currentAnim;

    private IEnumerator coroutine;

    private void OnEnable()
    {
        PlayAnimation(startAnimation);
    }

    public void PlayAnimation(string name)
    {
        
        if (currentAnim == name)
            return;

        if (coroutine != null)
            StopCoroutine(coroutine);

        foreach (Animation anim in animations)
        {
            if (anim.name == name)
            {
                currentAnim = name;
                currentFrame = 0;
                frameLength = anim.sprites.Length;
                coroutine = Animate(anim);
                StartCoroutine(coroutine);
                break;
            }
        }
    }

    IEnumerator Animate(Animation anim)
    {

        while (anim.loop || currentFrame < frameLength)
        {

            spriteRenderer.sprite = anim.sprites[currentFrame];

            yield return new WaitForSeconds(1f / anim.fps);

            currentFrame++;

            if (currentFrame >= frameLength && anim.loop)
            {
                currentFrame = 0;
            }

        }

    }

}
