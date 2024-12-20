﻿using ModelBase.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace CommonClass
{
    public class Command
    {
        public string c { get; set; }
    }
    public class Register : Command
    {
        public string ControllerUrl { get; set; }
    }
    public class Map : Command
    {
        public string DataType { get; set; }
    }

    public class CommandNotify : Command
    {
        public int WebSocketID { get; set; }
        //   public int TimeOut { get; set; }


        /*
         * 之所以引入此变量，是因为在2023年8月3日
         * 发现，一个路口在距离完成路径终点很近时，
         * 发生了传输数据的顺序颠倒，导致前台程序，
         * 不能正常运行。
         * 以上没有进行debug，只是推测。
         * 所以引入了AsynSend变量。顾名思义，异步
         * 发送。
         * 当传输BradCastAnimateOfOthersCar4对象时，
         * 所以引入了AsynSend=false
         */
        /// <summary>
        /// 是否异步发送
        /// </summary>
        public bool AsynSend { get; set; } = true;
    }

    public class GetPositionNotify : CommandNotify
    {
        // public Model.FastonPosition fp { get; set; }
        public string[] carsNames { get; set; }
        public string key { get; set; }
        public string PlayerName { get; set; }
    }
    public class GetPositionNotify_v2 : CommandNotify
    {
        public FPPosition fp { get; set; }

        // public Model.FastonPosition fp { get; set; }
        public int fPIndex { get; set; }
        public string key { get; set; }
        public string PlayerName { get; set; }
        public int positionInStation { get; set; }
        public int groupNumber { get; set; }
        //public string fPCode { get; set; }
        //public int height { get; set; }
    }
    public class GetOthersPositionNotify : CommandNotify
    {
        // public Model.FastonPosition fp { get; set; }
        public string[] carsNames { get; set; }
        public string key { get; set; }
        public string PlayerName { get; set; }
        public int fPIndex { get; set; }
    }
    public class GetOthersPositionNotify_v2 : CommandNotify
    {
        public FPPosition fp;

        //public string fPCode { get; set; }
        //public int height { get; set; }
        public string key { get; set; }
        public string PlayerName { get; set; }
        public int fPIndex { get; set; }
        public int positionInStation { get; set; }
        public bool isPlayer { get; set; }
        public bool isNPC { get; set; }
        public int Level { get; set; }
    }

    public class TaxNotify : CommandNotify
    {
        //   public Model.FastonPosition fp { get; set; }
        public long tax { get; set; }
        public int target { get; set; }
    }
    public class FrequencyNotify : CommandNotify
    {
        //public Model.FastonPosition fp { get; set; }
        //public long tax { get; set; }
        //public int target { get; set; }
        public int frequency { get; set; }
    }
    public class MoneyForSaveNotify : CommandNotify
    {
        public long MoneyForSave { get; set; }
        public long MoneyForFixRoad { get; set; }
    }
    public class GoodsSelectionNotify : CommandNotify
    {
        public double x { get; set; }

        public double z { get; set; }
        public double y { get; set; }
        public string[] selections { get; set; }
        public double[] positions { get; set; }
    }
    public class MoneyNotify : CommandNotify
    {
        public long Money { get; set; }
    }

    public class DriverNotify : CommandNotify
    {
        public int index { get; set; }
        public string name { get; set; }
        public int skill1Index { get; set; }
        public int skill2Index { get; set; }
        public string skill1Name { get; set; }
        public string skill2Name { get; set; }
        public string sex { get; set; }
        public string race { get; set; }
    }
    public class SpeedNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
    }
    public class NitrogenValueNotify : CommandNotify
    {
        public string Key { get; set; }
        public long NitrogenValue { get; set; }
    }
    public class ConfuseNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
    }
    public class LoseNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
    }
    public class FireNotify : CommandNotify
    {
        public string targetRoleID { get; set; }
        public string actionRoleID { get; set; }
    }
    public class WaterNotify : CommandNotify
    {
        public string targetRoleID { get; set; }
        public string actionRoleID { get; set; }
    }
    public class ElectricNotify : CommandNotify
    {
        public string targetRoleID { get; set; }
        public string actionRoleID { get; set; }
    }
    public class ElectricMarkNotify : CommandNotify
    {
        public double[] lineParameter { get; set; }
    }
    public class WaterMarkNotify : CommandNotify
    {
        public double[] lineParameter { get; set; }
    }
    public class FireMarkNotify : CommandNotify
    {
        public double[] lineParameter { get; set; }
    }
    public class ViewSearch : CommandNotify
    {
        public int mctX { get; set; }
        public int mctY { get; set; }
    }
    //public class SetBG:
    public class AttackNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
    }
    public class CollectCountNotify : CommandNotify
    {
        public string Key { get; set; }
        public long Count { get; set; }
    }

    public class DefenceNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
    }
    public class ConfusePrepareNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int EndZ { get; set; }
    }
    public class LostPrepareNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int EndZ { get; set; }
    }
    public class AmbushPrepareNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int StartZ { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public int EndZ { get; set; }
    }
    public class ControlPrepareNotify : CommandNotify
    {
        public string Key { get; set; }
        public bool On { get; set; }
    }
    public class OthersRemove : CommandNotify
    {
        public string othersKey;
    }
    public class DrawTarget : CommandNotify
    {
        public double x { get; set; }
        public double y { get; set; }
        public double h { get; set; }
    }
    public class WMsg : CommandNotify
    {
        public string Msg;
    }
    public class WMsg_WithShowTime : CommandNotify
    {
        public string Msg;
        public int ShowTime { get; set; }
    }
    public class DebtsRemove : CommandNotify
    {
        public string othersKey;
    }

    public class BustStateNotify : CommandNotify
    {
        public bool Bust { get; set; }
        public string Key { get; set; }
        public string KeyBust { get; set; }
        public string Name { get; set; }
    }

    public class TheLargestHolderChangedNotify : CommandNotify
    {
        public string operateKey { get; set; }
        public string ChangeTo { get; set; }
        public string operateName { get; set; }
        public string nameTo { get; set; }
    }
    public class SingleRoadPathData : CommandNotify
    {
        //  public List<double[]> meshPoints { get; set; }
        public List<int> meshPoints { get; set; }
        public List<int> basePoint { get; set; }
    }
    public class SingleRoadPathData_V2 : CommandNotify
    {
        //  public List<double[]> meshPoints { get; set; }
        public string DataHash { get; set; }
        public string RoadCode { get; set; }
    }

    public class ModelDataShow : CommandNotify
    {
        public string modelID { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public string amodel { get; set; }
        public double rotatey { get; set; }
        public bool existed { get; set; }
        //public string imageBase64 { get; set; }
        //public string objText { get; set; }
        //public string mtlText { get; set; }
        public string modelType { get; set; }
    }

    public class SupportNotify : CommandNotify
    {
        public long Money { get; set; }
    }
    public class LeftMoneyInDB : CommandNotify
    {
        public long Money { get; set; }
        public string address { get; set; }
    }

    public class IsAdministrator : CommandNotify { }

    public class StockScoreNotify : CommandNotify
    {
        public string showType { get; set; }
        public string baseBusinessAddr { get; set; }
        public string Msg { get; set; }
        public long PassCoin { get; set; }
        public long TradeScore { get; set; }
        public string Hash256Code { get; set; }
        public bool hasValue { get; set; }
        public string FailReason { get; set; }
    }

    public class AnimationEncryptedItem
    {
        public List<Int64> dataEncrypted { get; set; }
        public int startT { get; set; }
        public int privateKey { get; set; }
        public string Md5Code { get; set; }
        public bool isParking { get; set; }
    }
    public class AnimationData
    {
        public int deltaT { get; set; }
        public AnimationEncryptedItem[] animateData { get; set; }
        public string currentMd5 { get; set; }
        public string previousMd5 { get; set; }
        public int[] privateKeys { get; set; }
    }
    public class AnimationKeyData
    {
        public int deltaT { get; set; }
        public int privateKeyIndex { get; set; }
        public int privateKeyValue { get; set; }
        public string currentMd5 { get; set; }
        public string previousMd5 { get; set; }

    }
    //public class BradCastAnimateOfOthersCar2 : CommandNotify
    //{
    //    public string carID;

    //    public AnimationData Animate { get; set; }
    //    public string parentID { get; set; }


    //}
    public class BradCastAnimateOfOthersCar3 : CommandNotify
    {
        public string carID;

        public AnimationData Animate { get; set; }
        public string parentID { get; set; }
        //public bool passPrivateKeysOnly { get; set; }


    }
    public class BradCastAnimateOfOthersCar4 : CommandNotify
    {
        public string carID;

        public AnimationKeyData Animate { get; set; }
        public string parentID { get; set; }
        //public bool passPrivateKeysOnly { get; set; }


    }
    public class BradCastMoneyForSave : CommandNotify
    {
        public long Money { get; set; }
    }
    public class BradCastPromoteDiamondCount : CommandNotify
    {
        public int count { get; set; }
        public string pType { get; set; }
    }
    public class BradCastPromoteDiamondInCar : CommandNotify
    {
        public string pType { get; set; }
        public string roleID { get; set; }
    }
    public class SelectionIsWrong : CommandNotify
    {
        public long reduceValue { get; set; }
        public string postionCrossKey { get; set; }
    }
    public class BradCastAbility : CommandNotify
    {
        public string pType { get; set; }
        public string carIndexStr { get; set; }
        public long costValue { get; set; }
        public long sumValue { get; set; }
    }
    public class BradCastGroupAbility : CommandNotify
    {
        public string pType { get; set; }
        public long showValue { get; set; }
    }
    public class BradCarState : CommandNotify
    {
        public string State { get; set; }
        public string carID { get; set; }
        /// <summary>
        /// 作用是保证前台能按顺序接受状态！
        /// </summary>
        public int countStamp { get; set; }
    }
    public class BradDiamondPrice : CommandNotify
    {
        public string State { get; set; }
        public string carID { get; set; }
        public string priceType { get; set; }
        public long price { get; set; }
    }
    public class BradCarPurpose : CommandNotify
    {
        public string Purpose { get; set; }
        public string carID { get; set; }
    }
    public class BradCastCarAbility : CommandNotify
    {
        public object Number { get; set; }
        public string pType { get; set; }
        public string sumOrCost { get; set; }
    }

    public class AnimateC
    {

    }
    public class BradCastAnimateOfSelfCar : CommandNotify
    {
        public string carID;

        public AnimationData Animate { get; set; }
        public string parentID { get; set; }
        public decimal CostMile { get; set; }
        public int LeftMile { get; set; }
    }
    public class BradCastSocialResponsibility : CommandNotify
    {
        public long socialResponsibility { get; set; }
        // public string key { get; set; }
        public string otherKey { get; set; }
    }
    public class BradCastRightAndDuty : CommandNotify
    {
        public string playerKey { get; set; }
        public long right { get; set; }
        public long duty { get; set; }
        public int rightPercent { get; set; }
        public int dutyPercent { get; set; }
    }

    public class BradCastPromoteInfoDetail : CommandNotify
    {
        /// <summary>
        /// "mile","business","volume","speed"四种状态
        /// </summary>
        public string resultType { get; set; }
        // public Model.FastonPosition Fp { get; set; }
        public decimal Price { get; set; }
    }
    public class BradCastCollectInfoDetail : CommandNotify
    {
        //   public Model.FastonPosition Fp { get; set; }
        public int collectMoney { get; set; }
    }
    public class BradCastCollectInfoDetail_v2 : CommandNotify
    {
        //public Model.FastonPosition Fp { get; set; }
        public int collectMoney { get; set; }
        public int collectIndex { get; set; }
        public FPPosition Fp { get; set; }
    }

    public class BradCastWhetherGoTo : CommandNotify
    {
        // public Model.FastonPosition Fp { get; set; }
        public string msg { get; set; }
        public int select { get; set; }
        public string tsType { get; set; }
        public bool Live { get; set; }
        public string musicID { get; set; }
    }

    public class BradCastWhereToGoInSmallMap : CommandNotify
    {
        public float maxX { get; set; }
        public float maxY { get; set; }
        public float minX { get; set; }
        public float minY { get; set; }
        public List<DataItem> data { get; set; }
        public float currentX { get; set; }
        public float currentY { get; set; }
        public string base64 { get; set; }

        public bool isFineshed { get; set; }
        public string TimeStr { get; set; }
        public string ResultMsg { get; set; }

        /// <summary>
        /// 前台会依据此项，决定显示颜色
        /// </summary>
        public bool RecordedInDB { get; set; }
        public int groupNumber { get; set; }
        public string TaskName { get; set; }
        public string BTCAddr { get; set; }
        public bool HasValueToImproveSpeed { get; set; }

        public bool Live { get; set; }

        public class DataItem
        {
            public string DataType { get; set; }
            public int[] Path { get; set; }
        }
        //public Model.FastonPosition Fp { get; set; }
        //public string msg { get; set; }
        //public int select { get; set; }
        //public string tsType { get; set; }
    }

    public class BradCastMusicTheme : CommandNotify
    {
        public string theme { get; set; }
    }

    public class ShowDirectionOperator : CommandNotify
    {
        public string postionCrossKey;

        /// <summary>
        /// item为 旋转的弧度。0~2π
        /// </summary>
        public double[] direction { get; set; }
        public double positionX { get; set; }
        public double positionY { get; set; }
        public double positionZ { get; set; }
    }
    public class ShowRoadCrossSelectionsOperator : CommandNotify
    {
        public string postionCrossKey;
        public double[] crossDirects { get; set; }
    }
    public class BradCastBackground : CommandNotify
    {
        public string path { get; set; }
        public FPPosition fp { get; set; }
    }

    public class BradCastSelections : CommandNotify
    {
        public string path { get; set; }
        public FPPosition fp { get; set; }
        public List<FPItem> selections { get; set; }

        public class FPItem
        {
            public int x { get; set; }
            public int y { get; set; }
            public int z { get; set; }
            public string c { get; set; }
            public int h { get; set; }
            public double r { get; set; }
        }
    }
    public class BradCastCompass : CommandNotify
    {
        public CompassPosition position { get; set; }
    }
    public class BradCastGoldObj : BradCastCompass
    {
        public bool hasValue { get; set; }
    };
    public class BradCastTurbine : BradCastGoldObj { }
    public class BradCastSatelite : BradCastGoldObj { }
    public class BradCastBattery : BradCastGoldObj { }
    public class BradCastGear : BradCastGoldObj { }
    public class BradCastBitcoinAddr : BradCastGoldObj
    {
        public string bitcoinAddr { get; set; }
    }

    public class PlayerAdd : Command
    {
        public string Key { get; set; }
        public string FromUrl { get; set; }
        public int RoomIndex { get; set; }

        public string Check { get; set; }
        public int WebSocketID { get; set; }
        public string PlayerName { get; set; }
        public string[] CarsNames { get; set; }
    }
    public class PlayerAdd_V2 : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public string FromUrl { get; set; }
        public int RoomIndex { get; set; }

        public string Check { get; set; }
        public int WebSocketID { get; set; }
        public string PlayerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RefererAddr { get; set; }
        public int groupMemberCount { get; set; }
    }

    public class GetFrequency : Command
    {
    }

    public class PlayerCheck : PlayerAdd_V2 { }

    public class GetPosition : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }

    public class SetPromote : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        /// <summary>
        /// 取值如mile
        /// </summary>
        public string pType { get; set; }
        //   public string car { get; set; }

        /// <summary>
        /// 抖音ID
        /// </summary>
        public string Uid { get; set; }
    }
    public class SetCollect : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        /// <summary>
        /// 取值如findWork
        /// </summary>
        public string cType { get; set; }

        public string fastenpositionID { get; set; }
        /// <summary>
        /// 取值0~37
        /// </summary>
        public int collectIndex { get; set; }
        //public string car { get; set; }
    }

    public class SetAttack : Command
    {
        public string Key { get; set; }
        //public string car { get; set; }
        public string targetOwner { get; set; }
        public int target { get; set; }
    }
    public class MagicSkill : SetAttack
    {
        /// <summary>
        /// 魔法技能的选择。取值只能是1或2，出现其他值，系统报错不处理就对了。1一般为种族技能，2为性别技能。
        /// </summary>
        public int selectIndex { get; set; }
    }
    public class View : Command
    {
        public string Key { get; set; }
        /// <summary>
        /// camera X
        /// </summary>
        public double rotationY { get; set; }
        public string GroupKey { get; set; }
        public string postionCrossKey { get; set; }
        public string Uid { get; set; }
    }
    public class Ask : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }
    public class SaveInFile : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }
    public class TurnOnBeginnerMode : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }

    public class TakeApart : Command
    {
        public string Key { get; set; }
    }
    public class SetBust : Command
    {
        public string Key { get; set; }
        //  public string car { get; set; }
        public string targetOwner { get; set; }
        public int target { get; set; }
    }

    public class SetTax : Command
    {
        public string Key { get; set; }
        //public string car { get; set; }
        public int target { get; set; }
    }

    public class SetAbility : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        //public string car { get; set; }
        public string pType { get; set; }

        public int count { get; set; }
    }


    public class OrderToReturn : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        //public string car { get; set; }
    }
    public class OrderToReturnBySystem : OrderToReturn
    {
    }

    public class OrderToSubsidize : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public string address { get; set; }
        public string signature { get; set; }
        public long value { get; set; }
    }
    public class OrderToUpdateLevel : Command
    {
        public string Key { get; set; }
        public string address { get; set; }
        public string signature { get; set; }
    }
    public class SaveMoney : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public string dType { get; set; }
        public string address { get; set; }
    }
    public class GetTax : Command
    {
        public string Key { get; set; }
        public string car { get; set; }
        public string targetOwner { get; set; }
        public int target { get; set; }
    }
    public class GetBtns : Command
    {
        public string FromUrl { get; set; }
        public int RoomIndex { get; set; }
        public int WebSocketID { get; set; }
    }

    public class TeamCreate : Command
    {
        public string FromUrl { get; set; }
        public string CommandStart { get; set; }
        public int WebSocketID { get; set; }
        public string PlayerName { get; set; }
        public string GroupKey { get; set; }
        public string UpdateKey { get; set; }

    }
    public class GetPromoteMiles : CommonClass.Command
    {
        public string FromUrl { get; set; }
        public int WebSocketID { get; set; }
    }

    public class TeamBegain : Command
    {
        public int TeamNum { get; set; }
        public int RoomIndex { get; set; }
        public string GroupKey { get; set; }
    }

    public class TeamExit : Command
    {
        public int TeamNum { get; set; }
    }

    public class TeamJoin : Command
    {
        public string FromUrl { get; set; }
        public string CommandStart { get; set; }
        public int WebSocketID { get; set; }
        public string PlayerName { get; set; }
        public string TeamIndex { get; set; }
        public string Guid { get; set; }
        public string UpdateKey { get; set; }
    }

    public class LeaveTeam : Command
    {
        public string FromUrl { get; set; }
        public int WebSocketID { get; set; }
        public string TeamIndex { get; set; }
    }

    public class TeamUpdate : Command
    {
        public string FromUrl { get; set; }
        public int WebSocketID { get; set; }

        public int TeamNumber { get; set; }
        public string UpdateKey { get; set; }
        public string CommandStart { get; set; }
    }

    public class UpdateTeammateOfCaptal : Command
    {
        public int TeamNumber { get; set; }
    }
    public class CheckMembersIsAllOnLine : Command
    {
        public int TeamNumber { get; set; }
    }

    public class TeamMemberCount : Command
    {
        public int TeamNum { get; set; }
        //public int GroupKey { get; set; }
    }

    public class TeamDisplayItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string GUID { get; set; }
        public bool IsSelf { get; set; }
    }
    public class TeamCreateFinish : CommandNotify
    {
        public string CommandStart { get; set; }
        public int TeamNum { get; set; }
        public TeamDisplayItem PlayerDetail { get; set; }
    }
    public class TeamJoinBroadInfo : CommandNotify
    {
        public TeamDisplayItem Player { get; set; }
        //public string Guid { get; set; }
    }
    public class TeamJoinRemoveInfo : CommandNotify
    {
        public string Guid { get; set; }
    }

    public class TeamJoinFinish : CommandNotify
    {
        public int TeamNum { get; set; }
        public List<TeamDisplayItem> Players { get; set; }
    }
    public class TeamNumWithSecret : CommandNotify
    {
        public string Secret { get; set; }
        public string RefererAddr { get; set; }
        public string GroupKey { get; set; }
    }
    public class TeamResult : Command
    {
        public string FromUrl { get; set; }
        public int WebSocketID { get; set; }
        /// <summary>
        /// 作为队伍的索引
        /// </summary>
        public int TeamNumber { get; set; }
        public int Hash { get; set; }

        /// <summary>
        /// 主要作用是web前台断开重新连接而用！再MateWsAndHouse赋值，并返回。
        /// </summary>
        public string UpdateKey { get; set; }
    }
    public class TeamResultForGameBegain : TeamResult
    {
        public int roomIndex { get; set; }
    }

    public class TeamFoundResult : Command
    {
        public bool HasResult { get; set; }
        /// <summary>
        /// 作为队伍的索引
        /// </summary>
        public int TeamNumber { get; set; }
    }
    //public class TeamBegain : Command
    //{
    //    public int TeamNumber { get; set; }
    //}

    public class RoomNumberResult : Command
    {
        public int RoomIndex { get; set; }
        public string PassMd5 { get; set; }
        public string CheckMd5 { get; set; }

    }
    public class GetRewardFromBuildingM : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        //  public string selectObjName { get; set; }
    }
    /// <summary>
    /// 用于传输对话。
    /// </summary>
    public class DialogMsg : CommandNotify
    {
        /// <summary>
        /// ResponMsg 采用Key的URl与WebsocketID To=表示谁的消息，实际上相当于From
        /// </summary>
        public string Key { get; set; }
        public string GroupKey { get; set; }
        /// <summary>
        /// Reques时，采用To的URl与WebsocketID To=表示谁的消息。To=Self，表示自己的消息。
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }
    }
    public class PassRoomMd5Check
    {
        public int RoomIndex { get; set; }
        public string StartMd5 { get; set; }
        public string CheckMd5 { get; set; }

        public string RoomIndexWithAes { get; set; }
    }

    public class CheckCarState : Command
    {
        public string State { get; set; }
        public string Key { get; set; }
    }

    public class SetCrossBG : CommandNotify
    {
        public string CrossID { get; set; }
        public bool IsDetalt { get; set; }
        public string Md5Key { get; set; }
        public bool AddNew { get; set; }
        //public string px { get; set; }
        //public string nx { get; set; }
        //public string py { get; set; }
        //public string ny { get; set; }
        //public string pz { get; set; }
        //public string nz { get; set; }
    }
    public class GetFPBG : CommandNotify
    {
        public string fpID { get; set; }
        public bool FromDB { get; set; }

        public class Result
        {
            public bool hasValue { get; set; }
            public string px { get; set; }
            public string nx { get; set; }
            public string py { get; set; }
            public string ny { get; set; }
            public string pz { get; set; }
            public string nz { get; set; }
        }
    }
    public class GetResistanceObj : Command
    {
        public string KeyLookfor { get; set; }
        public string key { get; set; }
        public string GroupKey { get; set; }
        /// <summary>
        /// 0代表基础信息，1代表战斗抗性
        /// </summary>
        public int RequestType { get; set; }
    };
    public class ResistanceDisplay : CommandNotify
    {
        /// <summary>
        /// 自己，队友，玩家，NPC，敌对NPC
        /// </summary>
        public string Relation { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public long Money { get; set; }
        /// <summary>
        /// NPC Or player
        /// </summary>
        public string PlayerType { get; set; }

        public int Driver { get; set; }
        public string DriverName { get; set; }
        public int[] MileCount { get; set; }
        public int[] BusinessCount { get; set; }
        public int[] VolumeCount { get; set; }
        public int[] SpeedCount { get; set; }

        public string BossKey { get; set; }
        public string BossName { get; set; }
        public string BTCAddr { get; set; }
        public long Mile { get; set; }
        public long Business { get; set; }
        public long Volume { get; set; }
        public int Speed { get; set; }

        public string OnLineStr { get; set; }
        public string KeyLookfor { get; set; }

        //public long SpeedValue { get; set; }
        //public long DefenceValue { get; set; }
        //public long AttackValue { get; set; }
        //public long LoseValue { get; set; }
        //public long ConfuseValue { get; set; }

        // public long SpeedValue { get; set; }
        // public long SpeedValue { get; set; }
        //  public int 
    }

    public class ResistanceDisplay_V2 : CommandNotify
    {
        /// <summary>
        /// 自己，队友，玩家，NPC，敌对NPC
        /// </summary>
        public string Relation { get; set; }
        public string Name { get; set; }
        public long Money { get; set; }
        public string DriverName { get; set; }
        public int[] MileCount { get; set; }
        public int[] VolumeCount { get; set; }
        public int[] SpeedCount { get; set; }
        public string BTCAddr { get; set; }
        public long Mile { get; set; }
        public long Volume { get; set; }
        public int Speed { get; set; }

        public string OnLineStr { get; set; }
        public int CollectSum { get; set; }
        public string CollcetPercent { get; set; }
        public int SelectCount { get; set; }
        public string SelectRightPercent { get; set; }
        public int SpeedImproveCount { get; set; }
        public string SpeedImprovePercent { get; set; }
    }

    public class ResistanceDisplay_V3 : CommandNotify
    {
        public List<Item> Datas { get; set; }
        public class Item
        {
            public string Name { get; set; }
            public long Money { get; set; }

            public long Mile { get; set; }
            public int MileDiamond { get; set; }
            public long Volume { get; set; }
            public int VolumeDiamond { get; set; }

            public long Speed { get; set; }
            public int SpeedDiamond { get; set; }

            public long CollectAmount { get; set; }
            public long CollectPercent { get; set; }

            public int SelectAmount { get; set; }
            public int SelectRightPercent { get; set; }

            public string BTCAddr { get; set; }
            public bool OnLine { get; set; }

            public bool isFinished { get; set; }

        }
    }

    public class ParameterToEditPlayerMaterial
    {
        /// <summary>
        /// 自己，队友，玩家，NPC
        /// </summary>
        public string Relation { get; set; }
        public string singleName { get; set; }
        public int Driver { get; set; }
        public string Key { get; set; }
    }

    public class ResistanceDisplay2 : CommandNotify
    {
        //  public driversource.Resistance Resistance { get; set; }
        public string KeyLookfor { get; set; }

        /// <summary>
        /// 招募成功次数
        /// </summary> 
        public int recruit { get; set; }

        public int magicViolentValue { get; set; }
        public int magicViolentProbability { get; set; }

        public int controlImprove { get; set; }
        //public driversource.Resistance Ignore { get; set; }

        public int SpeedImproveProbability { get; set; }
        public int DefenseImproveProbability { get; set; }
        public int AttackImprove { get; set; }

        public int defensiveOfElectic { get; set; }
        public int defensiveOfWater { get; set; }
        public int defensiveOfFire { get; set; }
        public int defensiveOfLose { get; set; }
        public int defensiveOfConfuse { get; set; }
        public int defensiveOfAmbush { get; set; }
        public int defensiveOfPhysics { get; set; }

        //public int ignoreElectic { get; set; }
        //public int ignoreOfWater { get; set; }
        //public int ignoreFire { get; set; }
        public int ignoreLose { get; set; }
        public int ignoreConfuse { get; set; }
        public int ignoreAmbush { get; set; }
        public int ignorePhysics { get; set; }
        public Dictionary<int, int> buildingReward { get; set; }
        public int race { get; set; }

        public long SpeedValue { get; set; }
        public long DefenceValue { get; set; }
        public long AttackValue { get; set; }
        public long LoseValue { get; set; }
        public long ConfuseValue { get; set; }
        public int ignorePhysicsValue { get; set; }

        public int IgnoreFireMagicValue { get; set; }
        public int IgnoreElectricMagicValue { get; set; }
        public int IgnoreWaterMagicValue { get; set; }

        public int IgnoreAmbushValue { get; set; }
        public int IgnoreLostValue { get; set; }
        public int IgnoreConfuseValue { get; set; }


        public int SpeedImproveValue { get; set; }
        public int DefenseImproveValue { get; set; }
        public int AttackImproveProbability { get; set; }
        public int AttackImproveValue { get; set; }
        public int AmbushPropertyByDefendMagic { get; set; }
        public int ConfusePropertyByDefendMagic { get; set; }
        public int LostPropertyByDefendMagic { get; set; }
        public int DefenceAttackMagicAdd { get; set; }
        public int DefencePhysicsAdd { get; set; }
    }

    public class GetFightSituation : Command
    {
        public string Key { get; set; }
        public class GetFightSituationResult : Command
        {
            public string[] Parters { get; set; }
            public string[] Opponents { get; set; }
        }
    }
    public class GetStockTradeCenterDetail : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }

        public class Result : Command
        {
            public bool IsLogined { get; set; }
            public long Score { get; set; }
            public long Sotoshi { get; set; }
            public string BTCAddr { get; set; }
            public long price { get; set; }//score/satoshi
            public string DateTimeStr { get; set; }
            public long RewardSotoshiCost { get; set; }
            public long RewardScoreCost { get; set; }
        }
    }


    public class GetTaskCopyDetail : Command
    {
        public string Key { get; set; }
        public class GetTaskCopyResult : Command
        {
            public string[] Detail { get; set; }
        }
    }
    public class RemoveTaskCopyM : Command
    {
        public string Key { get; set; }
        public string Code { get; set; }
    }

    public class SetParameterIsLogin : CommandNotify
    {

    }
    public class SetParameterHasNewTask : CommandNotify
    {

    }
    public class ExitObj : Command
    {
        public string GroupKey { get; set; }
        public string Key { get; set; }
        public class ExitObjResult
        {
            public bool Success { get; set; }
            public string Msg { get; set; }
        }
    }

    public class GetOnLineState : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public class SetOnLineState : CommandNotify
        {
            public string Key { get; set; }
            public bool IsNPC { get; set; }
            public bool OnLine { get; set; }
            public bool IsPartner { get; set; }
            public bool IsEnemy { get; set; }
        }
    }
    public class SmallMapClick : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double radius { get; set; }
    }
    public class ConfirmPanelSelectResult : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }

        /// <summary>
        /// true，就是直接collect promote return操作；false当调用ConfirmPanelSelectResult 对象时，回询问！
        /// </summary>
        public bool GoToPosition { get; set; }
        public string FastenPositionID { get; set; }
    }



    public class GetAbtractmodels : Command
    {
        public string AmID { get; set; }
        //  public bool FromDB { get; set; }
        /// <summary>
        /// 是否从数据库获取，true，对应前台时editor，false，对应前台开启的debug机制调试，非
        /// </summary>
        public bool FromDB { get; set; }
        //public int Password { get; set; }
    }


    public class SetNextPlace : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public string FastenPositionID { get; set; }
        //public string car { get; set; }
    }

    public class DouyinLogContent : Command
    {
        // public douyin.log Log { get; set; }
    }
    public class SetGroupLive : Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }

    public class BradCastDouyinPlayerIsWaiting : CommandNotify
    {
        //   public List<string> DetailInfo { get; set; }
        public string NickName { get; set; }
        public int PositionIndex { get; set; }
        public string Score { get; set; }
        //  public string AttackLength { get; set; }
        // public string Point { get; set; }
    }

    public class BradCastAllDouyinPlayerIsWaiting : Command
    {
        public List<string> DetailInfo { get; set; }
    }

    public class Douyin
    {
        //public class DouyinAdviseSelect : CommandNotify
        //{
        //    public string select { get; set; }
        //    public log Detail { get; set; }
        //}

        public class DouyinZoomIn : CommandNotify
        {
            //public string select { get; set; }
            //public log Detail { get; set; }
        }
        public class DouyinZoomOut : CommandNotify
        {
            //public string select { get; set; }
            //public log Detail { get; set; }
        }
        public class DouyinRotateLeft : CommandNotify
        {
            //public string select { get; set; }
            //public log Detail { get; set; }
        }
        public class DouyinRotateRight : CommandNotify
        {
            //public string select { get; set; }
            //public log Detail { get; set; }
        }
        public class DouyinRotateHigh : CommandNotify
        {
            //public string select { get; set; }
            //public log Detail { get; set; }
        }
        public class DouyinRotateLow : CommandNotify
        {
            //public string select { get; set; }
            //public log Detail { get; set; }
        }
        //public class MarketFlag : CommandNotify
        //{
        //    public string stance;

        //    public int x { get; set; }
        //    public int y { get; set; }
        //    public int z { get; set; }
        //    public string nickName { get; set; }
        //    public string Msg { get; set; }
        //}
        public class MarketFlags : CommandNotify
        {
            //public string stance;

            //public int x { get; set; }
            //public int y { get; set; }
            //public int z { get; set; }
            //public string nickName { get; set; }
            //public string Msg { get; set; }
            public List<MarketFlagsWebShowObj> Flags { get; set; }

            public class MarketFlagsWebShowObj
            {
                public string imgUrl { get; set; }
                public string stance { get; set; }
                /// <summary>
                /// add or existed
                /// </summary>
                public string type { get; set; }
                public int x { get; set; }
                public int y { get; set; }
                public int z { get; set; }
                public string nickName { get; set; }
                public string uid { get; set; }
            }
        }
    }

    public class GetRoadMesh : Command
    {
        public string RoadCode { get; set; }
    }

    /// <summary>
    /// GAFATWGABT=GetAddrFromAndToWhenGenerateAgreementBetweenTwo
    /// </summary>
    public class GAFATWGABT : CommonClass.Command
    {
        /*
         * GetAddrFromAndToWhenGenerateAgreementBetweenTwo
         */
        public string Key { get; set; }
        public string GroupKey { get; set; }

        public class ReturnResultObj
        {
            public string addrFrom { get; set; }
            public string addrTo { get; set; }
        }
    }

    public class WebSelectPassData : CommonClass.Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
        public string code { get; set; }
        public int height { get; set; }
    }
    public class Navigate : CommonClass.Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }
    public class CollectPassData : CommonClass.Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }
    public class ChargePassData : CommonClass.Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }
    public class ReturnHomePassData : CommonClass.Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }

    public class SqlCommand : CommonClass.Command
    {
        public string Sql { get; set; }
    }
    public class CheckIsAdministrator : CommonClass.Command
    {
        public string Key { get; set; }
        public string GroupKey { get; set; }
    }
    public class UploadPositionJson : CommonClass.Command
    {
        public string fileName { get; set; }
        public string jsonString { get; set; }
    }
}
