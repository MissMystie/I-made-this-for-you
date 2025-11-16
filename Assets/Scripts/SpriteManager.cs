using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mystie.Utils;
using UnityEngine;

namespace Mystie
{
    public class SpriteManager : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer masterSprite;
        [SerializeField] public Animator anim { get; protected set; }
        [SerializeField] protected Rigidbody2D rb;

        public int faceDir = 1;
        public bool faceVelocityDir = true;
        public float changeDirMin = 2f;
        public bool overrideColor = true;
        public bool overrideAlpha = true;
        [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

        public string groundedAnim = "grounded";
        public string movespeedAnim = "moveSpeed";

        private Material baseMaterial;

        protected void Awake()
        {
            baseMaterial = masterSprite.sharedMaterial;

            if (masterSprite == null) masterSprite = GetComponentInChildren<SpriteRenderer>();
            if (anim == null) anim = GetComponentInChildren<Animator>();
            if (rb == null) rb = GetComponentInParent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            UpdateFaceDir();
            UpdateSprites();
            UpdateAnimation();
        }

        protected virtual void UpdateFaceDir()
        {
            if (rb != null)
            {
                if (faceVelocityDir)
                {
                    if (Math.Abs(rb.linearVelocityX) > changeDirMin)
                        faceDir = Math.Sign(rb.linearVelocityX);
                }
                //else
                //faceDir = phys.faceDir;
            }
        }

        public virtual void UpdateSprites()
        {
            if (masterSprite == null) return;

            //flips sprite
            Vector3 scale = masterSprite.transform.localScale;
            scale.x = Mathf.Sign(faceDir) * Mathf.Abs(scale.x);
            masterSprite.transform.localScale = scale;

            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprite != null)
                {
                    float alpha = sprite.color.a;

                    if (overrideColor) sprite.color = masterSprite.color;

                    if (overrideAlpha) sprite.SetAlpha(masterSprite.color.a);
                    else sprite.SetAlpha(alpha);

                    sprite.sharedMaterial = masterSprite.sharedMaterial;
                    sprite.sortingLayerID = masterSprite.sortingLayerID;
                }
            }
        }

        public void SetMaterial(Material mat)
        {
            masterSprite.sharedMaterial = mat;
            UpdateSprites();
        }

        public void ResetMaterial()
        {
            SetMaterial(baseMaterial);
        }

        protected virtual void UpdateAnimation()
        {
            if (anim != null)
            {
                //anim.SetFloat(movespeedAnim, phys.body.linearVelocity.x);
                //anim.SetBool(groundedAnim, phys.isGrounded);
            }
        }

        public static implicit operator SpriteRenderer(SpriteManager spriteManager)
        {
            return spriteManager.masterSprite;
        }

        private void Reset()
        {
            masterSprite = GetComponent<SpriteRenderer>();
            sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
            sprites.Remove(masterSprite);
        }

        private void OnValidate()
        {
            if (sprites.Contains(masterSprite))
                sprites.Remove(masterSprite);
        }
    }
}
