using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Utilities.FetchMail
{
    public enum FetchState : byte
    {
        Idle = 0,
        Fetching = 1,
        Paused = 2,
        Critical = 3,
        WaitingServer = 4,
        ServerError = 5
    }
    
    public static class FetchController
    {
        private static volatile FetchState _currentState = FetchState.Idle;
        public static void SetState(FetchState state)
        {
            _currentState = state;
            Console.WriteLine($"📘 [STATE] → {state}");
        }

        public static FetchState GetState()
        {
            return _currentState;
        }

        public static bool IsFetchAllowed()
        {
            return _currentState == FetchState.Idle || _currentState == FetchState.Fetching;
        }

        public static bool IsServerWaiting()
        {
            return _currentState == FetchState.WaitingServer;
        }

        public static bool IsError()
        {
            return _currentState == FetchState.ServerError || _currentState == FetchState.Critical;
        }

        public static void Pause() => SetState(FetchState.Paused);
        public static void Resume() => SetState(FetchState.Fetching);
    }
}
