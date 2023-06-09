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
                    met.rad = 1;
                    met.speed = 0.8F;
                    met.rotateSpeed = new Random().Next(30);
                    met.transform.setOffset(new Vector2(met.rad * 12, met.rad * 12));

                    break;
                case MeteorType.Big:
                    met.rad = 2;
                    met.speed = 0.4F;
                    met.rotateSpeed = new Random().Next(10);
                    met.transform.setOffset(new Vector2(met.rad * 12, met.rad * 12));
                    break;
                case MeteorType.Small:
                    met.rad = 0.5f;
                    met.speed = 1.2F;
                    met.rotateSpeed = new Random().Next(50);
                    met.transform.setOffset(new Vector2(met.rad * 12, met.rad * 12));
                    break;
            }
            return met;
        }
    }
}