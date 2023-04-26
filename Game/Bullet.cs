using System;
using System.Collections.Generic;
using System.Media;

namespace Game {
    public class Bullet {
        public Cuerpo Bala { get; private set; }

        public Bullet(Vector2 initialPos, Vector2 dir, float ang) {
            Bala = new Cuerpo(initialPos);
            Bala.ang = ang; 
            Bala.dir = dir; 
            Bala.speed = 3F;
            Bala.rad = 5;
        }

        public void Move() {

            Vector2 newPos;
            newPos.x = Bala.dir.x * Bala.speed;
            newPos.y = Bala.dir.y * Bala.speed;

            Bala.AplicarAceleracion(newPos);

        }

        public void Draw() {

            if (Bala.alive) {
                // add offset 
                Engine.Draw(Engine.GetTexture("bullet.png"), Bala.pos.x, Bala.pos.y, 1, 1, Bala.ang, 0,0);
            }

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), Bala.pos.x, Bala.pos.y, 2, 2);

        }

        public void Update() {
            
            if (Outside()) Kill();

            Move();

            Engine.Debug("updating bullet dirx: " + Bala.dir.x);
            Engine.Debug("updating bullet diry: " + Bala.dir.y);
            Engine.Debug("updating bullet speed: " + Bala.speed);
            Engine.Debug("updating bullet posx: " + Bala.pos.x);
            Engine.Debug("updating bullet posy: " + Bala.pos.y);

            //Nave.AplicarFriccion(1, 0.05f);
            Bala.CalcularFisica(1F);
        }

        public bool Outside() {
            return (Bala.pos.x > 1000 ||
                Bala.pos.x < 0 ||
                Bala.pos.y > 1000 ||
                Bala.pos.y < 0);
        }

        public void Kill() {
            Bala.alive = false;
        }

    }
}

