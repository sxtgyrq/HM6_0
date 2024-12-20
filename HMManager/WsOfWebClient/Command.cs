﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WsOfWebClient
{
    public class CheckSession : CommonClass.Command
    {
        public string session { get; set; }
        public string RefererAddr { get; set; }
    }

    public class MapRoadAndCrossMd5 : CommonClass.Command
    {
        public string mapRoadAndCrossMd5 { get; set; }
    }

    public class JoinGameSingle : CommonClass.Command
    {
        public string RefererAddr { get; set; }
    }


    public class CancleLookForBuildings : CommonClass.Command { }
    public class LookForBuildings : CommonClass.Command
    {
        public string selectObjName { get; set; }
        public double x { get; set; }
        public double z { get; set; }
    }
    public class GetRewardFromBuildings : CommonClass.Command
    {
        public string selectObjName { get; set; }
        public double x { get; set; }
        public double z { get; set; }
    }
    public class CreateTeam : CommonClass.Command
    {
        public string RefererAddr { get; set; }
    }
    public class DriverSelect : CommonClass.Command
    {
        public int driverIndex { get; set; }
    }
    public class JoinTeam : CommonClass.Command
    {
        //public string RefererAddr { get; set; }
    }

    public class SetPlayerName : CommonClass.Command
    {
        public string Name { get; set; }
    }
    public class SetCarsName : CommonClass.Command
    {
        public string[] Names { get; set; }
    }

    //public class SetCarName : CommonClass.Command
    //{
    //    public string Name { get; set; }
    //    public int CarIndex { get; set; }
    //}
    public class Promote : CommonClass.Command
    {
        public string pType { get; set; }
        //public string car { get; set; }
    }

    public class Collect : CommonClass.Command
    {
        public string cType { get; set; }
        //public string car { get; set; }
        public string fastenpositionID { get; set; }
        public int collectIndex { get; set; }
    }
    public class Attack : CommonClass.Command
    {
        public int Target { get; set; }
        public string TargetOwner { get; set; }
        //public string car { get; set; }
    }
    public class Skill1 : Attack
    {
    }
    public class Skill2 : Attack
    {
    }
    public class GetResistance
    {
        public string KeyLookfor { get; set; }
        public int RequestType { get; set; }
    }

    public class ViewAngle : CommonClass.Command
    {
        public double rotationY { get; set; }
        public string postionCrossKey { get; set; }
        public string uid { get; set; }
    }
    public class AskWhichToSelect : CommonClass.Command
    {

    }
    public class RequstToSaveInFile : CommonClass.Command { }

    public class AgreeTheTransaction : CommonClass.Command
    {
        public string hasCode { get; set; }
        public string businessAddr { get; set; }
    }
    public class CancleTheTransaction : AgreeTheTransaction { }

    public class ScoreTransaction : CommonClass.Command
    {
        public string scoreTranstractionToBitcoinAddr { get; set; }
        public double scoreTranstractionValue { get; set; }
    }

    public class AlipayReward : CommonClass.Command
    {
        public string SecretStr { get; set; }
    }
    public class StockCenerOrder : CommonClass.Command { }

    public class StockTradeInfo : CommonClass.Command
    {
        public string Msg { get; set; }
        public string Sign { get; set; }
    }
    public class CancelStock : CommonClass.Command
    {
        public string infosha256ID { get; set; }
    }


    public class Bust : Attack
    {
        public int Target { get; set; }
        public string TargetOwner { get; set; }
        //public string car { get; set; }
    }
    public class BuyDiamond : CommonClass.Command
    {
        public string pType { get; set; }
        public int count { get; set; }
    }
    public class Tax : CommonClass.Command
    {
        public int Target { get; set; }
        //public string car { get; set; }
    }
    public class Ability : CommonClass.Command
    {
        //  public int Target { get; set; }
        public string pType { get; set; }
        //public string car { get; set; }
        public int count { get; set; }
    }

    public class SetCarReturn : CommonClass.Command
    {
        //public string car { get; set; }
    }
    public class WebSelect : CommonClass.Command
    {
        public string code { get; set; }
        public int height { get; set; }
    }

    public class Donate : CommonClass.Command
    {
        //    objMain.ws.send(JSON.stringify({ c: 'Donate', dType: type, address: address }));
        public string dType { get; set; }
        public string address { get; set; }

    }

    public class GetSubsidize : CommonClass.Command
    {
        //    objMain.ws.send(JSON.stringify({ c: 'Donate', dType: type, address: address }));
        public string signature { get; set; }
        public string address { get; set; }
        public long value { get; set; }

    }
    public class UpdateLevel : CommonClass.Command
    {
        //    objMain.ws.send(JSON.stringify({ c: 'Donate', dType: type, address: address }));
        public string signature { get; set; }
        public string address { get; set; }

    }

    public class Collect1 : CommonClass.Command
    {
        public string cType { get; set; }
        public string car { get; set; }
    }
    public class Msg : CommonClass.Command
    {
        public string MsgPass { get; set; }
        public string To { get; set; }
    }

    public class GenerateAgreement : CommonClass.Command
    {
        public string addrFrom { get; set; }
        public string addrTo { get; set; }
        public double tranNum { get; set; }
        public string addrBussiness { get; set; }
    }

    public class GenerateAgreementBetweenTwo : CommonClass.Command
    {
        public double tranNum { get; set; }
        public double tranScoreNum { get; set; }
        public string addrBussiness { get; set; }
    }

    public class RewardSet : CommonClass.Command
    {
        public string administratorAddr { get; set; }
        public string signOfAdministrator { get; set; }
    }
    public class GenerateRewardAgreement : RewardSet
    {
        public string addrFrom { get; set; }
        public long tranNum { get; set; }
        public string addrBussiness { get; set; }
    }
    public class ModelTransSign : CommonClass.Command
    {
        public string msg { get; set; }
        public string sign { get; set; }
        public string addrBussiness { get; set; }
    }
    public class ModelTransSignWhenTrade : CommonClass.Command
    {
        public string msg { get; set; }
        public string sign { get; set; }
        public string addrBussiness { get; set; }
        public double tranScoreNum { get; set; }
    }
    public class RewardPublicSign : RewardSet
    {
        public string msg { get; set; }
        public string signOfAddrBussiness { get; set; }
        public string signOfAddrReward { get; set; }
    }
    public class AllStockAddr : RewardSet
    {
        public string bAddr { get; set; }
    }
    public class RewardInfomation : CommonClass.Command
    {
        public int Page { get; set; }
    }
    //public class RewardApplyInDB : CommonClass.databaseModel.traderewardapply
    //{
    //    public int satoshiShouldGet { get; set; }
    //    public string percentStr { get; set; }
    //}
    //public class RewardApply : CommonClass.ModelTranstraction.RewardApply
    //{

    //}

    public class RemoveTaskCopy : CommonClass.Command
    {
        public string Code { get; set; }
    }

    public class SetMaterial : CommonClass.Command
    {
        public string Key { get; set; }
        public string Base64 { get; set; }
    }
    public class SmallMapClick : CommonClass.Command
    {
        //  public string cType { get;  set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public double radius { get; set; }
    }
    public class GoToDoCollectOrPromote : CommonClass.Command
    {
        public string FastenPositionID { get; set; }
    }
}
