using Core.Character;
using Core.Hazards;
using Core.Util;
using UnityEngine;

namespace Core.Combat.Projectiles
{
    public class Bullet : AbstractProjectile
    {
        public float speed = 5.0f;

        private Vector3 direction;
        private Vector3 velocity;
        public override void SetForce(Vector2 force)
        {
            this.force = force;
            transform.localScale = new Vector3(force.x, 1, 1);
            direction = force.normalized;
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            // Can't shoot yourself
            if (collision.gameObject == Shooter)
                return;
            if(collision.gameObject.layer == gameObject.layer)
                return;
            if (collision.gameObject.GetComponent<Hazard>())
            {
                return;
            }
            // Projectile hit player
            var hittable = collision.GetComponent<Hittable>();
            if (hittable != null)
            {
                hittable.OnAttackHit(collision.transform.position, Vector2.zero, damage);
                
                CameraController.Instance.ShakeCamera(0.05f, 0.5f);
            }
        
            DestroyProjectile();
        }
        void Update()
        {
            //velocity += direction * speed * Time.deltaTime;
            transform.position += direction * speed * Time.deltaTime;
        }

        protected override void DestroyProjectile()
        {
            
            if (splatterSound != null) 
                SoundManager.Instance.PlaySoundAtLocation(splatterSound, transform.position, 0.75f);

            EffectManager.Instance.PlayOneShot(explosionEffect, transform.position);
            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject);
            
        }
    }
}