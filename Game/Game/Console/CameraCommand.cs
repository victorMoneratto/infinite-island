using Microsoft.Xna.Framework;
using XNAGameConsole;

namespace InfiniteIsland.Console
{
    internal class CameraCommand : IConsoleCommand
    {
        public string Execute(string[] arguments)
        {
            switch (arguments[0])
            {
                //case "pivot":
                //    InfiniteIsland.Camera.Viewport.Pivot =
                //        new Vector2(float.Parse(arguments[1]), float.Parse(arguments[2]))*
                //        InfiniteIsland.Camera.Viewport.Dimensions;
                //    break;
                //case "rotation":
                //    InfiniteIsland.Camera.Viewport.Rotation = float.Parse(arguments[1])*MathHelper.Pi/180f;
                //    break;
                //case "scale":
                //    InfiniteIsland.Camera.Viewport.Scale =
                //        new Vector2(float.Parse(arguments[1]), float.Parse(arguments[2]));
                //    break;
            }
            return string.Empty;
        }

        public string Name
        {
            get { return "camera"; }
        }

        public string Description
        {
            get
            {
                return "Change camera settings.\n" +
                       "   camera rotation [deg]\n" +
                       "   camera pivot [x y]\n" +
                       "   camera scale [x y]";
            }
        }
    }
}