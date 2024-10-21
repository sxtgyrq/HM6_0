using CommonClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {
        internal void NavigateF(string key, GetRandomPos gp)
        {
            if (this._PlayerInGroup.ContainsKey(key))
            {
                askWhetherGoToPositon2(key, gp);
            }
            //throw new NotImplementedException();
        }
        internal void askWhetherGoToPositon2(string key, GetRandomPos gp)
        {
            bool isFinished;
            string FinishedMsg;
            GetFineshedInfomation(key, out isFinished, out FinishedMsg);

            List<string> sendMsgs = new List<string>();

            if (this._PlayerInGroup.ContainsKey(key))
            {
                double minX = 360;
                double minY = 360;
                double maxX = -360;
                double maxY = -360;
                var player = this._PlayerInGroup[key];
                //   FastonPosition from;
                ModelBase.Data.FPPosition fromObj;
                if (player.getCar().targetFpIndex >= 0)
                    fromObj = gp.GetFpByIndex(player.getCar().targetFpIndex);
                else
                    fromObj = gp.GetFpByIndex(player.StartFPIndex);
                setBoundry(fromObj, ref minX, ref minY, ref maxX, ref maxY);
                foreach (var item in this._collectPosition)
                {
                    var collectPosition = gp.GetFpByIndex(item.Value);
                    setBoundry(collectPosition, ref minX, ref minY, ref maxX, ref maxY);
                }

                {
                    var milePosition = gp.GetFpByIndex(this.promoteMilePosition);
                    setBoundry(milePosition, ref minX, ref minY, ref maxX, ref maxY);

                    var volumePosition = gp.GetFpByIndex(this.promoteVolumePosition);
                    setBoundry(volumePosition, ref minX, ref minY, ref maxX, ref maxY);

                    //var speedPosition = gp.GetFpByIndex(this.promoteSpeedPosition);
                    //setBoundry(speedPosition, ref minX, ref minY, ref maxX, ref maxY);
                }



                var length = Math.Max(maxY - minY, maxX - minX) * 1.1;

                var centerX = (minX + maxX) / 2;
                var centerY = (minY + maxY) / 2;
                minX = centerX - length / 2;
                maxX = centerX + length / 2;
                minY = centerY - length / 2;
                maxY = centerY + length / 2;
                this.SetData(minX, maxX, minY, maxY, fromObj, player, isFinished, FinishedMsg, key, gp, ref sendMsgs);
            }
            Startup.sendSeveralMsgs(sendMsgs);
        }

        private void GetFineshedInfomation(string key, out bool isFinished, out string finishedMsg)
        {
            isFinished = false;
            finishedMsg = "";
            //   throw new NotImplementedException();
        }

        private void setBoundry(ModelBase.Data.FPPosition positon, ref double minX, ref double minY, ref double maxX, ref double maxY)
        {
            if (positon.lon < minX)
            {
                minX = positon.lon;
            }
            if (positon.lat < minY)
            {
                minY = positon.lat;
            }
            if (positon.lon > maxX)
            {
                maxX = positon.lon;
            }
            if (positon.lat > maxY)
            {
                maxY = positon.lat;
            }
        }

        void SetData(double minX, double maxX, double minY, double maxY, ModelBase.Data.FPPosition fromObj, Player player, bool isFinished, string FinishedMsg, string key, GetRandomPos gp, ref List<string> sendMsgs)
        {
            var length = Math.Max(maxY - minY, maxX - minX) * 1.1;

            var centerX = (minX + maxX) / 2;
            var centerY = (minY + maxY) / 2;
            minX = centerX - length / 2;
            maxX = centerX + length / 2;
            minY = centerY - length / 2;
            maxY = centerY + length / 2;

            var taskValue = AbilityAndState.GetTaskValueByGroupNumber(this.groupNumber);
            string taskName;
            taskName = $"模拟收集{(taskValue / 100)}.{(taskValue % 100).ToString("D2")}元任务";

            taskName = $"模拟收集{(taskValue / 100)}.{(taskValue % 100).ToString("D2")}元任务";
            BradCastWhereToGoInSmallMap smallMap;

            {
                smallMap = new BradCastWhereToGoInSmallMap()
                {
                    minX = Convert.ToSingle(minX),
                    minY = Convert.ToSingle(minY),
                    maxX = Convert.ToSingle(maxX),
                    maxY = Convert.ToSingle(maxY),
                    c = "BradCastWhereToGoInSmallMap",
                    currentX = Convert.ToSingle(fromObj.lon),
                    currentY = Convert.ToSingle(fromObj.lat),
                    data = new List<BradCastWhereToGoInSmallMap.DataItem>() { },
                    WebSocketID = player.WebSocketID,
                    isFineshed = isFinished,
                    TimeStr = FinishedMsg,
                    ResultMsg = "",
                    RecordedInDB = false,
                    base64 = "",
                    groupNumber = player.Group.groupNumber,
                    TaskName = taskName,
                    BTCAddr = string.IsNullOrEmpty(player.BTCAddress) ? "" : player.BTCAddress,
                    HasValueToImproveSpeed = false,
                    Live = false//this.WhetherGoLive
                };
            }
            if (player.getCar().targetFpIndex >= 0)
            {
                AddPath(ref smallMap, player.getCar().targetFpIndex, this.promoteMilePosition, "mile", gp);
                AddPath(ref smallMap, player.getCar().targetFpIndex, this.promoteVolumePosition, "volume", gp);
                // AddPath(ref smallMap, player.getCar().targetFpIndex, this.promoteSpeedPosition, "speed", gp);
                var rank = (from cItem in this._collectPosition
                            orderby this.getLength(gp.GetFpByIndex(cItem.Value), gp.GetFpByIndex(player.getCar().targetFpIndex)) ascending
                            select cItem.Key).ToList();

                for (int i = 0; i < rank.Count; i++)
                {
                    AddPath(ref smallMap, player.getCar().targetFpIndex, this._collectPosition[rank[i]], $"collect", gp);
                }
                AddPath(ref smallMap, player.getCar().targetFpIndex, player.StartFPIndex, "home", gp);
            }
            else
            {
                AddPath(ref smallMap, player.StartFPIndex, this.promoteMilePosition, "mile", gp);
                AddPath(ref smallMap, player.StartFPIndex, this.promoteVolumePosition, "volume", gp);
                //  AddPath(ref smallMap, player.StartFPIndex, this.promoteSpeedPosition, "speed", gp);

                var rank = (from cItem in this._collectPosition
                            orderby this.getLength(gp.GetFpByIndex(cItem.Value), gp.GetFpByIndex(player.StartFPIndex)) ascending
                            select cItem.Key).ToList();

                for (int i = 0; i < rank.Count; i++)
                {
                    AddPath(ref smallMap, player.StartFPIndex, this._collectPosition[rank[i]], $"collect", gp);
                }
            }
            var url = this._PlayerInGroup[key].FromUrl;
            var sendMsg = Newtonsoft.Json.JsonConvert.SerializeObject(smallMap);
            sendMsgs.Add(url);
            sendMsgs.Add(sendMsg);
        }

        private double getLength(ModelBase.Data.FPPosition A, ModelBase.Data.FPPosition B)
        {
            return CommonClass.Geography.getLengthOfTwoPoint.GetDistance(A.lat, A.lon, 0, B.lat, B.lon, 0);
        }

        private void AddPath(ref BradCastWhereToGoInSmallMap smallMap, int positinA, int positinB, string DataType, GetRandomPos gp)
        {
            const int ImageWidth = 600;
            List<int> path = new List<int>();
            //  Dictionary<int, Dictionary<int, bool>> numberUsed = new Dictionary<int, Dictionary<int, bool>>();
            var r = gp.GetAFromB(positinA, positinB);
            int lastX = 0;
            int lastY = 0;
            for (int i = 0; i < r.Count; i++)
            {
                var x = Convert.ToInt32((r[i].lon - smallMap.minX) / (smallMap.maxX - smallMap.minX) * ImageWidth);
                var y = Convert.ToInt32((r[i].lat - smallMap.minY) / (smallMap.maxY - smallMap.minY) * ImageWidth);

                //lastX = x;
                //lastY = y;
                if (x < 0) x = 0;
                else if (x > ImageWidth) x = ImageWidth;

                if (y < 0) y = 0;
                else if (y > ImageWidth) y = ImageWidth;
                if (i == 0)
                {
                    path.Add(x);
                    path.Add(y);
                    lastX = x;
                    lastY = y;
                }
                else if (lastX == x && lastY == y)
                { }

                else
                {
                    path.Add(x);
                    path.Add(y);
                    lastX = x;
                    lastY = y;
                }
            }
            smallMap.data.Add(new BradCastWhereToGoInSmallMap.DataItem()
            {
                DataType = DataType,
                Path = path.ToArray()
            });
        }


        internal void SmallMapClickF(SmallMapClick smc, GetRandomPos gp)
        {
            List<string> notifyMsgs = new List<string>();

            {
                List<ModelBase.Data.FPPosition> fps = new List<ModelBase.Data.FPPosition>();
                List<string> selection = new List<string>();

                int collectSelect = -1;//有结果0-37，无结果-1
                if (this._PlayerInGroup.ContainsKey(smc.Key))
                {
                    var player = this._PlayerInGroup[smc.Key];
                    if (player.getCar().state == Car.CarState.waitAtBaseStation)
                    {
                        foreach (var item in this._collectPosition)
                        {
                            var collectPosition = gp.GetFpByIndex(item.Value);
                            if (isInRegion(smc, collectPosition))
                            {
                                fps.Add(collectPosition);
                                selection.Add("collect");
                                collectSelect = item.Key;
                            }
                        }
                        {
                            var milePosition = gp.GetFpByIndex(this.promoteMilePosition);
                            if (isInRegion(smc, milePosition))
                            {
                                fps.Add(milePosition);
                                selection.Add("mile");
                            }

                            var volumePosition = gp.GetFpByIndex(this.promoteVolumePosition);
                            if (isInRegion(smc, volumePosition))
                            {
                                fps.Add(volumePosition);
                                selection.Add("volume");
                            }

                            //var speedPosition = gp.GetFpByIndex(this.promoteSpeedPosition);
                            //if (isInRegion(smc, speedPosition))
                            //{
                            //    fps.Add(speedPosition);
                            //    selection.Add("speed");
                            //}
                        }
                        if (fps.Count == 0)
                        {
                            askWhetherGoToPositon2(smc.Key, gp);
                        }
                        else if (fps.Count == 1)
                        {
                            //;
                            switch (selection[0])
                            {
                                case "collect":
                                    {
                                        //var Fp = fps[0];

                                        var rank = (from item in this._collectPosition
                                                    orderby this.getLength(gp.GetFpByIndex(item.Value), gp.GetFpByIndex(player.StartFPIndex)) ascending
                                                    select gp.GetFpByIndex(item.Value)).ToList();
                                        CollectFunction(player, fps, collectSelect, gp, rank);
                                    }; break;
                                case "mile":
                                    {
                                        // PromoteClickFunction(player, fps, selection[0], gp, ref notifyMsgs);
                                        //that.updatePromote(new SetPromote()
                                        //{
                                        //    c = "SetPromote",
                                        //    GroupKey = smc.GroupKey,
                                        //    Key = smc.Key,
                                        //    pType = "mile"
                                        //}, gp);
                                    }; break;
                                case "speed":
                                    {
                                        //  PromoteClickFunction(player, fps, selection[0], gp, ref notifyMsgs);
                                        //that.updatePromote(new SetPromote()
                                        //{
                                        //    c = "SetPromote",
                                        //    GroupKey = smc.GroupKey,
                                        //    Key = smc.Key,
                                        //    pType = "speed"
                                        //}, gp);
                                    }; break;
                                case "volume":
                                    {
                                        //     PromoteClickFunction(player, fps, selection[0], gp, ref notifyMsgs);
                                    }; break;

                            }
                        }
                        else if (fps.Count > 1)
                        {
                            askWhetherGoToPositon3(smc.Key, gp, smc, smc.radius / 2);
                        }
                    }
                    else if (player.getCar().state == Car.CarState.waitOnRoad)
                    {
                        foreach (var item in this._collectPosition)
                        {
                            var collectPosition = gp.GetFpByIndex(item.Value);
                            if (isInRegion(smc, collectPosition))
                            {
                                fps.Add(collectPosition);
                                selection.Add("collect");
                                collectSelect = item.Key;
                            }
                        }
                        {
                            var milePosition = gp.GetFpByIndex(this.promoteMilePosition);
                            if (isInRegion(smc, milePosition))
                            {
                                fps.Add(milePosition);
                                selection.Add("mile");
                            }

                            var volumePosition = gp.GetFpByIndex(this.promoteVolumePosition);
                            if (isInRegion(smc, volumePosition))
                            {
                                fps.Add(volumePosition);
                                selection.Add("volume");
                            }

                            //var speedPosition = gp.GetFpByIndex(this.promoteSpeedPosition);
                            //if (isInRegion(smc, speedPosition))
                            //{
                            //    fps.Add(speedPosition);
                            //    selection.Add("speed");
                            //}

                            var homePositon = gp.GetFpByIndex(player.StartFPIndex);
                            if (isInRegion(smc, homePositon))
                            {
                                fps.Add(homePositon);
                                selection.Add("home");
                            }
                        }
                        if (fps.Count == 0)
                        {
                            askWhetherGoToPositon2(smc.Key, gp);
                        }
                        else if (fps.Count == 1)
                        {
                            //;
                            switch (selection[0])
                            {
                                case "collect":
                                    {
                                        var rank = (from item in this._collectPosition
                                                    orderby this.getLength(
                                                        gp.GetFpByIndex(item.Value),
                                                        gp.GetFpByIndex(player.getCar().targetFpIndex)) ascending
                                                    select gp.GetFpByIndex(item.Value)).ToList();
                                        CollectFunction(player, fps, this._collectPosition[collectSelect], gp, rank);
                                    }; break;
                                case "home":
                                    {
                                        //that.OrderToReturn(new OrderToReturn()
                                        //{
                                        //    c = "OrderToReturn",
                                        //    GroupKey = smc.GroupKey,
                                        //    Key = smc.Key,
                                        //}, gp);
                                    }; break;
                                case "mile":
                                    {
                                        //   PromoteClickFunction(player, fps, selection[0], gp, ref notifyMsgs);
                                    }; break;
                                case "speed":
                                    {
                                        // PromoteClickFunction(player, fps, selection[0], gp, ref notifyMsgs);
                                    }; break;
                                case "volume":
                                    {
                                        // PromoteClickFunction(player, fps, selection[0], gp, ref notifyMsgs);
                                    }; break;

                            }
                        }
                        else if (fps.Count > 1)
                        {
                            askWhetherGoToPositon3(smc.Key, gp, smc, smc.radius / 2);
                        }
                    }
                }
            }
            Startup.sendSeveralMsgs(notifyMsgs);
        }

        private bool isInRegion(SmallMapClick smc, ModelBase.Data.FPPosition collectPosition)
        {
            return (collectPosition.lon - smc.lon) * (collectPosition.lon - smc.lon) + (collectPosition.lat - smc.lat) * (collectPosition.lat - smc.lat) < smc.radius * smc.radius;
        }

        private void askWhetherGoToPositon3(string key, GetRandomPos gp, SmallMapClick smc, double minLength)
        {
            bool isFinished;
            string FinishedMsg;
            GetFineshedInfomation(key, out isFinished, out FinishedMsg);
            List<string> sendMsgs = new List<string>();

            if (this._PlayerInGroup.ContainsKey(key))
            {
                var player = this._PlayerInGroup[key];
                ModelBase.Data.FPPosition fromObj;
                if (player.getCar().targetFpIndex >= 0)
                    fromObj = gp.GetFpByIndex(player.getCar().targetFpIndex);
                else
                    fromObj = gp.GetFpByIndex(player.StartFPIndex);

                var length = minLength / 2 * 24;//这里保证新的图像，不从在复选。

                var centerX = smc.lon;
                var centerY = smc.lat;
                double minX = centerX - length / 2;
                double maxX = centerX + length / 2;
                double minY = centerY - length / 2;
                double maxY = centerY + length / 2;
                this.SetData(minX, maxX, minY, maxY, fromObj, player, isFinished, FinishedMsg, key, gp, ref sendMsgs);
            }
            Startup.sendSeveralMsgs(sendMsgs);
        }


        public void CollectFunction(Player player, List<ModelBase.Data.FPPosition> fps, int collecIndex, GetRandomPos gp, List<ModelBase.Data.FPPosition> rank)
        {
            var fp = gp.GetFpByIndex(this._collectPosition[collecIndex]);
            player.rm.WebNotify(player, $"{fp.fPName}处有宝贝，请前往收集。");
        }

    }
}
