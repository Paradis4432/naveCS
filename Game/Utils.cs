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

        public static Vector2 zero {
            get { return new Vector2(0f, 0f); }
        }

        public static Vector2 Normalize(Vector2 val) {
            float leng = (float)Math.Sqrt(Math.Pow(val.x, 2) + Math.Pow(val.y, 2));
            return new Vector2(val.x / leng, val.y / leng);
        }

        // checks collition between two circles: c0 circle 0, c1 circle 1, Rc0 radious of circle 0, Rc1 radious of circle 1
        public static bool Colliding(Vector2 c0, Vector2 c1, float Rc0, float Rc1) {
            float distX = c0.x - c1.x;
            float distY = c0.y - c1.y;
            //Engine.Debug("dists: " + distX + ", " + distY);

            float totalDist = (float)Math.Sqrt(distX * distX + distY * distY);

            //Engine.Debug("totalDist : " +  totalDist);  

            return totalDist < Rc0 + Rc1;
        }
    }

    public struct AnimationsManager {
        public static Animation CreateAnimation(string p_animationID, string p_path, int p_texturesAmount, float p_animationSpeed, bool loop) {
            // Idle Animation
            List<Texture> animationFrames = new List<Texture>();

            for (int i = 1; i < p_texturesAmount; i++) {
                Engine.Debug($"adding {p_path}{i}.png to {p_animationID}");
                animationFrames.Add(Engine.GetTexture($"{p_path}{i}.png"));
            }

            Animation animation = new Animation(p_animationID, animationFrames, p_animationSpeed, loop);

            return animation;
        }
    }

    public struct Transform
    {
        public Vector2 position;
        public float rotation;
        public Vector2 scale;

        public Transform(Vector2 p_initialPosition,float p_initialRotation,Vector2 p_scale)
        {
            position = p_initialPosition;
            rotation = p_initialRotation;
            scale = p_scale;

        }
    }

    public enum GameStage {
        Menu,
        Gameplay,
        Win,
        Lost
    }


}
