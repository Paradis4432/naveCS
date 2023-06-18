using System;
using System.Collections.Generic;
using System.Media;

namespace Game
{
    public class Bullet : Cuerpo
    {
        public Bullet(Vector2 initialPos, Vector2 dir, float ang) : base(initialPos)
        {
            this.ang = ang;
            this.dir = dir;
            this.speed = 3F;
            this.rad = 5;
        }

        public void Move()
        {

            Vector2 newPos;
            newPos.x = this.dir.x * this.speed;
            newPos.y = this.dir.y * this.speed;

            this.AplicarAceleracion(newPos);

        }

        public void Draw()
        {

            if (this.alive)
            {
                // add offset 
                Engine.Draw(Engine.GetTexture("bullet.png"), this.pos.x, this.pos.y, 1, 1, this.ang, 0, 0);
            }

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), this.pos.x, this.pos.y, 2, 2);

        }

        public void Update()
        {
            if (Outside()) Kill();
            Move();
            this.CalcularFisica(1F);
        }

        public bool Outside()
        {
            return (this.pos.x > 1000 ||
                this.pos.x < 0 ||
                this.pos.y > 1000 ||
                this.pos.y < 0);
        }

        public void Kill()
        {
            this.alive = false;
            GameManager.bulletPool.Return(this);
        }

        public void Reinitialize(Vector2 position, Vector2 direction, float angle)
        {
            // Reset bullet's state
            this.pos = position;
            this.dir = direction;
            this.ang = angle;
            this.alive = true;
            this.speed = 3;
        }

    }
}

