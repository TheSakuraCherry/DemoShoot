﻿using System;
using Core.Character;
using Core.Util;
using UnityEngine;

namespace Core.Combat.Projectiles
{
    public abstract class AbstractProjectile : MonoBehaviour
    {
        public int damage;
        public ParticleSystem explosionEffect;
        public AudioClip splatterSound;

        public GameObject Shooter { get; set; }

        protected Vector2 force;

        public event Action<AbstractProjectile> OnProjectileDestroyed;
    
        public abstract void SetForce(Vector2 force);

        protected virtual void DestroyProjectile()
        {
            OnProjectileDestroyed?.Invoke(this);
        
            if (splatterSound != null)
                SoundManager.Instance.PlaySoundAtLocation(splatterSound, transform.position, 0.75f);

            EffectManager.Instance.PlayOneShot(explosionEffect, transform.position);

            Destroy(gameObject);
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            // Can't shoot yourself
            if (collision.gameObject == Shooter)
                return;

            // Projectile hit player
            var player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 force = this.force.normalized;
                player.Hurt((int)damage, new Vector2(force.x * 1000.0f,1000));
            }
        
            DestroyProjectile();
        }
    }
}