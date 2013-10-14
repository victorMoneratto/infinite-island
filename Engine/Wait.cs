using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace InfiniteIsland.Engine
{
    public static class Wait
    {
        public static readonly List<Waiter> Waiters = new List<Waiter>();

        public static Waiter Until(Step step, Action completed = null, int repeat = 1)
        {
            Waiter waiter = new Waiter(step, completed, repeat);
            Waiters.Add(waiter);
            return waiter;
        }

        //public static void For(float time, Action completed = null, int repeat = 1)
        //{
        //    Waiters.Add(
        //        new Waiter(elapsedTime => elapsedTime.Alive >= time, completed, repeat));
        //}

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

    public delegate bool Step(WaiterTime time);

    public struct WaiterTime
    {
        public float Alive;
        public float LastUpdate;
    }

    public class Waiter
    {
        private readonly Action _completed;
        private readonly Step _step;
        private int _timesToComplete;
        private WaiterTime _time;

        public Waiter(Step step, Action completed = null, int timesToComplete = 1)
        {
            _time = new WaiterTime();
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
            _time.LastUpdate = gameTime.ElapsedGameTime.Milliseconds*1e-3f;
            _time.Alive += _time.LastUpdate;

            if (_step(_time))
            {
                if (_timesToComplete > 0)
                {
                    --_timesToComplete;
                }
                if (_completed != null && _timesToComplete == 0)
                {
                    _completed();
                }
                _time.Alive = 0;
            }
        }
    }
}