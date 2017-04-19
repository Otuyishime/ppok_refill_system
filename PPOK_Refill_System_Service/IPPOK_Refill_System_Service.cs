using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PPOK_Refill_System_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPPOK_Refill_System_Service" in both code and config file together.
    [ServiceContract]
    public interface IPPOK_Refill_System_Service
    {
        [OperationContract]
        string DoWork();
        
        [OperationContract]
        void submitUserConfirmation(int userId, bool confirmation, int communticationId);

        [OperationContract]
        void unSubscribe(int userId);
    }
}
