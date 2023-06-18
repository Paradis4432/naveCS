using System;
using System.Collections.Generic;
using System.Media;

namespace Game
{
    public class Meteors : Cuerpo
    {
        public int rotateSpeed;
        public Meteors(Vector2 navePos, Vector2 metPos) : base(metPos)
        {
            rotateSpeed = 0;
            this.dir = Vector2.Normalize(new Vector2(navePos.x - metPos.x, navePos.y - metPos.y));
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
            if (this.alive) Engine.Draw(Engine.GetTexture("meteor.png"),
                this.pos.x, this.pos.y, this.rad, this.rad, this.ang, 12 * this.rad, 12 * this.rad);

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), this.pos.x, this.pos.y, 2, 2);
        }

        public void Update()
        {
            if (Outside()) Kill();
            this.ang += rotateSpeed;
            Move();
            this.CalcularFisica(1F);
        }

        // actualizar para que tome las esquinas y no el centro 
        public bool Outside()
        {
            return (this.pos.x > 800 || this.pos.x < 0 || this.pos.y > 600 || this.pos.y < 0);
        }

        public void Kill()
        {
            this.alive = false;
        }

        // convierte en angulo en radian para girar el objeto en direccion 
        private float CalcRadians(float ang)
        {
            return (float)(ang * Math.PI / 180f);
        }
    }
}
