using System;

namespace ParallaxScrollingExample
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ParallaxScrollingGame())
                game.Run();
        }
    }
}
