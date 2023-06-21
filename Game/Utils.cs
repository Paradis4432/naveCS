using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float p_x, float p_y)
        {
            x = p_x;
            y = p_y;
        }

        public static Vector2 zero
        {
            get { return new Vector2(0f, 0f); }
        }

        public static Vector2 Normalize(Vector2 val)
        {
            float leng = (float)Math.Sqrt(Math.Pow(val.x, 2) + Math.Pow(val.y, 2));
            return new Vector2(val.x / leng, val.y / leng);
        }

        // checks collition between two circles: c0 circle 0, c1 circle 1, Rc0 radious of circle 0, Rc1 radious of circle 1
        public static bool Colliding(Vector2 c0, Vector2 c1, float Rc0, float Rc1)
        {
            float distX = c0.x - c1.x;
            float distY = c0.y - c1.y;
            //Engine.Debug("dists: " + distX + ", " + distY);

            float totalDist = (float)Math.Sqrt(distX * distX + distY * distY);

            //Engine.Debug("totalDist : " +  totalDist);  

            return totalDist < Rc0 + Rc1;
        }
    }

    public struct AnimationsManager
    {
        public static Animation CreateAnimation(string p_animationID, string p_path, int p_texturesAmount, float p_animationSpeed, bool loop)
        {
            // Idle Animation
            List<Texture> animationFrames = new List<Texture>();

            for (int i = 1; i < p_texturesAmount; i++)
            {
                Engine.Debug($"adding {p_path}{i}.png to {p_animationID}");
                animationFrames.Add(Engine.GetTexture($"{p_path}{i}.png"));
            }

            Animation animation = new Animation(p_animationID, animationFrames, p_animationSpeed, loop);

            return animation;
        }
    }

    public struct Transform
    {
        private Vector2 position;
        private float rotation;
        private Vector2 scale;
        private Vector2 offset;

        public Transform(Vector2 p_initialPosition, float p_initialRotation, Vector2 p_scale, Vector2 p_offset)
        {
            Console.WriteLine($"Transform debug 0: p_initialPosition={p_initialPosition.x}");
            Console.WriteLine($"Transform debug 0.1: p_initialPosition={p_initialPosition.y}");
            Console.WriteLine($"Transform debug 0.2: p_initialRotation={p_initialRotation}");
            Console.WriteLine($"Transform debug 0.3: p_scale={p_scale.x}");
            Console.WriteLine($"Transform debug 0.4: p_scale={p_scale.y}");
            position = p_initialPosition;
            rotation = p_initialRotation;
            scale = p_scale;
            offset = p_offset;
        }

        public void setPosition(Vector2 pos)
        {
            position = pos;
        }

        public void setRotation(float rot)
        {
            rotation = rot;
        }

        public void setScale(Vector2 scal)
        {
            scale = scal;
        }

        public void setOffset(Vector2 off)
        {
            offset = off;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public float getRotation()
        {
            return rotation;
        }

        public Vector2 getScale()
        {
            return scale;
        }

        public Vector2 getOffset()
        {
            return offset;
        }

        
    }

    public enum GameStage
    {
        Menu,
        Gameplay,
        Win,
        Lost
    }


}
