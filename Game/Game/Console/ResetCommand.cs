using InfiniteIsland.Engine;
using InfiniteIsland.State;
using XNAGameConsole;

namespace InfiniteIsland.Console
{
    internal class ResetCommand : IConsoleCommand
    {
        private readonly Play _play;
        public ResetCommand(Play play)
        {
            _play = play;
        }
        public string Execute(string[] arguments)
        {
            float initialFactor = _play.Factor;
            Wait.Until(
                time => Tweening.Tween(
                    start: initialFactor, 
                    end:1f, 
                    progress:time.Alive, 
                    step:value => _play.Factor = value, 
                    scale:TweenScales.Cubic));
            return string.Empty;
        }

        public string Name
        {
            get { return "reset"; }
        }

        public string Description
        {
            get { return string.Empty; }
        }
    }
}