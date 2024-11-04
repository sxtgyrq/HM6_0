using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
        internal bool UpdateCurrentPosition(Player player, GetRandomPos grp, WebSelectPassData wspd, ref List<string> notifyMsg)
        {
            grp.GetFpByIndex(player.getCar().targetFpIndex);
            var target = grp.GetSelections(player.getCar().targetFpIndex);

            if (player.getCar().targetFpIndex == -1)
            {
                this.that.WebNotify(player, "当前无人机处于坠毁状态，请点击复活。");
            }
            else
            {
                var targetFind = -1;
                int index = -1;
                for (int i = 0; i < target.Count; i++)
                {
                    if (grp.GetFpByIndex(target[i]).fPCode == wspd.code && grp.GetFpByIndex(target[i]).Height == wspd.height)
                    {
                        targetFind = target[i];
                        index = i;
                        break;
                    }
                }
                if (targetFind >= 0)
                {
                    var enegy = grp.GetEnegy(player.getCar().targetFpIndex);
                    var costEnegy = enegy[index];

                    var timeCostToWait = DateTime.Now - player.LastActionTime;
                    var sumNeed = costEnegy + Convert.ToInt32(timeCostToWait.TotalSeconds);
                    if (player.getCar().ability.leftMile > sumNeed)
                    {
                        player.getCar().ability.setCostMiles(
                            player.getCar().ability.costMiles + sumNeed,
                            player,
                            player.getCar(),
                            ref notifyMsg);
                        player.getCar().targetFpIndexSet(targetFind, ref notifyMsg);
                        var fpFound = grp.GetFpByIndex(targetFind);
                        if (fpFound.CanGetScore)
                            player.rm.WebNotify(player, $"到达了{fpFound.fPName}");
                        else
                            player.rm.WebNotify(player, $"到达了{fpFound.fPName}上方{fpFound.Height}米处。");
                        player.rm.WebNotify(player, $"等待消耗{Convert.ToInt32(timeCostToWait.TotalSeconds)}能量。飞行消耗{costEnegy}能量。一共");
                        // player.rm.WebNotify(player, $"");
                        // var  
                        player.LastActionTime = DateTime.Now;

                        //   Set
                        if ((!player.improvementRecord.CollectIsDouble) && RewardDouble(player, targetFind, grp) > Program.rm.rm.NextDouble())
                        {
                            var fp = grp.GetFpByIndex(targetFind);
                            if (player.improvementRecord.addUsedCode(fp.fPCode))
                                player.improvementRecord.addAttack(player, ref notifyMsg);
                            else
                                this.that.WebNotify(player, $"本次航行，【{fp.fPName}】已经提供一次双倍收集服务！");

                        }


                        return true;
                    }
                    else
                    {
                        if (player.StartFPIndex == player.getCar().targetFpIndex)
                        {
                            that.WebNotify(player, "点击旋转的风车，可以给无人机补充能量！");
                        }
                        else
                        {
                            if (player.Group.groupNumber == 1)
                            {
                                that.WebNotify(player, "能量不足！你可以点击卫星来回城。");
                            }
                            else
                            {
                                that.WebNotify(player, "能量不足！你可以点击卫星来回城，或者请求队友来救援！");
                            }
                        }
                    }


                    // if (RewardDouble(player, targetFind, grp) > Program.rm.rm.NextDouble())


                }


            }
            return false;
            //throw new NotImplementedException();
        }

        private double RewardDouble(Player player, int targetFind, GetRandomPos grp)
        {
            double percentValue = 0;
            if (targetFind != this.StartFPIndex)
            {
                long satoshi = 0;
                var currentCode = grp.GetFpByIndex(targetFind).fPCode;
                // grp.{}
                {
                    var targetPlus = targetFind;
                    while (targetPlus < grp.GetFpCount())
                    {
                        var findFp = grp.GetFpByIndex(targetPlus);
                        if (findFp.fPCode != currentCode)
                        {
                            break;
                        }
                        satoshi += grp.GetSatoshi(player.BTCAddress, targetPlus);
                        targetPlus++;
                    }
                }
                {
                    var targetMinus = targetFind - 1;
                    while (targetMinus >= 0)
                    {
                        var findFp = grp.GetFpByIndex(targetMinus);
                        if (findFp.fPCode != currentCode)
                        {
                            break;
                        }
                        satoshi += grp.GetSatoshi(player.BTCAddress, targetMinus);
                        targetMinus--;
                    }
                }
                if (string.IsNullOrEmpty(player.BTCAddress))
                {
                    percentValue = 0.05;
                }
                else
                {
                    const double stepValue = 0.07;
                    percentValue = 0.1;
                    if (satoshi < 1)
                    {
                        percentValue = 0.03 + stepValue;
                    }
                    else if (satoshi < 10)
                    {
                        percentValue = 0.03 + stepValue * 1 + stepValue * (satoshi - 1) / 9;
                    }
                    else if (satoshi < 100)
                    {
                        percentValue = 0.03 + stepValue * 2 + stepValue * (satoshi - 10) / 90;
                    }
                    else if (satoshi < 1000)
                    {
                        percentValue = 0.03 + stepValue * 3 + stepValue * (satoshi - 100) / 900;
                    }
                    else if (satoshi < 10000)
                    {
                        percentValue = 0.03 + stepValue * 4 + stepValue * (satoshi - 1000) / 9000;
                    }
                    else if (satoshi < 100000)
                    {
                        percentValue = 0.03 + stepValue * 5 + stepValue * (satoshi - 10000) / 90000;
                    }
                    else if (satoshi < 1000000)
                    {
                        percentValue = 0.03 + stepValue * 6 + stepValue * (satoshi - 100000) / 900000;
                    }
                    else if (satoshi < 10000000)
                    {
                        percentValue = 0.03 + stepValue * 7 + stepValue * (satoshi - 1000000) / 9000000;
                    }
                    else if (satoshi < 100000000)
                    {
                        percentValue = 0.03 + stepValue * 8 + stepValue * (satoshi - 10000000) / 90000000;
                    }
                    else
                    {
                        percentValue = 0.03 + stepValue * 9 + stepValue;
                    }
                }
            }
            return percentValue;
        }
    }
}
