using Mystie.Utils;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystie
{
    public class Laser : MonoBehaviour
    {
        public bool on;

        [SerializeField] protected Transform emitPoint;
        [SerializeField] protected Transform anchor;
        [SerializeField] protected float angle = 0f;
        [SerializeField] protected float range = 10f;
        [SerializeField] protected LayerMask mask = -1;

        public Vector3 Vector { get => transform.up.XY().Rotate(-angle); }

        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] private float overlapCircleRadius = 0.1f;

        void Start()
        {

        }

        void FixedUpdate()
        {
            if (!on || anchor == null) return;

            //Collider2D collider = Physics2D.OverlapCircle(emitPoint.position, overlapCircleRadius, groundLayer);
            RaycastHit2D hit = Physics2D.Raycast(emitPoint.position, Vector, range, mask);
            bool hasHit = hit;

            Vector2 anchorPos = emitPoint.position + Vector * range;
            if (hit) anchorPos = hit.point;
            anchor.position = anchorPos;

            /*
            if (collider != null)
            {
                anchor.position = emitPoint.position + Vector * overlapCircleRadius;
                hasHit = false;
            }*/

            anchor.gameObject.SetActive(hasHit);
        }

        [Button]
        public void SetOn()
        {
            on = true;
        }

        [Button]
        public void SetOff()
        {
            on = false;
            if (anchor != null)
            {
                anchor.position = emitPoint.position;
                anchor.gameObject.SetActive(false);
            }

        }

        public void OnValidate()
        {
            angle = Mathf.Clamp(angle, -180f, 180f);

            if (!Application.isPlaying && emitPoint != null && anchor != null)
            {
                anchor.position = emitPoint.position + Vector * range;
            }
        }
    }
}
