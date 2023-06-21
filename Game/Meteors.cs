using System;
using System.Collections.Generic;
using System.Media;

namespace Game
{
    public class Meteors : Cuerpo , IGameOject, IKillable
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
                this.transform.getPosition().x, this.transform.getPosition().y, this.rad, this.rad, this.transform.getRotation(), 12 * this.rad, 12 * this.rad);

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), this.transform.getPosition().x, this.transform.getPosition().y, 2, 2);
        }

        public void Update()
        {
            if (Outside()) Kill();
            this.transform.setRotation(this.transform.getRotation() + rotateSpeed);
            Move();
            this.CalcularFisica(1F);
        }

        // actualizar para que tome las esquinas y no el centro 
        public bool Outside()
        {
            return (this.transform.getPosition().x > 800 || this.transform.getPosition().x < 0 || this.transform.getPosition().y > 600 || this.transform.getPosition().y < 0);
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
