using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace Game {
    // aplicar singleton a Ship porque solo puede haber 1 Nave
    public class Ship {
        public Cuerpo Nave { get; private set; }
        private Vector2 initialPos;

        private Animation cAnimation;
        private Animation foward;
        private Animation back;
        private Animation idle;
        private Animation explote;

        public bool exploded = false;

        private Ship(Vector2 initialPos) {
            this.initialPos = initialPos;
            Nave = new Cuerpo(initialPos);
            Nave.speed = 0.2F;
            Nave.rad = 30;

            foward = AnimationsManager.CreateAnimation("foward", "ship_foward", 5, 20, false);
            back = AnimationsManager.CreateAnimation("back", "ship_backward", 5, 20, false);
            idle = AnimationsManager.CreateAnimation("idle", "ship_foward", 2, 1, false);
            explote = AnimationsManager.CreateAnimation("explote", "ship_explote", 9, 15, false);

            cAnimation = idle;
            cAnimation.Reset();

        }

        private static Ship instance;

        public static Ship GetInstance() {
            if (instance == null) { instance = new Ship(new Vector2(375, 275)); }
            return instance;
        }

        public void Move(bool backwards) {
            cAnimation = backwards ? back : foward;
            Vector2 newPos;
            newPos.x = Nave.dir.x * (backwards ? -Nave.speed : Nave.speed);
            newPos.y = Nave.dir.y * (backwards ? -Nave.speed : Nave.speed);

            Nave.AplicarFuerza(newPos);

        }

        public void Rotate(float toRotate) {
            //Nave.ang += toRotate;
            Nave.AplicarTorque(toRotate);

        }


        public void Draw() {

            if (exploded)
            {
                cAnimation = explote;
                Nave.vel = Vector2.zero;
            }

            if (Nave.alive) {
                Engine.Debug(cAnimation.Id);
                Engine.Draw(cAnimation.CurrentFrame, Nave.pos.x, Nave.pos.y, 1, 1, Nave.ang, 35, 20);
            }

            foreach (var bullet in Program.bulletsShoot.ToList())
            {
                bullet.Draw();
            }

            Engine.Draw(Engine.GetTexture("dotGREEN.png"), Nave.pos.x, Nave.pos.y, 2, 2);

        }

        public void Shoot(int bullets) {
            if (exploded) return;
            Program.bulletsShoot.Add(new Bullet(Nave.pos, Nave.dir, Nave.ang));
        }

        public void Update() {
            cAnimation.Update();
            

            if (Engine.GetKey(Keys.A)) Rotate(-80);
            if (Engine.GetKey(Keys.D)) Rotate(+80);
            if (Engine.GetKey(Keys.W)) Move(false);
            else if (Engine.GetKey(Keys.S)) Move(true);
            else if (!exploded) {
                cAnimation.Reset();
                cAnimation = idle;
            }



            if (Engine.GetKeyDown(Keys.SPACE)) Shoot(1);

            if (Outside()) Kill();

            Nave.dir = new Vector2((float)Math.Cos(CalcRadians(Nave.ang)),
                (float)Math.Sin(CalcRadians(Nave.ang)));

            //Nave.AplicarFriccion(1, 0.05f);
            Nave.CalcularFisica(1F);
        }

        public bool Outside() {
            return (Nave.pos.x > 800 - 25 || Nave.pos.x < 0 + 25 ||
                Nave.pos.y > 600 - 25 || Nave.pos.y < 0 + 25);
        }

        public void Kill() {
            exploded = true;

        }

        public void SetAlive(bool b) { Nave.alive = b; }

        // convierte en angulo en radian para girar la Nave en direccion 
        private float CalcRadians(float ang) {
            return (float)(ang * Math.PI / 180f);
        }

        public void Reset() {
            Nave.alive = true;
            Nave.pos = initialPos;
            Nave.ace = new Vector2(0, 0);
            Nave.vel = new Vector2(0, 0);
            Nave.dir = new Vector2(1, 0);

            Nave.ang = 0;
            Nave.aang = 0;
            Nave.vang = 0;

            Program.bulletsShoot = new List<Bullet>();

            cAnimation.Reset();
            cAnimation = idle;
            
            exploded = false;

        }
    }
}
