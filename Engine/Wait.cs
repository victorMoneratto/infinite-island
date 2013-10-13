using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine
{
    public static class Wait
    {
        public static readonly List<Waiter> Waiters = new List<Waiter>();

        public static void Until(Step step, Action completed = null, int repeat = 1)
        {
            Waiters.Add(
                new Waiter(step, completed, repeat));
        }

        public static void For(float time, Action completed = null, int repeat = 1)
        {
            Waiters.Add(
                new Waiter(elapsedTime => elapsedTime >= time, completed, repeat));
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < Waiters.Count; ++i)
            {
                Waiters[i].Update(gameTime);
                if (Waiters[i].Completed)
                    Waiters.Remove(Waiters[i]);
            }
        }
    }

    public delegate bool Step(float elapsedTime);

    public class Waiter
    {
        private readonly Action _completed;
        private readonly Step _step;
        private float _timeAlive;
        private int _timesToComplete;

        public Waiter(Step step, Action completed = null, int timesToComplete = 1)
        {
            _step = step;
            _timesToComplete = timesToComplete;
            _completed = completed;
        }

        public bool Completed
        {
            get { return _timesToComplete == 0; }
        }

        public void Update(GameTime gameTime)
        {
            _timeAlive += gameTime.ElapsedGameTime.Milliseconds*1e-3f;

            if (_step(_timeAlive))
            {
                if (_timesToComplete > 0)
                {
                    --_timesToComplete;
                }
                if (_completed != null && _timesToComplete == 0)
                {
                    _completed();
                }
                _timeAlive = 0;
            }
        }
    }
}