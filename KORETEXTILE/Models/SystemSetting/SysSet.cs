using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BEEERP.Models.SystemSetting
{
    public  class SysSet
    {
        public  int Id { set; get; }
        public  bool IsPayVouAdjManually { set; get; }
        public int ProductionFloorStore { set; get; }
        public int FloorRM { set; get; }
        public int FactoryRM { set; get; }
        public int FactoryFG { get; internal set; }
    }
}