using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SusuCMS.Connect.Clients.Tencent.QQ
{
    [DataContract]
    class UserInfoResult : QQUserInfo
    {
        [DataMember]
        public int ret = 0;

        [DataMember]
        public string msg = null;
    }
}
