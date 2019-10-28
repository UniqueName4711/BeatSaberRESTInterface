using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatSaberRESTInterface
{
    [Serializable]
    public class ActionMsg
    {
        public string LevelID;      // Select a map with given ID
        public bool Start = false;  // Start the selected map
    }
}
