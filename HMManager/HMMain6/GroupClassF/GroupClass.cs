using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
        RoomMainF.RoomMain that;

        /// <summary>
        /// 团队收集的总金钱
        /// </summary>
        public long Money { get; private set; }
        public DateTime startTime { get; private set; }

        public int countOfAskRoad
        {
            get
            {
                int result = 0;
                foreach (var item in this._PlayerInGroup)
                {
                    result += item.Value.direcitonAndID.AskCount;
                }
                return result;
            }
        }
        public Dictionary<string, long> taskFineshedTime { get; private set; }
        Dictionary<string, string> recordErrorMsgs = new Dictionary<string, string>();
        public GroupClass(string gkey, RoomMainF.RoomMain roomMain)
        {
            _collectPosition = new Dictionary<int, int>();
            GroupKey = gkey;
            //this.PlayerLock_ = new LockObj()
            //{
            //    IsUsing = false,
            //    ThreadName = ""
            //};
            that = roomMain;
            this._PlayerInGroup = new Dictionary<string, Player>();
            this.EnegyChanged = that.GroupValueChanged2_0;
            this.Money = 0;
            this.startTime = DateTime.Now;
            //this.countOfAskRoad = 0;
            this.taskFineshedTime = new Dictionary<string, long>();
            this.costEnegy = 0;
            this.recordErrorMsgs = new Dictionary<string, string>();
<<<<<<< HEAD
            this.records = new Dictionary<string, bool>();
=======
            //this.records = new Dictionary<string, bool>();
>>>>>>> 5bbb0cdf3f891fa27c4db97f97ae3529c7f58980

            //this.groupAbility = new Dictionary<string, int>()
            //{
            //    {
            //        "mile",0
            //    },
            //    {
            //        "volume",0
            //    },
            //    {
            //        "speed",0
            //    }
            //};
            //  this.DataFileSaved = false;
        }

        public int GetRandomPosition(bool withWeight, GetRandomPos gp)
        {
            var rm = that.rm;
            int index;
            do
            {
                index = rm.Next(0, gp.GetFpCount());

                //if (withWeight)
                //    if (gp.GetFpByIndex(index).Weight + 1 < rm.Next(100))
                //    {
                //        continue;
                //    }
            }
            while (this.FpIsUsing(index) || (!gp.GetFpByIndex(index).CanGetScore));
            return index;
        }

        public bool FpIsUsing(int fpIndex)
        {
            foreach (var player in this._PlayerInGroup)
            {
                {
                    if (fpIndex == player.Value.getCar().targetFpIndex)
                    {
                        return true;
                    }
                }
            }
            var A = false
                 || fpIndex == this.StartFPIndex
                 || fpIndex == this.promoteMilePosition
                 || fpIndex == this.promoteVolumePosition
                 || fpIndex == this.promoteSpeedPosition
                 || this._collectPosition.ContainsValue(fpIndex);
            return A;
        }

        public Dictionary<int, int> _collectPosition = new Dictionary<int, int>();
        //public LockObj PlayerLock_ = new LockObj()
        //{
        //    IsUsing = false,
        //    ThreadName = ""
        //};
        public class LockObj
        {
            public bool IsUsing { get; set; }
            public string ThreadName { get; set; }
        }
        public string GroupKey { get; private set; }
        public Dictionary<string, Player> _PlayerInGroup { get; set; }
        public int StartFPIndex { get; private set; }
        public string RewardDate { get; private set; } = "";

        /// <summary>
        /// 表征group里有多少人！
        /// </summary>
        public int groupNumber { get { return this._groupNumber; } }



        int _groupNumber;


        bool _beginnerModeOn = false;
        /// <summary>
        /// 开启新手模式
        /// </summary>
        public bool beginnerModeOn
        {
            /*
             * 首先新手模式的开启，需要在单人模式下。
             * 其次新手模式的开启，需要在登录状态下。
             * 再次新手模式的开启，需要在无任何收集的状态下。
             * 
             * 新手模式，收集，需要提供30%的收集奖励作为新手保护费用。
             * 新手模式，选择错误，扣除4%的汽车上的积分。
             * 新手模式，问道功能，只需要花费10.00%*4%/2=20的积分
             */
            get
            {
                if (this.groupNumber == 1)
                {
                    return this._beginnerModeOn;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.groupNumber == 1)
                {
                    this._beginnerModeOn = value;
                }
                else
                {
                    this._beginnerModeOn = false;
                }
            }
        }

        public int GameStartBaseMoney
        {
            get
            {
                switch (groupNumber)
                {
                    case 1:
                        {

                            return 50000;
                        }
                    case 2: { return 60000 / groupNumber; }
                    case 3: { return 60000 / groupNumber; }
                    case 4: { return 60000 / groupNumber; }
                    case 5: { return 60000 / groupNumber; }
                    default: { return 0; }
                }
            }
        }

        public void SetGroupNumber(int input)
        {
            this._groupNumber = input;
        }
        //public
        public void LookFor(GetRandomPos gp)
        {
            //lock (this.PlayerLock_)
            {
                //var now = Convert.ToInt32((DateTime.Now - new DateTime(2000, 1, 1)).TotalDays);
                string c = File.ReadAllText("DBPublish/RewardFastenPositionIDAndDate.txt");
                c.Split(',');
                var fastenPositionID = c.Split(',')[0];
                var heiStr = c.Split(',')[1];
                var rewardDate = c.Split(',')[2];
                this.RewardDate = rewardDate;
                this.StartFPIndex = gp.FindIndexByID(fastenPositionID, int.Parse(heiStr));
                if (this.StartFPIndex < 0)
                {
                    this.StartFPIndex = 0;
                    this.RewardDate = "20000101";
                }
                // StartFPIndex = now % gp.GetFpCount(); // this.GetRandomPosition(false, gp);
                _collectPosition = new Dictionary<int, int>();
                //   SetLookForPromote(gp);
                SetLookForMoney(gp);
            }
        }



        public void SetLookForMoney(GetRandomPos gp)
        {
            /*
             * 0->100.00
             * 1,2->50.00
             * 3,4,5,6,7->20.00
             * 8,9,10,11,12,13,14,15,16,17->10.00
             * 18-37->5.00
             */


            for (var i = 0; i < 38; i++)
            {
                this._collectPosition.Add(i, GetRandomPosition(true, gp));
            }
        }

        public DateTime ActiveTime
        {
            get
            {
                var value1 = this.startTime;
                foreach (var item in this._PlayerInGroup)
                {
                    if (item.Value.ActiveTime > value1)
                    {
                        value1 = item.Value.ActiveTime;
                    }
                }
                return value1;
            }
        }

        internal void Clear()
        {
            // lock (this.PlayerLock)
            {
                List<string> keys = new List<string>();
                foreach (var item in this._PlayerInGroup)
                {
                    keys.Add(item.Key);
                }
                for (int i = 0; i < keys.Count; i++)
                {
                    this._PlayerInGroup[keys[i]] = null;
                }
                this._PlayerInGroup.Clear();
            }

            //   throw new NotImplementedException();
        }
    }
}
