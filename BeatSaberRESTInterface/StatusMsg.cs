using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberRESTInterface
{
    [Serializable]
    public class StatusMsg
    {
        public string UserName;
        public ulong UserId;
        public int Score;
    }
}
