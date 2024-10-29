using CommonClass;
//using CommonClass.driversource;
using HMMain6.interfaceOfEngine;
using HMMain6.RoomMainF;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using NBitcoin.Protocol;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using static HMMain6.Car;
using static HMMain6.Engine_MagicEngine;

//using static HMMain6.Engine_MagicEngine;
//using static HMMain6.Manager_Driver.ConfuseManger;
using static HMMain6.RoomMainF.RoomMain;
//using OssModel = Model;

namespace HMMain6
{
    public abstract class Engine : EngineAndManger
    {
    }
    public abstract class Engine_ContactEngine : Engine
    {




    }
    public partial class Engine_MagicEngine : Engine_ContactEngine, interfaceOfEngine.engine
    {


        public Engine_MagicEngine(RoomMain roomMain)
        {
            this.roomMain = roomMain;
        }
        public delegate void AttackMagicChanged(Player role, ref List<string> notifyMsgs);
        public delegate void CollectCountChanged(Player role, ref List<string> notifyMsgs);
        // public Engine_MagicEngine.CollectCountChanged collectMagicChanged;

        //internal void ConfigMagic(Player role)
        //{
        //    // role.confuseRecord = new Manager_Driver.ConfuseManger();
        //    role.improvementRecord = new Manager_Driver.ImproveManager();
        //    //role.speedMagicChanged = this.speedMagicChanged;
        //    //role.nitrogenValueChanged = this.nitrogenValueChanged;
        //    //role.attackMagicChanged = this.attackMagicChanged;
        //    //role.collectMagicChanged = this.collectMoneyCountMagicChanged;
        //    //role.defenceMagicChanged = this.defenceMagicChanged;
        //    //role.confusePrepareMagicChanged = this.confusePrepareMagicChanged;
        //    //role.lostPrepareMagicChanged = this.lostPrepareMagicChanged;
        //    //role.ambushPrepareMagicChanged = this.ambushPrepareMagicChanged;
        //    //role.controlPrepareMagicChanged = this.controlPrepareMagicChanged;

        //    //role.confuseMagicChanged = this.confuseMagicChanged;
        //    //role.loseMagicChanged = this.loseMagicChanged;

        //    //role.fireMagicChanged = this.fireMagicChanged;
        //    //role.waterMagicChanged = this.waterMagicChanged;
        //    //role.electricMagicChanged = this.electricMagicChanged;
        //}
    }

}
