using System;   
using System.Collections.Generic;
using System.Media;

namespace Game {
    // aplicar singleton a Ship porque solo puede haber 1 nave
    public struct Ship {

        //float x;
        //float y;
        //float vx;
        //float vy;
        //float ax;
        //float ay;
        //float ang;
        //float vang;
        //float aang;
        //float masa;

        //static Vector2 pos = new Vector2(100,100);
        //static Vector2 vel = new Vector2(0,0);
        //static Vector2 ace = new Vector2(0,0);
        //float ang;
        //float masa;
        //static Vector2 velAceAng = new Vector2(0,0);
        //static Vector2 dir = new Vector2(0,0);
        //static bool alive = true;
        //static int speed = 5;

        Cuerpo nave;
        private Vector2 initialPos;

        public Ship(Vector2 initialPos) {
            this.initialPos = initialPos;
            nave = new Cuerpo(initialPos);
            nave.speed = 0.2F;
            nave.rad = 30;
        }

        public Cuerpo GetNave() { return nave; }

        public void Move(bool backwards) {

            //float radians = Mathf.Deg2Rad * ang;


            //this.dir = new Vector2(Mathf.cos(r));
            //Ship.pos.x += Ship.dir.x * speed;
            //Ship.pos.y += Ship.dir.y * speed;

            Vector2 newPos;
            newPos.x = nave.dir.x * (backwards ? -nave.speed : nave.speed);
            newPos.y = nave.dir.y * (backwards ? -nave.speed : nave.speed);

            nave.AplicarFuerza(newPos);
        }

        public void Rotate(float toRotate) {
            //nave.ang += toRotate;
            nave.AplicarTorque(toRotate);
            
        }
        

        public void Draw() {

            if (nave.alive) {
                Engine.Draw(Engine.GetTexture("ship1.png"), nave.pos.x, nave.pos.y, 1, 1, nave.ang, 28, 23);
                
                //Engine.Draw(Engine.GetTexture("ship.png"), nave.pos.x, nave.pos.y, 1, 1, 90, 28, 23);
                //Engine.Draw(Engine.GetTexture("ship.png"), nave.pos.x, nave.pos.y, 1, 1, 180, 28, 23);
                //Engine.Draw(Engine.GetTexture("ship.png"), nave.pos.x, nave.pos.y, 1, 1, 250, 28, 23);
                //Engine.Draw(Engine.GetTexture("ship.png"), nave.pos.x, nave.pos.y, 1, 1, 360, 28, 23);
                //Engine.Draw(Engine.GetTexture("ship.png"), nave.pos.x, nave.pos.y, 1, 1, nave.ang, 28, 23);

            }

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), nave.pos.x, nave.pos.y, 2,2);

        }

        public void Update() {
            if (Engine.GetKey(Keys.A)) Rotate(-80);
            if (Engine.GetKey(Keys.D)) Rotate(+80);
            if (Engine.GetKey(Keys.W)) Move(false);

            if (Engine.GetKey(Keys.S)) Move(true);

            if (Outside()) Kill();

            nave.dir = new Vector2((float)Math.Cos(CalcRadians(nave.ang)),
                (float)Math.Sin(CalcRadians(nave.ang)));


            //nave.AplicarFriccion(1, 0.05f);
            nave.CalcularFisica(1F);
        }

        // actualizar para que tome las esquinas y no el centro 
        public bool Outside() {
            return (nave.pos.x > 800 - 25 || nave.pos.x < 0 + 25 ||
                nave.pos.y > 600 - 25 || nave.pos.y < 0 + 25);
        }

        public void Kill() {
            nave.alive = false;
            Program.SetStage(GameStage.Lost);
            
        }

        public void SetAlive(bool b) { nave.alive = b; }

        // convierte en angulo en radian para girar la nave en direccion 
        private float CalcRadians(float ang) {
            return (float)(ang * Math.PI / 180f);
        }

        public void Reset() {
            nave.alive = true;
            nave.pos = initialPos;
            nave.ace = new Vector2(0, 0);
            nave.vel = new Vector2(0, 0);
            nave.dir = new Vector2(1, 0);

            nave.ang = 0;
            nave.aang = 0;
            nave.vang = 0;
        }
    }
}
