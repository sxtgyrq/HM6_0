using HMMain6.GroupClassF;
using NBitcoin.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.RoomMainF
{
    public abstract class RoomMainBaseData
    {
        /// <summary>
        /// 随机数生产器
        /// </summary>
        public System.Random rm;

        /// <summary>
        /// 操作锁！
        /// </summary>
        //public object PlayerLock = new object();

        /// <summary>
        /// 玩家字典
        /// </summary>
        public Dictionary<string, GroupClass> _Groups;

        /// <summary>
        /// 游戏官方市场
        /// </summary>
        public HMMain6.Market Market { get; internal set; }

        /// <summary>
        /// 背景音乐选择器
        /// </summary>
       // public HouseManager5_0.Music Music { get; internal set; }

        /// <summary>
        /// 背景Cube
        /// </summary>
      //  public BackGround bg;


        //int _promoteMilePosition = -1;
        ////int _promoteBusinessPosition = -1;
        //int _promoteVolumePosition = -1;
        //int _promoteSpeedPosition = -1;

        //public int promoteMilePosition
        //{
        //    get
        //    {
        //        return this._promoteMilePosition;
        //    }
        //    set
        //    {
        //        lock (this.PlayerLock)
        //        {
        //            this._promoteMilePosition = value;
        //        }
        //    }
        //}
        //public int promoteBusinessPosition
        //{
        //    get
        //    {
        //        throw new Exception();
        //        //return this._promoteBusinessPosition;
        //    }
        //    set
        //    {
        //        lock (this.PlayerLock)
        //        {
        //            throw new Exception();
        //            // this._promoteBusinessPosition = value;
        //        }
        //    }
        //}
        //public int promoteVolumePosition
        //{
        //    get { return this._promoteVolumePosition; }
        //    set
        //    {
        //        lock (this.PlayerLock)
        //        {
        //            this._promoteVolumePosition = value;
        //        }
        //    }
        //}
        //public int promoteSpeedPosition
        //{
        //    get { return this._promoteSpeedPosition; }
        //    set
        //    {
        //        lock (this.PlayerLock)
        //        {
        //            this._promoteSpeedPosition = value;
        //        }
        //    }
        //}

        /// <summary>
        /// 宝石收集时间记录器
        /// </summary>
        public Dictionary<string, List<DateTime>> recordOfPromote = new Dictionary<string, List<DateTime>>();

        public Dictionary<string, long> promotePrice = new Dictionary<string, long>()
        {
            { "mile",1000},
            { "business",1000},
            { "volume",1000},
            { "speed",1000},
        };
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<Player> getGetAllRoles(GroupClass group)
        {
            List<Player> players = new List<Player>();

            foreach (var item in group._PlayerInGroup)
            {
                players.Add(item.Value);
            }
            return players;
        }

        /// <summary>
        /// 依据频率，获取价格。这个是随机获取地址的时候，就会获得。
        /// </summary>
        /// <param name="resultType">mile，business，volume，speed</param>
        /// <returns>返回结果为分，即1/100元</returns>
        protected long GetPriceOfPromotePosition(string resultType)
        {
            if (resultType == "mile" || resultType == "business" || resultType == "volume" || resultType == "speed")
            {
                this.recordOfPromote[resultType].Add(DateTime.Now);
            }
            else
            {
                throw new Exception($"错误地调用{resultType}");
            }
            if (this.recordOfPromote[resultType].Count < 10)
            {
                //  this.recordOfPromote[resultType].Add(DateTime.Now);
                return 10 * 100;
            }
            else
            {
                if (this.recordOfPromote[resultType].Count > 10)
                {
                    this.recordOfPromote[resultType].RemoveAt(0);
                }
                double sumHz = 0;
                for (var i = 1; i < this.recordOfPromote[resultType].Count; i++)
                {
                    var timeS = (this.recordOfPromote[resultType][i] - this.recordOfPromote[resultType][i - 1]).TotalSeconds;
                    timeS = Math.Max(1, timeS);
                    var itemHz = 1 / timeS;
                    sumHz += itemHz;
                }
                var averageValue = sumHz / (this.recordOfPromote[resultType].Count - 1);
                return Convert.ToInt32(50 * 100 * 60 * averageValue); //确保1分钟 的价格是50元
                //var calResult = Math.Round(Convert.ToDecimal(Math.Round(50 * 60 * averageValue, 2)), 2);
                //return Math.Max(0.01m, calResult);
            }
        }

        /// <summary>
        /// 收集金钱的东西
        /// </summary>
        //public Dictionary<int, int> _collectPosition;
        public bool FpIsUsing(int fpIndex, GroupClass group)
        {
            throw new Exception();
            //   return group.FpIsUsing(fpIndex);
            // throw new Exception();
            //var A = false
            //      || fpIndex == this._promoteMilePosition
            //      || fpIndex == this._promoteBusinessPosition
            //      || fpIndex == this._promoteVolumePosition
            //      || fpIndex == this._promoteSpeedPosition
            //      || this._collectPosition.ContainsValue(fpIndex);
            //;
            //foreach (var item in this._Players)
            //{

            //    A = item.Value.StartFPIndex == fpIndex || A;
            //    A = item.Value.getCar().targetFpIndex == fpIndex || A;
            //}
            //return A;
        }

        //   public Engine_AttackEngine attackE;

        // public Engine_DebtEngine debtE;

        // public Engine_Return retutnE;

        //public Engine_Tax taxE;

        //public Engine_CollectEngine collectE;

        // public Engine_PromoteEngine promoteE;

        // public Engine_DiamondOwnerEngine diamondOwnerE;

        //public Engine_Attach attachE;

        //public Engine_MagicEngine magicE;

        //public Engine_Check checkE;
        //以上为Engine
        //以下为Manager

        // public Manager_NPC NPCM;
        public Manager_Frequency frequencyM;
        //public Manager_Driver driverM;
        //public Manager_GoodsReward goodsM;
        //public Manager_Model modelM;
        //public Manager_Resistance modelR;
        //public Manager_Connection modelC;
        //public Manager_Level modelL;
        // public Manager_TaskCopy taskM;
        //public Manager_FileSave fileSM;
        //public Manager_BeginnerMode modelBM;
        public Manager_NewRecordReward breakRecordReward;
        public Manager_RoadFixFee roadFixFee;
    }

    public partial class RoomMain : RoomMainBaseData
    {
        // RoomMainF.RoomMain that;

        public RoomMain(GetRandomPos gp)
        {
            this._Groups = new Dictionary<string, GroupClass>();
            this.rm = new Random(DateTime.Now.GetHashCode());
            this.Market = new Market(this.priceChanged);
            // this.Music = new Music();
            //this.bg = new BackGround();
            //this.attackE = new Engine_AttackEngine(this);
            //this.debtE = new Engine_DebtEngine(this);
            //this.retutnE = new Engine_Return(this);
            //this.taxE = new Engine_Tax(this);
            //this.collectE = new Engine_CollectEngine(this);
            //this.promoteE = new Engine_PromoteEngine(this);
            //this.diamondOwnerE = new Engine_DiamondOwnerEngine(this);
            //this.attachE = new Engine_Attach(this);
            //this.magicE = new Engine_MagicEngine(this);
            //this.checkE = new Engine_Check(this);
            //  this.npcc = new NPCControle();

            // this.NPCM = new Manager_NPC(this);
            this.frequencyM = new Manager_Frequency(this);
            //this.driverM = new Manager_Driver(this);
            //this.goodsM = new Manager_GoodsReward(this, this.DrawGoodsSelection);
            //this.modelM = new Manager_Model(this);
            //this.modelR = new Manager_Resistance(this);
            //this.modelC = new Manager_Connection(this);
            //this.modelL = new Manager_Level(this);
            ////  this.taskM = new Manager_TaskCopy(this);
            //this.fileSM = new Manager_FileSave(this);
            //this.modelBM = new Manager_BeginnerMode(this);
            //this.breakRecordReward = new Manager_NewRecordReward(this);
            this.roadFixFee = new Manager_RoadFixFee(this);

            // lock (PlayerLock)
            {
                this._Groups = new Dictionary<string, GroupClass>();
                //    this._FpOwner = new Dictionary<int, string>();
                //this._PlayerFp = new Dictionary<string, int>();
            }
            //  LookFor(gp);
            this.recordOfPromote = new Dictionary<string, List<DateTime>>()
            {
                {  "mile" ,new List<DateTime>()},
              //  {  "business" ,new List<DateTime>() },
                {  "volume" ,new List<DateTime>() },
                {  "speed" ,new List<DateTime>() },
            };
            this.promotePrice = new Dictionary<string, long>()
            {
                {  "mile" ,10 * 100},
                //{  "business" ,10 * 100 },
                {  "volume" ,10 * 100 },
                {  "speed" ,10 * 100},
            };
        }


        public class commandWithTime
        {
            abstract public class baseC
            {
                public string c { get; set; }
                public string key { get; set; }
                public string groupKey { get; set; }
                //public string car { get; set; }
            }
            public class ReturningOjb
            {
                internal Node returnToBossAddrPath { get; private set; }
                internal Node returnToSelfAddrPath { get; private set; }
                public bool NeedToReturnBoss { get; private set; }
                public Player Boss { get; private set; }

                public static ReturningOjb ojbWithBoss(
                    Node returnToBossAddrPath,
                   Node returnToSelfAddrPath,
                     Player Boss
                    )
                {
                    return new ReturningOjb()
                    {
                        Boss = Boss,
                        NeedToReturnBoss = true,
                        returnToBossAddrPath = returnToBossAddrPath,
                        returnToSelfAddrPath = returnToSelfAddrPath
                    };
                }
                internal static ReturningOjb ojbWithoutBoss(Node returnToSelfAddrPath)
                {
                    return new ReturningOjb()
                    {
                        Boss = null,
                        NeedToReturnBoss = false,
                        returnToSelfAddrPath = returnToSelfAddrPath,
                        returnToBossAddrPath = null
                    };
                }
                //public static ReturningOjb ojbWithoutBoss(List<Model.MapGo.nyrqPosition> returnToSelfAddrPath)
                //{
                //    return new ReturningOjb()
                //    {
                //        Boss = null,
                //        NeedToReturnBoss = false,
                //        returnToSelfAddrPath = returnToSelfAddrPath,
                //        returnToBossAddrPath = null
                //    };
                //}
                public static ReturningOjb ojbWithoutBoss(ReturningOjb oldObj)
                {
                    return new ReturningOjb()
                    {
                        Boss = null,
                        NeedToReturnBoss = false,
                        returnToSelfAddrPath = oldObj.returnToSelfAddrPath,
                        returnToBossAddrPath = null
                    };
                }


            }
            public class returnning : baseC
            {

                public ReturningOjb returningOjb { get; set; }
                /// <summary>
                /// 返回路程的起点
                /// </summary>
                internal int target { get; set; }

                /// <summary>
                /// 取值如mile
                /// </summary>
                public ChangeType changeType { get; internal set; }


                /// <summary>
                /// 表征是否在税收方法执行之后！
                /// </summary>
                public enum ChangeType
                {
                    /// <summary>
                    /// 在收取税收之后
                    /// </summary>
                    AfterTax,
                    BeforeTax,
                }
            }

            //public class returnningWithBoss:

            public class diamondOwner : returnning
            {
                public int costMile { get; internal set; }
                public string diamondType { get; set; }

            }

            public class debtOwner : returnning
            {
                //  public int costMile { get; internal set; }
                public string victim { get; internal set; }
                public int costMile { get; internal set; }
            }
            public class speedSet : returnning
            {
                //  public int costMile { get; internal set; }
                public string beneficiary { get; internal set; }
                public int costMile { get; internal set; }
            }
            public class attackSet : returnning
            {
                //  public int costMile { get; internal set; }
                public string beneficiary { get; internal set; }
                public int costMile { get; internal set; }
            }
            public class defenseSet : returnning
            {
                //  public int costMile { get; internal set; }
                public string beneficiary { get; internal set; }
                public int costMile { get; internal set; }
            }
            public class bustSet : returnning
            {
                //  public int costMile { get; internal set; }
                public string victim { get; internal set; }
            }

            public class taxSet : returnning { }

            public class placeArriving : baseC
            {
                public int costMile { get; set; }
                internal int target { get; set; }

                public ReturningOjb returningOjb { get; set; }
                //internal int target { get; set; }

                ///// <summary>
                ///// 取值如mile
                ///// </summary>
                //public string changeType { get; internal set; }
            }

            public class comeBack : baseC
            {

            }
        }
    }
}
