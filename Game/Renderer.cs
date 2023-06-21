using System;

namespace Game
{
    public class Renderer
    {
        public static void Render(Texture texture, Transform transform)
        {
            Console.WriteLine($"Renderer debug 0.0: texture={texture.ToString()}");
            Console.WriteLine($"Renderer debug 0: transform.getOffset.x()={transform.getOffset().x}");
            Console.WriteLine($"Renderer debug 1: transform.getOffset.y()={transform.getOffset().y}");
            // Engine.Draw(texture, this.transform.getPosition().x, this.transform.getPosition().y, this.transform.getScale().x, this.transform.getScale().y, this.transform.getRotation(), 0, 0);
            Engine.Draw(texture,
            transform.getPosition().x,
            transform.getPosition().y,
            transform.getScale().x,
            transform.getScale().y,
            transform.getRotation(),
            transform.getOffset().x,
            transform.getOffset().y);

        }

        public static void Render(Texture texture, Transform transform, float rad)
        {
            Console.WriteLine($"Renderer debug 0.0: texture={texture.ToString()}");
            Console.WriteLine($"Renderer debug 0: transform.getOffset.x()={transform.getOffset().x}");
            Console.WriteLine($"Renderer debug 1: transform.getOffset.y()={transform.getOffset().y}");
            // Engine.Draw(texture, this.transform.getPosition().x, this.transform.getPosition().y, this.transform.getScale().x, this.transform.getScale().y, this.transform.getRotation(), 0, 0);
            Engine.Draw(texture,
            transform.getPosition().x,
            transform.getPosition().y,
            rad,
            rad,
            transform.getRotation(),
            transform.getOffset().x,
            transform.getOffset().y);

        }
    }
}