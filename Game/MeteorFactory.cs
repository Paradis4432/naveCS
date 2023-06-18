using System;
using System.Collections.Generic;
using System.Media;

namespace Game
{
    public enum MeteorType { Normal, Big, Small }
    public static class MeteorFactory
    {

        public static Meteors CreateMeteor(Vector2 navePos, Vector2 metPos, MeteorType type)
        {
            Meteors met = new Meteors(navePos, metPos);
            switch (type)
            {
                case MeteorType.Normal:
                    met.met.rad = 1;
                    met.met.speed = 0.4F;
                    met.rotateSpeed = new Random().Next(30);
                    break;
                case MeteorType.Big:
                    met.met.rad = 2;
                    met.met.speed = 0.2F;
                    met.rotateSpeed = new Random().Next(10);
                    break;
                case MeteorType.Small:
                    met.met.rad = 0.5f;
                    met.met.speed = 0.6F;
                    met.rotateSpeed = new Random().Next(50);
                    break;
            }
            return met;
        }
    }
}