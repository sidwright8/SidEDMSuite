using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAQ
{
    public interface TranslationStageControllable
    {
        void TSConnect();
        void TSDisconnect();
        void TSInitialize(double acceleration, double deceleration, double distance, double velocity);
        void TSOn();
        void TSDoAll(double acceleration, double deceleration, double distanceF, double distanceB, double velocity);
        void TSLoadExperimentProfile(double acceleration, double deceleration, double distanceF, double distanceB, double velocity);
        void TSGo();
        void TSOff();
        void TSRead();
        void TSReturn();
        void TSRestart();
        void TSClear();
        void TSCheckStatus();
        void TSAutoTriggerEnable();
        void TSAutoTriggerDisable();
    }
}
