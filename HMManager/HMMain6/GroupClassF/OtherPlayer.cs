﻿using CommonClass;
using HMMain6.interfaceOfEngine;
using Org.BouncyCastle.Pqc.Crypto.Falcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HMMain6.GroupClassF
{
    public partial class GroupClass
    {


       
    }

    public partial class GroupClass
    {


        internal void AddOtherPlayer(string key, ref List<string> msgsWithUrl)
        {
            if (this._PlayerInGroup.ContainsKey(key))
            {
                var players = getGetAllRoles();
                for (var i = 0; i < players.Count; i++)
                {
                    if (players[i].Key == key)
                    {
                        /*
                         * 保证自己不会算作其他人
                         */
                    }
                    else
                    {
                        {
                            /*
                             * 告诉自己，场景中有哪些人！
                             * 告诉场景中的其他人，场景中有我！
                             */
                            {
                                var self = this._PlayerInGroup[key];
                                var other = players[i];
                                that.addPlayerRecord(self, other, ref msgsWithUrl);

                            }
                            {
                                var self = players[i];
                                var other = this._PlayerInGroup[key];
                                that.addPlayerRecord(self, other, ref msgsWithUrl);
                            }
                        }
                    }
                }
            }
        }

        public List<Player> getGetAllRoles()
        {
            return that.getGetAllRoles(this);
        }


    }
}
