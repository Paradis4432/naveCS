using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;

namespace Game
{
    // aplicar singleton a Ship porque solo puede haber 1 Nave
    public class Ship : Cuerpo, IKillable
    {
        // public Cuerpo Nave { get; private set; }
        // private Vector2 initialPos;
        public Vector2 initialPos;
        private Animation cAnimation;
        private Animation foward;
        private Animation back;
        private Animation idle;
        private Animation explote;
        // private static GameManager gameManager = Program.gameManager;
        public bool exploded = false;

        private Ship(Vector2 initialPos) : base(initialPos, new Vector2(35, 20))
        {
            this.initialPos = initialPos;
            // Nave = new Cuerpo(initialPos);
            // en vez de llamar a cuerpo con initial transform.getPosition() lo heredamos con base(initialPos)
            this.speed = 0.2F;
            this.rad = 30;

            foward = AnimationsManager.CreateAnimation("foward", "ship_foward", 5, 20, false);
            back = AnimationsManager.CreateAnimation("back", "ship_backward", 5, 20, false);
            idle = AnimationsManager.CreateAnimation("idle", "ship_foward", 2, 1, false);
            explote = AnimationsManager.CreateAnimation("explote", "ship_explote", 9, 15, false);

            cAnimation = idle;
            cAnimation.Reset();

        }

        private static Ship instance;

        public static Ship GetInstance()
        {
            if (instance == null) { instance = new Ship(new Vector2(375, 275)); }
            return instance;
        }

        public void Move(bool backwards)
        {
            cAnimation = backwards ? back : foward;
            Vector2 newPos;
            newPos.x = this.dir.x * (backwards ? -this.speed : this.speed);
            newPos.y = this.dir.y * (backwards ? -this.speed : this.speed);

            this.AplicarFuerza(newPos);

        }

        public void Rotate(float toRotate)
        {
            this.AplicarTorque(toRotate);
        }


        public void Draw()
        {
            if (Program.debug) Console.WriteLine("ship draw debug 0");

            if (exploded)
            {
                if (Program.debug) Console.WriteLine("ship draw debug 1");
                cAnimation = explote;
                if (Program.debug) Console.WriteLine("ship draw debug 2");
                this.vel = Vector2.zero;
                if (Program.debug) Console.WriteLine("ship draw debug 3");
            }

            if (Program.debug) Console.WriteLine("ship draw debug 4");

            if (this.alive)
            {
                if (Program.debug) Console.WriteLine("ship draw debug 5");
                if (Program.debug) Engine.Debug(cAnimation.Id);
                if (Program.debug) Console.WriteLine("ship draw debug 6");
                // Engine.Draw(cAnimation.CurrentFrame, this.transform.getPosition().x, this.transform.getPosition().y, 1, 1, this.transform.getRotation(), 35, 20);
                Renderer.Render(cAnimation.CurrentFrame, this.transform);
                if (Program.debug) Console.WriteLine("ship draw debug 7");
            }

            if (Program.debug) Console.WriteLine("ship draw debug 8");

            // foreach (var bullet in Program.gameManager.bulletsShoot.ToList())
            // {
            if (Program.debug) // Console.WriteLine("ship draw debug 9");
                // bullet.Draw();
                if (Program.debug) // Console.WriteLine("ship draw debug 10");
                                   // }

                    if (Program.debug) Console.WriteLine("ship draw debug 11");
            // este es debug
            Engine.Draw(Engine.GetTexture("dotGREEN.png"), this.transform.getPosition().x, this.transform.getPosition().y, 2, 2);

            if (Program.debug) Console.WriteLine("ship draw debug 12");
        }


        public void Shoot(int bullets)
        {
            if (Program.debug1) Console.WriteLine("shoot");
            if (exploded) return;
            // Program.gameManager.bulletsShoot.Add(new Bullet(this.transform.getPosition(), this.dir, this.ang));
            if (Program.debug1) Console.WriteLine("shoot 2");
            // Program.gameManager.bulletsShoot.Add(GameManager.bulletPool.Get(this.transform.getPosition(), this.dir, this.ang));

            // Program.gameManager.bulletPool.(this.transform.getPosition(), this.dir, this.ang);
            // GameManager.bulletPool.Get(this.transform.getPosition(), this.dir, this.ang);
            Bullet bullet = GameManager.bulletPool.Get(this.transform.getPosition(), this.dir, this.transform.getRotation());
            if (Program.debug1) Console.WriteLine("shoot 3");
            GameManager.bulletPool.AddActiveBullet(bullet);
        }

        public void Update()
        {
            cAnimation.Update();


            if (Engine.GetKey(Keys.A)) Rotate(-80);
            if (Engine.GetKey(Keys.D)) Rotate(+80);
            if (Engine.GetKey(Keys.W)) Move(false);
            else if (Engine.GetKey(Keys.S)) Move(true);
            else if (!exploded)
            {
                cAnimation.Reset();
                cAnimation = idle;
            }



            if (Engine.GetKeyDown(Keys.SPACE)) Shoot(1);

            if (Outside()) Kill();

            this.dir = new Vector2((float)Math.Cos(CalcRadians(this.transform.getRotation())),
                (float)Math.Sin(CalcRadians(this.transform.getRotation())));

            //this.AplicarFriccion(1, 0.05f);
            this.CalcularFisica(1F);
        }

        public bool Outside()
        {
            return (this.transform.getPosition().x > 800 - 25 || this.transform.getPosition().x < 0 + 25 ||
                this.transform.getPosition().y > 600 - 25 || this.transform.getPosition().y < 0 + 25);
        }

        public void Kill()
        {
            exploded = true;

        }

        public void SetAlive(bool b) { this.alive = b; }

        // convierte en angulo en radian para girar la this en direccion 
        private float CalcRadians(float ang)
        {
            return (float)(ang * Math.PI / 180f);
        }

        public void Reset()
        {
            this.alive = true;
            Console.WriteLine(initialPos.x);
            Console.WriteLine(initialPos.y);
            Console.WriteLine(this.transform.getPosition().x);
            Console.WriteLine(this.transform.getPosition().y);
            // this.transform.setPosition(initialPos);
            Console.WriteLine(this.transform.getPosition().x);
            Console.WriteLine(this.transform.getPosition().y);
            this.ace = new Vector2(0, 0);
            this.vel = new Vector2(0, 0);
            this.dir = new Vector2(1, 0);

            // this.ang = 0;
            this.transform.setRotation(0);
            this.aang = 0;
            this.vang = 0;

            // Program.gameManager.bulletsShoot = new List<Bullet>();

            cAnimation.Reset();
            cAnimation = idle;

            exploded = false;
        }
    }
}
