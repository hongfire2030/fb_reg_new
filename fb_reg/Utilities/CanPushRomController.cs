using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg.Utilities
{
   
    public enum PushState : byte
    {
        Idle = 0,
        pushing = 1,
        Paused = 2,
        Critical = 3,
        WaitingServer = 4,
        ServerError = 5
    }

    public static class CanPushRomController
    {
        private static volatile PushState _currentState = PushState.Idle;
        private static volatile int countProcess = 0;
        public static void SetState(int  count)
        {
            countProcess = count;
            Console.WriteLine($"📘 [STATE] → {count}");
        }

        public static int GetState()
        {
            return countProcess;
        }

        public static bool IsPushAllowed()
        {
            if (countProcess < 2)
            {
                return true;
            }
            return false;
        }


        public static bool IsError()
        {
            return _currentState == PushState.ServerError || _currentState == PushState.Critical;
        }

    }
}
