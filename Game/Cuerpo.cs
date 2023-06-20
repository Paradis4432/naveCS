using System;
using System.Collections.Generic;
using System.Media;

namespace Game {
    public class Cuerpo {
        // public Vector2 pos { get; set; }
        public Vector2 vel { get; set; }
        public Vector2 ace { get; set; }
        public Vector2 dir { get; set; }

        // public float ang { get; set; }
        // public Transform transform { get; set; }
        public Transform transform;
        public float vang { get; set; }
        public float aang { get; set; }

        public float masa { get; set; }
        public bool alive { get; set; }
        public float speed { get; set; }
        public float rad { get; set; }

        public Cuerpo(Vector2 pos) {
            transform = new Transform(pos, 0, new Vector2(1, 1));
            // this.pos = pos;
            Console.WriteLine($"Cuerpo debug 0: pos={pos.x}");
            Console.WriteLine($"Cuerpo debug 0.1: pos={pos.y}");
            transform.setPosition(pos);
            Console.WriteLine($"Cuerpo debug 1: transform.getPosition()={transform.getPosition().x}");
            Console.WriteLine($"Cuerpo debug 2: transform.getPosition()={transform.getPosition().y}");
            vel = new Vector2(0, 0);
            ace = new Vector2(0, 0);
            // ang = 0;
            transform.setRotation(0);

            vang = 0;
            aang = 0;
            masa = 3;
            alive = true;
            speed = 1;
            rad = 0;
            dir = new Vector2(1, 0);
        }

        public void CalcularFisica(float dt) {
            // a -> v -> x
            vel = new Vector2(vel.x + ace.x * dt, vel.y + ace.y * dt);
            transform.setPosition(new Vector2(transform.getPosition().x + vel.x * dt, transform.getPosition().y + vel.y * dt));

            vang += aang * dt;
            // ang += vang * dt;
            transform.setRotation(transform.getRotation() + vang * dt);

            aang = 0;
            ace = Vector2.zero;
        }

        public void AplicarFuerza(Vector2 f) {
            ace = new Vector2(ace.x + f.x / masa, ace.y + f.y / masa);
        }

        public void AplicarAceleracion(Vector2 f) {
            vel = f;
        }

        public void AplicarTorque(float t) {
            aang += t / (masa * 500);
        }

        public void AplicarFriccion(float dt, float mult) {
            Vector2 fric = new Vector2(-vel.x * mult, -vel.y * mult);

            AplicarFuerza(fric);
        }
    }
}