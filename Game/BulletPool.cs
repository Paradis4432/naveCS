using System;
using System.Collections.Generic;
using System.Media;

namespace Game
{
    public class BulletPool
    {
        public Queue<Bullet> pool;
        public List<Bullet> activeBullets = new List<Bullet>();

        public BulletPool()
        {
            this.pool = new Queue<Bullet>();
        }

        public Bullet Get(Vector2 position, Vector2 direction, float angle)
        {
            Console.WriteLine($"Bullet Get debug 0: position={position}, direction={direction}, angle={angle}");
            Console.WriteLine($"Bullet Get debug 0.1: pool.Count={pool.Count}");
            Console.WriteLine($"Bullet Get debug 0.2: activeBullets.Count={activeBullets.Count}");

            if (pool.Count > 0)
            {
                Console.WriteLine($"Bullet Get debug 1: pool.Count={pool.Count}");
                Bullet item = pool.Dequeue();
                Console.WriteLine("Bullet Get debug 2");
                item.Reinitialize(position, direction, angle);
                Console.WriteLine("Bullet Get debug 3");
                return item;
            }
            else
            {
                Console.WriteLine("Bullet Get debug 4");
                Bullet bullet = new Bullet(position, direction, angle);
                // pool.Enqueue(bullet);
                // return new Bullet(position, direction, angle);
                return bullet;
            }
        }


        public void Return(Bullet item)
        {
            activeBullets.Remove(item);
            pool.Enqueue(item);
        }

        public void Clear()
        {
            pool.Clear();
        }

        public Queue<Bullet> GetPool()
        {
            return pool;
        }

        public void AddActiveBullet(Bullet bullet)
        {
            // pool.Enqueue(bullet);
            activeBullets.Add(bullet);
        }
    }

}