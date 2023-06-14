using System;
using System.Collections.Generic;
using System.Media;

namespace Game {
    public struct Meteors {
        public Cuerpo met { get; private set; }
        private int rotateSpeed;
        
        public Meteors(Vector2 navePos, Vector2 metPos) {
            met = new Cuerpo(metPos);
            met.dir = Vector2.Normalize(new Vector2(navePos.x - metPos.x,navePos.y - metPos.y));
            met.rad = 25;
            rotateSpeed = new Random().Next(30);
        }
            
        public void Move() {
            Vector2 newPos;
            newPos.x = met.dir.x * met.speed;
            newPos.y = met.dir.y * met.speed;

            met.AplicarAceleracion(newPos);
        }

        public void Rotate(float toRotate) {
            //nave.ang += toRotate;
            //nave.AplicarTorque(toRotate);

        }


        public void Draw() {
            if (met.alive) Engine.Draw(Engine.GetTexture("meteor.png"),
                met.pos.x, met.pos.y, 1, 1, met.ang, 12,12);

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), met.pos.x, met.pos.y, 2, 2);
        }

        public void Update() {
            if (Outside()) Kill();
            //Engine.Debug("updating met");
            //Engine.Debug(this.met.dir.x);
            //Engine.Debug(this.met.dir.y);
            met.ang += rotateSpeed;
            //Engine.Debug(rotateSpeed);
            Move();
            //nave.dir = new Vector2((float)Math.Cos(CalcRadians(nave.ang)),
             //   (float)Math.Sin(CalcRadians(nave.ang)));


            met.CalcularFisica(1F);
        }

        // actualizar para que tome las esquinas y no el centro 
        public bool Outside() {
            return (met.pos.x > 800 || met.pos.x < 0 || met.pos.y > 600 || met.pos.y < 0);
        }

        public void Kill() {
            //nave.alive = false;
            met.alive = false;
        }

        public void SetAlive(bool b) { //nave.alive = b;
                                       }

        // convierte en angulo en radian para girar el objeto en direccion 
        private float CalcRadians(float ang) {
            return (float)(ang * Math.PI / 180f);
        }

        public void Reset() {
            //nave.alive = true;
            //nave.pos = new Vector2(100, 100);
            //nave.ace = new Vector2(0, 0);
            //nave.vel = new Vector2(0, 0);
            //nave.dir = new Vector2(1, 0);
            //
            //nave.ang = 0;
            //nave.aang = 0;
            //nave.vang = 0;


        }
    }
}
