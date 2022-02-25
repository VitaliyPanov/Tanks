using General.Services;
using UnityEngine;

namespace TanksGB.Core.Infrastructure.Services
{
    internal class TimeService :  ITimeService
    {
        public float DeltaTime() => Time.deltaTime;
        public float FixedDeltaTime() => Time.fixedDeltaTime;
        public float RealtimeSinceStartup() => Time.realtimeSinceStartup;
    }
}