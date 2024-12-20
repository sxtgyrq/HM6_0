﻿var nyrqUrl =
{
    set: function (addr) {
        const url = new URL(window.location.href);
        url.searchParams.set('addr', addr);
        window.history.replaceState(null, null, url);
    },
    get: function () {
        const url = new URL(window.location.href);
        var parameter = url.searchParams.get('addr');
        if (parameter == null || parameter == undefined) {
            return '';
        }
        else return parameter;
    }
}

function TaskClass(s, c) {
    this._state = s;
    this._carSelect = c;
    this._oldState = s;
    this._newState = s;
}
TaskClass.prototype.__defineGetter__("state", function () {
    //ShowGetInfo("Age"); 
    return this._state;
});

TaskClass.prototype.__defineSetter__("state", function (val) {
    //if (this._state == 'getTax' && val != 'getTax') {
    //    Tax.trunOffAnimate();
    //}
    //else if (this._state != 'getTax' && val == 'getTax') {
    //    Tax.trunOnAnimate();
    //}
    this._state = val;
    if (this._oldState != this._state) {
        //  operatePanel.refresh();
        this._oldState = val;
    }
    //ShowSetInfo("Age");
});

TaskClass.prototype.__defineGetter__("carSelect", function () {
    //ShowGetInfo("Age"); 
    return this._carSelect;
});

TaskClass.prototype.__defineSetter__("carSelect", function (val) {
    this._carSelect = val;
    //ShowSetInfo("Age");
});

import { gearRecive, gearShow } from './modelShow/gear.js';

var objMain =
{
    debug: (function () {
        if (window.location.hostname == 'www.nyrq123.com') return 2;
        else if (window.location.hostname == '192.168.0.112') return 1;
        else return 0;
    })(),
    indexKey: '',
    displayName: '',
    positionInStation: 0,
    MoneyForSave: 0,
    Money: 0,
    state: '',
    receivedState: '',
    scene: null,
    renderer: null,
    labelRenderer: null,
    centerPosition: { lon: 112.573463, lat: 37.891474 },
    roadGroup: null,
    basePoint: null,
    fpIndex: -1,
    othersBasePoint: {},
    playerGroup: null,
    carGroup: null,
    collectGroup: null,
    getOutGroup: null,
    batteryGroup: null,
    robotModel: null,
    buildingModel: {},
    buildingGroup: null,
    fightSituationGroup: null,
    cars: {},
    rmbModel: {},
    ModelInput: {
        speed: null,
        attack: null,
        shield: null,
        confusePrepare: null,
        lostPrepare: null,
        ambushPrepare: null,
        water: null,
        direction: null,
        directionArrow: null,
        directionArrowA: null,
        directionArrowB: null,
        directionArrowC: null,
        Opponent: null,
        Teammate: null,
        NitrogenEffect: null,
        CollectCoinIcon: null,
        GoldIngotIcon: null,
        vehicle: null,
        cubeCore: null,
        compass: null,
        turbine: null,
        satelite: null,
        battery: null,
        doubleRewardIcon: null,
        gearIcon: null
    },
    shieldGroup: null,
    confusePrepareGroup: null,
    lostPrepareGroup: null,
    ambushPrepareGroup: null,
    waterGroup: null,
    waterMarkGroup: null,
    fireGroup: null,
    fireMarkGroup: null,
    lightningGroup: null,
    lightningMarkGroup: null,
    absorbGroup: null,
    directionGroup: null,
    crossSelectionsOperator: null,
    targetGroup: null,
    marketGroup: null,
    bitcoinCharacterGroup: null,
    clock: null,
    leaveGameModel: null,
    profileModel: null,
    light1: null,
    controls: null,
    raycaster: null,
    mouse: null,
    raycasterOfSelector: null,
    selectorPosition: { x: 0, y: 0.5 },
    selectObj: { obj: null, type: '' },
    canSelect: false,
    carsNames: null,
    carsAnimateData: {},
    PromoteState: -1,
    PromotePositions:
    {
        mile: null,
        business: null,
        volume: null,
        speed: null
    },
    PromoteList: ['mile', 'business', 'volume', 'speed'],
    PromoteDiamondCount:
    {
        mile: 0,
        business: 0,
        volume: 0,
        speed: 0
    },
    CollectPosition: {},
    diamondGeometry: null,
    mirrorCubeCamera: null,
    promoteDiamond: null,
    columnGroup: null,
    mainF:
    {
        drawLineOfFpToRoad: function (fp, group, color, lineName) {
            {
                var objName = 'approach_' + lineName;
                if (group == null || group == undefined) { }
                else if (group.getObjectByName(objName) == null || group.getObjectByName(objName) == undefined) {
                    var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
                    var end = new THREE.Vector3(MercatorGetXbyLongitude(fp.positionLongitudeOnRoad), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.positionLatitudeOnRoad));
                    var lineGeometry = new THREE.Geometry();
                    lineGeometry.vertices.push(start);
                    lineGeometry.vertices.push(end);
                    var lineMaterial = new THREE.LineBasicMaterial({ color: color });
                    var line = new THREE.Line(lineGeometry, lineMaterial);
                    line.name = objName;
                    group.add(line);
                }
            }
        },
        lookAtPosition: function (fp) {

            var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
            var end = new THREE.Vector3(MercatorGetXbyLongitude(fp.positionLongitudeOnRoad), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.positionLatitudeOnRoad));

            var cc = new Complex(end.x - start.x, end.z - start.z);
            cc.toOne();
            var minDistance = objMain.controls.minDistance * 5;
            var maxPolarAngle = objMain.controls.maxPolarAngle - Math.PI / 30;
            {
                var planePosition = new THREE.Vector3(start.x + cc.r * minDistance * Math.sin(maxPolarAngle), start.y + minDistance * Math.abs(Math.cos(maxPolarAngle)), start.z + cc.i * minDistance * Math.sin(maxPolarAngle));
                objMain.camera.position.set(planePosition.x, planePosition.y, planePosition.z);

                objMain.controls.target.set(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
                objMain.camera.lookAt(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
                objMain.renderer.render(objMain.scene, objMain.camera);
                objMain.labelRenderer.render(objMain.scene, objMain.camera);
            }
        },
        lookAtPosition2: function () {

            //var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), 0, -MercatorGetYbyLatitude(fp.Latitde));
            //var end = new THREE.Vector3(MercatorGetXbyLongitude(fp.positionLongitudeOnRoad), 0, -MercatorGetYbyLatitude(fp.positionLatitudeOnRoad));

            var cc = new Complex(1, 0);
            cc.toOne();
            var minDistance = objMain.controls.minDistance * 1.1;
            var maxPolarAngle = objMain.controls.maxPolarAngle - Math.PI / 30;
            {
                var start = objMain.transtractionData;
                var planePosition = new THREE.Vector3(start.x + cc.r * minDistance * Math.sin(maxPolarAngle), start.y + minDistance * Math.cos(maxPolarAngle), start.z + cc.i * minDistance * Math.sin(maxPolarAngle));
                objMain.camera.position.set(planePosition.x, planePosition.y, planePosition.z);

                objMain.controls.target.set(start.x, 0, start.z);
                objMain.camera.lookAt(start.x, 0, start.z);
            }
        },
        initilizeCars: function (fp, color, key, isSelf, postionInStation) {
            var carName = 'car' + '_' + key;
            if (objMain.carGroup == null || objMain.carGroup == undefined) { }
            else if (objMain.carGroup.getObjectByName(carName) == null || objMain.carGroup.getObjectByName(carName) == undefined) {
                if (isSelf == undefined) {
                    isSelf = true;
                }
                var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde))
                var end = new THREE.Vector3(MercatorGetXbyLongitude(fp.positionLongitudeOnRoad), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.positionLatitudeOnRoad))
                var cc = new Complex(end.x - start.x, end.z - start.z);
                cc.toOne();

                var positon1 = cc.multiply(new Complex(-0.309016994, 0.951056516));
                var positon2 = positon1.multiply(new Complex(0.809016994, 0.587785252));
                var positon3 = positon2.multiply(new Complex(0.809016994, 0.587785252));
                var positon4 = positon3.multiply(new Complex(0.809016994, 0.587785252));
                var positon5 = positon4.multiply(new Complex(0.809016994, 0.587785252));

                var positons = [positon1, positon2, positon3, positon4, positon5];

                var names = ['carA', 'carB', 'carC', 'carD', 'carE'];
                var percentOfPosition = 0.25;
                {
                    var i = postionInStation;
                    var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
                    var end = new THREE.Vector3(start.x + positons[i].r * percentOfPosition, MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, start.z + positons[i].i * percentOfPosition);
                    var lineGeometry = new THREE.Geometry();
                    lineGeometry.vertices.push(start);
                    lineGeometry.vertices.push(end);
                    var lineMaterial = new THREE.LineBasicMaterial({ color: color });
                    var line = new THREE.Line(lineGeometry, lineMaterial);
                    line.name = 'carRoad' + '_' + key;
                    if (key === objMain.indexKey) {
                        isSelf = true;
                    }

                    line.userData = { objectType: 'carRoad', parent: key, };
                    objMain.carGroup.add(line);
                    var model = null;
                    if (isSelf) {
                        model = objMain.cars[names[i]].clone();
                    }
                    else {
                        model = objMain.cars['carO'].clone();
                    }
                    model.name = carName;
                    model.position.set(end.x, end.y, end.z);
                    model.scale.set(0.002, 0.002, 0.002);
                    model.rotateY(-positons[i].toAngle());
                    model.userData = { objectType: 'car', parent: key, };
                    objMain.carGroup.add(model);

                    if (isSelf) {
                        stateSet.nitrogeneffect.add();
                    }
                }
            }
        },
        lookTwoPositionCenter: function (p1, p2) {

            var start = p1;
            var end = p2;
            var lengthTwoPoint = objMain.mainF.getLength(start, end);
            if (lengthTwoPoint > 0.2) {
                var cc = new Complex(end.x - start.x, end.z - start.z);
                cc.toOne();
                var x1 = lengthTwoPoint / (1 / Math.tan(Math.PI / 6) - 1 / Math.tan(Math.PI * 42 / 180));
                var x2 = x1 / Math.tan(Math.PI * 42 / 180);
                var x3 = x2 + lengthTwoPoint / 2;
                var minDistance = x3 / Math.cos(Math.PI * 36 / 180);
            }
            //var minDistance = objMain.controls.minDistance * 1.1;
            var maxPolarAngle = Math.PI * 54 / 180;
            {
                var planePosition = new THREE.Vector3(start.x + cc.r * minDistance * Math.sin(maxPolarAngle), start.y + minDistance * Math.cos(maxPolarAngle), start.z + cc.i * minDistance * Math.sin(maxPolarAngle));
                objMain.camera.position.set(planePosition.x, planePosition.y, planePosition.z);

                objMain.controls.target.set((start.x + end.x) / 2, 0, (start.z + end.z) / 2);
                objMain.camera.lookAt((start.x + end.x) / 2, 0, (start.z + end.z) / 2);
            }
        },
        getLength: function (p1, p2) {
            return Math.sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.y - p2.y) * (p1.y - p2.y) + (p1.z - p2.z) * (p1.z - p2.z));
        },
        removeF:
        {
            removePanle: function (id) {
                //carsSelectionPanel
                while (document.getElementById(id) != null) {
                    document.getElementById(id).remove();
                }
            },
            clearGroup: function (group) {
                var startIndex = group.children.length - 1;
                for (var i = startIndex; i >= 0; i--) {
                    group.remove(group.children[i]);
                }
            }
        },
        refreshPromotionDiamondAndPanle: function (received_obj) {
            //if (received_obj.resultType == objMain.Task.state)
            {
                /*
                 * 这里进行了Task的状态验证，确保3D资源没有加载前，不会调用此方法
                 */
                if (objMain.state == "OnLine" || 'LookForBuildings' == objMain.state) {
                    var diamondName = "diamond_" + received_obj.resultType;
                    if (objMain.promoteDiamond.getObjectByName(diamondName) != undefined) {
                        objMain.promoteDiamond.remove(objMain.promoteDiamond.getObjectByName(diamondName));
                    }
                    var lineName = "approach_diamond_" + received_obj.resultType;
                    if (objMain.promoteDiamond.getObjectByName(lineName) != undefined) {
                        objMain.promoteDiamond.remove(objMain.promoteDiamond.getObjectByName(lineName));
                    }
                    // var lineName=
                    var color = 0x000000;

                    var objShow = null;
                    switch (received_obj.resultType) {
                        case 'mile':
                            {
                                objShow = DiamondModel.red;
                                color = 0xff0000;
                            }; break;
                        case 'business':
                            {
                                objShow = DiamondModel.green;
                                color = 0x00ff00;
                            }; break;
                        case 'volume':
                            {
                                objShow = DiamondModel.blue;
                                color = 0x0000ff;
                            }; break;
                        case 'speed':
                            {
                                objShow = DiamondModel.black;
                                color = 0x000000;
                            }; break;
                    }
                    if (objShow == null) {
                        return;
                    }

                    var diamond = objShow.children[0].clone();// new THREE.Mesh(geometry, material);
                    diamond.userData.Fp = objMain.PromotePositions[received_obj.resultType].Fp;
                    diamond.name = 'diamond' + '_' + received_obj.resultType;
                    diamond.scale.set(0.2, 0.22, 0.2);
                    diamond.position.set(
                        MercatorGetXbyLongitude(objMain.PromotePositions[received_obj.resultType].Fp.Longitude),
                        MercatorGetZbyHeight(objMain.PromotePositions[received_obj.resultType].Fp.Height) * objMain.heightAmplify,
                        -MercatorGetYbyLatitude(objMain.PromotePositions[received_obj.resultType].Fp.Latitde));

                    objMain.promoteDiamond.add(diamond);
                    objMain.mainF.drawLineOfFpToRoad(objMain.PromotePositions[received_obj.resultType].Fp, objMain.promoteDiamond, color, "diamond_" + received_obj.resultType);
                }
            }
        },
        refreshCollectAndPanle: function (collectIndex) {
            //if (objMain.state == "OnLine")
            if (objMain.collectGroup != null) {
                var objName = 'moneymodel' + collectIndex;
                var obj = objMain.collectGroup.getObjectByName(objName);
                if (obj == undefined) { }
                else {
                    objMain.collectGroup.remove(obj);
                }
                //'approach_' + lineName;
                var lineName = 'approach_' + 'money' + collectIndex;
                var line = objMain.collectGroup.getObjectByName(lineName);
                if (line == undefined) { }
                else {
                    objMain.collectGroup.remove(line);
                }

                var changeIndexToMoney = function (inputIndex) {
                    switch (inputIndex) {
                        case 0:
                            {
                                return 1;
                            };
                        case 1:
                        case 2:
                            {
                                return 1;
                            };
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            {
                                return 1;
                            };
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                        case 17:
                            {
                                return 1;
                            };
                        case 18:
                        case 19:
                        case 20:
                        case 21:
                        case 22:
                        case 23:
                        case 24:
                        case 25:
                        case 26:
                        case 27:
                        case 28:
                        case 29:
                        case 30:
                        case 31:
                        case 32:
                        case 33:
                        case 34:
                        case 35:
                        case 36:
                        case 37:
                            {
                                return 1;
                            };
                        default: return 0;
                    }
                };
                var model;

                switch (changeIndexToMoney(collectIndex)) {
                    case 1:
                        {
                            model = objMain.ModelInput.GoldIngotIcon.obj.clone();//objMain.rmbModel['rmb1'].clone();
                        }; break;
                    case 5:
                        {
                            model = objMain.rmbModel['rmb5'].clone();
                        }; break;
                    case 10:
                        {
                            model = objMain.rmbModel['rmb10'].clone();
                        }; break;
                    case 20:
                        {
                            model = objMain.rmbModel['rmb20'].clone();
                        }; break;
                    case 50:
                        {
                            model = objMain.rmbModel['rmb50'].clone();
                        }; break;
                    case 100:
                        {
                            model = objMain.rmbModel['rmb100'].clone();
                        }; break;
                    default: return;
                }

                if (objMain.CollectPosition[collectIndex] == undefined) {
                    return;
                }

                model.position.set(MercatorGetXbyLongitude(objMain.CollectPosition[collectIndex].Fp.Longitude), MercatorGetZbyHeight(objMain.CollectPosition[collectIndex].Fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(objMain.CollectPosition[collectIndex].Fp.Latitde));
                model.name = objName;
                model.userData.collectPosition = objMain.CollectPosition[collectIndex];
                objMain.collectGroup.add(model);

                var color = 0xFFD700;

                objMain.mainF.drawLineOfFpToRoad(objMain.CollectPosition[collectIndex].Fp, objMain.collectGroup, color, 'money' + collectIndex);

                //if (objMain.Task.state == 'collect') {
                //    objMain.mainF.drawPanelOfCollect(endF);
                //}
            }
        },
        drawPanelOfCollect: function (endF) {

            var lengthOfObjs = objMain.groupOfOperatePanle.children.length;
            for (var i = lengthOfObjs - 1; i >= 0; i--) {
                objMain.groupOfOperatePanle.remove(objMain.groupOfOperatePanle.children[i]);
            }
            for (var i = 0; i < 38; i++) {
                if (objMain.CollectPosition[i] == undefined) {
                    continue;
                }
                var element = document.createElement('div');
                element.style.width = '10em';
                element.style.marginTop = '3em';
                var color = '#ff0000';
                element.style.border = '2px solid ' + color;
                element.style.borderTopLeftRadius = '0.5em';
                element.style.backgroundColor = 'rgba(155, 55, 255, 0.3)';
                element.style.color = '#1504f6';

                var div2 = document.createElement('div');
                div2.style.fontSize = '0.5em';

                var b = document.createElement('b');
                b.innerHTML = '到[<span style="color:#f5ffba">' + objMain.CollectPosition[i].Fp.FastenPositionName + '</span>]回收<span style="color:#f5ffba">' + (objMain.CollectPosition[i].collectMoney).toFixed(2) + '元</span>现金。';
                div2.appendChild(b);

                element.appendChild(div2);

                var object = new THREE.CSS2DObject(element);
                var fp = objMain.CollectPosition[i].Fp;
                object.position.set(MercatorGetXbyLongitude(fp.Longitude), 0, -MercatorGetYbyLatitude(fp.Latitde));

                objMain.groupOfOperatePanle.add(object);
            }



        },
        removeRole: function (roleID) {
            var carRoad_ID = 'carRoad_' + roleID;

            objMain.carGroup.remove(objMain.carGroup.getObjectByName(carRoad_ID));

            var car_ID = 'car_' + roleID;
            stateSet.speed.clear(car_ID);
            stateSet.attck.clear(car_ID);
            stateSet.confuse.clear(car_ID);
            stateSet.lost.clear(car_ID);

            objMain.carGroup.remove(objMain.carGroup.getObjectByName(car_ID));


            var approachId = 'approach_' + roleID;//;216596b5fddf7bc24f05bfebb2b1f10d
            objMain.playerGroup.remove(objMain.playerGroup.getObjectByName(approachId));

            var flagId = 'flag_' + roleID;//;216596b5fddf7bc24f05bfebb2b1f10d
            objMain.playerGroup.remove(objMain.playerGroup.getObjectByName(flagId));

            stateSet.lightning.clear(roleID);
            stateSet.water.clear(roleID);
            stateSet.fire.clear(roleID);

            stateSet.defend.clear(roleID);

            delete objMain.othersBasePoint[roleID];
        },
        drawDiamondCollected: function () {
            var fp = objMain.basePoint;
            var key = objMain.indexKey;//objMain.basePoint
            var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde))
            var end = new THREE.Vector3(MercatorGetXbyLongitude(fp.positionLongitudeOnRoad), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.positionLatitudeOnRoad))
            var cc = new Complex(end.x - start.x, end.z - start.z);
            cc.toOne();

            var positon1 = cc.multiply(new Complex(0, 1));
            var positon2 = positon1.multiply(new Complex(0.5, 0.86602));
            var positon3 = positon2.multiply(new Complex(0.5, 0.86602));
            var positon4 = positon3.multiply(new Complex(0.5, 0.86602));

            var positons = [positon1, positon2, positon3, positon4];

            var names = ['BatteryMile', 'BatteryBusiness', 'BatteryVolume', 'BatterySpeed'];
            var index = ['mile', 'business', 'volume', 'speed'];
            var colors = [0xff0000, 0x00ff00, 0x0000ff, 0x000000];
            //   console.log('positons', positons);
            var percentOfPosition = 0.5;
            for (var i = 0; i < positons.length; i++) {
                var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
                var end = new THREE.Vector3(start.x + positons[i].r * percentOfPosition, MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, start.z + positons[i].i * percentOfPosition);
                //var lineGeometry = new THREE.Geometry();
                //lineGeometry.vertices.push(start);
                //lineGeometry.vertices.push(end);
                //var lineMaterial = new THREE.LineBasicMaterial({ color: color });
                //var line = new THREE.Line(lineGeometry, lineMaterial);
                //line.name = 'carRoad11' + 'ABCDE'[i] + '_' + key;
                //line.userData = { objectType: 'carRoad', parent: key, index: (i + 0) };
                //objMain.carGroup.add(line);
                var mesh;
                if (objMain.columnGroup.getObjectByName(names[i]) == undefined) {
                    var geometryCylinder = new THREE.CylinderGeometry(0.05, 0.05, 0.05, 16);
                    var color = colors[i];
                    var materialCylinder = new THREE.MeshPhongMaterial({ color: color, transparent: true, opacity: 0.6 });
                    mesh = new THREE.Mesh(geometryCylinder, materialCylinder);
                    mesh.castShadow = true;
                    mesh.receiveShadow = true;
                    mesh.name = names[i];
                    mesh.scale.setY(0.01);
                    mesh.position.set(end.x, end.y, end.z);
                    mesh.userData.index = index[i];
                    objMain.columnGroup.add(mesh);
                }
                else {
                    mesh = objMain.columnGroup.getObjectByName(names[i]);
                }

                if (objMain.PromoteDiamondCount[index[i]] != undefined) {
                    var scale = objMain.PromoteDiamondCount[index[i]];
                    scale = Math.max(0.01, scale);
                    mesh.scale.setY(scale);
                }
                //var model = objMain.cars[names[i]].clone();
                //model.name = names[i] + '_' + key;
                //model.position.set(end.x, 0, end.z);
                //model.scale.set(0.002, 0.002, 0.002);
                //model.rotateY(-positons[i].toAngle());
                //model.userData = { objectType: 'car', parent: key, index: names[i] };
                //objMain.carGroup.add(model);
            }
        },
        updateCollectGroup: function () {
            /*
             * 后台已停用
             */
            // theLagestHoderKey.updateCollectGroup();
        },
        refreshBuyPanel: function () {
            objMain.mainF.removeF.clearGroup(objMain.groupOfOperatePanle);
            {
                {
                    var element = document.createElement('div');

                    element.style.width = '10em';
                    element.style.marginTop = '3em';
                    var color = '#ff0000';
                    //var colorName = '红';
                    //switch (type) {
                    //    case 'mile':
                    //        {
                    //            color = '#ff0000';
                    //            colorName = '红';
                    //        }; break;
                    //    case 'business': {
                    //        color = '#00ff00';
                    //        colorName = '绿';
                    //    }; break;
                    //    case 'volume': { 
                    //        color = '#0000ff';
                    //        colorName = '蓝';
                    //    }; break;
                    //    case 'speed': { 
                    //        color = '#000000';
                    //        colorName = '黑';
                    //    }; break; 
                    //}
                    element.style.border = '2px solid ' + color;
                    element.style.borderTopLeftRadius = '0.5em';
                    element.style.backgroundColor = 'rgba(255, 255, 255, 0.5)';
                    element.style.color = '#1504f6';

                    var div2 = document.createElement('div');

                    var b = document.createElement('b');
                    b.innerHTML = '现在售价如下红宝石10.00，蓝宝石10.00，蓝宝石10.00，黑宝石10.00！点击柱状仓库，进行购买！';
                    div2.appendChild(b);



                    element.appendChild(div2);

                    var object = new THREE.CSS2DObject(element);
                    var fp = objMain.basePoint;
                    object.position.set(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));

                    objMain.groupOfOperatePanle.add(object);
                }


            }
        }
    },
    Task: new TaskClass('', ''),
    animation:
    {
        animateCameraByCarAndTask: function () {
            if (objMain.Task.state == 'mile') {
                if (objMain.Task.carSelect != '') {
                    var p1 = objMain.promoteDiamond.children[0].position;
                    var p2 = objMain.carGroup.getObjectByName(objMain.Task.carSelect).position;

                }
            }
        }
    },
    groupOfOperatePanle: null,
    groupOfTaskCopy: null,
    GetPositionNotify: {
        data: null, F: function (data) {

            console.log(data);
            var inputObj = JSON.parse(data);
            var path = '';

            objMain.indexKey = inputObj.key;
            var vehicle = objMain.ModelInput.vehicle.clone();
            vehicle.name = 'vehicle_' + objMain.indexKey;
            objMain.carGroup.add(vehicle);


            // carAbility.drawChanel()；
            carAbility.drawPanel(stateSet, 'car');
            //if (objMain.debug == 0) {
            //    path = 'http://127.0.0.1:11001/pic/' + inputObj.fp.fPCode + '/' + inputObj.fp.Height + '/';
            //}
            //else { }
            //if (path != '') {
            //    objMain.background.path = path;

            //}
            //var objInput = JSON.parse(data);
            //objMain.basePoint = objInput.fp;
            ////objMain.carsNames = objInput.carsNames;
            //objMain.indexKey = objInput.key;
            //objMain.displayName = objInput.PlayerName;
            //objMain.fpIndex = objInput.fPIndex;
            //objMain.groupNumber = objInput.groupNumber;
            //objMain.positionInStation = objInput.positionInStation;
            ////if (objMain.receivedState == 'WaitingToGetTeam') {
            ////    objMain.ws.send(received_msg);
            ////}
            ////小车用 https://threejs.org/examples/#webgl_animation_skinning_morph
            ////小车用 基地用 https://threejs.org/examples/#webgl_animation_cloth
            //// drawFlag(); 
            //drawPoint('green', objMain.basePoint, objMain.indexKey);
            ///*画引线*/
            //objMain.mainF.drawLineOfFpToRoad(objMain.basePoint, objMain.playerGroup, 'green', objMain.indexKey);
            //objMain.mainF.drawDiamondCollected();
            //objMain.mainF.lookAtPosition(objMain.basePoint);
            //objMain.mainF.initilizeCars(objMain.basePoint, 'green', objMain.indexKey, true, objMain.positionInStation);



            //objMain.GetPositionNotify.data = null;
            SysOperatePanel.draw();
            //currentSelectionPreparationShow.show();
            //operateStateShow.show();


            //var startBase = new THREE.Vector3(MercatorGetXbyLongitude(objMain.basePoint.Longitude), MercatorGetZbyHeight(objMain.basePoint.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(objMain.basePoint.Latitde));
            //var animationData =
            //{
            //    old: {
            //        x: objMain.controls.target.x,
            //        y: objMain.controls.target.y,
            //        z: objMain.controls.target.z,
            //        t: Date.now()
            //    },
            //    newT:
            //    {
            //        x: startBase.x,
            //        y: startBase.y,
            //        z: startBase.z,
            //        t: Date.now() + 3000
            //    }
            //};
            //objMain.camaraAnimateData = animationData;

        }, otherData: []
    },
    Tax: {},
    taxGroup: null,
    buildingSelectionGroup: null,
    msg:
    {

    },
    panOrRotate: 'rotate',
    carState: { 'stamp': -1 },
    carStateTimestamp: {},
    music:
    {
        theme: '',
        change: function () {
            //if(dou)
            if (whetherGo.obj != null && whetherGo.obj.Live) {
                var bgm = document.getElementById('backGroudMusick');
                //   var paused = bgm.paused;
                if (bgm.currentTime === 0 || bgm.ended) {

                    var itemCount = bgm.children.length - 1;
                    for (var i = itemCount; i >= 0; i--) {
                        bgm.children[i].remove();
                    }
                    var source1 = document.createElement('source');
                    source1.src = 'bgm/windyhillbgm.ogg';
                    source1.type = 'audio/ogg';

                    var source2 = document.createElement('source');
                    source2.src = 'bgm/windyhillbgm.mp3';
                    source2.type = 'audio/mpeg';

                    bgm.appendChild(source1);
                    bgm.appendChild(source2);

                    bgm.oncanplaythrough = function () {
                        if (objMain.music.on)
                            this.play();
                    };

                    bgm.load();
                    bgm.addEventListener('ended', function () {
                        // 当音频播放完成时，将当前播放时间重置为0，实现单曲循环
                        this.currentTime = 0;
                        this.play();
                    });
                }
            }
            else {
                var bgm = document.getElementById('backGroudMusick');
                //   var paused = bgm.paused;
                if (bgm.currentTime === 0 || bgm.ended) {
                    switch (this.theme) {
                        case '':
                            {
                                var itemCount = bgm.children.length - 1;
                                for (var i = itemCount; i >= 0; i--) {
                                    bgm.children[i].remove();
                                }
                                var source1 = document.createElement('source');
                                source1.src = 'bgm/GameofThrones.ogg';
                                source1.type = 'audio/ogg';

                                var source2 = document.createElement('source');
                                source2.src = 'bgm/GameofThrones.mp3';
                                source2.type = 'audio/mpeg';

                                bgm.appendChild(source1);
                                bgm.appendChild(source2);

                                bgm.oncanplaythrough = function () {
                                    if (objMain.music.on)
                                        this.play();
                                };

                                bgm.load();
                                //if (this.on)
                                //    bgm.play();
                            }; break;
                        default:
                            {
                                var itemCount = bgm.children.length - 1;
                                for (var i = itemCount; i >= 0; i--) {
                                    bgm.children[i].remove();
                                }
                                var source1 = document.createElement('source');
                                source1.src = 'bgm/' + this.theme + '.ogg';
                                source1.type = 'audio/ogg';

                                var source2 = document.createElement('source');
                                source2.src = 'bgm/' + this.theme + '.mp3';
                                source2.type = 'audio/mpeg';

                                bgm.appendChild(source1);
                                bgm.appendChild(source2);

                                bgm.oncanplaythrough = function () {
                                    if (objMain.music.on)
                                        this.play();
                                };
                                bgm.load();
                                //if (this.on)
                                //    bgm.play();
                            }; break;
                    }

                }
            }
        },
        on: true,
        MarketRepeat: function () {
        },
        isSetByWeb: false
    },
    targetMusic:
    {
        theme: [],
        change: function (themeItem) {
            try {
                if (themeItem == '' || themeItem == null) {
                    return;
                }
                else if (/^[A-Z]{10}$/.test(themeItem)) {
                    objMain.targetMusic.theme.push(themeItem);
                    if (objMain.targetMusic.theme.length > 3) {
                        objMain.targetMusic.theme.shift();
                    }
                }
                else if (themeItem == 'pass' || themeItem == 'last') {

                }
                else {
                    return;
                }
                if (objMain.targetMusic.theme.length > 0) {
                    var bgm = document.getElementById('fpBgSoundMusick');
                    if (objMain.debug == 2) {
                        if (bgm.currentTime === 0 || bgm.ended) {
                            var fpID = objMain.targetMusic.theme[Math.floor(Math.random() * objMain.targetMusic.theme.length)];
                            if (themeItem == 'last')
                                fpID = objMain.targetMusic.theme[objMain.targetMusic.theme.length - 1];
                            var itemCount = bgm.children.length - 1;
                            for (var i = itemCount; i >= 0; i--) {
                                bgm.children[i].remove();
                            }
                            var source1 = document.createElement('source');
                            source1.src = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/fpbgmuic/' + fpID + '.ogg';
                            source1.type = 'audio/ogg';

                            var source2 = document.createElement('source');
                            source2.src = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/fpbgmuic/' + fpID + '.mp3';;
                            source2.type = 'audio/mpeg';

                            bgm.appendChild(source1);
                            bgm.appendChild(source2);

                            bgm.oncanplaythrough = function () {
                                if (objMain.music.on) {
                                    this.play();
                                    var anotherBGM = document.getElementById('backGroudMusick');
                                    //anotherBGM.volume = 0.07;
                                    anotherBGM.pause();
                                }
                            };
                            bgm.load();
                            bgm.onended = function () {
                                setTimeout(() => {
                                    var bgm = document.getElementById('fpBgSoundMusick');
                                    if (bgm.currentTime === 0 || bgm.ended) {
                                        var anotherBGM = document.getElementById('backGroudMusick');
                                        if (objMain.music.on)
                                            anotherBGM.play();
                                    }
                                    //var anotherBGM = document.getElementById('backGroudMusick');
                                    //anotherBGM.volume = 1;
                                }, 1000);
                                // bgm.volume = 0.07;
                            }
                        }
                    }
                }
            }
            catch (e) {
                console.info('targetMusic音乐报错', e);
                // console.log('targetMusic音乐报错', e);
            }
        },
        addTheme: function (themeItem) {
            if (/^[A-Z]{10}$/.test(themeItem)) {
                objMain.targetMusic.theme.push(themeItem);
                if (objMain.targetMusic.theme.length > 3) {
                    objMain.targetMusic.theme.shift();
                }
            }
        },
        on: true,
        MarketRepeat: function () {
        },
        isSetByWeb: false
    },
    background:
    {
        path: '',
        change: function () {

            {
                var cubeTextureLoader = new THREE.CubeTextureLoader();
                cubeTextureLoader.setPath(this.path);
                cubeTextureLoader.load([
                    "px.jpg", "nx.jpg",
                    "py.jpg", "ny.jpg",
                    "pz.jpg", "nz.jpg"
                ], function (newTexture) {
                    objMain.scene.background.images[0] = newTexture.images[0]; // px
                    objMain.scene.background.images[1] = newTexture.images[1]; // nx
                    objMain.scene.background.images[2] = newTexture.images[2]; // py
                    objMain.scene.background.images[3] = newTexture.images[3]; // ny
                    objMain.scene.background.images[4] = newTexture.images[4]; // pz
                    objMain.scene.background.images[5] = newTexture.images[5]; // nz
                    objMain.scene.background.needsUpdate = true;
                    cubeTextureLoader = null;
                });
                //  objMain.background.backgroundData[this.path] = cubeTexture;
            }

            //if (/^[A-Z]{10}$/.test(this.path)) {
            //    //if (objMain.background.backgroundData[this.path] == undefined)
            //    {
            //        var cubeTextureLoader = new THREE.CubeTextureLoader();
            //        //http://yrqmodeldata.oss-cn-beijing.aliyuncs.com/fpbgimg/IJZJBCUKEK/nx.jpg
            //        if (objMain.debug != 2)
            //            cubeTextureLoader.setPath(this.path);
            //        else
            //            cubeTextureLoader.setPath('https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/fpbgimg/' + this.path + '/');
            //        // window.location.hostname + '/bgimg?key=1&y=2&p=px'
            //        cubeTextureLoader.load(
            //            [
            //                "px.jpg", "nx.jpg",
            //                "py.jpg", "ny.jpg",
            //                "pz.jpg", "nz.jpg"], function (newTexture) {
            //                    objMain.scene.background.images[0] = newTexture.images[0]; // px
            //                    objMain.scene.background.images[1] = newTexture.images[1]; // nx
            //                    objMain.scene.background.images[2] = newTexture.images[2]; // py
            //                    objMain.scene.background.images[3] = newTexture.images[3]; // ny
            //                    objMain.scene.background.images[4] = newTexture.images[4]; // pz
            //                    objMain.scene.background.images[5] = newTexture.images[5]; // nz
            //                    objMain.scene.background.needsUpdate = true;
            //                    cubeTextureLoader = null;
            //                });
            //        // objMain.background.backgroundData[this.path] = cubeTexture;
            //    }
            //    //objMain.scene.background = objMain.background.backgroundData[this.path];
            //    //delete objMain.background.backgroundData[this.path];
            //}
            //else {
            //    switch (this.path) {
            //        case '':
            //            {
            //                //if (objMain.background.backgroundData['main'] == undefined)
            //                {
            //                    var cubeTextureLoader = new THREE.CubeTextureLoader();
            //                    cubeTextureLoader.setPath('Pic/');
            //                    cubeTextureLoader.load([
            //                        "px.jpg", "nx.jpg",
            //                        "py.jpg", "ny.jpg",
            //                        "pz.jpg", "nz.jpg"
            //                    ], function (newTexture) {
            //                        objMain.scene.background.images[0] = newTexture.images[0]; // px
            //                        objMain.scene.background.images[1] = newTexture.images[1]; // nx
            //                        objMain.scene.background.images[2] = newTexture.images[2]; // py
            //                        objMain.scene.background.images[3] = newTexture.images[3]; // ny
            //                        objMain.scene.background.images[4] = newTexture.images[4]; // pz
            //                        objMain.scene.background.images[5] = newTexture.images[5]; // nz
            //                        objMain.scene.background.needsUpdate = true;
            //                        cubeTextureLoader = null;
            //                    });
            //                    //objMain.background.backgroundData['main'] = cubeTexture;
            //                    //objMain.scene.background = objMain.background.backgroundData['main'];
            //                }
            //                /* objMain.scene.background = objMain.background.backgroundData['main'];*/
            //                //delete objMain.background.backgroundData['main']; 
            //            }; break;
            //        default:
            //            {
            //                //if (objMain.background.backgroundData[this.path] == undefined)
            //                {
            //                    var cubeTextureLoader = new THREE.CubeTextureLoader();
            //                    cubeTextureLoader.setPath('Pic/' + this.path + '/');
            //                    cubeTextureLoader.load([
            //                        "px.jpg", "nx.jpg",
            //                        "py.jpg", "ny.jpg",
            //                        "pz.jpg", "nz.jpg"
            //                    ], function (newTexture) {
            //                        objMain.scene.background.images[0] = newTexture.images[0]; // px
            //                        objMain.scene.background.images[1] = newTexture.images[1]; // nx
            //                        objMain.scene.background.images[2] = newTexture.images[2]; // py
            //                        objMain.scene.background.images[3] = newTexture.images[3]; // ny
            //                        objMain.scene.background.images[4] = newTexture.images[4]; // pz
            //                        objMain.scene.background.images[5] = newTexture.images[5]; // nz
            //                        objMain.scene.background.needsUpdate = true;
            //                        cubeTextureLoader = null;
            //                    });
            //                    //  objMain.background.backgroundData[this.path] = cubeTexture;
            //                }
            //                //objMain.scene.background = objMain.background.backgroundData[this.path];
            //                //delete objMain.background.backgroundData[this.path];

            //            }; break;
            //    }
            //}
        },
        backgroundData: {},
        changeWhenIsCross: function (r) {
            if (r.IsDetalt) {
                var cubeTextureLoader = new THREE.CubeTextureLoader();
                cubeTextureLoader.setPath('Pic/');
                //var cubeTexture = cubeTextureLoader.load([
                //    "xi_r.jpg", "dong_r.jpg",
                //    "ding_r.jpg", "di_r.jpg",
                //    "nan_r.jpg", "bei_r.jpg"
                //]);
                cubeTextureLoader.load([
                    "px.jpg", "nx.jpg",
                    "py.jpg", "ny.jpg",
                    "pz.jpg", "nz.jpg"
                ],
                    function (newTexture) {
                        objMain.scene.background.images[0] = newTexture.images[0]; // px
                        objMain.scene.background.images[1] = newTexture.images[1]; // nx
                        objMain.scene.background.images[2] = newTexture.images[2]; // py
                        objMain.scene.background.images[3] = newTexture.images[3]; // ny
                        objMain.scene.background.images[4] = newTexture.images[4]; // pz
                        objMain.scene.background.images[5] = newTexture.images[5]; // nz
                        objMain.scene.background.needsUpdate = true;
                        cubeTextureLoader = null;
                    }
                );
                //  objMain.scene.background = objMain.background.backgroundData['main'];
            }
            else {
                //if (objMain.background.backgroundData[r.Md5Key] == undefined)
                {
                    var cubeTextureLoader = new THREE.CubeTextureLoader();
                    if (objMain.debug != 2) {
                        cubeTextureLoader.setPath('http://127.0.0.1:11001/bgimg/' + r.Md5Key + '/');
                    }
                    else {
                        cubeTextureLoader.setPath('https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/bgimg/' + r.Md5Key + '/'); //http://yrqmodeldata.oss-cn-beijing.aliyuncs.com/bgimg/b46fc059ea0a9d7aac06b539d469dfe5/ny.jpg
                        // cubeTextureLoader.setPath('https://www.nyrq123.com/imgtaiyuan/' + r.Md5Key + '/');
                    }
                    cubeTextureLoader.load(
                        [
                            "px.jpg", "nx.jpg",
                            "py.jpg", "ny.jpg",
                            "pz.jpg", "nz.jpg"],
                        function (newTexture) {
                            objMain.scene.background.images[0] = newTexture.images[0]; // px
                            objMain.scene.background.images[1] = newTexture.images[1]; // nx
                            objMain.scene.background.images[2] = newTexture.images[2]; // py
                            objMain.scene.background.images[3] = newTexture.images[3]; // ny
                            objMain.scene.background.images[4] = newTexture.images[4]; // pz
                            objMain.scene.background.images[5] = newTexture.images[5]; // nz
                            objMain.scene.background.needsUpdate = true;
                            cubeTextureLoader = null;
                        });
                    //  objMain.background.backgroundData[r.Md5Key] = cubeTexture;
                }
                // objMain.scene.background = objMain.background.backgroundData[r.Md5Key];
            }
        },
        rotateOthers: function (r) {
            var groups = [objMain.roadGroup, objMain.carGroup, objMain.buildingGroup, objMain.collectGroup, objMain.directionGroup, objMain.targetGroup, objMain.getOutGroup, objMain.bitcoinCharacterGroup, objMain.batteryGroup];
            for (var i = 0; i < groups.length; i++) {
                var group = groups[i];
                group.rotation.y = r;
            }
            if (this.mainFP != null) {
                while (r < 0) {
                    r += Math.PI * 2;
                }
                while (r > Math.PI * 2) {
                    r -= Math.PI * 2;
                }
                this.rotateOtherScript = 'UPDATE fpdetail SET ObjInSceneRotation =' + r + ' WHERE FPCode=\'' + this.mainFP.fPCode + '\' AND Height=' + this.mainFP.Height + ';';
            }
        },
        mainFP: null,
        rotateOtherScript: '',
        changeWithJson: function (code, height) {
            var url = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/bgImg/' + code + '_' + height + '.json';
            $.getJSON(url, function (params) {

                //console.log('p', params);
                var cubeTextureLoader = new THREE.CubeTextureLoader();
                //  cubeTextureLoader.setPath(this.path);
                cubeTextureLoader.load([
                    "data:image/jpeg;base64," + params.px, "data:image/jpeg;base64," + params.nx,
                    "data:image/jpeg;base64," + params.py, "data:image/jpeg;base64," + params.ny,
                    "data:image/jpeg;base64," + params.pz, "data:image/jpeg;base64," + params.nz,
                ], function (newTexture) {
                    objMain.scene.background.images[0] = newTexture.images[0]; // px
                    objMain.scene.background.images[1] = newTexture.images[1]; // nx
                    objMain.scene.background.images[2] = newTexture.images[2]; // py
                    objMain.scene.background.images[3] = newTexture.images[3]; // ny
                    objMain.scene.background.images[4] = newTexture.images[4]; // pz
                    objMain.scene.background.images[5] = newTexture.images[5]; // nz
                    if (objMain.scene.background == undefined || objMain.scene.background == null) { }
                    else { objMain.scene.background.needsUpdate = true; }
                    cubeTextureLoader = null;
                });
            })
        },
        keyZ: function () {
            if (this.mainFP != null) {
                this.mainFP.ObjInSceneRotation += 0.01;
                this.rotateOthers(this.mainFP.ObjInSceneRotation);
            }
        },
        keyX: function () {
            if (this.mainFP != null) {
                this.mainFP.ObjInSceneRotation -= 0.01;
                this.rotateOthers(this.mainFP.ObjInSceneRotation);
            }
        },
        keyC: function () {
            if (this.mainFP != null) {
                alert(this.rotateOtherScript);
            }
        },
        keyPassToOrigin: function () {
            //    objMain.ws.send('SetOnLine');  objMain.
            var passObj = { 'c': 'SqlCommand', 'Sql': this.rotateOtherScript };
            objMain.ws.send(JSON.stringify(passObj));
        }
    },
    rightAndDuty:
    {
        data: {},
        update: function () { }
    },
    dealWithReceivedObj: function (received_obj, evt, received_msg) {
        console.log('reveivedMsg', received_obj);
        switch (received_obj.c) {
            case 'setState':
                {
                    objMain.receivedState = received_obj.state;
                    switch (objMain.receivedState) {
                        case 'selectSingleTeamJoin':
                            {
                                selectSingleTeamJoinHtml();
                            }; break;
                        case 'OnLine':
                            {
                                switch (objMain.state) {
                                    case 'LookForBuildings': {
                                        objMain.state = objMain.receivedState;
                                        setTransactionHtml.cancle();
                                        setTransactionHtml.change();
                                        //  operatePanel.refresh();
                                    }; break;
                                    default: {
                                        set3DHtml();
                                        //  objMain.state = objMain.receivedState;
                                        objMain.ws.send('SetOnLine');
                                        UpdateOtherBasePoint();
                                        objMain.state = objMain.receivedState;
                                    }; break;
                                }
                                objMain.state = objMain.receivedState;

                                {
                                    var confirmFirst = function () {
                                        if (confirm("确认离开www.nyrq123.com?")) {
                                        } else {
                                            return false;
                                        }
                                    }
                                    window.onbeforeunload = confirmFirst;


                                }
                            }; break;
                        case 'WaitingToStart':
                            {
                                setWaitingToStart();
                            }; break;
                        case 'WaitingToGetTeam':
                            {
                                setWaitingToGetTeam();
                            }; break;
                        case 'LookForBuildings':
                            {
                                if (objMain.state == 'OnLine') {
                                    objMain.state = objMain.receivedState;
                                    setTransactionHtml.change();
                                    //  operatePanel.refresh();
                                    UpdateOtherBasePoint();
                                }
                            }; break;
                        case 'QueryReward':
                            {
                                objMain.state = objMain.receivedState;
                                objMain.ws.send(JSON.stringify({ 'c': 'RewardInfomation', 'Page': 0 }));
                            }; break;
                        case 'Guid':
                            {
                                objMain.state = objMain.receivedState;
                                GuidObj.gameIntroShow();
                            }; break;
                        case 'empty':
                            {
                                if (objMain.state == 'OnLine') {
                                    objMain.ws.close();
                                    location.reload();
                                }
                            }; break;
                    }
                }; break;
            case 'setSession':
                {
                    sessionStorage['session'] = received_obj.session;

                    if (window.localStorage.getItem('notSaveSession') == null) {
                        window.localStorage.setItem('session', sessionStorage['session'])
                    }
                }; break;
            case 'TeamCreateFinish':
                {
                    //  alert();
                    console.log('提示', '队伍创建成功');
                    if (objMain.receivedState == 'WaitingToStart') {
                        //{"CommandStart":"182be0c5cdcd5072bb1864cdee4d3d6e","WebSocketID":3,"TeamNum":0,"c":"TeamCreateFinish"}
                        token.CommandStart = received_obj.CommandStart;
                        createTeam(received_obj);
                    }
                    //  sessionStorage['session'] = received_obj.session;
                }; break;
            case 'TeamJoinFinish':
                {
                    console.log('提示', '加入队伍成功');
                    if (objMain.receivedState == 'WaitingToGetTeam') {
                        joinTeamDetail(received_obj);
                    }
                }; break;
            case 'Alert':
                {
                    alert(received_obj.msg);
                }; break;
            case 'TeamJoinBroadInfo':
                {
                    if (objMain.receivedState == 'WaitingToGetTeam' || objMain.receivedState == 'WaitingToStart') {
                        broadTeamJoin(received_obj);
                    }
                }; break;
            case 'TeamJoinRemoveInfo':
                {
                    if (objMain.receivedState == 'WaitingToGetTeam' || objMain.receivedState == 'WaitingToStart') {
                        broadTeamMemberRemove(received_obj);
                    }
                }; break;
            case 'TeamNumWithSecret':
                {
                    if (objMain.receivedState == 'WaitingToGetTeam') {
                        console.log('secret', received_msg);
                        // var obj = JSON.parse(
                        var objPass = { 'Secret': received_obj.Secret, 'WebSocketID': received_obj.WebSocketID, 'c': 'TeamNumWithSecret', 'RefererAddr': nyrqUrl.get() }
                        objMain.ws.send(JSON.stringify(objPass));

                    }
                }; break;
            case 'GetOthersPositionNotify_v2':
                {
                    /*
                     * 其他玩家的状态刷新
                     */
                    //  console.log(evt.data);
                    var objInput = received_obj;

                    if (objInput.key == objMain.indexKey) { }
                    else {
                        var basePoint = objInput.fp;
                        //var carsNames = objInput.carsNames;
                        var indexKey = objInput.key;
                        var PlayerName = objInput.PlayerName;
                        var fPIndex = objInput.fPIndex;
                        var positionInStation = objInput.positionInStation;
                        var isNPC = objInput.isNPC;
                        var isPlayer = objInput.isPlayer;
                        var Level = objInput.Level;
                        objMain.othersBasePoint[indexKey] =
                        {
                            'basePoint': basePoint,
                            'indexKey': indexKey,
                            'playerName': PlayerName,
                            'fPIndex': fPIndex,
                            'isNPC': isNPC,
                            'isPlayer': isPlayer,
                            'Level': Level,
                            'positionInStation': positionInStation
                        };
                        UpdateOtherBasePoint();
                        //小车用 https://threejs.org/examples/#webgl_animation_skinning_morph
                        //小车用 基地用 https://threejs.org/examples/#webgl_animation_cloth 
                    }
                }; break;
            case 'GetPositionNotify_v2':
                {
                    /*
                     * 命令GetPositionNotify 与setState objMain.state="OnLine"发生顺序不定。但是画数据，需要在3D初始化之后。
                     */
                    if (objMain.state == "OnLine") {
                        var msg = '先执行了 OnLine命令';
                        console.log('提示', msg);
                        objMain.GetPositionNotify.F(evt.data);
                    }
                    else {
                        /*
                         * 
                         */
                        var msg = 'GetPositionNotify出入时，状态为' + objMain.state;
                        console.log('提示', msg);
                        throw (msg);
                        //  objMain.GetPositionNotify.data = evt.data;
                        return;
                    }

                    //var model = objMain.robotModel.clone();
                    //model.position.set(objMain.basePoint.MacatuoX, 0, -objMain.basePoint.MacatuoY);
                    //objMain.roadGroup.add(model);
                }; break;
            case 'MapRoadAndCrossJson':
                {
                    switch (received_obj.action) {
                        case 'start':
                            {
                                Map.roadAndCrossJson = '';
                                objMain.ws.send('MapRoadAndCrossJson,start');
                            }; break;
                        case 'mid':
                            {
                                Map.roadAndCrossJson += received_obj.passStr;
                                objMain.ws.send('MapRoadAndCrossJson,mid');
                            }; break;
                        case 'end':
                            {
                                Map.roadAndCross = JSON.parse(Map.roadAndCrossJson);
                                Map.roadAndCrossJson = '';
                                objMain.ws.send('MapRoadAndCrossJson,end');
                            }; break;
                    }
                }; break;
            case 'SetRobot':
                {
                    //  console.log(evt.data);
                    var f = function (received_obj, mIndex, field) {
                        var manager = new THREE.LoadingManager();
                        new THREE.MTLLoader(manager)
                            .loadTextOnly(received_obj.modelBase64[1], 'data:image/png;base64,' + received_obj.modelBase64[mIndex], function (materials) {
                                materials.preload();
                                // materials.depthTest = false;
                                new THREE.OBJLoader(manager)
                                    .setMaterials(materials)
                                    //.setPath('/Pic/')
                                    .loadTextOnly(received_obj.modelBase64[0], function (object) {
                                        console.log('o', object);
                                        for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                                            if (object.children[iOfO].isMesh) {
                                                for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                                                    //object.children[iOfO].material[mi].depthTest = false;
                                                    object.children[iOfO].material[mi].transparent = true;
                                                    object.children[iOfO].material[mi].opacity = 1;
                                                    object.children[iOfO].material[mi].side = THREE.FrontSide;
                                                    // console.log('color', object.children[iOfO].material[mi].color);
                                                    //switch (level) {
                                                    //    case 'high':
                                                    //        {
                                                    //            //FR-2HZB-033
                                                    //            object.children[iOfO].material[mi].color = new THREE.Color(1.5, 1, 1);
                                                    //        }; break;
                                                    //    case 'mid':
                                                    //        {
                                                    //            object.children[iOfO].material[mi].color = new THREE.Color(1, 1.5, 1);
                                                    //        }; break;
                                                    //    case 'low':
                                                    //        {
                                                    //            object.children[iOfO].material[mi].color = new THREE.Color(1, 1, 1.5);
                                                    //        }; break;
                                                    //}
                                                    //object.children[iOfO].material[mi].color = new THREE.Color(1, 1, 1);
                                                }
                                            }
                                        }
                                        //  console.log('o', object);
                                        objMain.cars[field] = object;
                                        //var model = objMain.car1.clone();
                                        //model.position.set(objMain.basePoint.MacatuoX, 0, -objMain.basePoint.MacatuoY);
                                        //model.scale.set(0.002, 0.002, 0.002);
                                        //objMain.roadGroup.add(model);

                                    }, function () { }, function () { });
                            });
                    };
                    f(received_obj, 2, 'carA');
                    f(received_obj, 3, 'carB');
                    f(received_obj, 4, 'carC');
                    f(received_obj, 5, 'carD');
                    f(received_obj, 6, 'carE');
                    f(received_obj, 7, 'carO');
                    f(received_obj, 8, 'carO2');
                    objMain.ws.send('SetRobot');
                }; break;
            case 'SetRMB':
                {
                    var f = function (received_obj, field) {
                        var manager = new THREE.LoadingManager();
                        new THREE.MTLLoader(manager)
                            .loadTextOnly(received_obj.modelBase64[0], 'data:image/jpeg;base64,' + received_obj.modelBase64[1], function (materials) {
                                materials.preload();
                                // materials.depthTest = false;
                                new THREE.OBJLoader(manager)
                                    .setMaterials(materials)
                                    //.setPath('/Pic/')
                                    .loadTextOnly(objMain.rmbModel.geometry, function (object) {
                                        console.log('o', object);
                                        for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                                            if (object.children[iOfO].isMesh) {
                                                for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                                                    object.children[iOfO].material[mi].transparent = true;
                                                    object.children[iOfO].material[mi].opacity = 1;
                                                    object.children[iOfO].material[mi].side = THREE.FrontSide;
                                                    object.children[iOfO].material[mi].color = new THREE.Color(0.45, 0.45, 0.45);
                                                }
                                            }
                                        }
                                        object.scale.set(0.003, 0.003, 0.003);
                                        object.rotateX(-Math.PI / 2);
                                        objMain.rmbModel[field] = object;

                                    }, function () { }, function () { });
                            });
                    };
                    //  f(received_obj, received_obj.faceValue);
                    switch (received_obj.faceValue) {
                        case 'model':
                            {
                                objMain.rmbModel.geometry = received_obj.modelBase64;
                                objMain.ws.send('SetRMB');
                            }; break;
                        case 'rmb100':
                        case 'rmb50':
                        case 'rmb20':
                        case 'rmb10':
                        case 'rmb5':
                            {
                                f(received_obj, received_obj.faceValue);
                                objMain.ws.send('SetRMB');
                            }; break;
                        case 'rmb1':
                            {
                                f(received_obj, received_obj.faceValue);
                                objMain.rmbModel.geometry = undefined;
                                objMain.ws.send('SetRMB');
                            }; break;
                        //case 'rmb1':
                        //    {
                        //        f(received_obj, received_obj.faceValue);
                        //        objMain.rmbModel.geometry = undefined;
                        //        objMain.ws.send('SetRMB');
                        //    }; break;
                    };
                }; break;
            case 'SetSpeedIcon':
                {
                    //    loadSpeedIcon(function () { });
                    loadSpeedIcon(objMain, function (objectInput) {
                        //   console.log('SetDoubleRewardIcon', objectInput);
                        objMain.ws.send('SetSpeedIcon');
                        objMain.ModelInput.speed = objectInput;
                        //   objMain.ModelInput.doubleRewardIcon = objectInput;
                    })
                    //ModelOperateF.f(received_obj, {
                    //    bind: function (objectInput) {
                    //        objMain.ModelInput.speed = objectInput;

                    //    },
                    //    transparent: { opacity: 0.7 },
                    //    color: { r: 2, g: 1, b: 2 }
                    //});
                    //objMain.ws.send('SetSpeedIcon');
                }; break;
            case 'SetVehicle':
                {
                    loadAerialVehicle(objMain, function (objectInput) {
                        console.log('objectInput', objectInput);
                        objMain.ws.send('SetVehicle');
                        objMain.ModelInput.vehicle = objectInput;
                    })
                }; break;
            case 'SetCubeCore':
                {
                    loadCore(objMain, function (objectInput) {
                        console.log('objectInput', objectInput);
                        objMain.ws.send('SetCubeCore');
                        objMain.ModelInput.cubeCore = objectInput;
                    })
                }; break;
            case 'SetGoldBaby':
                {
                    loadGoldBaby(objMain, function (objectInput) {
                        console.log('SetGoldBaby', objectInput);
                        objMain.ws.send('SetGoldBaby');
                        objMain.ModelInput.GoldIngotIcon = objectInput;
                    })
                }; break;
            case 'SetTurbine':
                {
                    loadTurbine(objMain, function (objectInput) {
                        console.log('SetTurbine', objectInput);
                        objMain.ws.send('SetTurbine');
                        objMain.ModelInput.turbine = objectInput;
                    })
                }; break;
            case 'SetCompass':
                {
                    loadCompass(objMain, function (objectInput) {
                        console.log('SetCompass', objectInput);
                        objMain.ws.send('SetCompass');
                        objMain.ModelInput.compass = objectInput;
                    })
                }; break;
            case 'SetSatelite':
                {
                    loadSatelite(objMain, function (objectInput) {
                        console.log('SetSatelite', objectInput);
                        objMain.ws.send('SetSatelite');
                        objMain.ModelInput.satelite = objectInput;
                    })
                }; break;
            case 'SetBattery':
                {
                    loadBattery(objMain, function (objectInput) {
                        console.log('SetBattery', objectInput);
                        objMain.ws.send('SetBattery');
                        objMain.ModelInput.battery = objectInput;
                    })
                }; break;
            case 'SetDoubleRewardIcon':
                {
                    loadDoubleRewardIcon(objMain, function (objectInput) {
                        //   console.log('SetDoubleRewardIcon', objectInput);
                        objMain.ws.send('SetDoubleRewardIcon');
                        objMain.ModelInput.doubleRewardIcon = objectInput;
                    })
                }; break;
            case 'SetGearIcon':
                {
                    loadGearIcon(objMain, function (objectInput) {
                        //   console.log('SetDoubleRewardIcon', objectInput);
                        objMain.ws.send('SetGearIcon');
                        objMain.ModelInput.gearIcon = objectInput;
                        //objMain.ModelInput.doubleRewardIcon = objectInput;
                    })
                }; break;
            case 'SetAttackIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            objMain.ModelInput.attack = objectInput;

                        },
                        transparent: { opacity: 0.5 },
                        color: { r: 2, g: 2, b: 0.4 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetAttackIcon');
                }; break;
            case 'SetShield':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            objMain.ModelInput.shield = objectInput;

                        },
                        transparent: { opacity: 0.8 },
                        color: { r: 2, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetShield');
                }; break;
            case 'SetConfusePrepareIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            objMain.ModelInput.confusePrepare = objectInput;

                        },
                        transparent: { opacity: 0.8 },
                        color: { r: 1.5, g: 1.5, b: 1.5 }
                    });
                    objMain.ws.send('SetConfusePrepareIcon');
                }; break;
            case 'SetLostPrepareIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            objMain.ModelInput.lostPrepare = objectInput;

                        },
                        transparent: { opacity: 1 },
                        color: { r: 1, g: 1.2, b: 1.2 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetLostPrepareIcon');
                }; break;
            case 'SetAmbushPrepareIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            objMain.ModelInput.ambushPrepare = objectInput;

                        },
                        transparent: { opacity: 1 },
                        color: { r: 1.3, g: 0.9, b: 0.9 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetAmbushPrepareIcon');
                }; break;
            case 'SetWaterIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            objMain.ModelInput.water = objectInput;
                        },
                        //transparent: { opacity: 0.2 },
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetWaterIcon');
                }; break;
            case 'SetDirectionIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            objMain.ModelInput.direction = objectInput;
                        },
                        transparent: { opacity: 0.3 },
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetDirectionIcon');
                }; break;
            case 'SetDirectionArrowIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            var oldM = objectInput.children[0].material;
                            var newM = objectInput.children[0].material.clone();
                            newM.transparent = false;
                            newM.color = new THREE.Color(1, 2, 1);
                            objMain.ModelInput.directionArrow =
                            {
                                'obj': objectInput,
                                'oldM': oldM,
                                'newM': newM
                            };//objectInput;
                        },
                        transparent: { opacity: 0.3 },
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetDirectionArrowIcon');
                }; break;
            case 'SetDirectionArrowIconA':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            var oldM = objectInput.children[0].material;
                            var newM = objectInput.children[0].material.clone();
                            newM.transparent = false;
                            newM.color = new THREE.Color(1, 2, 1);
                            objMain.ModelInput.directionArrowA =
                            {
                                'obj': objectInput,
                                'oldM': oldM,
                                'newM': newM
                            };//objectInput;
                        },
                        transparent: { opacity: 0.61 },
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetDirectionArrowIconA');
                }; break;
            case 'SetDirectionArrowIconB':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            var oldM = objectInput.children[0].material;
                            var newM = objectInput.children[0].material.clone();
                            newM.transparent = false;
                            newM.color = new THREE.Color(1, 2, 1);
                            objMain.ModelInput.directionArrowB =
                            {
                                'obj': objectInput,
                                'oldM': oldM,
                                'newM': newM
                            };//objectInput;
                        },
                        transparent: { opacity: 0.61 },
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetDirectionArrowIconB');
                }; break;
            case 'SetDirectionArrowIconC':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            var oldM = objectInput.children[0].material;
                            var newM = objectInput.children[0].material.clone();
                            newM.transparent = false;
                            newM.color = new THREE.Color(1, 2, 1);
                            objMain.ModelInput.directionArrowC =
                            {
                                'obj': objectInput,
                                'oldM': oldM,
                                'newM': newM
                            };//objectInput;
                        },
                        transparent: { opacity: 0.61 },
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetDirectionArrowIconC');
                }; break;
            case 'SetOpponentIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            var oldM = objectInput.children[0].material;
                            var newM = objectInput.children[0].material.clone();
                            newM.transparent = false;
                            newM.color = new THREE.Color(1.2, 1.2, 1.2);
                            objMain.ModelInput.Opponent =
                            {
                                'obj': objectInput
                            };//objectInput;
                        },
                        transparent: { opacity: 0.8 },
                        scale: { x: 1, y: 1, z: 1 },
                        rotateX: Math.PI
                        //color: { r: 1, g: 1, b: 1 }
                        //transparent: { opacity: 0 }
                    });
                    objMain.ws.send('SetOpponentIcon');
                }; break;
            case 'SetTeammateIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            var oldM = objectInput.children[0].material;
                            var newM = objectInput.children[0].material.clone();
                            newM.transparent = false;
                            newM.color = new THREE.Color(1.2, 1.2, 1.2);
                            objMain.ModelInput.Teammate =
                            {
                                'obj': objectInput
                            };//objectInput;
                        },
                        transparent: { opacity: 0.8 },
                        scale: { x: 1, y: 1, z: 1 },
                        rotateX: Math.PI
                    });
                    objMain.ws.send('SetTeammateIcon');
                }; break;
            case 'SetNitrogenEffectIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            //var oldM = objectInput.children[0].material;
                            //var newM = objectInput.children[0].material.clone();
                            //newM.transparent = false;
                            //newM.color = new THREE.Color(1.2, 1.2, 1.2);
                            objectInput.name = 'selfnitrogenEffectIcon'
                            objMain.ModelInput.NitrogenEffect =
                            {
                                'obj': objectInput
                            };//objectInput;
                        },
                        transparent: { opacity: 0.8 },
                        scale: { x: 1, y: 1, z: 1 },
                        rotateX: Math.PI
                    });
                    objMain.ws.send('SetNitrogenEffectIcon');
                }; break;
            case 'SetCoinIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            //var oldM = objectInput.children[0].material;
                            //var newM = objectInput.children[0].material.clone();
                            //newM.transparent = false;
                            //newM.color = new THREE.Color(1.2, 1.2, 1.2);
                            objectInput.name = 'collectCoinIcon'
                            objMain.ModelInput.CollectCoinIcon =
                            {
                                'obj': objectInput
                            };//objectInput;
                        },
                        transparent: { opacity: 0.8 },
                        scale: { x: 1, y: 1, z: 1 },
                        rotateX: Math.PI
                    });
                    objMain.ws.send('SetCoinIcon');
                }; break;
            case 'SetGoldIngotIcon':
                {
                    ModelOperateF.f(received_obj, {
                        bind: function (objectInput) {
                            //objMain.ModelInput.ambushPrepare = objectInput;
                            //objMain.ModelInput.directionArrow = objectInput;
                            //var oldM = objectInput.children[0].material;
                            //var newM = objectInput.children[0].material.clone();
                            //newM.transparent = false;
                            //newM.color = new THREE.Color(1.2, 1.2, 1.2);
                            objectInput.name = 'goldIngotIcon'
                            objMain.ModelInput.GoldIngotIcon =
                            {
                                'obj': objectInput
                            };//objectInput;
                        },
                        transparent: { opacity: 0.8 },
                        scale: { x: 0.006, y: 0.006, z: 0.006 },
                        rotateX: -Math.PI / 2
                    });
                    objMain.ws.send('SetGoldIngotIcon');

                }; break;
            case 'BradCastAnimateOfOthersCar3':
                {
                    var passObj = JSON.parse(evt.data);
                    // console.log('BradCastAnimateOfOthersCar3', passObj);
                    passObj.passPrivateKeysOnly = false;
                    carAnimationData(passObj);
                }; break;
            case 'BradCastAnimateOfOthersCar4':
                {
                    var passObj = JSON.parse(evt.data);
                    // console.log('BradCastAnimateOfOthersCar3', passObj);
                    passObj.passPrivateKeysOnly = true;
                    carAnimationData(passObj);
                }; break;
            case 'BradCastPromoteInfoDetail':
                {
                    //alert('1');
                    console.log('显示', received_obj);
                    //  switch (received_obj.
                    objMain.PromotePositions[received_obj.resultType] = received_obj;
                    objMain.mainF.refreshPromotionDiamondAndPanle(received_obj);


                }; break;
            case 'BradCastCollectInfoDetail':
                {
                    //  console.log('显示', received_obj);
                    //  switch (received_obj.
                    objMain.CollectPosition = received_obj;
                    //objMain.mainF.refreshCollectAndPanle(received_obj.);
                    //  objMain.mainF.refreshCollectPositionAndPanle(received_obj);
                    // objMain.mainF.refreshPromotionDiamondAndPanle(received_obj);
                }; break;
            case 'SetDiamond':
                {
                    DiamondModel.initialize(received_obj);
                    var manager = new THREE.LoadingManager();
                    new THREE.OBJLoader(manager)
                        .loadTextOnly(received_obj.objText, function (object) {
                            console.log('SetDiamond', object.children[0].geometry);
                            var geometry = object.children[0].geometry;
                            objMain.diamondGeometry = geometry;
                        }, function () { }, function () { });

                    objMain.ws.send('SetDiamond');
                }; break;
            case 'DialogMsg':
                {
                    dialogSys.dealWithMsg(received_obj);
                }; break;
            case 'BradCastMoneyForSave':
                {
                    objMain.MoneyForSave = received_obj.Money;
                    //  moneyOperator.updateSaveMoneyNotify();
                }; break;
            case 'BradCastPromoteDiamondCount':
                {
                    if (false) {
                        objMain.PromoteDiamondCount[received_obj.pType] = received_obj.count;
                        objMain.mainF.drawDiamondCollected();
                    }
                }; break;
            case 'BradCastAbility':
                {
                    carAbility.drawPanel(stateSet, 'car');
                    carAbility.setData(received_obj);

                    carAbility.drawChanel(stateSet, received_obj.carIndexStr);
                    carAbility.refreshPosition();
                    //carAbility.updateNotify();
                }; break;
            case 'MoneyForSaveNotify':
                {
                    moneyOperator.MoneyForSave = received_obj.MoneyForSave;
                    moneyOperator.MoneyForFixRoad = received_obj.MoneyForFixRoad;

                    if (moneyOperator.MoneyForSave > 1) {
                        if (sessionStorage['addrAfterSuccess'] == undefined) { }
                        else if (sessionStorage['addrAfterSuccess'] == '') { }
                        else {
                            /*从LeftMoneyInDB 移到 MoneyForSaveNotify*/
                            /*预防只是登录一下，刷新真实数据*/
                            nyrqUrl.set(sessionStorage['addrAfterSuccess']);
                        }
                    }
                    moneyOperator.updateMoneyForSave();
                }; break;
            case 'LeftMoneyInDB':
                {
                    subsidizeSys.LeftMoneyInDB[received_obj.address] = received_obj.Money;
                    subsidizeSys.updateMoneyOfSumSubsidizing();
                }; break;
            case 'SupportNotify':
                {
                    subsidizeSys.SupportMoney = received_obj.Money;
                    subsidizeSys.updateMoneyOfSumSubsidized();
                }; break;
            case 'TheLargestHolderChangedNotify':
                {
                    theLagestHoderKey.data[received_obj.operateKey] = received_obj;
                    objMain.mainF.updateCollectGroup();
                }; break;
            case 'OthersRemove':
                {
                    objMain.mainF.removeRole(received_obj.othersKey);
                    theLagestHoderKey.removeData(received_obj.othersKey);

                    delete SocialResponsibility.data[received_obj.othersKey];
                    delete objMain.rightAndDuty.data[received_obj.othersKey];
                    // SocialResponsibility.data[received_obj.otherKey] = received_obj.socialResponsibility;
                }; break;
            case 'SingleRoadPathData':
                {
                    // MapData.meshPoints.push(received_obj.meshPoints);
                    var basePoint = received_obj.basePoint;
                    for (var i = 0; i < received_obj.meshPoints.length; i += 12) {
                        var itemData = [
                            (received_obj.meshPoints[i + 0] + basePoint[0]) / 1000000,
                            (received_obj.meshPoints[i + 1] + basePoint[1]) / 1000000,
                            (received_obj.meshPoints[i + 2] + basePoint[2]) / 1000000,
                            (received_obj.meshPoints[i + 3] + basePoint[0]) / 1000000,
                            (received_obj.meshPoints[i + 4] + basePoint[1]) / 1000000,
                            (received_obj.meshPoints[i + 5] + basePoint[2]) / 1000000,
                            (received_obj.meshPoints[i + 6] + basePoint[0]) / 1000000,
                            (received_obj.meshPoints[i + 7] + basePoint[1]) / 1000000,
                            (received_obj.meshPoints[i + 8] + basePoint[2]) / 1000000,
                            (received_obj.meshPoints[i + 9] + basePoint[0]) / 1000000,
                            (received_obj.meshPoints[i + 10] + basePoint[1]) / 1000000,
                            (received_obj.meshPoints[i + 11] + basePoint[2]) / 1000000
                        ];
                        //var itemData = [
                        //    (received_obj.meshPoints[i + 0] + basePoint[0]) / 1000000,
                        //    (received_obj.meshPoints[i + 1] + basePoint[1]) / 1000000,
                        //    (received_obj.meshPoints[i + 2] + basePoint[0]) / 1000000,
                        //    (received_obj.meshPoints[i + 3] + basePoint[1]) / 1000000,
                        //    (received_obj.meshPoints[i + 4] + basePoint[0]) / 1000000,
                        //    (received_obj.meshPoints[i + 5] + basePoint[1]) / 1000000,
                        //    (received_obj.meshPoints[i + 6] + basePoint[0]) / 1000000,
                        //    (received_obj.meshPoints[i + 7] + basePoint[1]) / 1000000,
                        //];
                        MapData.meshPoints.push(itemData);
                        // MapData.meshPoints.push(received_obj.meshPoints[i]);
                    }

                    var drawRoadInfomation = function () {

                        {
                            var group = objMain.roadGroup;
                            var startIndex = group.children.length - 1;
                            for (var i = startIndex; i >= 0; i--) {
                                //if (group.children[i].type == "Mesh")
                                {
                                    group.children[i].geometry.dispose();
                                    group.children[i].material.dispose();
                                }
                            }
                        }

                        objMain.mainF.removeF.clearGroup(objMain.roadGroup);
                        //  objMain.F.clearGroup(
                        var obj = MapData.meshPoints;

                        var positions = [];
                        //   var colors = [];
                        for (var i = 0; i < obj.length; i++) {
                            positions.push(
                                MercatorGetXbyLongitude(obj[i][0]), MercatorGetXbyLongitude(obj[i][2]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][1]),
                                MercatorGetXbyLongitude(obj[i][3]), MercatorGetXbyLongitude(obj[i][5]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][4]),
                                MercatorGetXbyLongitude(obj[i][6]), MercatorGetXbyLongitude(obj[i][8]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][7]),
                                MercatorGetXbyLongitude(obj[i][6]), MercatorGetXbyLongitude(obj[i][8]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][7]),
                                MercatorGetXbyLongitude(obj[i][9]), MercatorGetXbyLongitude(obj[i][11]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][10]),
                                MercatorGetXbyLongitude(obj[i][0]), MercatorGetXbyLongitude(obj[i][2]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][1]),

                            );
                        }
                        function disposeArray() {

                            this.array = null;

                        }
                        //  console.log('p', positions);
                        //var vertices = new Float32Array(positions);
                        var geometry = new THREE.BufferGeometry();
                        geometry.addAttribute('position', new THREE.Float32BufferAttribute(positions, 3).onUpload(disposeArray));
                        //geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3).onUpload(disposeArray));
                        geometry.computeBoundingSphere();
                        //var material = new THREE.MeshBasicMaterial({ vertexColors: THREE.VertexColors });
                        var material = new THREE.MeshBasicMaterial({ color: 0xeedd78 });
                        var mesh = new THREE.Mesh(geometry, material);

                        objMain.roadGroup.add(mesh);


                        var edges = new THREE.EdgesGeometry(geometry);
                        var line = new THREE.LineSegments(edges, new THREE.LineBasicMaterial({ color: 0xeeddf0 }));
                        objMain.roadGroup.add(line);
                    };
                    drawRoadInfomation();
                }; break;
            case 'SingleRoadPathData_V2':
                {
                    var roadCode = received_obj.RoadCode;
                    var dataHash = received_obj.DataHash;
                    roadDisplayFunction.show(dataHash, roadCode);
                }; break;
            case 'SetLeaveGameIcon':
                {
                    objMain.ws.send('SetLeaveGameIcon');
                    //alert('SetLeaveGameIcon');
                    var obj = received_obj.data[0];
                    var mtl = received_obj.data[1];
                    var imageBase64s = {};
                    for (var i = 2; i < received_obj.data.length; i += 2) {
                        var index = received_obj.data[i];
                        var data = received_obj.data[i + 1];
                        imageBase64s[index] = 'data:image/jpeg;base64,' + data;
                    }
                    console.log('imageBase64s', imageBase64s);
                    // objMain.leaveGameModel = received_obj.
                    var f = function (obj, mtl, imageBase64s) {
                        var manager = new THREE.LoadingManager();
                        new THREE.MTLLoader(manager)
                            .loadTextWithMulImg(mtl, imageBase64s, function (materials) {
                                materials.preload();
                                // materials.depthTest = false;
                                new THREE.OBJLoader(manager)
                                    .setMaterials(materials)
                                    //.setPath('/Pic/')
                                    .loadTextOnly(obj, function (object) {
                                        console.log('o', object);
                                        //for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                                        //    if (object.children[iOfO].isMesh) {
                                        //        for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                                        //            object.children[iOfO].material[mi].transparent = true;
                                        //            object.children[iOfO].material[mi].opacity = 1;
                                        //            object.children[iOfO].material[mi].side = THREE.FrontSide;
                                        //            object.children[iOfO].material[mi].color = new THREE.Color(0.45, 0.45, 0.45);
                                        //        }
                                        //    }
                                        //}
                                        console.log('o', object);
                                        object.scale.set(0.003, 0.003, 0.003);
                                        object.rotateX(-Math.PI / 2);
                                        objMain.leaveGameModel = object;
                                        // objMain.rmbModel[field] = object;

                                    }, function () { }, function () { });
                            });
                    };
                    f(obj, mtl, imageBase64s);
                }; break;
            case 'SetProfileIcon':
                {
                    objMain.ws.send('ProfileIcon');
                    //alert('SetLeaveGameIcon');
                    var obj = received_obj.data[0];
                    var mtl = received_obj.data[1];
                    var imageBase64s = {};
                    for (var i = 2; i < received_obj.data.length; i += 2) {
                        var index = received_obj.data[i];
                        var data = received_obj.data[i + 1];
                        imageBase64s[index] = 'data:image/jpeg;base64,' + data;
                    }
                    console.log('imageBase64s', imageBase64s);
                    // objMain.leaveGameModel = received_obj.
                    var f = function (obj, mtl, imageBase64s) {
                        var manager = new THREE.LoadingManager();
                        new THREE.MTLLoader(manager)
                            .loadTextWithMulImg(mtl, imageBase64s, function (materials) {
                                materials.preload();
                                // materials.depthTest = false;
                                new THREE.OBJLoader(manager)
                                    .setMaterials(materials)
                                    //.setPath('/Pic/')
                                    .loadTextOnly(obj, function (object) {
                                        console.log('o', object);
                                        //for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                                        //    if (object.children[iOfO].isMesh) {
                                        //        for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                                        //            object.children[iOfO].material[mi].transparent = true;
                                        //            object.children[iOfO].material[mi].opacity = 1;
                                        //            object.children[iOfO].material[mi].side = THREE.FrontSide;
                                        //            object.children[iOfO].material[mi].color = new THREE.Color(0.45, 0.45, 0.45);
                                        //        }
                                        //    }
                                        //}
                                        console.log('o', object);
                                        object.scale.set(0.003, 0.003, 0.003);
                                        object.rotateX(-Math.PI / 2);
                                        objMain.profileModel = object;
                                        // objMain.rmbModel[field] = object;

                                    }, function () { }, function () { });
                            });
                    };
                    f(obj, mtl, imageBase64s);
                }; break;
            case 'MoneyNotify':
                {
                    objMain.Money = received_obj.Money;
                    moneyShow.show();
                }; break;
            case 'BradCastSocialResponsibility':
                {
                    SocialResponsibility.data[received_obj.otherKey] = received_obj.socialResponsibility;
                    //SocialResponsibility.data.add
                    if (objMain.indexKey == received_obj.otherKey) {
                        SocialResponsibility.show();
                    }
                }; break;
            case 'GetName':
                {
                    if (document.getElementById('playerNameTextArea') != undefined) {
                        document.getElementById('playerNameTextArea').value = received_obj.name;
                    }
                    localStorage.playerName = received_obj.name;
                }; break;
            case 'GetCarsName':
                {
                    for (var i = 0; i < 5; i++) {
                        var iName = 'car' + (i + 1) + 'NameTextArea';
                        if (document.getElementById(iName) != undefined) {
                            document.getElementById(iName).value = received_obj.names[i];
                        }
                    }

                }; break;
            case 'BradCarState':
                {
                    if (received_obj.countStamp > objMain.carState.stamp) {
                        objMain.carState.stamp = received_obj.countStamp;
                        objMain.carState[received_obj.carID] = received_obj.State;
                        //if()
                        // var oldLength = objMain.mainF.getLength(objMain.camera.position, objMain.controls.target);
                        //objMain.carStateTimestamp[received_obj.carID] = { 't': Date.now(), 'l': oldLength };
                        objNotify.notifyCar(received_obj.carID, received_obj.State);
                        //    operatePanel.refresh();
                        //setInterval(function () {
                        //    objMain.music.MarketRepeat();
                        //}, 1000);//这里要用setTimeOut主要是考虑加速时，carState 与speed 传输不一致，加个时间，做缓冲。

                    }
                }; break;
            case 'BradCastCollectInfoDetail_v2':
                {
                    if (false) {
                        console.log('显示', received_obj);
                        objMain.CollectPosition[received_obj.collectIndex] = received_obj;
                        ////  switch (received_obj.
                        //objMain.CollectPosition = received_obj;
                        for (var i = 0; i < 38; i++) {
                            objMain.mainF.refreshCollectAndPanle(i + 0, undefined);
                        }
                    }
                }; break;
            case 'BradCarPurpose':
                {
                    console.log('显示', 'BradCarPurpose');
                }; break;
            case 'BradCastRightAndDuty':
                {
                    objMain.rightAndDuty.data[received_obj.playerKey] =
                    {
                        right: received_obj.right,
                        duty: received_obj.duty,
                        rightPercent: received_obj.rightPercent,
                        dutyPercent: received_obj.dutyPercent
                    }
                }; break;
            case 'BradCastMusicTheme':
                {
                    if (objMain.music.isSetByWeb) { }
                    else
                        objMain.music.theme = received_obj.theme;
                }; break;
            case 'BradCastBackground':
                {
                    ChangeBG(received_obj, objMain);
                    //objMain.background.path = received_obj.path;
                    //objMain.background.change();
                }; break;
            case 'WMsg':
                {
                    $.notify(received_obj.Msg, { style: "happyblue", autoHideDelay: 15000 });
                }; break;
            case 'WMsg_WithShowTime':
                {
                    // $.notify('你好', { style: "happyblue" });
                    $.notify(received_obj.Msg, { style: "happyblue", autoHideDelay: received_obj.ShowTime });
                }; break;
            case 'BradDiamondPrice':
                {
                    objMain.diamondPrice[received_obj.priceType] = received_obj.price;
                }; break;
            case 'DriverNotify':
                {
                    objMain.driver.driverIndex = received_obj.index;
                    objMain.driver.name = received_obj.name;
                    objMain.driver.skill1.name = received_obj.skill1Name;
                    objMain.driver.skill1.skillIndex = received_obj.skill1Index;
                    objMain.driver.skill2.name = received_obj.skill2Name;
                    objMain.driver.skill2.skillIndex = received_obj.skill2Index;
                    objMain.driver.sex = received_obj.sex;
                    objMain.driver.race = received_obj.race;
                    driverSys.drawIcon(objMain.driver);
                    //    operatePanel.refresh();
                }; break;
            case 'SpeedNotify':
                {
                    if (received_obj.On) { stateSet.speed.add(received_obj.Key); }
                    else { stateSet.speed.clear(received_obj.Key); }
                }; break;
            case 'AttackNotify':
                {
                    //if (received_obj.On) { stateSet.attck.add(received_obj.Key); }
                    //else { stateSet.attck.clear(received_obj.Key); }
                    if (received_obj.On)
                        stateSet.attck.add(received_obj.Key);
                    else
                        stateSet.attck.clear(received_obj.Key);
                }; break;
            case 'DefenceNotify':
                {
                    if (received_obj.On) { stateSet.defend.add(received_obj.Key); }
                    else { stateSet.defend.clear(received_obj.Key); }
                }; break;
            case 'ViewSearch':
                {
                    var animationData =
                    {
                        old: {
                            x: objMain.controls.target.x,
                            y: objMain.controls.target.y,
                            z: objMain.controls.target.z,
                            t: Date.now()
                        },
                        newT:
                        {
                            x: received_obj.mctX,
                            y: objMain.controls.target.y,
                            z: 0 - received_obj.mctY,
                            t: Date.now() + 3000
                        }
                    };
                    objMain.camaraAnimateData = animationData;
                }; break;
            case 'ConfusePrepareNotify':
                {
                    if (received_obj.On) {
                        stateSet.control.clear(received_obj.Key);
                        var animateData = { startX: received_obj.StartX / 256, startY: objMain.controls.target.y, startZ: received_obj.StartY / -256, start: Date.now(), endX: received_obj.EndX / 256, endY: objMain.controls.target.y, endZ: received_obj.EndY / -256 };
                        stateSet.confusePrepare.add(received_obj.Key, animateData);
                    }
                }; break;
            case 'LostPrepareNotify':
                {
                    if (received_obj.On) {
                        stateSet.control.clear(received_obj.Key);
                        var animateData = { startX: received_obj.StartX / 256, startY: objMain.controls.target.y, startZ: received_obj.StartY / -256, start: Date.now(), endX: received_obj.EndX / 256, endY: objMain.controls.target.y, endZ: received_obj.EndY / -256 };
                        stateSet.lostPrepare.add(received_obj.Key, animateData);
                    }
                }; break;
            case 'AmbushPrepareNotify':
                {
                    if (received_obj.On) {
                        stateSet.control.clear(received_obj.Key);
                        var animateData = { startX: received_obj.StartX / 256, startY: objMain.controls.target.y, startZ: received_obj.StartY / -256, start: Date.now(), endX: received_obj.EndX / 256, endY: objMain.controls.target.y, endZ: received_obj.EndY / -256 };
                        stateSet.ambusePrepare.add(received_obj.Key, animateData);
                    }
                }; break;
            case 'ControlPrepareNotify':
                {
                    if (received_obj.On) {
                    }
                    else {
                        // stateSet.confusePrepare.clear(received_obj.Key);
                        stateSet.control.clear(received_obj.Key);
                    }
                }; break;
            case 'LoseNotify':
                {
                    if (received_obj.On) { stateSet.lost.add(received_obj.Key); }
                    else { stateSet.lost.clear(received_obj.Key); }
                }; break;
            case 'ConfuseNotify':
                {
                    if (received_obj.On) { stateSet.confuse.add(received_obj.Key); }
                    else { stateSet.confuse.clear(received_obj.Key); }
                }; break;
            case 'FireNotify':
                {
                    stateSet.fire.add(received_obj.targetRoleID, received_obj.actionRoleID);
                }; break;
            case 'WaterNotify':
                {
                    stateSet.water.add(received_obj.targetRoleID, received_obj.actionRoleID);
                }; break;
            case 'ElectricNotify':
                {
                    stateSet.lightning.add(received_obj.targetRoleID, received_obj.actionRoleID);
                }; break;
            case 'ShowDirectionOperator':
                {
                    DirectionOperator.show(received_obj);
                }; break;
            case 'ModelDataShow':
                {
                    var modelDataShow = received_obj;
                    objMain.buildingData.dModel[modelDataShow.modelID] = modelDataShow;
                    BuildingModelObj.Refresh();
                    //BuildingModelObj.f(modelDataShow);

                }; break;
            case 'ModelDataShow_Whether_Existed':
                {
                    var modelDataShow = received_obj;
                    BuildingModelObj.respon(modelDataShow)

                }; break;
            case 'ReceiveResult':
                {
                    console.log('ReceiveResult', received_obj);
                    var modelDataShow = received_obj;
                    objMain.transtractionData = { x: received_obj.x, y: received_obj.y, z: received_obj.z };
                    //BuildingModelObj.f(modelDataShow);
                    setTransactionHtml.editRootContainer();
                    setTransactionHtml.drawAddr(received_obj.bussinessAddress);
                    setTransactionHtml.drawAgreementEditor();
                    setTransactionHtml.drawStockTable();
                    setTransactionHtml.drawTradeTable();
                    setTransactionHtml.originalTable();
                    transactionBussiness().showAuthor(received_obj.author);
                    //   operatePanel.refresh();
                }; break;
            case 'TradeDetail':
                {
                    var addrStr = received_obj.addr;
                    var valueStr = received_obj.value;
                    var indexStr = received_obj.index;
                    transactionBussiness().addOriginItem(addrStr, valueStr);
                    // objMain.ws.send(indexStr);
                }; break;
            case 'TradeDetail2':
                {
                    var mainAddr = received_obj.mainAddr;
                    var agreeMent = received_obj.agreeMent;
                    var sign = received_obj.sign;
                    //  var indexStr = received_obj.index;
                    transactionBussiness().addTradeItem(mainAddr, agreeMent, sign);
                    // objMain.ws.send(indexStr);
                }; break;
            case 'TradeDetail3':
                {
                    var detail = received_obj.detail;
                    //  detail = [];

                    var addrStr = received_obj.addrStr;
                    var valueStr = received_obj.valueStr;
                    // var indexStr = received_obj.indexStr;
                    var percentValue = received_obj.percentValue;
                    transactionBussiness().addStockItem(addrStr, valueStr, percentValue);
                    // objMain.ws.send(indexStr);
                }; break;
            case 'ShowAgreement':
                {
                    transactionBussiness().showAgreement(received_obj.agreement);
                }; break;
            case 'ShowAgreementMsg':
                {
                    $.notify(received_obj.msg, 'error');
                    GuidObj.charging.showNotifyMsg(received_obj.msg);
                    reward.notifyMsg(received_obj.msg);
                    transactionBussiness().showErrMsg(received_obj.msg);
                }; break;
            case 'ShowAllPts':
                {
                    //   transactionBussiness().ShowAllPts(received_obj.list);
                }; break;
            case 'ClearTradeInfomation':
                {
                    transactionBussiness().ClearItem();
                    objMain.ws.send('ClearTradeInfomation');
                }; break;
            case 'SetCrossBG':
                {
                    objMain.background.changeWhenIsCross(received_obj);
                }; break;
            case 'BustStateNotify':
                {
                    if (objMain.indexKey == received_obj.KeyBust) {
                        SetBustPage();
                    }
                }; break;
            case 'GoodsSelectionNotify':
                {
                    /*
                     * 建筑物的画线
                     */
                    drawGoodsSelection.f(received_obj);
                }; break;
            case 'ResistanceDisplay':
                {
                    resistance.display(received_obj);
                }; break;
            case 'ResistanceDisplay2':
                {
                    resistance.display2(received_obj);
                }; break;
            case 'SelectionIsWrong':
                {
                    moneyAbsorb.copyModel(received_obj.reduceValue);
                    DirectionOperator.showWhenIsWrong(received_obj.postionCrossKey);
                    //  operatePanel.refresh();
                    //objMain.se
                }; break;
            case 'DrawTarget':
                {
                    //绘制目标黄色圆圈。
                    targetShow.draw(received_obj.x, received_obj.y, received_obj.h * objMain.heightAmplify);
                }; break;
            case 'addOption':
                {
                    var id = received_obj.id;
                    var op = document.createElement('option');
                    op.value = received_obj.value;
                    op.innerText = received_obj.value;
                    document.getElementById(id).add(op);
                    //addOption
                    //document.getElementById('buidingAddrForAddReward').add();
                }; break;
            case 'ShowRewardAgreement':
                {
                    reward.showAgreement(received_obj.agreement);
                }; break;
            case 'GetRewardInfomationHasNotResult':
                {
                    reward.hasNoData(received_obj.title);
                }; break;
            case 'GetRewardInfomationHasResult':
                {
                    //reward.hasData(received_obj.title, received_obj.data, received_obj.list, received_obj.indexNumber);
                    // alert('有数据');
                    reward.hasData(received_obj.title, received_obj.data, received_obj.array, received_obj.forwardArray, received_obj.indexNumber);
                }; break;
            case 'VerifyCodePic':
                {
                    localStorage['nyrqVerifyImg'] = received_obj.base64String;
                    GuidObj.charging.SetImage(received_obj.base64String);
                }; break;
            case 'ElectricMarkNotify':
                {
                    stateSet.lightning.mark.add(received_obj.lineParameter);
                }; break;
            case 'WaterMarkNotify':
                {
                    stateSet.water.mark.add(received_obj.lineParameter);
                }; break;
            case 'FireMarkNotify':
                {
                    stateSet.fire.mark.add(received_obj.lineParameter);
                }; break;
            case 'GetFightSituationResult':
                {
                    dialogSys.ShowFightSituation(received_obj);
                }; break;
            case 'GetTaskCopyResult':
                {
                    dialogSys.drawPanelOfTaskCoyp(received_obj.Detail);
                }; break;
            case 'BradCastPromoteDiamondOnCar':
                {
                    stateSet.diamond.add(received_obj.roleID, received_obj.pType);
                }; break;
            case 'SetParameterIsLogin':
                {
                    objMain.stateNeedToChange.isLogin = true;
                    subsidizeSys.removeBtnsGuid();
                }; break;
            case 'SetParameterHasNewTask':
                {
                    objMain.stateNeedToChange.HasNewTask = true;
                    dialogSys.AlertNewTask();
                }; break;
            case 'ClearSession':
                {
                    objMain.ws.send('ClearSession');
                    if (sessionStorage['session'] == undefined) {

                    }
                    else {
                        delete sessionStorage.session;
                        //objMain.ws.send('ClearSession');

                        //location.reload();
                    }
                    window.localStorage.removeItem('session');

                    if (objMain.state == 'OnLine') {
                        //  location.reload();
                    }

                    //stateNeedToChange:
                    //{
                    //    'isLogin': false,
                    //        'HasNewTask': false
                    //}
                }; break;
            case 'SetOnLineState':
                {
                    dialogSys.dealWithOnLine(received_obj);
                }; break;
            case 'SetMaterial':
                {
                    var carName = 'car_' + received_obj.Key;
                    var car = objMain.carGroup.getObjectByName(carName);
                    if (car != null && car != undefined) {
                        var material = car.children[0].material;
                        var newM = material.clone();
                        var textureData = 'data:image/png;base64,' + received_obj.Base64;
                        var texture = new THREE.TextureLoader().load(textureData);
                        newM.map = texture;
                        for (var i = 0; i < 5; i++) {
                            car.children[i].material = newM;

                        }
                        newM.needsUpdate = true;
                    }
                }; break;
            case 'BradCastWhetherGoTo':
                {
                    whetherGo.obj = received_obj;
                    whetherGo.show(received_obj.msg);
                    objMain.targetMusic.addTheme(received_obj.musicID);
                    //objMain.targetMusic.change(received_obj.musicID);
                }; break;
            case 'BradCastWhereToGoInSmallMap':
                {
                    /*received_obj.base64 =*/
                    if (received_obj.isFineshed) {
                        if (received_obj.base64 == "") {
                            whetherGo.obj = JSON.parse(JSON.stringify(received_obj));
                            received_obj.data = [];
                            objMain.ws.send(JSON.stringify(received_obj));
                        }
                        else {
                            whetherGo.obj.base64 = received_obj.base64;
                            whetherGo.show2();
                            smallMapClass.draw2(whetherGo.obj);

                        }
                    }
                    else {
                        smallMapClass.draw(received_obj);
                        whetherGo.obj = received_obj;
                        whetherGo.show2();

                        if (received_obj.Live) {

                        }
                        //if (document.getElementById(douyinPanleShow.operateAddress) != null) {
                        //    whetherGo.minus();
                        //}
                    }
                }; break;
            case 'TeamStartFailed':
                {
                    creatTeamObj.teamStartFailed();
                }; break;
            case 'BradCastDouyinReward1':
                {
                    //      douyinPanleShow.add(received_obj.DetailInfo);
                }; break;
            case 'BradCastAllDouyinPlayerIsWaiting':
                {
                    douyinPanleShow.add2(received_obj.DetailInfo);
                }; break;
            case 'BradCastDouyinPlayerIsWaiting':
                {
                    douyinPanleShow.add3(received_obj);
                }; break;
            case 'DouyinAdviseSelect':
                {
                    DirectionOperator.AdviseSelect(received_obj);
                }; break;
            case 'DouyinZoomIn':
                {
                    objMain.controls.dollyOut(0.8);
                    objMain.controls.update();
                }; break;
            case 'DouyinZoomOut':
                {
                    objMain.controls.dollyOut(1.2);
                    objMain.controls.update();
                }; break;
            case 'DouyinRotateLeft':
                {
                    objMain.controls.rotateLeft(Math.PI / 12);
                    objMain.controls.update();
                }; break;
            case 'DouyinRotateRight': {
                objMain.controls.rotateLeft(-Math.PI / 12);
                objMain.controls.update();
            }; break;
            case 'DouyinRotateHigh': {
                objMain.controls.rotateUp(Math.PI / 30)
                objMain.controls.update();
            }; break;
            case 'DouyinRotateLow': {
                objMain.controls.rotateUp(-Math.PI / 30)
                objMain.controls.update();
            }; break;
            case 'MarketFlag':
                {
                    douyinPanleShow.drawFlag(received_obj);
                }; break;
            case 'MarketFlags':
                {
                    douyinPanleShow.drawFlags(received_obj);
                }; break;
            case 'ResistanceDisplay_V3':
                {
                    resistance.display_V3(received_obj);
                }; break;
            case 'ChargingLookFor.Result':
                {
                    GuidObj.chargingLookingfor.addTabel(received_obj);
                }; break;
            case 'ScoreTransferLookFor.OutputScoreResult':
                {
                    GuidObj.scoreTransferLookingfor.addTabelOutput(received_obj);
                }; break;
            case 'ScoreTransferLookFor.InputScoreResult':
                {
                    GuidObj.scoreTransferLookingfor.addTabelInput(received_obj);
                }; break;
            case 'NitrogenValueNotify':
                {
                    stateSet.nitrogeneffect.showVisible(received_obj.NitrogenValue);
                }; break;
            case 'ShowRoadCrossSelectionsOperator':
                {
                    ShowRoadCrossSelectionsOperator.show(received_obj);
                }; break;
            case 'CollectCountNotify':
                {
                    stateSet.coinIcon.addWithKey(received_obj.Key, received_obj.Count);
                }; break;
            case 'SetPlayerNameSuccess':
                {
                    localStorage.playerName = received_obj.playerName;
                }; break;
            case 'StockScoreNotify':
                {
                    setTransactionHtml.editAgreementPanelWhenTransactionWithScore(received_obj);
                }; break;
            case 'GetStockTradeCenterDetail.Result':
                {
                    //{IsLogined: true, Score: 0, Sotoshi: 0, BTCAddr: '115NqAdiyiDAf2VFvF2gznxhF7DQZUw8Wy', price: 1, …}
                    if (received_obj.IsLogined)
                        stocktradecenter.add(received_obj);
                }; break;
            case 'StockCenerOrderDetail.Result':
                {
                    stocktradecenter.addStockCenterDetail(received_obj);
                }; break;
            case 'BradCastSelections':
                {
                    console.log('BradCastSelections', received_obj);
                    drawLineOfSelections(received_obj, objMain);
                    // objMain.targetGroup
                }; break;
            case 'BradCastCompass':
                {
                    console.log('BradCastSelections', received_obj);
                    drawCompass(received_obj, objMain);
                }; break;
            case 'BradCastGoldObj':
                {
                    console.log('BradCastGoldObj', received_obj);
                    drawGoldObj(received_obj);
                }; break;
            case 'BradCastTurbine':
                {
                    console.log('BradCastTurbine', received_obj);
                    drawTurbine(received_obj);
                }; break;
            case 'BradCastSatelite':
                {
                    console.log('BradCastSatelite', received_obj);
                    drawStatelite(received_obj);
                }; break;
            case 'IsAdministrator':
                {
                    operatePanel.refresh();
                    setUpPlaceObj();
                }; break;
            case 'BradCastBitcoinAddr':
                {
                    loadFont(received_obj);
                }; break;
            case 'BradCastBattery':
                {
                    console.log('BradCastSatelite', received_obj);
                    drawBattery(received_obj);
                }; break;
            default:
                {
                    if (gearRecive(received_obj.c)) {
                        gearShow();
                    }
                    else {
                        console.log('命令未注册', received_obj.c + "__没有注册。");
                    }
                }; break;
        }
        moneyOperator.updateSaveMoneyNotify();
    },
    diamondPrice:
    {
        'mile': 0,
        'business': 0,
        'volume': 0,
        'speed': 0
    },
    driver:
    {
        driverIndex: -1,
        sex: null,
        name: '',
        skill1: { name: '', skillIndex: -1 },
        skill2: { name: '', skillIndex: -1 },
        race: ''
    },
    camaraAnimateData: null,
    transtractionData: null,
    trade:
    {
        'countPerOperate': 1
    },
    animateObj: null,
    pingTime: 0,
    heightAmplify: 5,
    heightLevel: 0,
    stateNeedToChange:
    {
        'isLogin': false,
        'HasNewTask': false
    },
    buildingData: {
        aModel: {},
        dModel: {}
    },
    selection: {
        first: -1, isZooming: false, initialize: function (msg2) {
            objMain.selection.first = -1;
            objMain.selection.isZooming = false;
            currentSelectionPreparationShow.updateFrequency('');
            if (msg2 == undefined)
                operateStateShow.update('');
            else
                operateStateShow.update(msg2);
        }
    },
    animateParameter:
    {
        loopCount: 0
    },
    groupNumber: -1,
    jscwObj: null
};
var startA = function () {
    var connected = false;
    var wsConnect = '';
    switch (objMain.debug) {
        case 0:
            {
                var r = prompt('输入选项', 'A');
                switch (r) {
                    case 'A':
                        {
                            wsConnect = 'ws://127.0.0.1:11001/websocket';
                        }; break;
                    case 'B':
                        {
                            wsConnect = 'ws://127.0.0.1:11002/websocket';
                        }; break;
                    default:
                        {
                            wsConnect = 'ws://127.0.0.1:11001/websocket';
                        }
                }

            }; break;
        case 1:
            {
                wsConnect = 'ws://192.168.0.112:11001/websocket';
            }; break;
        default:
            {
                wsConnect = 'wss://www.nyrq123.com/websocket' + window.location.pathname.split('/')[1] + '/';
            }; break;
    }


    var ws = new WebSocket(wsConnect);
    ws.onopen = function () {
        {
            var session = '';
            if (sessionStorage['session'] == undefined) {
                if (window.localStorage.getItem('notSaveSession') == null) {
                    session = window.localStorage.getItem('session');
                    if (session == null || session == undefined) {
                        session = '';
                    }
                    else {
                        sessionStorage['session'] = session;
                    }
                }
            }
            else {
                session = sessionStorage['session'];
            }
            ws.send(JSON.stringify({ c: 'CheckSession', session: session, RefererAddr: nyrqUrl.get() }));
            objMain.clicktrail = clicktrail.initialize();
            objMain.jscwObj = new jscw({ "wpm": 17 });
        }
        //   alert("数据发送中...");
    };
    ws.onmessage = function (evt) {
        var received_msg = evt.data;
        //console.log(evt.data);
        var received_obj = JSON.parse(evt.data);
        objMain.dealWithReceivedObj(received_obj, evt, received_msg);
    };
    ws.onclose = function () {
        // 关闭 websocket
        alert("连接已关闭...不要慌，千万不要退出。点击确认后，刷新网页，重新连接");
        connectionBroken();

    };
    objMain.ws = ws;
    $.notify.addStyle('happyblue', {
        html: "<div><span data-notify-text/></div>",
        classes: {
            base: {
                "opacity": "0.85",
                "width": "90%",
                "max-width": "90%",
                "background": "#F5F5F5",
                "padding": "5px",
                "border- radius": "10px"
            },
            superblue: {
                "opacity": "0.85",
                "width": "90%",
                "max-width": "90%",
                "background": "#F5F5F5",
                "padding": "5px",
                "border- radius": "10px"
            }
        }
    });
    if (typeof window.okxwallet !== 'undefined') {
        okxwallet.bitcoin.connect();
    }
}
startA();
window.requestAnimationFrame =
    window.requestAnimationFrame || window.mozRequestAnimationFrame || window.webkitRequestAnimationFrame
    || window.msRequestAnimationFrame;

var lastTime = 0;
var deltaTime = 0;
var targetFPS = 60; // 目标帧率
function animate() {
    {
        var currentTime = Date.now()
        deltaTime = currentTime - lastTime;

        objMain.animateObj = requestAnimationFrame(animate);
        if (deltaTime > 1000 / targetFPS) {
            lastTime = currentTime;
        }
        else { return; }

        if (objMain.state != objMain.receivedState) {
            objMain.state = objMain.receivedState;
        }
        switch (objMain.state) {
            case 'OnLine':
                {
                    objMain.animateParameter.loopCount++;
                    const lengthOfCC = objMain.mainF.getLength(objMain.camera.position, objMain.controls.target);
                    //var deltaYOfSelectObj = 0;
                    // deltaYOfSelectObj = animateDetailF.moveCamara(lengthOfCC);
                    //for (var i = 0; i < objMain.collectGroup.children.length; i++) {
                    //    /*
                    //     * 初始化人民币的大小
                    //     */

                    //    if (objMain.collectGroup.children[i].isGroup) {
                    //        //if (objMain.Task.state == 'collect') {
                    //        //    var scale = objMain.mainF.getLength(objMain.camera.position, objMain.controls.target) / 1226;
                    //        //    objMain.collectGroup.children[i].scale.set(scale, scale, scale);
                    //        //}
                    //        //else
                    //        {
                    //            var scale = 0.020;//; objMain.mainF.getLength(objMain.camera.position, objMain.controls.target) / 1840;
                    //            objMain.collectGroup.children[i].scale.set(scale, scale, scale);
                    //        }
                    //        objMain.collectGroup.children[i].rotation.set(-Math.PI / 2, 0, Date.now() % 30000 / 30000 * Math.PI * 2);
                    //        //  objMain.collectGroup.children[i].position.y = 0;
                    //    }
                    //}

                    for (var i = 0; i < objMain.fightSituationGroup.children.length; i++) {
                        /*
                         * 初始化态势标志大小
                         */
                        if (objMain.fightSituationGroup.children[i].isGroup) {
                            var scale = lengthOfCC / 8;//; objMain.mainF.getLength(objMain.camera.position, objMain.controls.target) / 1840;
                            objMain.fightSituationGroup.children[i].scale.set(scale, scale, scale);
                            objMain.fightSituationGroup.children[i].rotation.y = Date.now() % 5000 / 5000 * Math.PI * 2;
                        }
                    }

                    for (var i = 0; i < objMain.playerGroup.children.length; i++) {
                        /*
                         * 初始化 旗帜的大小
                         */

                        if (objMain.playerGroup.children[i].isMesh) {
                            {
                                objMain.playerGroup.children[i].scale.set(0.0015, 0.0015, 0.0015);
                                // objMain.playerGroup.children[i].position.y = 0.1;
                                stateSet.defend.Animate(objMain.playerGroup.children[i].name.split('_')[1]);
                                stateSet.confusePrepare.Animate(objMain.playerGroup.children[i].name.split('_')[1]);
                                stateSet.lostPrepare.Animate(objMain.playerGroup.children[i].name.split('_')[1]);
                                stateSet.ambusePrepare.Animate(objMain.playerGroup.children[i].name.split('_')[1]);
                                stateSet.water.Animate(objMain.playerGroup.children[i].name.split('_')[1]);

                                //  stateSet.lightning.Animate(objMain.playerGroup.children[i].name.split('_')[1]);
                            }
                        }
                    }

                    stateSet.lightning.mark.Animate();
                    stateSet.water.mark.Animate();
                    stateSet.fire.mark.Animate();

                    for (var i = 0; i < objMain.promoteDiamond.children.length; i++) {
                        /*
                         * 初始化 砖石/人民币的大小
                         */
                        if (objMain.promoteDiamond.children[i].isMesh) {
                            objMain.promoteDiamond.children[i].scale.set(0.2, 0.22, 0.2);

                            var fp = objMain.promoteDiamond.children[i].userData.Fp;// 
                            objMain.promoteDiamond.children[i].position.set(
                                MercatorGetXbyLongitude(fp.Longitude),
                                MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify,
                                -MercatorGetYbyLatitude(fp.Latitde));

                            // objMain.promoteDiamond.children[i].position.y = 0;
                            // objMain.promoteDiamond.children[i].rotation.y=
                        }
                    }

                    for (var i = 0; i < objMain.columnGroup.children.length; i++) {

                        if (objMain.columnGroup.children[i].isMesh) {
                            objMain.columnGroup.children[i].scale.setX(1);
                            objMain.columnGroup.children[i].scale.setZ(1);
                        }
                    }
                    //for (var i = 0; i < objMain.buildingGroup.children.length; i++) {
                    //    objMain.buildingGroup.children[i].scale.setX(1);
                    //    objMain.buildingGroup.children[i].scale.setZ(1);
                    //    objMain.buildingGroup.children[i].visible = false;
                    //    if (objMain.mainF.getLength(objMain.controls.target, objMain.buildingGroup.children[i].position) < lengthOfCC * 5) {
                    //        objMain.buildingGroup.children[i].visible = true;
                    //    }
                    //    if (objMain.buildingGroup.children[i].visible) {
                    //        continue;
                    //    }
                    //    else {
                    //        var selfCar = objMain.carGroup.getObjectByName('car_' + objMain.indexKey);
                    //        if (objMain.mainF.getLength(selfCar.position, objMain.buildingGroup.children[i].position) < lengthOfCC * 5) {
                    //            objMain.buildingGroup.children[i].visible = true;
                    //        }
                    //    }
                    //    if (objMain.buildingGroup.children[i].visible) {
                    //        continue;
                    //    }
                    //    else {
                    //        for (var j = 0; j < drawGoodsSelection.data.length; j++) {
                    //            var end = drawGoodsSelection.data[j];
                    //            if (objMain.mainF.getLength(end, objMain.buildingGroup.children[i].position) < lengthOfCC) {
                    //                objMain.buildingGroup.children[i].visible = true;
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    //显示3km以内的建筑物
                    //}
                    //{
                    //    var lengthOfObjs = objMain.groupOfOperatePanle.children.length;
                    //    for (var i = lengthOfObjs - 1; i >= 0; i--) {
                    //        objMain.groupOfOperatePanle.remove(objMain.groupOfOperatePanle.children[i]);
                    //    }
                    //}
                    if (objMain.canSelect) {

                        var objMainTaskstate = '';
                        var scale = 0.01;
                        objMain.raycasterOfSelector.setFromCamera(objMain.selectorPosition, objMain.camera);
                        var selectObj = null;
                        var maxCosA = -1;
                        {
                            for (var i = 0; i < objMain.collectGroup.children.length; i++) {
                                if (objMain.collectGroup.children[i].isGroup) {
                                    var position = objMain.collectGroup.children[i].position;
                                    var d = new THREE.Vector3(position.x - objMain.camera.position.x, position.y - objMain.camera.position.y, position.z - objMain.camera.position.z);
                                    var cosA = objMain.raycasterOfSelector.ray.direction.dot(d) / d.length() / objMain.raycasterOfSelector.ray.direction.length();
                                    if (cosA > 0.984807753)
                                        if (cosA > maxCosA) {
                                            maxCosA = cosA;
                                            objMainTaskstate = 'collect';
                                            selectObj = objMain.collectGroup.children[i];
                                            scale = 0.01 * objMain.mainF.getLength(objMain.camera.position, position) / 10;
                                            scale = Math.max(scale, 0.03);
                                        }
                                }
                            }

                            for (var i = 0; i < objMain.playerGroup.children.length; i++) {
                                if (objMain.playerGroup.children[i].isMesh) {
                                    //flag_d59195aa49213765a72fdff81b1e18c6
                                    if (objMain.playerGroup.children[i].name.split('_')[1] == objMain.indexKey) {
                                        var position = objMain.playerGroup.children[i].position;
                                        var d = new THREE.Vector3(position.x - objMain.camera.position.x, position.y - objMain.camera.position.y, position.z - objMain.camera.position.z);
                                        var cosA = objMain.raycasterOfSelector.ray.direction.dot(d) / d.length() / objMain.raycasterOfSelector.ray.direction.length();
                                        if (cosA > 0.984807753)
                                            if (cosA > maxCosA) {
                                                maxCosA = cosA;
                                                objMainTaskstate = 'setReturn';
                                                selectObj = objMain.playerGroup.children[i];
                                                scale = 0.0025 * objMain.mainF.getLength(objMain.camera.position, position) / 10;
                                                scale = Math.max(scale, 0.0015);
                                            }
                                    }
                                    else {
                                        var position = objMain.playerGroup.children[i].position;
                                        var d = new THREE.Vector3(position.x - objMain.camera.position.x, position.y - objMain.camera.position.y, position.z - objMain.camera.position.z);
                                        var cosA = objMain.raycasterOfSelector.ray.direction.dot(d) / d.length() / objMain.raycasterOfSelector.ray.direction.length();
                                        if (cosA > 0.984807753)
                                            if (cosA > maxCosA) {
                                                maxCosA = cosA;
                                                objMainTaskstate = 'attack';
                                                selectObj = objMain.playerGroup.children[i];
                                                scale = 0.0025 * objMain.mainF.getLength(objMain.camera.position, position) / 10;
                                                scale = Math.max(scale, 0.0015);
                                            }
                                    }
                                }
                            }

                            for (var i = 0; i < objMain.promoteDiamond.children.length; i++) {
                                if (objMain.promoteDiamond.children[i].isMesh) {
                                    {
                                        var position = objMain.promoteDiamond.children[i].position;
                                        var d = new THREE.Vector3(position.x - objMain.camera.position.x, position.y - objMain.camera.position.y, position.z - objMain.camera.position.z);
                                        var cosA = objMain.raycasterOfSelector.ray.direction.dot(d) / d.length() / objMain.raycasterOfSelector.ray.direction.length();
                                        if (cosA > 0.984807753)
                                            if (cosA > maxCosA) {
                                                maxCosA = cosA;
                                                selectObj = objMain.promoteDiamond.children[i];
                                                scale = objMain.mainF.getLength(objMain.camera.position, position) / 20;
                                                scale = Math.max(scale, 0.2);
                                                //  scale = Math.min(scale, 2);
                                                switch (selectObj.name) {
                                                    case 'diamond_mile':
                                                        {
                                                            objMainTaskstate = 'mile';
                                                        }; break;
                                                    case 'diamond_business':
                                                        {
                                                            objMainTaskstate = 'business';
                                                        }; break;
                                                    case 'diamond_volume':
                                                        {
                                                            objMainTaskstate = 'volume';
                                                        }; break;
                                                    case 'diamond_speed':
                                                        {
                                                            objMainTaskstate = 'speed';
                                                        }; break;
                                                    default: { }; break;
                                                }
                                            }
                                    }
                                }
                            }

                            for (var i = 0; i < objMain.buildingGroup.children.length; i++) {
                                if (objMain.buildingGroup.visible) {
                                    var position = objMain.buildingGroup.children[i].position;
                                    var d = new THREE.Vector3(position.x - objMain.camera.position.x, position.y - objMain.camera.position.y, position.z - objMain.camera.position.z);
                                    var cosA = objMain.raycasterOfSelector.ray.direction.dot(d) / d.length() / objMain.raycasterOfSelector.ray.direction.length();
                                    if (cosA > 0.984807753)
                                        if (cosA > maxCosA) {
                                            maxCosA = cosA;
                                            objMainTaskstate = 'building';
                                            selectObj = objMain.buildingGroup.children[i];
                                        }
                                }
                            }

                            // if (objMain.carState.car == 'waitAtBaseStation')
                            // if (lengthOfCC < 3)
                            for (var i = 0; i < objMain.columnGroup.children.length; i++) {
                                if (objMain.columnGroup.children[i].isMesh) {
                                    {
                                        var position = objMain.columnGroup.children[i].position;
                                        var d = new THREE.Vector3(position.x - objMain.camera.position.x, position.y - objMain.camera.position.y, position.z - objMain.camera.position.z);
                                        var cosA = objMain.raycasterOfSelector.ray.direction.dot(d) / d.length() / objMain.raycasterOfSelector.ray.direction.length();
                                        if (cosA > 0.984807753)
                                            if (cosA > maxCosA) {
                                                maxCosA = cosA;
                                                if (objMain.carState.car == 'waitAtBaseStation')
                                                    if (lengthOfCC < 4) {
                                                        selectObj = objMain.columnGroup.children[i];
                                                        switch (selectObj.name) {
                                                            case 'BatteryMile':
                                                                {
                                                                    objMainTaskstate = 'ability';
                                                                }; break;
                                                            case 'BatteryBusiness':
                                                                {
                                                                    objMainTaskstate = 'ability';
                                                                }; break;
                                                            case 'BatteryVolume':
                                                                {
                                                                    objMainTaskstate = 'ability';
                                                                }; break;
                                                            case 'BatterySpeed':
                                                                {
                                                                    objMainTaskstate = 'ability';
                                                                }; break;
                                                            default: { }; break;
                                                        }
                                                    }
                                                    else {
                                                        selectObj = objMain.playerGroup.getObjectByName('flag_' + objMain.indexKey);
                                                        objMainTaskstate = 'setReturn';
                                                        scale = 0.0025 * objMain.mainF.getLength(objMain.camera.position, position) / 10;
                                                        scale = Math.max(scale, 0.0015);
                                                    }
                                                else {
                                                    selectObj = objMain.playerGroup.getObjectByName('flag_' + objMain.indexKey);
                                                    objMainTaskstate = 'setReturn';
                                                    scale = 0.0025 * objMain.mainF.getLength(objMain.camera.position, position) / 10;
                                                    scale = Math.max(scale, 0.0015);
                                                }
                                            }
                                    }
                                }
                            }
                            objMain.Task.state = objMainTaskstate;
                            switch (objMain.Task.state) {
                                case 'collect':
                                    {
                                        //selectObj.scale.set(scale, scale, scale);
                                        //objMain.selectObj.obj = selectObj;
                                        //objMain.selectObj.type = objMain.Task.state;
                                        //selectObj.rotation.set(-Math.PI / 2, 0, Date.now() % 6000 / 6000 * Math.PI * 2);
                                        //{
                                        //    var collectPosition = selectObj.userData.collectPosition;
                                        //    var element = document.createElement('div');
                                        //    element.style.width = '10em';
                                        //    element.style.marginTop = '3em';
                                        //    var color = '#ff0000';
                                        //    element.style.border = '2px solid ' + color;
                                        //    element.style.borderTopLeftRadius = '0.5em';
                                        //    element.style.backgroundColor = 'rgba(245, 255, 179, 0.9)';
                                        //    element.style.color = '#1504f6';

                                        //    var div2 = document.createElement('div');
                                        //    div2.style.fontSize = '0.5em';

                                        //    var b = document.createElement('b');
                                        //    b.innerHTML = '到' + collectPosition.Fp.region + '[<span style="color:#02020f">' + collectPosition.Fp.FastenPositionName + '</span>]回收<span style="color:#02020f">' + (collectPosition.collectMoney).toFixed(2) + '元</span>现金。';
                                        //    div2.appendChild(b);

                                        //    element.appendChild(div2);

                                        //    var object = new THREE.CSS2DObject(element);
                                        //    var fp = collectPosition.Fp;
                                        //    object.position.set(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));

                                        //    objMain.groupOfOperatePanle.add(object);
                                        //}
                                    }; break;
                                case 'ability':
                                    {
                                        objMain.selectObj.obj = selectObj;
                                        objMain.selectObj.type = objMain.Task.state;
                                        selectObj.scale.setX((Date.now() % 2000 / 2000) + 1);
                                        selectObj.scale.setZ((Date.now() % 2000 / 2000) + 1);
                                        {
                                            var element = document.createElement('div');
                                            element.style.width = '10em';
                                            element.style.marginTop = '3em';
                                            var color = '#ff0000';
                                            element.style.border = '2px solid ' + color;
                                            element.style.borderTopLeftRadius = '0.5em';
                                            element.style.backgroundColor = 'rgba(245, 255, 179, 0.9)';
                                            element.style.color = '#1504f6';

                                            var div2 = document.createElement('div');
                                            div2.style.fontSize = '0.5em';

                                            var b = document.createElement('b');
                                            switch (selectObj.name) {
                                                case 'BatteryMile':
                                                    {
                                                        b.innerHTML = '红宝石市价:' + (objMain.diamondPrice.mile / 100.0).toFixed(2) + "银两。用其提升最大里程。你有"
                                                            + objMain.PromoteDiamondCount.mile + '块';

                                                    }; break;
                                                case 'BatteryBusiness':
                                                    {
                                                        b.innerHTML = '绿宝石市价:' + (objMain.diamondPrice.business / 100.0).toFixed(2) + "银两。用其提升最大业务能力。你有"
                                                            + objMain.PromoteDiamondCount.business + '块';
                                                    }; break;
                                                case 'BatteryVolume':
                                                    {
                                                        b.innerHTML = '蓝宝石市价:' + (objMain.diamondPrice.volume / 100.0).toFixed(2) + "银两。用其提升最大收集能力。你有"
                                                            + objMain.PromoteDiamondCount.volume + '块';
                                                    }; break;
                                                case 'BatterySpeed':
                                                    {
                                                        b.innerHTML = '黑宝石市价:' + (objMain.diamondPrice.speed / 100.0).toFixed(2) + "银两。用其提升速度。你有"
                                                            + objMain.PromoteDiamondCount.speed + '块';
                                                    }; break;
                                            }

                                            div2.appendChild(b);

                                            element.appendChild(div2);

                                            var object = new THREE.CSS2DObject(element);
                                            //  var fp = collectPosition.Fp;
                                            object.position.set(selectObj.position.x, selectObj.position.y, selectObj.position.z);

                                            objMain.groupOfOperatePanle.add(object);
                                        }
                                    }; break;
                                case 'setReturn':
                                    {
                                        selectObj.scale.set(scale, scale, scale);
                                        objMain.selectObj.obj = selectObj;
                                        objMain.selectObj.type = objMain.Task.state;
                                        selectObj.rotation.set(-Math.PI / 2, 0, Date.now() % 300 / 300 * Math.PI * 2);
                                        {
                                            var element = document.createElement('div');
                                            element.style.width = '10em';
                                            element.style.marginTop = '3em';
                                            var color = '#ff0000';
                                            element.style.border = '2px solid ' + color;
                                            element.style.borderTopLeftRadius = '0.5em';
                                            element.style.backgroundColor = 'rgba(245, 255, 179, 0.9)';
                                            element.style.color = '#1504f6';

                                            var div2 = document.createElement('div');
                                            div2.style.fontSize = '0.5em';

                                            var b = document.createElement('b');
                                            b.innerHTML = '回到基地->' + objMain.basePoint.FastenPositionName + '(' + objMain.basePoint.Longitude.toFixed(2) + ',' + objMain.basePoint.Latitde.toFixed(2) + ')';
                                            div2.appendChild(b);

                                            element.appendChild(div2);

                                            var object = new THREE.CSS2DObject(element);
                                            //  var fp = collectPosition.Fp;
                                            object.position.set(selectObj.position.x, selectObj.position.y, selectObj.position.z);

                                            objMain.groupOfOperatePanle.add(object);
                                        }
                                    }; break;
                                case 'attack':
                                    {
                                        selectObj.scale.set(scale, scale, scale);
                                        objMain.selectObj.obj = selectObj;
                                        objMain.selectObj.type = objMain.Task.state;
                                        selectObj.rotation.set(-Math.PI / 2, 0, Date.now() % 300 / 300 * Math.PI * 2);
                                        {
                                            var element = document.createElement('div');
                                            element.style.width = '10em';
                                            element.style.marginTop = '3em';
                                            var color = '#ff0000';
                                            element.style.border = '2px solid ' + color;
                                            element.style.borderTopLeftRadius = '0.5em';
                                            element.style.backgroundColor = 'rgba(245, 255, 179, 0.9)';
                                            element.style.color = '#1504f6';

                                            var div2 = document.createElement('div');
                                            div2.style.fontSize = '0.5em';

                                            var b = document.createElement('b');

                                            var customTagIndexKey = selectObj.name.substring(5);
                                            if (objMain.othersBasePoint[customTagIndexKey] == undefined)
                                                b.innerHTML = '攻击';
                                            else {
                                                if (objMain.othersBasePoint[customTagIndexKey].isNPC) {
                                                    //b.innerHTML = '[NPC]->到';
                                                    b.innerHTML = b.innerHTML = '到(' + objMain.othersBasePoint[customTagIndexKey].basePoint.region + ')'
                                                        + objMain.othersBasePoint[customTagIndexKey].basePoint.FastenPositionName
                                                        + '(' + objMain.othersBasePoint[customTagIndexKey].basePoint.Longitude.toFixed(2) + ','
                                                        + objMain.othersBasePoint[customTagIndexKey].basePoint.Latitde.toFixed(2) + ')攻击NPC'
                                                        + '【' + objMain.othersBasePoint[customTagIndexKey].playerName + '】'
                                                        + '(' + objMain.othersBasePoint[customTagIndexKey].Level + '级)';

                                                }
                                                else if (objMain.othersBasePoint[customTagIndexKey].isPlayer) {
                                                    b.innerHTML = '到(' + objMain.othersBasePoint[customTagIndexKey].basePoint.region + ')'
                                                        + objMain.othersBasePoint[customTagIndexKey].basePoint.FastenPositionName
                                                        + '(' + objMain.othersBasePoint[customTagIndexKey].basePoint.Longitude.toFixed(2) + ','
                                                        + objMain.othersBasePoint[customTagIndexKey].basePoint.Latitde.toFixed(2) + ')攻击玩家'
                                                        + '【' + objMain.othersBasePoint[customTagIndexKey].playerName + '】'
                                                        + '(' + objMain.othersBasePoint[customTagIndexKey].Level + '级)';
                                                }
                                                else {
                                                    b.innerHTML = '攻击';
                                                }
                                            }
                                            div2.appendChild(b);

                                            element.appendChild(div2);

                                            var object = new THREE.CSS2DObject(element);
                                            //  var fp = collectPosition.Fp;
                                            object.position.set(selectObj.position.x, selectObj.position.y, selectObj.position.z);

                                            objMain.groupOfOperatePanle.add(object);
                                        }
                                    }; break;
                                case 'mile':
                                case 'business':
                                case 'volume':
                                case 'speed':
                                    {
                                        objMain.selectObj.obj = selectObj;
                                        objMain.selectObj.type = objMain.Task.state;

                                        selectObj.scale.set(scale, scale * 1.1, scale);

                                        var fp = selectObj.userData.Fp;//
                                        var baseY = MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify;
                                        selectObj.position.y = baseY + Math.sin(Date.now() % 3000 / 3000 * Math.PI) * scale / 4;
                                        selectObj.rotation.y = (Date.now() % 8000 / 8000) * Math.PI * 2;
                                        //objMain.ws.send(JSON.stringify({ 'c': 'Promote', 'pType': objMain.Task.state }));
                                    }; break;

                                case 'building':
                                    {
                                        objMain.selectObj.obj = selectObj;
                                        objMain.selectObj.type = objMain.Task.state;
                                        if (objMain.selectObj.obj.userData.modelType == 'building') {
                                            var inteview = Date.now() % 4000;
                                            if (inteview > 2000) {
                                                selectObj.scale.setX(Math.sin(inteview % 2000 / 2000 * Math.PI * 2) * -0.05 + 1);
                                                selectObj.scale.setZ(Math.sin(inteview % 2000 / 2000 * Math.PI * 2) * -0.05 + 1);
                                            }
                                            else {
                                                selectObj.scale.setX(Math.sin(inteview % 2000 / 2000 * Math.PI * 2) * -0.1 + 1);
                                                selectObj.scale.setZ(Math.sin(inteview % 2000 / 2000 * Math.PI * 2) * -0.1 + 1);
                                            }
                                        }
                                    }; break;
                            }
                        }
                    }
                    else {
                        objMain.Task.state = '';
                    }
                    for (var i = 0; i < objMain.playerGroup.children.length; i++) {
                        if (objMain.playerGroup.children[i].isMesh) {
                            objMain.playerGroup.children[i].userData.animateDataYrq.simulate(Date.now());
                            objMain.playerGroup.children[i].userData.animateDataYrq.refresh(Date.now());
                        }
                    }
                    for (var i = 0; i < objMain.carGroup.children.length; i++) {
                        /*
                         * 初始化汽车的大小
                         */
                        //if (objMain.carGroup.children[i].isGroup) {
                        //    var scale = lengthOfCC * 0.001;
                        //    if (scale < 0.002) {
                        //        scale = 0.002;
                        //    }
                        //    objMain.carGroup.children[i].scale.set(scale, scale, scale);
                        //    objMain.carGroup.children[1].name.split('_')[1]
                        //    stateSet.speed.Animate(objMain.carGroup.children[i].name.split('_')[1]);
                        stateSet.attck.Animate(objMain.carGroup.children[i].name.split('_')[1]);
                        //    stateSet.confuse.Animate(objMain.carGroup.children[i].name.split('_')[1]);
                        //    stateSet.lost.Animate(objMain.carGroup.children[i].name.split('_')[1]);

                        //    stateSet.lightning.Animate(objMain.carGroup.children[i].name.split('_')[1]);
                        //    stateSet.fire.Animate(objMain.carGroup.children[i].name.split('_')[1]);
                        //}
                    }
                    {
                        /*放大选中的汽车*/
                        var scale = lengthOfCC * 0.001;
                        if (scale < 0.002) {
                            scale = 0.002;
                        }
                        if (objMain.Task.carSelect != '') {
                            if (objMain.carGroup.getObjectByName(objMain.Task.carSelect) != undefined) {
                                objMain.carGroup.getObjectByName(objMain.Task.carSelect).scale.set(scale, scale, scale);
                            }
                        }

                    }

                    if (objMain.buildingGroup.children.length > 0)
                        objMain.buildingGroup.children[0].children[1].rotation.x = (Date.now() % 2000) / 2000 * Math.PI * 2;

                    //if (objMain.carState.car == 'selecting') {
                    //    if (objMain.directionGroup.children.length > 0) {
                    //        var newDirectionModle = objMain.directionGroup.children[0];
                    //        var roleKey = 'car_' + objMain.indexKey;
                    //        var car_self = objMain.carGroup.getObjectByName(roleKey);
                    //        if (car_self) {
                    //            var x = newDirectionModle.position.x;
                    //            var y = newDirectionModle.position.y + 0.1;
                    //            var z = newDirectionModle.position.z;
                    //            car_self.position.set(x, y, z);
                    //        }
                    //    }
                    //}
                    var selfsCarIsMoving = false;
                    {
                        var keys = Object.keys(objMain.carsAnimateData);//获取素有的Key
                        for (var i = 0; i < keys.length; i++) {
                            var carKey = keys[i];
                            var animateDataOfSingleCar = objMain.carsAnimateData[carKey];
                            var previous = animateDataOfSingleCar.previous;
                            var current = animateDataOfSingleCar.current;
                            var isSelf = (carKey == 'car_' + objMain.indexKey);
                            var now = Date.now();
                            var moving = false;
                            if (previous != null) {
                                moving = animationF(carKey, previous, now, isSelf, lengthOfCC);
                            }
                            if (current != null && !moving) {
                                moving = animationF(carKey, current, now, isSelf, lengthOfCC);
                            }
                            if (moving && isSelf) {
                                selfsCarIsMoving = true;
                            }
                        }
                    }
                    //if (objMain.carState.car == 'selecting') {
                    //    objMain.directionGroup.visible = true;
                    //    if (objMain.directionGroup.children.length > 0) {
                    //    }

                    //    objMain.controls.maxPolarAngle = Math.PI / 2 + Math.PI / 3;
                    //}
                    //else {
                    //    objMain.directionGroup.visible = false;
                    //    objMain.controls.maxPolarAngle = Math.PI / 2 + Math.PI / 3;//Math.PI / 2 - Math.PI / 36;
                    //}

                    // var carPosition = 
                    if (selfsCarIsMoving) {
                        var deltaYOfCar = 0;
                        if (objMain.carGroup.getObjectByName('car_' + objMain.indexKey)) {
                            deltaYOfCar = objMain.carGroup.getObjectByName('car_' + objMain.indexKey).position.y;
                        }
                        // sceneYUpdate(deltaYOfCar);
                        objMain.heightLevel = deltaYOfCar;
                    }
                    else if (objMain.camaraAnimateData != null) {
                        // sceneYUpdate(deltaYOfSelectObj);
                        //  objMain.heightLevel = deltaYOfSelectObj;
                    }
                    else {
                        //sceneYUpdate(objMain.heightLevel);
                    }
                    moneyAbsorb.animate(objMain);
                    targetShow.animate(objMain);
                    objMain.animation.animateCameraByCarAndTask();

                    //   theLagestHoderKey.animate();
                    objMain.renderer.render(objMain.scene, objMain.camera);
                    objMain.labelRenderer.render(objMain.scene, objMain.camera);
                    objMain.light1.position.set(objMain.camera.position.x + lengthOfCC / 3, objMain.camera.position.y, objMain.camera.position.z + lengthOfCC / 3);

                    if (objMain.animateParameter.loopCount % 5000 == 0) {
                        onWindowResize();
                    }
                    animateAerialVehicle(objMain);
                }; break;
            case 'LookForBuildings':
                {
                    if (objMain.transtractionData != null) {
                        objMain.camera.lookAt(objMain.transtractionData.x, objMain.transtractionData.y, objMain.transtractionData.z);
                        objMain.controls.target.set(objMain.transtractionData.x, objMain.transtractionData.y, objMain.transtractionData.z);
                    }
                    objMain.renderer.render(objMain.scene, objMain.camera);
                    objMain.labelRenderer.render(objMain.scene, objMain.camera);
                    objMain.light1.position.set(objMain.camera.position.x, objMain.camera.position.y, objMain.camera.position.z);

                }; break;
            case 'QueryReward':
                {
                    if (objMain.renderer != null)
                        objMain.renderer.render(objMain.scene, objMain.camera);
                    if (objMain.labelRenderer != null)
                        objMain.labelRenderer.render(objMain.scene, objMain.camera);
                    if (objMain.renderer != null) {
                        var lengthOfCC = objMain.mainF.getLength(objMain.camera.position, objMain.controls.target);
                        // var deltaYOfSelectObj = 0;
                        animateDetailF.moveCamara(lengthOfCC);
                        objMain.light1.position.set(objMain.camera.position.x + lengthOfCC / 3, objMain.camera.position.y, objMain.camera.position.z + lengthOfCC / 3);
                    }

                }; break;
        }
        //if (objMain.state == 'OnLine') {


        //}
        //else if ('LookForBuildings' == objMain.state) {
        //    //if (objMain.transtractionData != null)

        //}

        if (Date.now() - objMain.pingTime > 100) {
            objMain.pingTime = Date.now();
            // objMain.ws.send('{"c":"c"}');
        }
    }
}
animate();

var animateDetailF =
{
    moveCamara: function (lengthOfCC) {
        return 0;
        if (false) {
            if (objMain.camaraAnimateData != null) {
                if (objMain.camaraAnimateData.newT.t < Date.now()) {
                    objMain.camaraAnimateData = null;
                    return 0;
                }
                else {
                    var percent1 = (Date.now() - objMain.camaraAnimateData.old.t) / 3000 * Math.PI;
                    var percent2 = (Math.sin(percent1 - Math.PI / 2) + 1) / 2;
                    var x = objMain.camaraAnimateData.old.x + (objMain.camaraAnimateData.newT.x - objMain.camaraAnimateData.old.x) * percent2;
                    var y = objMain.camaraAnimateData.old.y + (objMain.camaraAnimateData.newT.y - objMain.camaraAnimateData.old.y) * percent2;
                    var z = objMain.camaraAnimateData.old.z + (objMain.camaraAnimateData.newT.z - objMain.camaraAnimateData.old.z) * percent2;
                    objMain.controls.target.set(x, y, z);
                    var angle = objMain.controls.getPolarAngle();
                    //if(
                    //var dCal = objMain.mainF.getLength(objMain.camera.position, objMain.controls.target);
                    var distance = lengthOfCC;
                    var unitY = Math.abs(distance * Math.cos(angle));
                    var unitZX = distance * Math.sin(angle);

                    var angleOfCamara = objMain.controls.getAzimuthalAngle();
                    var unitX = unitZX * Math.sin(angleOfCamara);
                    var unitZ = unitZX * Math.cos(angleOfCamara);

                    objMain.camera.position.set(x + unitX, y + unitY, z + unitZ);
                    objMain.camera.lookAt(x, y, z);
                    return y;
                }
            }
            else return 0;
        }
    }
};


var setNameFuncton = function () {
    if (localStorage.playerName != undefined) {
        // localStorage.playerName==
        {
            var name = localStorage.playerName;
            var reg = /^[\u4e00-\u9fa5]{1}[a-zA-Z0-9\u4e00-\u9fa5]{1,8}$/;
            if (reg.test(name)) {
                objMain.ws.send(JSON.stringify({ c: 'SetPlayerName', 'Name': name }));
                objMain.ws.send(JSON.stringify({ c: 'GetName' }));
                setNameFuncton = null;
            }
        }
    }
}
var selectSingleTeamJoinHtml = function () {

    document.getElementById('rootContainer').innerHTML = selectSingleTeamJoinHtmlF.drawHtml();
    selectSingleTeamJoinHtmlF.canBtnClick = true;

    if (setNameFuncton != null) {
        setNameFuncton();
    }
}
var buttonClick = function (v) {
    if (objMain.receivedState == 'selectSingleTeamJoin') {
        if (selectSingleTeamJoinHtmlF.canBtnClick) {
            switch (v) {
                case 'single':
                    {
                        objMain.ws.send(JSON.stringify({ c: 'JoinGameSingle', RefererAddr: nyrqUrl.get() }));
                        objMain.receivedState = '';
                    }; break;
                case 'team':
                    {
                        alert('暂不开放');
                        return;
                        objMain.ws.send(JSON.stringify({ c: 'CreateTeam', RefererAddr: nyrqUrl.get() }));
                        objMain.receivedState = '';
                    }; break;
                case 'join':
                    {
                        alert('暂不开放');
                        return;
                        objMain.ws.send(JSON.stringify({ c: 'JoinTeam' }));
                        objMain.receivedState = '';
                    }; break;
                case 'setName':
                    {
                        // objMain.ws.send(JSON.stringify({ c: 'JoinTeam' }));
                        objMain.receivedState = 'setName';
                        selectSingleTeamJoinHtmlF.setNameHtmlShow();
                        objMain.ws.send(JSON.stringify({ c: 'GetName' }));
                    }; break;
                case 'setCarsName':
                    {
                        // objMain.ws.send(JSON.stringify({ c: 'JoinTeam' }));
                        objMain.receivedState = 'setCarsName';
                        selectSingleTeamJoinHtmlF.setCarsNameHtmlShow();
                        objMain.ws.send(JSON.stringify({ c: 'GetCarsName' }));
                    }; break;
                case 'QueryReward':
                    {
                        alert('暂不开放');
                        return;
                        //selectSingleTeamJoinHtmlF.setNameHtmlShow();
                        objMain.ws.send(JSON.stringify({ c: 'QueryReward' }));
                    }; break;
                case 'HelpAndGuide':
                    {
                        alert('暂不开放');
                        return;
                        objMain.ws.send(JSON.stringify({ c: 'Guid' }));
                    }; break;

                //case 'lookForBuildings':
                //    {
                //        objMain.ws.send(JSON.stringify({ c: 'LookForBuildings' }));
                //        objMain.receivedState = '';
                //    }; break;
            }
            // objMain.receivedState = '';
        }
    }
    selectSingleTeamJoinHtmlF.canBtnClick = false;
}

var QueryReward =
{
    draw3D: function () {
        document.getElementById('rootContainer').innerHTML = '';
        var mainC = document.createElement('div');
        mainC.id = 'mainC';

        mainC.className = 'container';
        document.getElementById('rootContainer').appendChild(mainC);
        objMain.scene = new THREE.Scene();

        var cubeTextureLoader = new THREE.CubeTextureLoader();
        cubeTextureLoader.setPath('Pic/');
        var cubeTexture = cubeTextureLoader.load([
            "px.jpg", "nx.jpg",
            "py.jpg", "ny.jpg",
            "pz.jpg", "nz.jpg"
        ]);
        objMain.scene.background = cubeTexture;

        objMain.renderer = new THREE.WebGLRenderer({ alpha: true });
        objMain.renderer.setClearColor(0x000000, 0); // the default
        objMain.renderer.setPixelRatio(window.devicePixelRatio);
        objMain.renderer.setSize(window.innerWidth, window.innerHeight);
        objMain.renderer.domElement.className = 'renderDom';
        document.getElementById('mainC').appendChild(objMain.renderer.domElement);
        //  document.body

        objMain.labelRenderer = new THREE.CSS2DRenderer();
        objMain.labelRenderer.setSize(window.innerWidth, window.innerHeight);
        objMain.labelRenderer.domElement.className = 'labelRenderer';
        //objMain.labelRenderer.domElement.style.curs
        document.getElementById('mainC').appendChild(objMain.labelRenderer.domElement);

        objMain.camera = new THREE.PerspectiveCamera(35, 1, 0.1, 30000);
        objMain.camera.position.set(4000, 2000, 0);
        objMain.camera.position.set(MercatorGetXbyLongitude(objMain.centerPosition.lon), 20, -MercatorGetYbyLatitude(objMain.centerPosition.lat));

        objMain.controls = new THREE.OrbitControls(objMain.camera, objMain.labelRenderer.domElement);
        objMain.controls.center.set(MercatorGetXbyLongitude(objMain.centerPosition.lon), 0, -MercatorGetYbyLatitude(objMain.centerPosition.lat));

        objMain.roadGroup = new THREE.Group();
        objMain.scene.add(objMain.roadGroup);

        objMain.playerGroup = new THREE.Group();
        objMain.scene.add(objMain.playerGroup);

        objMain.promoteDiamond = new THREE.Group();
        objMain.scene.add(objMain.promoteDiamond);

        objMain.columnGroup = new THREE.Group();
        objMain.scene.add(objMain.columnGroup);

        objMain.carGroup = new THREE.Group();
        objMain.scene.add(objMain.carGroup);

        objMain.groupOfOperatePanle = new THREE.Group();
        objMain.scene.add(objMain.groupOfOperatePanle);

        objMain.collectGroup = new THREE.Group();
        objMain.scene.add(objMain.collectGroup);

        objMain.getOutGroup = new THREE.Group();
        objMain.scene.add(objMain.getOutGroup);

        objMain.taxGroup = new THREE.Group();
        objMain.scene.add(objMain.taxGroup);

        objMain.shieldGroup = new THREE.Group();
        objMain.scene.add(objMain.shieldGroup);

        objMain.confusePrepareGroup = new THREE.Group();
        objMain.scene.add(objMain.confusePrepareGroup);

        objMain.lostPrepareGroup = new THREE.Group();
        objMain.scene.add(objMain.lostPrepareGroup);

        objMain.ambushPrepareGroup = new THREE.Group();
        objMain.scene.add(objMain.ambushPrepareGroup);

        objMain.waterGroup = new THREE.Group();
        objMain.scene.add(objMain.waterGroup);

        objMain.fireGroup = new THREE.Group();
        objMain.scene.add(objMain.fireGroup);

        objMain.lightningGroup = new THREE.Group();
        objMain.scene.add(objMain.lightningGroup);

        objMain.directionGroup = new THREE.Group();
        objMain.scene.add(objMain.directionGroup);

        objMain.buildingGroup = new THREE.Group();
        objMain.scene.add(objMain.buildingGroup);

        objMain.clock = new THREE.Clock();

        {
            objMain.light1 = new THREE.PointLight(0xffffff);
            objMain.light1.position.set(-100, 300, -100);
            objMain.light1.intensity = 2;
            objMain.scene.add(objMain.light1);
        }

        {
            //objMain.controls.minDistance = 3;
            // objMain.controls.maxPolarAngle = Math.PI;
            objMain.controls.minPolarAngle = Math.PI / 600;
            objMain.controls.maxPolarAngle = Math.PI / 2 - Math.PI / 36;
            objMain.controls.minDistance = 2;
            objMain.controls.maxDistance = 256;
        }

        objMain.raycaster = new THREE.Raycaster();
        objMain.raycaster.linePrecision = 0.2;

        objMain.raycasterOfSelector = new THREE.Raycaster();
        //objMain.raycasterOfSelector.linePrecision = 100;

        objMain.mouse = new THREE.Vector2();

        //objMain.labelRenderer.domElement.addEventListener

        var operateEnd = function (event) {
            //   operatePanel.refresh();
            return;
        }
        var operateStart = function (event) {
            objMain.canSelect = true;
            objMain.music.change();
            objMain.music.MarketRepeat();
        }
        objMain.labelRenderer.domElement.addEventListener('mouseup', operateEnd, false);
        objMain.labelRenderer.domElement.addEventListener('mousedown', operateStart, false);


        objMain.labelRenderer.domElement.addEventListener('touchstart', operateStart, false);
        objMain.labelRenderer.domElement.addEventListener('touchend', operateEnd, false);

        window.addEventListener('resize', onWindowResize, false);
        onWindowResize();
    },
    lookAt: function () {
        if (objMain.state == 'QueryReward') {
            var selectBuilding = objMain.buildingGroup.children[0];
            var animationData =
            {
                old: {
                    x: objMain.controls.target.x,
                    y: objMain.controls.target.y,
                    z: objMain.controls.target.z,
                    t: Date.now()
                },
                newT:
                {
                    x: selectBuilding.position.x,
                    y: selectBuilding.position.y,
                    z: selectBuilding.position.z,
                    t: Date.now() + 3000
                }
            };
            objMain.camaraAnimateData = animationData;
        }
    },
    drawToolBar: function (title) {
        if (objMain.state == 'QueryReward') {
            while (document.getElementById('sysOperatePanel') != null) {
                document.getElementById('sysOperatePanel').remove();
            }
            var divSysOperatePanel = document.createElement('div');
            divSysOperatePanel.id = 'sysOperatePanel';
            divSysOperatePanel.style.position = 'absolute';
            divSysOperatePanel.style.zIndex = '7';
            divSysOperatePanel.style.top = 'calc(100% - 2.5em - 8px)';
            divSysOperatePanel.style.left = '8px';
            divSysOperatePanel.style.width = 'calc(100% - 16px)';
            {
                var img = document.createElement('img');
                img.id = 'QueryRewardExit';
                img.src = 'Pic/settingIcon.png';
                img.classList.add('chatdialog');
                img.style.border = 'solid 1px orange';
                img.style.borderRadius = '5px';
                img.style.height = 'calc(2.5em - 2px)';
                img.style.width = 'auto';
                img.style.marginLeft = 'calc(100% - 2.5em - 2px)';
                // img.style.right = '0.5em';

                img.onclick = function () {
                    while (document.getElementById('sysOperatePanel') != null) {
                        document.getElementById('sysOperatePanel').remove();
                    }
                    objMain.mainF.removeF.clearGroup(objMain.roadGroup);
                    MapData.meshPoints = [];
                    objMain.mainF.removeF.clearGroup(objMain.buildingGroup);

                    objMain.renderer = null;
                    objMain.labelRenderer = null;
                    document.getElementById('rootContainer').innerHTML = '';
                    objMain.ws.send(JSON.stringify({ 'c': 'RewardInfomation', 'Page': reward.page }));
                    //alert(title);
                };

                divSysOperatePanel.appendChild(img);
            }
            document.body.appendChild(divSysOperatePanel);
        }

    },
}

var setTransactionHtml =
{
    bussinessAddress: '',
    draw3D: function () {
        //var text = "";
        //text += "  <div>";
        //text += "            3D界面";
        //text += "        </div>";
        //document.getElementById('rootContainer').innerHTML = text;
        document.getElementById('rootContainer').innerHTML = '';

        //<div id="mainC" class="container" onclick="testTop();">
        //    <!--<img />-->
        //    <!--<a href="DAL/MapImage.ashx">DAL/MapImage.ashx</a>-->
        //    <img src="Pic/11.png" />
        //</div>
        var mainC = document.createElement('div');
        mainC.id = 'mainC';

        mainC.className = 'container_Show';
        document.getElementById('rootContainer').appendChild(mainC);
        document.getElementById('rootContainer').style.overflow = 'scroll';
        objMain.scene = new THREE.Scene();
        //objMain.scene.background = new THREE.Color(0x7c9dd4);
        //objMain.scene.fog = new THREE.FogExp2(0x7c9dd4, 0.2);

        var cubeTextureLoader = new THREE.CubeTextureLoader();
        cubeTextureLoader.setPath('Pic/');
        //var cubeTexture = cubeTextureLoader.load([
        //    "xi_r.jpg", "dong_r.jpg",
        //    "ding_r.jpg", "di_r.jpg",
        //    "nan_r.jpg", "bei_r.jpg"
        //]);
        var cubeTexture = cubeTextureLoader.load([
            "px.jpg", "nx.jpg",
            "py.jpg", "ny.jpg",
            "pz.jpg", "nz.jpg"
        ]);
        objMain.scene.background = cubeTexture;

        objMain.renderer = new THREE.WebGLRenderer({ alpha: true });
        objMain.renderer.setClearColor(0x000000, 0); // the default
        objMain.renderer.setPixelRatio(window.devicePixelRatio);
        objMain.renderer.setSize(300, 300);
        objMain.renderer.domElement.className = 'renderDom_Trans';
        document.getElementById('mainC').appendChild(objMain.renderer.domElement);
        //  document.body

        objMain.labelRenderer = new THREE.CSS2DRenderer();
        objMain.labelRenderer.setSize(300, 300);
        objMain.labelRenderer.domElement.className = 'labelRenderer_Trans';
        //objMain.labelRenderer.domElement.style.curs
        document.getElementById('mainC').appendChild(objMain.labelRenderer.domElement);

        objMain.camera = new THREE.PerspectiveCamera(35, 1, 0.1, 30000);
        objMain.camera.position.set(4000, 2000, 0);
        objMain.camera.position.set(MercatorGetXbyLongitude(objMain.centerPosition.lon), 20, -MercatorGetYbyLatitude(objMain.centerPosition.lat));

        objMain.controls = new THREE.OrbitControls(objMain.camera, objMain.labelRenderer.domElement);
        objMain.controls.center.set(MercatorGetXbyLongitude(objMain.centerPosition.lon), 0, -MercatorGetYbyLatitude(objMain.centerPosition.lat));

        objMain.roadGroup = new THREE.Group();
        objMain.scene.add(objMain.roadGroup);

        objMain.playerGroup = new THREE.Group();
        objMain.scene.add(objMain.playerGroup);

        objMain.promoteDiamond = new THREE.Group();
        objMain.scene.add(objMain.promoteDiamond);

        objMain.columnGroup = new THREE.Group();
        objMain.scene.add(objMain.columnGroup);

        objMain.carGroup = new THREE.Group();
        objMain.scene.add(objMain.carGroup);

        objMain.groupOfOperatePanle = new THREE.Group();
        objMain.scene.add(objMain.groupOfOperatePanle);

        objMain.collectGroup = new THREE.Group();
        objMain.scene.add(objMain.collectGroup);

        objMain.getOutGroup = new THREE.Group();
        objMain.scene.add(objMain.getOutGroup);

        objMain.taxGroup = new THREE.Group();
        objMain.scene.add(objMain.taxGroup);

        objMain.shieldGroup = new THREE.Group();
        objMain.scene.add(objMain.shieldGroup);

        objMain.confusePrepareGroup = new THREE.Group();
        objMain.scene.add(objMain.confusePrepareGroup);

        objMain.lostPrepareGroup = new THREE.Group();
        objMain.scene.add(objMain.lostPrepareGroup);

        objMain.ambushPrepareGroup = new THREE.Group();
        objMain.scene.add(objMain.ambushPrepareGroup);

        objMain.waterGroup = new THREE.Group();
        objMain.scene.add(objMain.waterGroup);

        objMain.fireGroup = new THREE.Group();
        objMain.scene.add(objMain.fireGroup);

        objMain.lightningGroup = new THREE.Group();
        objMain.scene.add(objMain.lightningGroup);

        objMain.directionGroup = new THREE.Group();
        objMain.scene.add(objMain.directionGroup);

        objMain.buildingGroup = new THREE.Group();
        objMain.scene.add(objMain.buildingGroup);

        objMain.clock = new THREE.Clock();

        {
            objMain.light1 = new THREE.PointLight(0xffffff);
            objMain.light1.position.set(-100, 300, -100);
            objMain.light1.intensity = 2;
            objMain.scene.add(objMain.light1);
        }

        {
            //objMain.controls.minDistance = 3;
            // objMain.controls.maxPolarAngle = Math.PI;
            objMain.controls.minPolarAngle = Math.PI / 600;
            objMain.controls.maxPolarAngle = Math.PI / 2 - Math.PI / 36;
            objMain.controls.minDistance = 2;
            objMain.controls.maxDistance = 256;
        }

        objMain.raycaster = new THREE.Raycaster();
        objMain.raycaster.linePrecision = 0.2;

        objMain.raycasterOfSelector = new THREE.Raycaster();
        //objMain.raycasterOfSelector.linePrecision = 100;

        objMain.mouse = new THREE.Vector2();

        //objMain.labelRenderer.domElement.addEventListener

        var operateEnd = function (event) {
            //   operatePanel.refresh();
            return;
        }
        var operateStart = function (event) {
            objMain.canSelect = true;
            objMain.music.change();
            objMain.music.MarketRepeat();
        }
        objMain.labelRenderer.domElement.addEventListener('mouseup', operateEnd, false);
        objMain.labelRenderer.domElement.addEventListener('mousedown', operateStart, false);


        objMain.labelRenderer.domElement.addEventListener('touchstart', operateStart, false);
        objMain.labelRenderer.domElement.addEventListener('touchend', operateEnd, false);
        //scope.domElement.removeEventListener('touchstart', onTouchStart, false);
        //scope.domElement.removeEventListener('touchend', onTouchEnd, false);
        //drawCarBtnsFrame();
        //objNotify.carNotifyShow();
        window.addEventListener('resize', onWindowResize, false);
    },
    change: function () {
        switch (objMain.state) {
            case 'LookForBuildings':
                {
                    var mainC = document.getElementById('mainC');
                    mainC.classList.add('small');
                    objMain.renderer.setSize(300, 300);
                    objMain.labelRenderer.setSize(300, 300);
                    objMain.camera.aspect = 1;
                    objMain.camera.updateProjectionMatrix();
                }; break;
            default:
                {
                    var mainC = document.getElementById('mainC');
                    mainC.classList.remove('small');
                    objMain.labelRenderer.setSize(window.innerWidth, window.innerHeight);
                    objMain.renderer.setSize(window.innerWidth, window.innerHeight);
                    objMain.camera.aspect = window.innerWidth / window.innerHeight;
                    objMain.camera.updateProjectionMatrix();
                }; break;
        }


    },
    drawAddr: function (addr) {
        transactionBussiness().drawAddr(addr);
        setTransactionHtml.bussinessAddress = addr;
    },
    drawAgreementEditor: function () {
        transactionBussiness().drawAgreementEditor();
    },
    drawStockTable: function () {
        transactionBussiness().drawStockTable();
    },
    drawTradeTable: function () {
        transactionBussiness().drawTradeTable();
    },
    originalTable: function () {
        transactionBussiness().originalTable();
    },
    generateAgreement: function () {
        objMain.ws.send(transactionBussiness().generateAgreement(setTransactionHtml.bussinessAddress));
    },
    generateTransactionWithScore: function () {
        objMain.ws.send(transactionBussiness().generateTransactionWithScore(setTransactionHtml.bussinessAddress));
    },
    transSign: function () {
        objMain.ws.send(transactionBussiness().transSign(setTransactionHtml.bussinessAddress));
    },
    transSignWhenTrade: function () {
        var tradeJson = transactionBussiness().transSignWhenTrade(setTransactionHtml.bussinessAddress);
        if (tradeJson != null)
            objMain.ws.send(transactionBussiness().transSignWhenTrade(setTransactionHtml.bussinessAddress));
    },
    editRootContainer: function () {
        transactionBussiness().editRootContainer();
        //objMain.camera.aspect = 1;
        //objMain.camera.updateProjectionMatrix();
        //objMain.labelRenderer.setSize(300, 300);
        //objMain.renderer.setSize(300, 300);
    },
    cancle: function () {
        transactionBussiness().Cancle();
        transactionBussiness().notifyMsg(false);
    },
    editAgreementPanelWhenTransactionWithScore: function (inputObj) {
        if (setTransactionHtml.bussinessAddress == inputObj.baseBusinessAddr)
            transactionBussiness().editAgreementPanelWhenTransactionWithScore(inputObj);
    },
    confirmTransaction: function (hasCode) {
        objMain.ws.send(transactionBussiness().confirmTransaction(hasCode, setTransactionHtml.bussinessAddress));
    },
    cancleTransaction: function (hasCode) {
        objMain.ws.send(transactionBussiness().cancleTransaction(hasCode, setTransactionHtml.bussinessAddress));
    }
}



var set3DHtml = function () {
    //var text = "";
    //text += "  <div>";
    //text += "            3D界面";
    //text += "        </div>";
    //document.getElementById('rootContainer').innerHTML = text;
    document.getElementById('rootContainer').innerHTML = '';

    //<div id="mainC" class="container" onclick="testTop();">
    //    <!--<img />-->
    //    <!--<a href="DAL/MapImage.ashx">DAL/MapImage.ashx</a>-->
    //    <img src="Pic/11.png" />
    //</div>
    var mainC = document.createElement('div');
    mainC.id = 'mainC';

    mainC.className = 'container';
    document.getElementById('rootContainer').appendChild(mainC);

    objMain.scene = new THREE.Scene();
    //objMain.scene.background = new THREE.Color(0x7c9dd4);
    //objMain.scene.fog = new THREE.FogExp2(0x7c9dd4, 0.2);

    var cubeTextureLoader = new THREE.CubeTextureLoader();
    cubeTextureLoader.setPath('Pic/');
    //var cubeTexture = cubeTextureLoader.load([
    //    "xi_r.jpg", "dong_r.jpg",
    //    "ding_r.jpg", "di_r.jpg",
    //    "nan_r.jpg", "bei_r.jpg"
    //]);
    var cubeTexture = cubeTextureLoader.load([
        "px.jpg", "nx.jpg",
        "py.jpg", "ny.jpg",
        "pz.jpg", "nz.jpg"
    ], function (newt) {
        objMain.scene.background = newt;
        objMain.scene.background.needsUpdate = true;
        cubeTextureLoader = null;
    }
    );
    // objMain.background.backgroundData['main'] = cubeTexture;
    //objMain.background.backgroundData['main'];

    objMain.renderer = new THREE.WebGLRenderer({ alpha: true });
    objMain.renderer.setClearColor(0x000000, 0); // the default
    objMain.renderer.setPixelRatio(window.devicePixelRatio);
    objMain.renderer.setSize(window.innerWidth, window.innerHeight);
    objMain.renderer.domElement.className = 'renderDom';
    document.getElementById('mainC').appendChild(objMain.renderer.domElement);
    //  document.body

    objMain.labelRenderer = new THREE.CSS2DRenderer();
    objMain.labelRenderer.setSize(window.innerWidth, window.innerHeight);
    objMain.labelRenderer.domElement.className = 'labelRenderer';
    //objMain.labelRenderer.domElement.style.curs
    document.getElementById('mainC').appendChild(objMain.labelRenderer.domElement);

    objMain.camera = new THREE.PerspectiveCamera(35, window.innerWidth / window.innerHeight, 0.1, 300000);
    // objMain.camera.position.set(4000, 2000, 0);
    objMain.camera.position.set(4.8, 6, 6.4);

    //objMain.controls.maxDistance = 10;
    //objMain.controls.minDistance = 10;

    objMain.controls = new THREE.OrbitControls(objMain.camera, objMain.labelRenderer.domElement);
    objMain.controls.center.set(0, 0, 0);

    {
        var registGroup = function (g) {
            g = new THREE.Group();
            objMain.scene.add(g);
            return g;
        }
        objMain.roadGroup = registGroup(objMain.roadGroup);
        objMain.playerGroup = registGroup(objMain.playerGroup);
        objMain.promoteDiamond = registGroup(objMain.promoteDiamond);
        objMain.columnGroup = registGroup(objMain.columnGroup);
        objMain.carGroup = registGroup(objMain.carGroup);
        objMain.groupOfOperatePanle = registGroup(objMain.groupOfOperatePanle);
        objMain.collectGroup = registGroup(objMain.collectGroup);
        objMain.getOutGroup = registGroup(objMain.getOutGroup);
        objMain.taxGroup = registGroup(objMain.taxGroup);
        objMain.shieldGroup = registGroup(objMain.shieldGroup);
        objMain.confusePrepareGroup = registGroup(objMain.confusePrepareGroup);
        objMain.lostPrepareGroup = registGroup(objMain.lostPrepareGroup);
        objMain.ambushPrepareGroup = registGroup(objMain.ambushPrepareGroup);
        objMain.waterGroup = registGroup(objMain.waterGroup);
        objMain.waterMarkGroup = registGroup(objMain.waterMarkGroup);
        objMain.fireGroup = registGroup(objMain.fireGroup);
        objMain.fireMarkGroup = registGroup(objMain.fireGroup);
        objMain.lightningGroup = registGroup(objMain.lightningGroup);
        objMain.lightningMarkGroup = registGroup(objMain.lightningMarkGroup);
        objMain.absorbGroup = registGroup(objMain.absorbGroup);
        objMain.directionGroup = registGroup(objMain.directionGroup);
        objMain.buildingGroup = registGroup(objMain.buildingGroup);
        objMain.targetGroup = registGroup(objMain.targetGroup);
        objMain.buildingSelectionGroup = registGroup(objMain.buildingSelectionGroup);
        objMain.fightSituationGroup = registGroup(objMain.fightSituationGroup);
        objMain.groupOfTaskCopy = registGroup(objMain.groupOfTaskCopy);
        objMain.marketGroup = registGroup(objMain.marketGroup);
        objMain.crossSelectionsOperator = registGroup(objMain.crossSelectionsOperator);
        objMain.bitcoinCharacterGroup = registGroup(objMain.bitcoinCharacterGroup);
        objMain.batteryGroup = registGroup(objMain.batteryGroup);
    }
    if (false) {

    }
    objMain.clock = new THREE.Clock();

    {
        objMain.light1 = new THREE.PointLight(0xffffff);
        objMain.light1.position.set(-100, 300, -100);
        objMain.light1.intensity = 2;
        objMain.scene.add(objMain.light1);
    }

    {
        //objMain.controls.minDistance = 3;
        // objMain.controls.maxPolarAngle = Math.PI;
        objMain.controls.minPolarAngle = 0;
        objMain.controls.maxPolarAngle = Math.PI;// / 2;//'- Math.PI / 36;
        objMain.controls.minDistance = 10;
        objMain.controls.maxDistance = 10;
    }

    objMain.raycaster = new THREE.Raycaster();
    objMain.raycaster.linePrecision = 0.2;

    objMain.raycasterOfSelector = new THREE.Raycaster();
    //objMain.raycasterOfSelector.linePrecision = 100;

    objMain.mouse = new THREE.Vector2();

    //objMain.labelRenderer.domElement.addEventListener

    var operateEnd = function (event) {
        var roadLineSelected = false;
        var targetSelected = false;
        var compassIconIsClicked = false;
        var goldObjIsClicked = false;
        var isSelected = false;
        var turbineIsClicked = false;
        var sateliteIsClicked = false;
        var btcAddrIsCLicked = false;
        var batteryIsClicked = false;
        var selfVehicleIsClicked = false;

        if (event.clientX != undefined && event.clientX != null) {

            if (whetherGo.isMask()) {
                return;
            }
            //此处对应鼠标
            objMain.mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
            objMain.mouse.y = - (event.clientY / window.innerHeight) * 2 + 1;
            // lookInfoForCar();
            selfVehicleIsClicked = selfVehicleClicked();
            isSelected = isSelected || selfVehicleIsClicked;

            if (!isSelected) {
                roadLineSelected = lookInfoForSelect();
                isSelected = isSelected || roadLineSelected;
            }
            if (!isSelected) {
                targetSelected = confirmSelect();
                isSelected = isSelected || targetSelected;
            }
            if (!isSelected) {
                compassIconIsClicked = compassClicked();
                isSelected = isSelected || compassIconIsClicked;
            }
            if (!isSelected) {
                goldObjIsClicked = goldObjClicked();
                isSelected = isSelected || goldObjIsClicked;
            }
            if (!isSelected) {
                turbineIsClicked = turbineClicked();
                isSelected = isSelected || turbineIsClicked;
            }
            if (!isSelected) {
                sateliteIsClicked = sataliteClicked();
                isSelected = isSelected || sateliteIsClicked;
            }
            if (!isSelected) {
                btcAddrIsCLicked = btcAddrClicked();
                isSelected = isSelected || btcAddrIsCLicked;
            }
            if (!isSelected) {
                batteryIsClicked = batteryClicked();
                isSelected = isSelected || batteryIsClicked;
            }
        }
        else if (event.changedTouches && event.changedTouches.length > 0) {
            if (whetherGo.isMask()) {
                return;
            }
            // 获取第一个触摸点。这个对应手机触摸屏。
            var touch = event.changedTouches[0];
            objMain.mouse.x = (touch.clientX / window.innerWidth) * 2 - 1;
            objMain.mouse.y = - (touch.clientY / window.innerHeight) * 2 + 1;
            // lookInfoForCar();

            selfVehicleIsClicked = selfVehicleClicked();
            isSelected = isSelected || selfVehicleIsClicked;

            if (!isSelected) {
                roadLineSelected = lookInfoForSelect();
                isSelected = isSelected || roadLineSelected;
            }
            if (!isSelected) {
                targetSelected = confirmSelect();
                isSelected = isSelected || targetSelected;
            }
            if (!isSelected) {
                compassIconIsClicked = compassClicked();
                isSelected = isSelected || compassIconIsClicked;
            }
            if (!isSelected) {
                goldObjIsClicked = goldObjClicked();
                isSelected = isSelected || goldObjIsClicked;
            }
            if (!isSelected) {
                turbineIsClicked = turbineClicked();
                isSelected = isSelected || turbineIsClicked;
            }
            if (!isSelected) {
                sateliteIsClicked = sataliteClicked();
                isSelected = isSelected || sateliteIsClicked;
            }
            if (!isSelected) {
                btcAddrIsCLicked = btcAddrClicked();
                isSelected = isSelected || btcAddrIsCLicked;
            }
            if (!isSelected) {
                batteryIsClicked = batteryClicked();
                isSelected = isSelected || batteryIsClicked;
            }
        }

        if (isSelected) { }
        else {
            objMain.mainF.removeF.clearGroup(objMain.targetGroup);
        }

        //operatePanel.refresh();

        //if (objMain.directionGroup.visible) {


        //    for (var i = 0; i < objMain.roadGroup.children; i++) {

        //    }

        //    //var minAngle = Math.PI / 20;
        //    //var selectIndex = -1;
        //    //for (var i = 1; i < objMain.directionGroup.children.length; i++) {
        //    //    // objMain.directionGroup.children[i].children[0].material = objMain.ModelInput.directionArrow.oldM;
        //    //    var delta = (objMain.directionGroup.children[i].rotation.y - (objMain.controls.getAzimuthalAngle() + Math.PI / 2) + Math.PI * 4) % (Math.PI * 2);
        //    //    if (delta < minAngle) {
        //    //        minAngle = delta;
        //    //        selectIndex = i;
        //    //    }
        //    //    else {
        //    //        continue;
        //    //    }
        //    //}
        //    //if (objMain.selection.isZooming) {
        //    //    objMain.selection.isZooming = false;
        //    //}
        //    //else if (selectIndex > 0) {
        //    //    if (objMain.selection.first == selectIndex) {
        //    //        var rotationY = objMain.directionGroup.children[selectIndex].rotation.y;
        //    //        if (objMain.directionGroup.children[selectIndex].userData.objState > 0) {
        //    //        }
        //    //        else {
        //    //            var json = JSON.stringify({ c: 'ViewAngle', 'rotationY': rotationY, 'postionCrossKey': objMain.directionGroup.children[selectIndex].userData.postionCrossKey, 'uid': '' });
        //    //            objMain.ws.send(json);
        //    //            //userData.objState
        //    //            objMain.directionGroup.children[selectIndex].userData.objState = 1;
        //    //            objMain.selection.initialize();
        //    //            switch (selectIndex) {
        //    //                case 1: { objMain.jscwObj.play('A'); }; break;
        //    //                case 2: { objMain.jscwObj.play('B'); }; break;
        //    //                case 3: { objMain.jscwObj.play('C'); }; break;
        //    //            }
        //    //            objMain.targetMusic.change();

        //    //            setTimeout(() => {
        //    //                objMain.targetMusic.change('pass');
        //    //            }, 3000);
        //    //        }
        //    //    }
        //    //    else {
        //    //        objMain.selection.first = selectIndex;
        //    //        switch (selectIndex) {
        //    //            case 1: {
        //    //                //$.notify('再次点击选择A', 'msg');
        //    //                currentSelectionPreparationShow.updateFrequency('确认选A');
        //    //                operateStateShow.update('请再点击');
        //    //            }; break;
        //    //            case 2: {
        //    //                //$.notify('再次点击选择B', 'msg');
        //    //                currentSelectionPreparationShow.updateFrequency('确认选B');
        //    //                operateStateShow.update('请再点击');
        //    //            }; break;
        //    //            case 3: {
        //    //                //$.notify('再次点击选择C', 'msg');
        //    //                currentSelectionPreparationShow.updateFrequency('确认选C');
        //    //                operateStateShow.update('请再点击');
        //    //            }; break;
        //    //        }

        //    //    }

        //    //    operatePanel.refresh();
        //    //    onWindowResize();
        //    //}
        //}
        //objMain.ws
        return;
    }
    var operateStart = function (event) {
        objMain.canSelect = true;
        objMain.music.change();
        objMain.music.MarketRepeat();
    }
    objMain.labelRenderer.domElement.addEventListener('mouseup', operateEnd, false);
    objMain.labelRenderer.domElement.addEventListener('mousedown', operateStart, false);


    objMain.labelRenderer.domElement.addEventListener('touchstart', operateStart, false);
    objMain.labelRenderer.domElement.addEventListener('touchend', operateEnd, false);
    //scope.domElement.removeEventListener('touchstart', onTouchStart, false);
    //scope.domElement.removeEventListener('touchend', onTouchEnd, false);
    //drawCarBtnsFrame();
    //objNotify.carNotifyShow();
    window.addEventListener('resize', onWindowResize, false);
    // douyinPanleShow.drawFlagThemeDetail('Ukraine');
    // douyinPanleShow.drawFlagThemeDetail('Russia');
}
function onWindowResize() {
    switch (objMain.state) {
        case 'OnLine':
            {
                objMain.camera.aspect = window.innerWidth / window.innerHeight;
                objMain.camera.updateProjectionMatrix();

                objMain.labelRenderer.setSize(window.innerWidth, window.innerHeight);
                objMain.renderer.setSize(window.innerWidth, window.innerHeight);
                carAbility.refreshPosition();
            }; break;
        case 'LookForBuildings':
            {
                objMain.camera.aspect = 1;
                objMain.camera.updateProjectionMatrix();

                objMain.labelRenderer.setSize(300, 300);
                objMain.renderer.setSize(300, 300);
            }; break;
        case 'QueryReward': {
            objMain.camera.aspect = window.innerWidth / window.innerHeight;
            objMain.camera.updateProjectionMatrix();

            objMain.labelRenderer.setSize(window.innerWidth, window.innerHeight);
            objMain.renderer.setSize(window.innerWidth, window.innerHeight);
        }; break;
    }


    var touchTrail = document.getElementById('touch-trail');
    touchTrail.width = window.innerWidth;
    touchTrail.height = window.innerHeight;
}


var MapData =
{
    roadAndCrossJson: '',
    roadAndCross: null,
    meshPoints: [],
    initialize: function () {
        this.roadAndCross = '';
        this.roadAndCross = null;
        this.meshPoints = [];
    }
};
var drawPoint = function (color, fp, indexKey) {
    var flagName = 'flag_' + indexKey;
    if (objMain.playerGroup == null || objMain.playerGroup == undefined) {
        return;
    }
    else if (objMain.playerGroup.getObjectByName(flagName) == null || objMain.playerGroup.getObjectByName(flagName) == undefined) {
        var createFlag = function (color) {
            var that = this;
            this.windStrengthDelta = 0;
            this.DAMPING = 0.03;
            this.DRAG = 1 - this.DAMPING;
            this.MASS = 0.1;
            this.restDistance = 25;
            this.xSegs = 10;
            this.ySegs = 10;
            function plane(width, height) {

                return function (u, v, target) {

                    var x = (u - 0.5) * width;
                    var y = (v + 0.5) * height;
                    var z = 0;

                    target.set(x, y, z);

                };

            }
            var clothFunction = plane(this.restDistance * this.xSegs, this.restDistance * this.ySegs);
            function Cloth(w, h) {

                w = w || 10;
                h = h || 10;
                this.w = w;
                this.h = h;

                var particles = [];
                var constraints = [];

                // Create particles
                for (let v = 0; v <= h; v++) {

                    for (let u = 0; u <= w; u++) {

                        particles.push(
                            new Particle(u / w, v / h, 0, that.MASS)
                        );

                    }

                }

                // Structural

                for (let v = 0; v < h; v++) {

                    for (let u = 0; u < w; u++) {

                        constraints.push([
                            particles[index(u, v)],
                            particles[index(u, v + 1)],
                            that.restDistance
                        ]);

                        constraints.push([
                            particles[index(u, v)],
                            particles[index(u + 1, v)],
                            that.restDistance
                        ]);

                    }

                }

                for (let u = w, v = 0; v < h; v++) {

                    constraints.push([
                        particles[index(u, v)],
                        particles[index(u, v + 1)],
                        that.restDistance

                    ]);

                }

                for (let v = h, u = 0; u < w; u++) {

                    constraints.push([
                        particles[index(u, v)],
                        particles[index(u + 1, v)],
                        that.restDistance
                    ]);

                }
                this.particles = particles;
                this.constraints = constraints;

                function index(u, v) {

                    return u + v * (w + 1);

                }

                this.index = index;

            }
            var cloth = new Cloth(this.xSegs, this.ySegs);
            this.cloth = cloth;

            this.GRAVITY = 981 * 1.4;
            this.gravity = new THREE.Vector3(0, -  this.GRAVITY, 0).multiplyScalar(this.MASS);

            this.TIMESTEP = 18 / 1000;
            this.TIMESTEP_SQ = this.TIMESTEP * this.TIMESTEP;

            this.pins = [];

            this.windForce = new THREE.Vector3(0, 0, 0);

            //this. ballPosition = new THREE.Vector3(0, - 45, 0);
            //this. ballSize = 60; //40

            this.tmpForce = new THREE.Vector3();

            function Particle(x, y, z, mass) {

                this.position = new THREE.Vector3();
                this.previous = new THREE.Vector3();
                this.original = new THREE.Vector3();
                this.a = new THREE.Vector3(0, 0, 0); // acceleration
                this.mass = mass;
                this.invMass = 1 / mass;
                this.tmp = new THREE.Vector3();
                this.tmp2 = new THREE.Vector3();

                // init

                clothFunction(x, y, this.position); // position
                clothFunction(x, y, this.previous); // previous
                clothFunction(x, y, this.original);

            }

            // Force -> Acceleration

            Particle.prototype.addForce = function (force) {

                this.a.add(
                    this.tmp2.copy(force).multiplyScalar(this.invMass)
                );

            };


            // Performs Verlet integration

            Particle.prototype.integrate = function (timesq) {

                var newPos = this.tmp.subVectors(this.position, this.previous);
                newPos.multiplyScalar(that.DRAG).add(this.position);
                newPos.add(this.a.multiplyScalar(timesq));

                this.tmp = this.previous;
                this.previous = this.position;
                this.position = newPos;

                this.a.set(0, 0, 0);

            };

            this.diff = new THREE.Vector3();
            function satisfyConstraints(p1, p2, distance) {

                that.diff.subVectors(p2.position, p1.position);
                var currentDist = that.diff.length();
                if (currentDist === 0) return; // prevents division by 0
                var correction = that.diff.multiplyScalar(1 - distance / currentDist);
                var correctionHalf = correction.multiplyScalar(0.5);
                p1.position.add(correctionHalf);
                p2.position.sub(correctionHalf);

            }
            this.simulate = function (now) {
                //这里进行动画
                var windStrength = Math.cos(now / 7000) * 20 + 40 + that.windStrengthDelta;

                that.windForce.set(Math.sin(now / 2000), Math.cos(now / 3000), Math.sin(now / 1000));
                that.windForce.normalize();
                that.windForce.multiplyScalar(windStrength);

                // Aerodynamics forces

                var particles = cloth.particles;

                {

                    let indx;
                    var normal = new THREE.Vector3();
                    var indices = clothGeometry.index;
                    var normals = clothGeometry.attributes.normal;

                    for (let i = 0, il = indices.count; i < il; i += 3) {

                        for (let j = 0; j < 3; j++) {

                            indx = indices.getX(i + j);
                            normal.fromBufferAttribute(normals, indx);
                            that.tmpForce.copy(normal).normalize().multiplyScalar(normal.dot(that.windForce));
                            particles[indx].addForce(that.tmpForce);

                        }

                    }

                }

                for (let i = 0, il = particles.length; i < il; i++) {

                    var particle = particles[i];
                    particle.addForce(that.gravity);

                    particle.integrate(that.TIMESTEP_SQ);

                }

                // Start Constraints

                var constraints = cloth.constraints;
                var il = constraints.length;

                for (let i = 0; i < il; i++) {

                    var constraint = constraints[i];
                    satisfyConstraints(constraint[0], constraint[1], constraint[2]);

                }

                // Ball Constraints




                // Floor Constraints

                for (let i = 0, il = particles.length; i < il; i++) {

                    var particle = particles[i];
                    var pos = particle.position;
                    if (pos.y < - 250) {

                        pos.y = - 250;

                    }

                }

                // Pin Constraints

                for (let i = 0, il = that.pins.length; i < il; i++) {

                    var xy = that.pins[i];
                    var p = particles[xy];
                    p.position.copy(p.original);
                    p.previous.copy(p.original);

                }


            }

            this.pinsFormation = [];
            this.pins = [6];

            this.pinsFormation.push(this.pins);

            this.pins = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            this.pinsFormation.push(this.pins);

            this.pins = [0];
            this.pinsFormation.push(this.pins);

            this.pins = []; // cut the rope ;)
            this.pinsFormation.push(this.pins);

            this.pins = [0, cloth.w]; // classic 2 pins
            this.pinsFormation.push(this.pins);

            this.pins = this.pinsFormation[1];

            function togglePins() {

                pins = pinsFormation[~ ~(Math.random() * pinsFormation.length)];

            }
            this.clothMaterial = new THREE.MeshLambertMaterial({
                side: THREE.DoubleSide,
                alphaTest: 0.5,
                color: color,
                emissive: color
            });
            var clothGeometry = new THREE.ParametricBufferGeometry(clothFunction, cloth.w, cloth.h);
            this.clothGeometry = clothGeometry;


            this.refresh = function () {
                var p = that.cloth.particles;
                for (let i = 0, il = p.length; i < il; i++) {

                    var v = p[i].position;

                    that.clothGeometry.attributes.position.setXYZ(i, v.x, v.y, v.z);

                }
                that.clothGeometry.attributes.position.needsUpdate = true;
                that.clothGeometry.computeVertexNormals();
            };

            return this;
        }
        var objToShow = new createFlag(color);
        object = new THREE.Mesh(objToShow.clothGeometry, objToShow.clothMaterial);
        object.userData['animateDataYrq'] = objToShow;
        object.position.set(MercatorGetXbyLongitude(fp.Longitude), 0.1 + MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
        object.scale.set(0.0005, 0.0005, 0.0005);

        var start = new THREE.Vector3(MercatorGetXbyLongitude(fp.Longitude), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.Latitde));
        var end = new THREE.Vector3(MercatorGetXbyLongitude(fp.positionLongitudeOnRoad), MercatorGetZbyHeight(fp.Height) * objMain.heightAmplify, -MercatorGetYbyLatitude(fp.positionLatitudeOnRoad));
        var cc = new Complex(end.x - start.x, end.z - start.z);
        cc.toOne();
        object.rotateY(-cc.toAngle() + Math.PI / 2);
        object.name = flagName;
        objMain.playerGroup.add(object);
        return;
    }
}


var SysOperatePanel =
{
    draw: function () {
        while (document.getElementById('sysOperatePanel') != null) {
            document.getElementById('sysOperatePanel').remove();
        }
        var divSysOperatePanel = document.createElement('div');
        divSysOperatePanel.id = 'sysOperatePanel';
        divSysOperatePanel.style.position = 'absolute';
        divSysOperatePanel.style.zIndex = '7';
        divSysOperatePanel.style.top = 'calc(100% - 2.5em - 8px)';
        divSysOperatePanel.style.left = '8px';
        divSysOperatePanel.style.width = 'calc(100% - 16px)';
        {
            var img = document.createElement('img');
            img.id = 'msgToNotify'
            img.src = 'Pic/chatPng.png';
            img.classList.add('chatdialog');
            img.style.border = 'solid 1px yellowgreen';
            img.style.borderRadius = '5px';
            img.style.height = 'calc(2.5em - 2px)';
            img.style.width = 'auto';

            img.onclick = function () {
                // alert('打开聊天框');
                dialogSys.show();
            };
            img.classList.add('costomButton');
            divSysOperatePanel.appendChild(img);
        }
        {
            var img = document.createElement('img');
            img.id = 'moneyServe';
            img.src = 'Pic/trade.png';
            img.classList.add('chatdialog');
            img.style.border = 'solid 1px yellowgreen';
            img.style.borderRadius = '5px';
            img.style.height = 'calc(2.5em - 2px)';
            img.style.width = 'auto';
            img.style.marginLeft = "0.5em";
            img.onclick = function () {
                // alert('打开聊天框');
                //      dialogSys.show();
                //alert('弹出对话框');
                moneyOperator.add();
            };
            img.classList.add('costomButton');
            divSysOperatePanel.appendChild(img);
        }
        {
            var img = document.createElement('img');
            img.id = 'moneySubsidize';
            img.src = 'Pic/subsidize.png';
            img.classList.add('chatdialog');
            img.style.border = 'solid 1px yellowgreen';
            img.style.borderRadius = '5px';
            img.style.height = 'calc(2.5em - 2px)';
            img.style.width = 'auto';
            img.style.marginLeft = "0.5em";
            img.onclick = function () {
                // alert('打开聊天框');
                //      dialogSys.show();
                //alert('弹出对话框');
                // var el = img;
                subsidizeSys.add();
                //moneyOperator.add();
            };
            img.classList.add('costomButton');
            if (objMain.stateNeedToChange.isLogin) { }
            else
                img.classList.add('msg');
            divSysOperatePanel.appendChild(img);
        }
        {
            var img = document.createElement('img');
            img.id = 'moneySubsidize';
            img.src = 'Pic/trophy.png';
            img.classList.add('chatdialog');
            img.style.border = 'solid 1px yellowgreen';
            img.style.borderRadius = '5px';
            img.style.height = 'calc(2.5em - 2px)';
            img.style.width = 'auto';
            img.style.marginLeft = "0.5em";
            img.onclick = function () {
                // alert('债务框！');
                //      dialogSys.show();
                //alert('弹出对话框');
                //subsidizeSys.add();
                //moneyOperator.add();

                var removePanel = function (panelId) {
                    if (document.getElementById(panelId) == null) {
                    }
                    else {
                        document.getElementById(panelId).remove();
                    }
                }
                removePanel("douyinRankPanle");
                removePanel("douyinPanleShow_DouyiOperatePanle");

                if (document.getElementById("douyinPanleShow") == null) {
                    resistance.bindData(objMain.indexKey);
                }
                else {
                    document.getElementById("douyinPanleShow").remove();
                }
            };
            img.classList.add('costomButton');
            divSysOperatePanel.appendChild(img);
        }
        {
            var img = document.createElement('img');
            img.id = 'gameFrontSetting';
            img.src = 'Pic/settingIcon.png';
            img.classList.add('chatdialog');
            img.style.border = 'solid 1px yellowgreen';
            img.style.borderRadius = '5px';
            img.style.height = 'calc(2.5em - 2px)';
            img.style.width = 'auto';
            img.style.marginLeft = 'calc(100% - 15em + 10px)';
            // img.style.right = '0.5em';

            img.onclick = function () {
                if (objMain.state == 'LookForBuildings') {
                    objMain.ws.send(JSON.stringify({ c: 'CancleLookForBuildings' }));

                    setTransactionHtml.editRootContainer();
                    setTransactionHtml.drawAddr('');
                    setTransactionHtml.drawAgreementEditor();
                    setTransactionHtml.drawStockTable();
                    setTransactionHtml.drawTradeTable();
                    setTransactionHtml.originalTable();
                    transactionBussiness().showAuthor('');
                }
                else {
                    settingSys.add();
                }
            };
            img.classList.add('costomButton');
            divSysOperatePanel.appendChild(img);
        }
        document.body.appendChild(divSysOperatePanel);
    },
    notifyMsg: function () {
        var element = document.getElementById('msgToNotify');
        element.classList.add('msg');
    },
    //needToLogin: function ()
    //{

    //},
    cancelNotifyMsg: function () {
        var element = document.getElementById('msgToNotify');
        element.classList.remove('msg');
    },
    remove: function () {
        while (document.getElementById('sysOperatePanel') != null) {
            document.getElementById('sysOperatePanel').remove();
        }
    }
};

var setWaitingToStart = function () {
    var text = "";
    text += "  <div>";
    text += "          请等待";
    text += "        </div>";
    document.getElementById('rootContainer').innerHTML = text;
}
var token =
{
    CommandStart: '',

}


var createTeam = function (teamCreateFinish) {

    creatTeamObj.createMain(teamCreateFinish);
    return;
    document.getElementById('rootContainer').innerHTML = '';
    var div1 = document.createElement('div');
    div1.style.textAlign = 'center';
    var addDiv = function (title, content, isSelf) {
        var div = document.createElement('div');
        var label = document.createElement('label');
        var b = document.createElement('b');
        label.innerText = title;
        b.innerText = content;
        div.appendChild(label);
        div.appendChild(b);
        if (isSelf) {
            div.style.background = 'green';
            div.style.color = 'yellow';
        }
        return div;
    }
    div1.appendChild(addDiv('房间号：', teamCreateFinish.TeamNum));
    div1.appendChild(addDiv(teamCreateFinish.PlayerDetail.Description + '：', teamCreateFinish.PlayerDetail.Name, true));

    document.getElementById('rootContainer').appendChild(div1);


    {
        var div2 = document.createElement('div');
        div2.style.textAlign = 'center';
        var button_Start = document.createElement("button");
        button_Start.innerText = '开始';
        button_Start.style.width = "5em";
        button_Start.style.height = "3em";
        button_Start.style.marginTop = "1em";



        var div3 = document.createElement('div');
        div3.style.textAlign = 'center';
        var button_Exit = document.createElement("button");
        button_Exit.innerText = '解散';
        button_Exit.style.width = "5em";
        button_Exit.style.height = "3em";
        button_Exit.style.marginTop = "5em";
        button_Exit.style.backgroundColor = "orange";
        button_Exit.onclick = function () {
            objMain.ws.send(token.CommandStart + 'exit');
            button_Start.onclick = function () { };
            button_Exit.onclick = function () { };
        };

        button_Start.onclick = function () {
            objMain.ws.send(token.CommandStart);
            // this.onclick = function () { };
            button_Start.onclick = function () { };
            button_Exit.onclick = function () { };
        };

        var div4 = document.createElement('div');
        div4.style.textAlign = 'center';
        var button_ClearOffLine = document.createElement("button");
        button_ClearOffLine.innerText = '清理离线';
        button_ClearOffLine.style.width = "5em";
        button_ClearOffLine.style.height = "3em";
        button_ClearOffLine.style.marginTop = "5em";
        // button_ClearOffLine.style.backgroundColor = "orange";
        button_ClearOffLine.onclick = function () {
            objMain.ws.send(token.CommandStart + 'clear');
        };

        div2.appendChild(button_Start);
        div3.appendChild(button_Exit);
        div4.appendChild(button_ClearOffLine);

        document.getElementById('rootContainer').appendChild(div2);
        document.getElementById('rootContainer').appendChild(div4);
        document.getElementById('rootContainer').appendChild(div3);
    }

}

var setWaitingToGetTeam = function () {
    creatTeamObj.join();
    return;

    document.getElementById('rootContainer').innerHTML = '';
    var div1 = document.createElement('div');
    div1.style.textAlign = 'center';
    div1.style.marginTop = '2em';
    var label = document.createElement('label');
    label.innerText = '房间号';
    var input = document.createElement('input');
    input.id = 'roomNumInput';
    input.type = 'number';
    div1.appendChild(label);
    div1.appendChild(input);
    document.getElementById('rootContainer').appendChild(div1);

    var div2 = document.createElement('div');
    div2.style.textAlign = 'center';

    var button = document.createElement("button");
    button.innerText = '加入';
    button.style.width = "5em";
    button.style.height = "3em";
    button.style.marginTop = "1em";
    button.onclick = function () {
        console.log('提示', '加入事件还没有写写哦');
        var roomNumInput = document.getElementById('roomNumInput').value;
        if (roomNumInput == '') {
            alert('不要输入空');
        }
        else {
            objMain.ws.send(roomNumInput);
            this.onclick = function () { };
        }
    };
    div2.appendChild(button);
    document.getElementById('rootContainer').appendChild(div2);
}


var joinTeamDetail = function (teamJoinFinish) {
    creatTeamObj.joinTeamDetail(teamJoinFinish);
    return;
    document.getElementById('rootContainer').innerHTML = '';
    var div1 = document.createElement('div');
    div1.style.textAlign = 'center';

    var addDiv = function (title, content, id, isSelf) {
        var div = document.createElement('div');
        var label = document.createElement('label');
        var b = document.createElement('b');
        label.innerText = title;
        b.innerText = content;
        div.appendChild(label);
        div.appendChild(b);
        if (id != undefined && id != null && id != '') div.id = id;
        if (isSelf) {
            div.style.background = 'green';
            div.style.color = 'yellow';
        }
        return div;
    }
    div1.appendChild(addDiv('房间号：', teamJoinFinish.TeamNum));
    div1.appendChild(addDiv(teamJoinFinish.Players[0].Description + '：', teamJoinFinish.Players[0].Name, teamJoinFinish.Players[0].GUID));

    for (var i = 1; i < teamJoinFinish.Players.length; i++) {
        div1.appendChild(
            addDiv(
                teamJoinFinish.Players[i].Description + '：',
                teamJoinFinish.Players[i].Name,
                teamJoinFinish.Players[i].GUID,
                teamJoinFinish.Players[i].IsSelf));
    }

    document.getElementById('rootContainer').appendChild(div1);

    var div2 = document.createElement('div');
    div2.style.textAlign = 'center';

    var button = document.createElement("button");
    button.innerText = '离开房间';
    button.style.width = "5em";
    button.style.height = "3em";
    button.style.marginTop = "5em";
    button.style.backgroundColor = "orange";
    button.onclick = function () {
        objMain.ws.send(JSON.stringify({ c: 'LeaveTeam' }));
    };
    div2.appendChild(button);

    document.getElementById('rootContainer').appendChild(div2);
}

var broadTeamJoin = function (teamJoinBroadInfo) {
    creatTeamObj.broadTeamJoin(teamJoinBroadInfo);
    return;
}

var broadTeamMemberRemove = function (removeInfo) {
    while (document.getElementById(removeInfo.Guid) != null) {
        document.getElementById(removeInfo.Guid).remove();
    }
}

var marketOperate =
{
    refresh: function () {
        marketOperate.clearPanel();
        marketOperate.state = '';
        switch (objMain.carState["car"]) {
            case 'waitAtBaseStation':
                {
                    marketOperate.drawCarBtns();
                }; break;
            default:
                {
                }; break;
        }
    },
    clearPanel: function () {
        while (document.getElementById('taskOperatingPanel') != null) {
            document.getElementById('taskOperatingPanel').remove();
        }
    },
    drawCarBtns: function () {
        {
            var clearBtnOfObj = function (id) {
                if (document.getElementById(id) == null) { }
                else {
                    var tp = document.getElementById(id);
                    while (tp.children.length > 0) {
                        tp.children[0].remove();
                    }
                }
            }

            var ff2 = function () {
                var divTaskOperatingPanel = document.createElement('div');
                divTaskOperatingPanel.id = 'taskOperatingPanel';

                divTaskOperatingPanel.style.position = 'absolute';
                divTaskOperatingPanel.style.zIndex = '7';
                divTaskOperatingPanel.style.right = '20px';
                divTaskOperatingPanel.style.border = 'none';
                divTaskOperatingPanel.style.width = '5em';
                divTaskOperatingPanel.style.color = 'green';
                //每个子对象1.3em 8px 共2.5个
                divTaskOperatingPanel.style.top = 'calc(50% - 3.25em - 20px)';

                var addItemToTaskOperatingPanle = function (btnName, id, clickF) {
                    var div = document.createElement('div');
                    div.style.width = 'calc(5em - 4px)';
                    div.style.textAlign = 'center';
                    div.style.border = '2px inset #ffc403';
                    div.style.borderRadius = '0.3em';
                    div.style.marginTop = '4px';
                    div.style.marginBottom = '4px';
                    div.style.background = 'rgba(0, 191, 255, 0.6)';
                    div.style.height = '1.3em';
                    div.id = id;

                    var span = document.createElement('span');
                    span.innerText = btnName;
                    div.appendChild(span);
                    //  <span id="carASpan">提升续航</span>

                    div.onclick = function () { clickF(); }
                    divTaskOperatingPanel.appendChild(div);
                }

                addItemToTaskOperatingPanle('宝石', 'useGetDiamondBtn', function () {


                    var tmp_S1 = arguments;
                    clearBtnOfObj('taskOperatingPanel');
                    addItemToTaskOperatingPanle('购买', 'buyDiamondBtn', function () {

                        objMain.ws.send(JSON.stringify({ 'c': 'BuyDiamond', 'pType': objMain.selectObj.obj.userData.index }));
                    });
                    addItemToTaskOperatingPanle('出售', 'sellDiamondBtn', function () {
                        objMain.ws.send(JSON.stringify({ 'c': 'SellDiamond', 'pType': objMain.selectObj.obj.userData.index }));
                    });
                    addItemToTaskOperatingPanle('取消', 'cancleBtn', function () {
                        marketOperate.state = '';
                        marketOperate.refresh();

                    });



                    /*------*/

                });

                document.body.appendChild(divTaskOperatingPanel);
            }

            ff2();
        }

    },
    state: ''
};

var operatePanel =
{
    clearPanel: function () {
        while (document.getElementById('taskOperatingPanel') != null) {
            document.getElementById('taskOperatingPanel').remove();
        }
    },
    refresh: function () {
        var addParent = function () {
            var divTaskOperatingPanel = document.createElement('div');
            divTaskOperatingPanel.id = 'taskOperatingPanel';

            divTaskOperatingPanel.style.position = 'absolute';
            divTaskOperatingPanel.style.zIndex = '7';
            divTaskOperatingPanel.style.right = '20px';
            divTaskOperatingPanel.style.border = 'none';
            divTaskOperatingPanel.style.width = '5em';
            divTaskOperatingPanel.style.color = 'green';
            //根据底部计算所得
            divTaskOperatingPanel.style.bottom = 'calc(2.5em + 14px)';
            document.body.appendChild(divTaskOperatingPanel);
            return divTaskOperatingPanel;
        };
        operatePanel.clearPanel();
        operatePanel.divTaskOperatingPanel = addParent();
        var clearBtnOfObj = function (id) {
            if (document.getElementById(id) == null) { }
            else {
                var tp = document.getElementById(id);
                while (tp.children.length > 0) {
                    tp.children[0].remove();
                }
            }
        }



        var addItemToTaskOperatingPanle = function (btnName, id, clickF) {
            var div = document.createElement('div');
            div.style.width = 'calc(5em - 4px)';
            div.style.textAlign = 'center';
            div.style.border = '2px inset #ffc403';
            div.style.borderRadius = '0.3em';
            div.style.marginTop = '4px';
            div.style.marginBottom = '4px';
            div.style.background = 'rgba(0, 191, 255, 0.6)';
            div.style.height = '1.3em';
            div.style.padding = '0.5em 0.1em 0.5em 0.1em';
            div.style.color = 'white';
            div.id = id;

            var span = document.createElement('span');
            span.innerText = btnName;
            div.appendChild(span);
            //  <span id="carASpan">提升续航</span>

            div.onclick = function () { clickF(); }
            div.classList.add('costomButton');
            operatePanel.divTaskOperatingPanel.appendChild(div);
        }

        var addItemToTaskOperatingPanle2 = function (bgRc, id, clickF, objState) {
            var div = document.createElement('div');
            div.style.width = 'calc(5em - 4px)';
            div.style.textAlign = 'center';
            div.style.border = '2px inset #ffc403';
            div.style.borderRadius = '0.3em';
            div.style.marginTop = '4px';
            div.style.marginBottom = '4px';
            div.style.background = 'rgba(0, 191, 255, 0.6)';
            div.style.height = 'calc(3.09em - 2.47px)';

            switch (objState) {
                case 0:
                    {
                        div.style.backgroundImage = 'url("' + bgRc + '")';
                        div.onclick = function () {
                            clickF();
                        }
                    }; break;
                case 1:
                    {
                        div.style.backgroundImage = '';
                    }; break;
                case 2:
                    {
                        div.style.backgroundImage = 'url("Pic/crossimg/wrong.png")';
                    }; break;
            }
            //if (objHasBeenSelected) {
            //    div.style.backgroundImage = 'url("Pic/crossimg/wrong.png")';
            //}
            //else {

            //}
            div.style.backgroundSize = 'auto calc(1.5em - 1.2px)';
            div.style.backgroundPosition = 'center center';
            div.style.backgroundRepeat = 'no-repeat';
            div.id = id;
            //if (objHasBeenSelected) {
            //}
            //else {

            //}

            div.classList.add('costomButton');
            operatePanel.divTaskOperatingPanel.appendChild(div);
        }


        var seleModel = function (choseObjFInput) {
            operatePanel.clearPanel();
            operatePanel.divTaskOperatingPanel = addParent();

            addItemToTaskOperatingPanle('0精', 'cancelBuildingDetailF', function () {
                placeObj.model = 0;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('1xM', 'cancelBuildingDetailF', function () {
                placeObj.model = 1;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('2zM', 'cancelBuildingDetailF', function () {
                placeObj.model = 2;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('3yM', 'cancelBuildingDetailF', function () {
                placeObj.model = 3;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('4xR', 'cancelBuildingDetailF', function () {
                placeObj.model = 4;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('5zR', 'cancelBuildingDetailF', function () {
                placeObj.model = 5;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('6yR', 'cancelBuildingDetailF', function () {
                placeObj.model = 6;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('7s', 'cancelBuildingDetailF', function () {
                placeObj.model = 7;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('8O', 'cancelBuildingDetailF', function () {
                choseObjFInput();
            });
            addItemToTaskOperatingPanle('9SAndL', 'cancelBuildingDetailF', function () {
                placeObj.model = 9;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('BACK', 'cancelBuildingDetailF', function () {
                operatePanel.refresh();
            });
        };
        var administratorOperator = function (seleModelFInput, choseObjFInput) {
            addItemToTaskOperatingPanle('Key_Z', 'cancelBuildingDetailF', function () {
                try {
                    objMain.background.keyZ();
                }
                catch (e) { }
            });
            addItemToTaskOperatingPanle('Key_X', 'cancelBuildingDetailF', function () {
                try {
                    objMain.background.keyX();
                }
                catch (e) { }
            });
            addItemToTaskOperatingPanle('Key_C', 'cancelBuildingDetailF', function () {
                try {
                    if (confirm('确定保存转角？')) {
                        objMain.background.keyPassToOrigin();
                    }
                }
                catch (e) { }
            });
            addItemToTaskOperatingPanle('Key_A', 'cancelBuildingDetailF', function () {
                //  placeObj.changeModelAKey();
                seleModelFInput(choseObjFInput);
            });
            addItemToTaskOperatingPanle('Key_S', 'cancelBuildingDetailF', function () {
                placeObj.addSkey();
            });
            addItemToTaskOperatingPanle('Key_D', 'cancelBuildingDetailF', function () {
                placeObj.minusDkey();
            });
            addItemToTaskOperatingPanle('Key_F', 'cancelBuildingDetailF', function () {
                if (confirm('确定保存方位？'))
                    placeObj.uploadKeyF();
            });
        };
        var choseObj = function () {
            operatePanel.clearPanel();
            operatePanel.divTaskOperatingPanel = addParent();//  addParent();

            addItemToTaskOperatingPanle('元宝', 'cancelBuildingDetailF', function () {
                placeObj.operateIndex = 0;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('卫星', 'cancelBuildingDetailF', function () {
                placeObj.operateIndex = 1;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('风车', 'cancelBuildingDetailF', function () {
                placeObj.operateIndex = 2;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('指南针', 'cancelBuildingDetailF', function () {
                placeObj.operateIndex = 3;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('BTC', 'cancelBuildingDetailF', function () {
                placeObj.operateIndex = 4;
                operatePanel.refresh();
            });
            addItemToTaskOperatingPanle('BACK', 'cancelBuildingDetailF', function () {
                operatePanel.refresh();
            });
        }

        administratorOperator(seleModel, choseObj);
    },
    divTaskOperatingPanel: null
};
var ModelOperateF =
{
    f: function (received_obj, config) {
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager)
            .loadTextOnly(received_obj.Mtl, 'data:image/jpeg;base64,' + received_obj.Img, function (materials) {
                materials.preload();
                new THREE.OBJLoader(manager)
                    .setMaterials(materials)
                    //.setPath('/Pic/')
                    .loadTextOnly(received_obj.Obj, function (object) {
                        //console.log('o', object);
                        //for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                        //    if (object.children[iOfO].isMesh) {
                        //        for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                        //            object.children[iOfO].material[mi].transparent = true;
                        //            object.children[iOfO].material[mi].opacity = 1;
                        //            object.children[iOfO].material[mi].side = THREE.FrontSide;
                        //            object.children[iOfO].material[mi].color = new THREE.Color(0.45, 0.45, 0.45);
                        //        }
                        //    }
                        //}
                        //console.log('o', object);
                        if (config.scale == undefined)
                            object.scale.set(0.003, 0.003, 0.003);
                        else
                            object.scale.set(config.scale.x, config.scale.y, config.scale.z);
                        if (config.rotateX == undefined) { }
                        else {
                            object.rotateX(config.rotateX);
                        }
                        // object.rotateX(-Math.PI / 2);
                        //  objMain.Model
                        // objMain.rmbModel[field] = object;
                        if (config.transparent != undefined) {
                            for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                                if (object.children[iOfO].isMesh) {
                                    if (object.children[iOfO].material.isMaterial) {
                                        object.children[iOfO].material.transparent = true;
                                        object.children[iOfO].material.opacity = config.transparent.opacity;
                                    }
                                    else
                                        for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                                            object.children[iOfO].material[mi].transparent = true;
                                            object.children[iOfO].material[mi].opacity = config.transparent.opacity;
                                        }
                                }
                            }
                        }
                        if (config.color != undefined) {
                            for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
                                if (object.children[iOfO].isMesh) {
                                    for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
                                        object.children[iOfO].material[mi].color = new THREE.Color(config.color.r, config.color.g, config.color.b);
                                    }
                                }
                            }
                        }
                        if (config.rotate != undefined) {
                            object.rotateX(config.rotate.x);
                            object.rotateX(config.rotate.y);
                            object.rotateX(config.rotate.z);
                        }
                        if (config.bind != undefined) {
                            config.bind(object);
                        }



                    }, function () { }, function () { });
            });
    }
};

var stateSet =
{
    speed:
    {
        add: function (carId) {
            for (var i = 0; i < 2; i++) {
                var meshCopy = objMain.ModelInput.speed.children[0].clone();
                meshCopy.name = 'fire' + i.toString() + '_' + carId;
                meshCopy.position.x = 77;//97.11
                meshCopy.position.z = 4 - i * 18;
                meshCopy.rotateY(Math.PI / 2 * 3);
                meshCopy.scale.z = 2;//3
                var car = objMain.carGroup.getObjectByName('car_' + carId);
                if (car)
                    if (!car.getObjectByName(meshCopy.name))
                        car.add(meshCopy);
            }
        },
        Animate: function (carId) {
            //input Is group
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car)
                for (var i = 0; i < 2; i++) {
                    var name = 'fire' + i.toString() + '_' + carId;
                    var fire = car.getObjectByName(name);
                    if (fire) {
                        var percent = (Date.now() % 500) / 500;
                        fire.position.x = 77 + (97.11 - 77) * percent;
                        fire.scale.z = 2 + (3 - 2) * percent;//3
                    }
                }
        },
        clear: function (carId) {
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car)
                for (var i = 0; i < 2; i++) {
                    var name = 'fire' + i.toString() + '_' + carId;
                    var fire = car.getObjectByName(name);
                    if (fire) {
                        car.remove(fire);
                    }
                }
        },
        selfHasFire: function () {
            var car = objMain.carGroup.getObjectByName('car_' + objMain.indexKey);
            var fire0 = car.getObjectByName('fire0_' + objMain.indexKey);
            if (fire0 == undefined || fire == null) {
                return false;
            }
            else return true;
        }
    },
    attck:
    {
        add: function (carId) {
            {

                var meshCopy = objMain.ModelInput.doubleRewardIcon.clone();
                meshCopy.name = 'fist_' + carId;
                meshCopy.position.x = 0;//97.11
                meshCopy.position.y = 0.15;
                meshCopy.position.z = 0;
                //  meshCopy.rotateX(Math.PI / 2);
                //meshCopy.rotateY(Math.PI);
                meshCopy.scale.set(0.008, 0.008, 0.008);// = 2;//3
                var car = objMain.carGroup.getObjectByName('vehicle_' + carId);
                if (car)
                    if (!car.getObjectByName(meshCopy.name))
                        car.add(meshCopy);
            }
        },
        Animate: function (carId) {
            //input Is group
            var car = objMain.carGroup.getObjectByName('vehicle_' + carId);
            if (car) {
                var name = 'fist_' + carId;
                var fist = car.getObjectByName(name);
                if (fist) {
                    fist.rotation.y = Date.now() % 1000 / 1000 * Math.PI * 2;
                }
            }
        },
        clear: function (carId) {
            var car = objMain.carGroup.getObjectByName('vehicle_' + carId);
            if (car) {
                var name = 'fist_' + carId;
                var fist = car.getObjectByName(name);
                if (fist) {
                    car.remove(fist);
                }
            }
        },
    },
    defend:
    {
        add: function (carId) {
            {
                var flag = objMain.playerGroup.getObjectByName('flag_' + carId);
                if (flag) {
                    var O3d = new THREE.Object3D();
                    O3d.name = 'defender_' + carId;
                    O3d.position.set(flag.position.x, flag.position.y, flag.position.z);

                    for (var i = 0; i < 3; i++) {
                        var meshCopy = objMain.ModelInput.shield.children[0].clone();
                        meshCopy.name = 'shield' + i.toString() + '_' + carId;
                        meshCopy.position.x = 0.8 * Math.cos(i * 2 * Math.PI / 3);//97.11
                        meshCopy.position.y = 0.5;
                        meshCopy.position.z = 0.8 * Math.sin(i * 2 * Math.PI / 3);
                        meshCopy.scale.set(0.003, 0.003, 0.003);
                        meshCopy.rotateX(Math.PI / 2);
                        meshCopy.rotateZ(-Math.PI / 2 + i * 2 * Math.PI / 3);
                        O3d.add(meshCopy);
                    }
                    if (!objMain.shieldGroup.getObjectByName(O3d.name))
                        objMain.shieldGroup.add(O3d);
                }
            }
        },
        Animate: function (carId) {
            //input Is group
            var O3dname = 'defender_' + carId;

            var O3d = objMain.shieldGroup.getObjectByName(O3dname);
            if (O3d) {
                O3d.rotation.y = (Math.sin((Date.now() % 2000) / 2000 * Math.PI * 2) + 1) * Math.PI;
            }
        },
        clear: function (carId) {
            var O3dname = 'defender_' + carId;
            var O3d = objMain.shieldGroup.getObjectByName(O3dname);
            if (O3d) {
                var length = O3d.children.length;
                for (var i = length - 1; i >= 0; i--) {
                    O3d.remove(O3d.children[i]);
                }
                objMain.shieldGroup.remove(O3d);
            }
        }
    },
    confusePrepare:
    {
        add: function (carId, animateData) {
            {
                //if (flag)
                // var animateData = { startX: objMain.controls.target.x, startY: objMain.controls.target.y, startZ: objMain.controls.target.z, start: Date.now(), endX: objMain.controls.target.x + 5, endY: objMain.controls.target.y, endZ: objMain.controls.target.z }
                {
                    var O3d = new THREE.Object3D();
                    O3d.name = 'confusePrepare_' + carId;
                    O3d.position.set(animateData.startX, animateData.startY, animateData.startZ);

                    {
                        var meshCopy = objMain.ModelInput.confusePrepare.children[0].clone();
                        meshCopy.name = 'confusePrepareChild_' + carId;
                        meshCopy.position.x = 0;//97.11
                        meshCopy.position.y = 0;
                        meshCopy.position.z = 0;
                        meshCopy.scale.set(0.05, 0.05, 0.05);
                        meshCopy.rotateX(Math.PI / 2);
                        //meshCopy.rotateZ(-Math.PI / 2 + i * 2 * Math.PI / 3);
                        O3d.add(meshCopy);
                    }
                    O3d.userData.animateData = animateData;
                    if (!objMain.confusePrepareGroup.getObjectByName(O3d.name))
                        objMain.confusePrepareGroup.add(O3d);
                }
            }
        },
        Animate: function (carId) {
            //input Is group
            var O3dname = 'confusePrepare_' + carId;

            var O3d = objMain.confusePrepareGroup.getObjectByName(O3dname);
            if (O3d) {
                var userData = O3d.userData.animateData;
                var percent = ((Date.now() - userData.start) % 3000) / 3000;
                var positionX = userData.startX + percent * (userData.endX - userData.startX);
                var positionY = userData.startY + percent * (userData.endY - userData.startY);
                var positionZ = userData.startZ + percent * (userData.endZ - userData.startZ);
                O3d.position.set(positionX, positionY, positionZ);

                O3d.children[0].position.set(0.1 * Math.sin(percent * 20), 0.06 * Math.sin(percent * 10), 0.08 * Math.sin(percent * 15));
                O3d.children[0].rotation.set((percent * 1.7 % 1) * Math.PI * 2, (percent * 1.1 % 1) * Math.PI * 2, (percent * 1.3 % 1) * Math.PI * 2)

            }
        },
        clear: function (carId) {
            var O3dname = 'confusePrepare_' + carId;
            var O3d = objMain.confusePrepareGroup.getObjectByName(O3dname);
            if (O3d) {
                var length = O3d.children.length;
                for (var i = length - 1; i >= 0; i--) {
                    O3d.remove(O3d.children[i]);
                }
                objMain.confusePrepareGroup.remove(O3d);
            }
        }
    },
    confuse:
    {
        add: function (carId) {
            var meshCopy = objMain.ModelInput.confusePrepare.children[0].clone();
            meshCopy.name = 'confuse' + '_' + carId;
            meshCopy.position.y = 18;
            meshCopy.scale.set(5, 5, 5);
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car)
                if (!car.getObjectByName(meshCopy.name))
                    car.add(meshCopy);
        },
        Animate: function (carId) {
            //input Is group
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car) {
                var name = 'confuse' + '_' + carId;
                var confuseTag = car.getObjectByName(name);
                if (confuseTag) {
                    var t = (Date.now() % 1500 - 750) / 750;
                    var deltaT = (Date.now() % 10000) / 10000;
                    confuseTag.rotation.set(0, Math.abs(t * Math.PI) + deltaT * 2 * Math.PI, 0, 'XYZ')
                }
            }
        },
        clear: function (carId) {
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car) {
                var name = 'confuse' + '_' + carId;
                var confuseTag = car.getObjectByName(name);
                if (confuseTag) {
                    car.remove(confuseTag);
                }
            }
        }
    },
    lostPrepare:
    {
        add: function (carId, animateData) {
            {
                //if (flag)
                // var animateData = { startX: objMain.controls.target.x, startY: objMain.controls.target.y, startZ: objMain.controls.target.z, start: Date.now(), endX: objMain.controls.target.x + 5, endY: objMain.controls.target.y, endZ: objMain.controls.target.z }
                {
                    var O3d = new THREE.Object3D();
                    O3d.name = 'lostPrepare_' + carId;
                    O3d.position.set(animateData.startX, animateData.startY, animateData.startZ);

                    {
                        var meshCopy = objMain.ModelInput.lostPrepare.children[0].clone();
                        meshCopy.name = 'lostPrepareChild_' + carId;
                        meshCopy.position.x = 0;//97.11
                        meshCopy.position.y = 0;
                        meshCopy.position.z = 0.0;
                        meshCopy.scale.set(0.015, 0.015, 0.015);
                        //meshCopy.rotateX(Math.PI / 2);
                        meshCopy.rotateZ(Math.PI / 2);
                        //meshCopy.rotateZ(-Math.PI / 2 + i * 2 * Math.PI / 3);
                        O3d.add(meshCopy);
                    }
                    O3d.userData.animateData = animateData;
                    if (!objMain.lostPrepareGroup.getObjectByName(O3d.name))
                        objMain.lostPrepareGroup.add(O3d);
                }
            }
        },
        Animate: function (carId) {
            //input Is group
            var O3dname = 'lostPrepare_' + carId;

            var O3d = objMain.lostPrepareGroup.getObjectByName(O3dname);
            if (O3d) {
                var userData = O3d.userData.animateData;
                var percent = ((Date.now() - userData.start) % 3000) / 3000;
                var positionX = userData.startX + percent * (userData.endX - userData.startX);
                var positionY = userData.startY + percent * (userData.endY - userData.startY);
                var positionZ = userData.startZ + percent * (userData.endZ - userData.startZ);
                O3d.position.set(positionX, positionY, positionZ);

                //O3d.children[0].position.set(0.1 * Math.sin(percent * 20), 0.06 * Math.sin(percent * 10), 0.08 * Math.sin(percent * 15));
                O3d.children[0].rotation.set((percent * 2 % 1) * Math.PI * 2, 0, Math.PI / 2)

            }
        },
        clear: function (carId) {
            var O3dname = 'lostPrepare_' + carId;
            var O3d = objMain.lostPrepareGroup.getObjectByName(O3dname);
            if (O3d) {
                var length = O3d.children.length;
                for (var i = length - 1; i >= 0; i--) {
                    O3d.remove(O3d.children[i]);
                }
                objMain.lostPrepareGroup.remove(O3d);
            }
        }
    },
    lost:
    {
        add: function (carId) {
            var meshCopy = objMain.ModelInput.lostPrepare.children[0].clone();
            meshCopy.name = 'lost' + '_' + carId;
            meshCopy.position.y = 35;
            meshCopy.scale.set(1, 1, 1);
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car)
                if (!car.getObjectByName(meshCopy.name))
                    car.add(meshCopy);
        },
        Animate: function (carId) {
            //input Is group
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car) {
                var name = 'lost' + '_' + carId;;
                var loseTag = car.getObjectByName(name);
                if (loseTag) {
                    var t = (Date.now() % 2500) / 2500;
                    loseTag.rotation.set(0, t * 2 * Math.PI, 0, 'XYZ');
                }
            }
        },
        clear: function (carId) {
            var car = objMain.carGroup.getObjectByName('car_' + carId);
            if (car) {
                var name = 'lost' + '_' + carId;;
                var loseTag = car.getObjectByName(name);
                if (loseTag) {
                    car.remove(loseTag);
                }
            }
        }
    },
    ambusePrepare:
    {
        add: function (carId, animateData) {
            {
                //if (flag)
                // var animateData = { startX: objMain.controls.target.x, startY: objMain.controls.target.y, startZ: objMain.controls.target.z, start: Date.now(), endX: objMain.controls.target.x + 5, endY: objMain.controls.target.y, endZ: objMain.controls.target.z }
                {
                    var O3d = new THREE.Object3D();
                    O3d.name = 'ambusePrepare_' + carId;
                    O3d.position.set(animateData.startX, animateData.startY, animateData.startZ);

                    {
                        var meshCopy = objMain.ModelInput.ambushPrepare.children[0].clone();
                        meshCopy.name = 'ambusePrepareChild_' + carId;
                        meshCopy.position.x = 0;//97.11
                        meshCopy.position.y = 0;
                        meshCopy.position.z = 0;
                        meshCopy.scale.set(0.005, 0.005, 0.005);
                        //meshCopy.rotateX(Math.PI / 2);
                        //meshCopy.rotateZ(-Math.PI / 2 + i * 2 * Math.PI / 3);
                        O3d.add(meshCopy);
                    }
                    O3d.userData.animateData = animateData;
                    if (!objMain.ambushPrepareGroup.getObjectByName(O3d.name)) {
                        objMain.ambushPrepareGroup.add(O3d);
                    }
                }
            }
        },
        Animate: function (carId) {
            //input Is group
            var O3dname = 'ambusePrepare_' + carId;

            var O3d = objMain.ambushPrepareGroup.getObjectByName(O3dname);
            if (O3d) {
                var userData = O3d.userData.animateData;
                var percent = ((Date.now() - userData.start) % 3000) / 3000;
                var positionX = userData.startX + percent * (userData.endX - userData.startX);
                var positionY = userData.startY + percent * (userData.endY - userData.startY);
                var positionZ = userData.startZ + percent * (userData.endZ - userData.startZ);
                O3d.position.set(positionX, positionY, positionZ);

                // O3d.children[0].position.set(0.1 * Math.sin(percent * 20), 0.06 * Math.sin(percent * 10), 0.08 * Math.sin(percent * 15));
                //O3d.children[0].rotation.set((percent * 1.7 % 1) * Math.PI * 2, (percent * 1.1 % 1) * Math.PI * 2, (percent * 1.3 % 1) * Math.PI * 2)

            }
        },
        clear: function (carId) {
            var O3dname = 'ambusePrepare_' + carId;
            var O3d = objMain.ambushPrepareGroup.getObjectByName(O3dname);
            if (O3d) {
                var length = O3d.children.length;
                for (var i = length - 1; i >= 0; i--) {
                    O3d.remove(O3d.children[i]);
                }
                objMain.ambushPrepareGroup.remove(O3d);
            }
            //var car = objMain.carGroup.getObjectByName('car_' + carId);
            //if (car) {
            //    var name = 'fist_' + carId;
            //    var fist = car.getObjectByName(name);
            //    if (fist) {
            //        car.remove(fist);
            //    }
            //}
        }
    },
    control:
    {
        clear: function (carId) {
            stateSet.confusePrepare.clear(carId);
            stateSet.lostPrepare.clear(carId);
            stateSet.ambusePrepare.clear(carId);
        }
    },
    water:
    {
        add: function (targetRoleID, actionRole) {
            {
                this.clear(actionRole);
                let targetCar = objMain.carGroup.getObjectByName('car_' + targetRoleID);
                if (targetCar) {
                    //lightningStrikeMesh.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                    //lightningStrikeMesh.userData = { startT: Date.now(), targetRoleID: targetRoleID };
                    //objMain.lightningGroup.add(lightningStrikeMesh);
                    //particleFireMesh1.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                    //particleFireMesh1.name = 'fire_' + actionRole;
                    //particleFireMesh1.userData = { startT: Date.now(), targetRoleID: targetRoleID };
                    //objMain.fireGroup.add(particleFireMesh1);
                }

                var flag = objMain.playerGroup.getObjectByName('flag_' + targetRoleID);
                if (flag) {
                    //var O3d = new THREE.Object3D();

                    // O3d.position.set(flag.position.x, flag.position.y, flag.position.z);
                    var waterCopy = objMain.ModelInput.water.clone();

                    for (var i = 0; i < waterCopy.children.length; i++) {
                        waterCopy.children[i].material.transparent = true;
                        waterCopy.children[i].material.opacity = 0.6;
                    }
                    waterCopy.name = 'water_' + actionRole;
                    waterCopy.position.set(flag.position.x, flag.position.y, flag.position.z);
                    waterCopy.scale.set(0.15, 0.5, 0.15);
                    waterCopy.userData = { startT: Date.now() };
                    objMain.waterGroup.add(waterCopy);
                }
            }
        },
        Animate: function (actionRole) {
            var name = 'water_' + actionRole;
            var water = objMain.waterGroup.getObjectByName(name);
            if (water) {
                var percent = (Date.now() - water.userData.startT) / 10000;
                if (percent < 1) {
                    //var secondPercent = 
                    var shuilang = water.getObjectByName('shuilang');
                    shuilang.scale.set(1, 1 - percent, 1);
                    var shuizhu = water.getObjectByName('shuizhu');
                    shuizhu.scale.set(1 + Math.pow(percent * 10, 0.66), 1, 1 + Math.pow(percent * 10, 0.66));
                }
                else {
                    objMain.waterGroup.remove(water);
                }
            }
        },
        clear: function (actionRole) {
            var name = 'water_' + actionRole;
            var water = objMain.waterGroup.getObjectByName(name);
            if (water) {
                objMain.waterGroup.remove(water);
            }
        },
        mark:
        {
            material: new THREE.LineBasicMaterial({ color: 0x0000ff }),
            add: function (pointsArray) {
                //  this.clear(actionRole);
                const points = [];
                points.push(new THREE.Vector3(pointsArray[0], 0, -pointsArray[1]));
                points.push(new THREE.Vector3(pointsArray[2], 0, -pointsArray[3]));
                points.push(new THREE.Vector3(pointsArray[4], 0, -pointsArray[5]));
                var geometry = new THREE.BufferGeometry().setFromPoints(points);
                var line = new THREE.Line(geometry, this.material);
                line.userData = { startT: Date.now() };
                objMain.waterMarkGroup.add(line);
            },
            Animate: function () {
                if (Date.now() % 1000 > 500) {
                    this.material.color.r = 0;
                    this.material.color.g = 1;
                    this.material.color.b = 0.45;
                }
                else {
                    this.material.color.r = 0.1;
                    this.material.color.g = 0.45;
                    this.material.color.b = 1;
                }
                var group = objMain.waterMarkGroup;
                var startIndex = group.children.length - 1;
                for (var i = startIndex; i >= 0; i--) {
                    if (Date.now() - group.children[i].userData.startT > 20000)
                        group.remove(group.children[i]);
                }
            }
        }
    },
    fire:
    {
        particleCount: 400,
        add: function (targetRoleID, actionRole) {
            this.clear(actionRole);
            var particleFire = new fire();
            particleFire.install({ THREE: THREE });

            var geometry1 = new particleFire.Geometry(10, 35, this.particleCount);
            var material1 = new particleFire.Material({ color: 0x800080 });
            var height = window.innerHeight;
            material1.setPerspective(objMain.camera.fov, height);
            var particleFireMesh1 = new THREE.Points(geometry1, material1);

            let targetCar = objMain.carGroup.getObjectByName('car_' + targetRoleID);
            if (targetCar) {
                //lightningStrikeMesh.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                //lightningStrikeMesh.userData = { startT: Date.now(), targetRoleID: targetRoleID };
                //objMain.lightningGroup.add(lightningStrikeMesh);
                particleFireMesh1.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                particleFireMesh1.name = 'fire_' + actionRole;
                particleFireMesh1.userData = { startT: Date.now(), targetRoleID: targetRoleID };
                objMain.fireGroup.add(particleFireMesh1);
            }
            if (false) {
                var flag = objMain.playerGroup.getObjectByName('flag_' + targetRoleID);
                if (flag) {
                    //  mesh.position.set(flag.position.x, 0, flag.position.z);
                    particleFireMesh1.position.set(flag.position.x, flag.position.y, flag.position.z);
                    particleFireMesh1.name = 'fire_' + actionRole;
                    particleFireMesh1.userData = { startT: Date.now() };
                    objMain.fireGroup.add(particleFireMesh1);
                }
            }
        },
        Animate: function (actionRole) {
            var name = 'fire_' + actionRole;
            var fire = objMain.fireGroup.getObjectByName(name);
            if (fire) {
                fire.material.update(Date.now() % 2000 / 2000 * 20);
                //particleFireMesh1.material.update(delta);
                //particleFireMesh2.material.update(delta);
                var percent = (Date.now() - fire.userData.startT) / 10000;
                if (percent < 1) {
                    var car = objMain.carGroup.getObjectByName('car_' + actionRole);
                    if (car) {
                        var deltaX = car.position.x - fire.position.x;
                        var deltaZ = car.position.z - fire.position.z;

                        for (let i = this.particleCount - this.particleCount / 4; i < this.particleCount; i++) {
                            var positionPercent = (i - (this.particleCount - this.particleCount / 4)) / (this.particleCount / 4);

                            fire.geometry.attributes.position.array[i * 3 + 0] = positionPercent * deltaX;
                            fire.geometry.attributes.position.array[i * 3 + 1] = (positionPercent * positionPercent - positionPercent) * -100;
                            fire.geometry.attributes.position.array[i * 3 + 2] = positionPercent * deltaZ;

                        }
                        fire.geometry.attributes.position.needsUpdate = true;

                        var targetRoleID = fire.userData.targetRoleID;
                        let targetCar = objMain.carGroup.getObjectByName('car_' + targetRoleID);
                        if (targetCar) {
                            fire.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                        }
                    }
                }
                else {
                    fire.material.dispose();
                    fire.geometry.dispose();
                    objMain.fireGroup.remove(fire);
                }
            }
        },
        clear: function (actionRole) {
            var name = 'fire_' + actionRole;
            var fire = objMain.fireGroup.getObjectByName(name);
            if (fire) {
                fire.material.dispose();
                fire.geometry.dispose();
                objMain.fireGroup.remove(fire);
            }
        },
        mark:
        {
            material: new THREE.LineBasicMaterial({ color: 0x090f10 }),
            add: function (pointsArray) {
                //  this.clear(actionRole);
                const points = [];
                for (var i = 0; i < pointsArray.length; i += 2) {
                    points.push(new THREE.Vector3(pointsArray[i], 0, -pointsArray[i + 1]));
                }
                var geometry = new THREE.BufferGeometry().setFromPoints(points);
                var line = new THREE.Line(geometry, this.material);
                line.userData = { startT: Date.now() };
                objMain.fireMarkGroup.add(line);
            },
            Animate: function () {
                if (Date.now() % 1000 > 500) {
                    this.material.color.r = 2;
                    this.material.color.g = 0.4;
                    this.material.color.b = 0.3;
                }
                else {
                    this.material.color.r = 1.2;
                    this.material.color.g = 1.2;
                    this.material.color.b = 0.3;
                }
                var group = objMain.fireMarkGroup;
                var startIndex = group.children.length - 1;
                for (var i = startIndex; i >= 0; i--) {
                    if (Date.now() - group.children[i].userData.startT > 20000)
                        group.remove(group.children[i]);

                }
            }
        }
    },
    lightning:
    {
        add: function (targetRoleID, actionRole) {
            this.clear(actionRole);
            let rayParams = {

                sourceOffset: new THREE.Vector3(),
                destOffset: new THREE.Vector3(),
                radius0: 0.07,
                radius1: 0.08,
                minRadius: 0.05,
                maxIterations: 7,
                isEternal: true,

                timeScale: 0.7,

                propagationTimeFactor: 0.05,
                vanishingTimeFactor: 0.95,
                subrayPeriod: 3.5,
                subrayDutyCycle: 0.6,
                maxSubrayRecursion: 3,
                ramification: 7,
                recursionProbability: 0.6,

                roughness: 0.85,
                straightness: 0.8

            };
            let lightningStrike = new THREE.LightningStrike(rayParams);

            // r=176 g=15 b=254
            let lightningMaterial = new THREE.MeshBasicMaterial({ color: new THREE.Color(0xb0fffe) });
            let lightningStrikeMesh = new THREE.Mesh(lightningStrike, lightningMaterial);
            lightningStrikeMesh.name = 'lightning_' + actionRole;

            //let flag = objMain.playerGroup.getObjectByName('flag_' + targetRoleID);
            //if (flag) {
            //    lightningStrikeMesh.position.set(flag.position.x, flag.position.y, flag.position.z);
            //    lightningStrikeMesh.userData = { startT: Date.now() };
            //    objMain.lightningGroup.add(lightningStrikeMesh);
            //}
            let targetCar = objMain.carGroup.getObjectByName('car_' + targetRoleID);
            if (targetCar) {
                lightningStrikeMesh.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                lightningStrikeMesh.userData = { startT: Date.now(), targetRoleID: targetRoleID };
                objMain.lightningGroup.add(lightningStrikeMesh);
            }
        },
        Animate: function (actionRole) {
            let name = 'lightning_' + actionRole;
            var lightningStrikeMesh = objMain.lightningGroup.getObjectByName(name);
            if (lightningStrikeMesh) {
                var percent = (Date.now() - lightningStrikeMesh.userData.startT) / 15000;
                if (percent < 1) {
                    //objMain.carGroup.getObjectByName('car_'+objMain.indexKey).position
                    var car = objMain.carGroup.getObjectByName('car_' + actionRole);
                    var deltaX = car.position.x - lightningStrikeMesh.position.x;
                    var deltaZ = car.position.z - lightningStrikeMesh.position.z;

                    lightningStrikeMesh.geometry.rayParameters.sourceOffset.copy(new THREE.Vector3(deltaX, 10, deltaZ));
                    lightningStrikeMesh.geometry.rayParameters.sourceOffset.y += 1;
                    lightningStrikeMesh.geometry.rayParameters.destOffset.copy(new THREE.Vector3(Math.sin(percent * Math.PI * 5) * 3.3, 0, Math.cos(percent * Math.PI * 1.5)));
                    lightningStrikeMesh.geometry.rayParameters.destOffset.y -= 1;
                    lightningStrikeMesh.geometry.update(Date.now() / 2000);

                    var targetRoleID = lightningStrikeMesh.userData.targetRoleID;
                    let targetCar = objMain.carGroup.getObjectByName('car_' + targetRoleID);
                    if (targetCar) {
                        lightningStrikeMesh.position.set(targetCar.position.x, targetCar.position.y, targetCar.position.z);
                    }
                }
                else {
                    lightningStrikeMesh.material.dispose();
                    lightningStrikeMesh.geometry.dispose();
                    objMain.lightningGroup.remove(lightningStrikeMesh);
                }
            }
        },
        clear: function (actionRole) {
            let name = 'lightning_' + actionRole;
            var lightningStrikeMesh = objMain.lightningGroup.getObjectByName(name);
            if (lightningStrikeMesh) {
                lightningStrikeMesh.material.dispose();
                lightningStrikeMesh.geometry.dispose();
                objMain.lightningGroup.remove(lightningStrikeMesh);
            }
        },
        mark:
        {
            material: new THREE.LineBasicMaterial({ color: 0xb0fffe }),
            add: function (pointsArray) {
                //  this.clear(actionRole);
                const points = [];
                points.push(new THREE.Vector3(pointsArray[0], 0, -pointsArray[1]));
                points.push(new THREE.Vector3(pointsArray[2], 0, -pointsArray[3]));
                points.push(new THREE.Vector3(pointsArray[4], 0, -pointsArray[5]));
                points.push(new THREE.Vector3(pointsArray[6], 0, -pointsArray[7]));
                points.push(new THREE.Vector3(pointsArray[0], 0, -pointsArray[1]));
                var geometry = new THREE.BufferGeometry().setFromPoints(points);
                var line = new THREE.Line(geometry, this.material);
                line.userData = { startT: Date.now() };
                objMain.lightningMarkGroup.add(line);
            },
            Animate: function () {
                if (Date.now() % 1000 > 500) {
                    this.material.color.r = 0.68;
                    this.material.color.g = 0.05859375;
                    this.material.color.b = 0.9921875;
                }
                else {
                    this.material.color.g = 2;
                    this.material.color.r = 2;
                    this.material.color.b = 1;
                }
                var group = objMain.lightningMarkGroup;
                var startIndex = group.children.length - 1;
                for (var i = startIndex; i >= 0; i--) {
                    if (Date.now() - group.children[i].userData.startT > 20000)
                        group.remove(group.children[i]);

                }
            }
        }
    },
    diamond: {
        add: function (roleID, diamondName) {
            if (diamondName == '') {
                this.clear(roleID, 'mile');
                // this.clear(roleID, 'business');
                this.clear(roleID, 'volume');
                // this.clear(roleID, 'speed');
            }
            else {
                if (diamondName == 'mile') {
                    var battery = objMain.ModelInput.battery.clone();
                    battery.name = 'vehicle_' + diamondName + '_' + roleID;
                    battery.name = 'vehicle_' + diamondName + '_' + roleID;
                    battery.position.set(0, 0, -0.14);
                    battery.scale.set(0.05, 0.05, 0.05);
                    battery.rotateX(Math.PI / 2);
                    var car = objMain.carGroup.getObjectByName('vehicle_' + roleID);
                    car.add(battery);
                    stateSet.diamond.updateHeight(roleID);
                }
                else if (diamondName == 'volume') {

                }
                if (false) {
                    var diamond = objMain.promoteDiamond.getObjectByName('diamond_' + diamondName);
                    if (diamond) {
                        var diamondOnCar = objMain.promoteDiamond.getObjectByName('diamond_' + diamondName).clone();
                        diamondOnCar.name = 'vehicle_' + diamondName + '_' + roleID;
                        diamondOnCar.position.set(9, 35, 0);
                        diamondOnCar.scale.set(10, 11, 10);
                        var car = objMain.carGroup.getObjectByName('car_' + roleID);
                        if (car)
                            if (!car.getObjectByName(diamondOnCar.name)) {
                                car.add(diamondOnCar);
                                if (roleID == objMain.indexKey) {
                                    stateSet.diamond.updateHeight(roleID);
                                }
                            }
                    }
                }
            }
        },
        clear: function (roleID, diamondName) {
            var car = objMain.carGroup.getObjectByName('vehicle_' + roleID);
            if (car) {
                var name = 'vehicle_' + diamondName + '_' + roleID;
                var diamondOnCar = car.getObjectByName(name);
                if (diamondOnCar) {
                    car.remove(diamondOnCar);
                }
            }
        },
        updateHeight: function (roleID) {

            var goldCount = function (roleID) {
                var result = 0;
                var car = objMain.carGroup.getObjectByName('vehicle_' + roleID);
                for (var i = 0; i < 10000; i++) {
                    var oIcon = car.getObjectByName('collectCoinIcon_' + i.toString());
                    if (oIcon) {
                        result++;
                    }
                    else {
                        break;
                    }
                }
                return result;
            }(roleID);
            var diamondNames = ['mile', 'volume'];
            for (var i = 0; i < diamondNames.length; i++) {
                var diamondName = diamondNames[i];
                var diamondOnCarName = 'vehicle_' + diamondName + '_' + roleID;
                var car = objMain.carGroup.getObjectByName('vehicle_' + roleID);
                if (car)
                    if (car.getObjectByName(diamondOnCarName)) {
                        var positionY = 0.13;
                        var singleDiamond = car.getObjectByName(diamondOnCarName);
                        singleDiamond.position.y = positionY - goldCount * 0.1;
                    }
            }
        }
    },
    clicktrail: null,
    nitrogeneffect:
    {
        add: function () {
            var car = objMain.carGroup.getObjectByName('car_' + objMain.indexKey);
            if (car) {
                objMain.ModelInput.NitrogenEffect.obj.position.set(34, 25, 0);
                objMain.ModelInput.NitrogenEffect.obj.scale.set(10, 10, 10);
                objMain.ModelInput.NitrogenEffect.obj.rotateZ(1.021017612416683);
                objMain.ModelInput.NitrogenEffect.obj.rotateY(Math.PI * 5 / 10);
                car.add(objMain.ModelInput.NitrogenEffect.obj);
                stateSet.nitrogeneffect.showVisible(0);
            }
        },
        showVisible: function (level) {
            var car = objMain.carGroup.getObjectByName('car_' + objMain.indexKey);
            if (car) {
                var oIcon = car.getObjectByName('selfnitrogenEffectIcon');
                if (oIcon) {
                    oIcon.getObjectByName('level1').visible = level > 0;
                    oIcon.getObjectByName('level2').visible = level > 1;
                    oIcon.getObjectByName('level3').visible = level > 2;
                    oIcon.getObjectByName('level4').visible = level > 3;
                    oIcon.getObjectByName('level5').visible = level > 4;
                    oIcon.getObjectByName('level6').visible = level > 5;
                    oIcon.getObjectByName('level7').visible = level > 6;
                    oIcon.getObjectByName('level8').visible = level > 7;
                    oIcon.getObjectByName('level9').visible = level > 8;
                    oIcon.getObjectByName('levelfull').visible = level > 9;
                }
            }
        }
    },
    coinIcon:
    {
        iconCount: 0,
        add: function (iconCount) {
            stateSet.coinIcon.iconCount = iconCount;
            var car = objMain.carGroup.getObjectByName('vehicle_' + objMain.indexKey);
            if (car) {
                stateSet.coinIcon.iconCount = iconCount;
                var positionX = 0;
                var positionY = 0.07;
                var positionZ = 0;
                for (var i = 0; i < 10000; i++) {
                    var oIcon = car.getObjectByName('collectCoinIcon_' + i.toString());
                    if (oIcon) {
                        car.remove(oIcon);
                    }
                    else {
                        break;
                    }
                }
                for (var i = 0; i < iconCount; i++) {
                    var objAdd = objMain.ModelInput.GoldIngotIcon.children[0].clone();
                    objAdd.name = 'collectCoinIcon_' + i.toString();
                    objAdd.scale.set(0.1, 0.1, 0.1);
                    objAdd.rotateY(Math.PI / 2);
                    objAdd.position.set(positionX, positionY - i * 0.1, positionZ);
                    car.add(objAdd);

                }
                stateSet.diamond.updateHeight(objMain.indexKey);
            }
        },
        addWithKey: function (operateKey, iconCount) {
            var car = objMain.carGroup.getObjectByName('vehicle_' + operateKey);
            if (car) {
                stateSet.coinIcon.iconCount = iconCount;
                var positionX = 13;
                var positionY = 32;
                var positionZ = 0;
                for (var i = 0; i < 10000; i++) {
                    var oIcon = car.getObjectByName('collectCoinIcon_' + operateKey + '_' + i.toString());
                    if (oIcon) {
                        car.remove(oIcon);
                    }
                    else {
                        break;
                    }
                }
                for (var i = 0; i < iconCount; i++) {
                    var objAdd = objMain.ModelInput.GoldIngotIcon.children[0];
                    objAdd.name = 'collectCoinIcon_' + operateKey + '_' + i.toString();
                    objAdd.scale.set(3, 3, 3);
                    objAdd.rotateX(Math.PI / 2);
                    objAdd.position.set(positionX, positionY + i * 4, positionZ);
                    car.add(objAdd);

                }
                stateSet.diamond.updateHeight(operateKey);
            }
        },
    },
}
var DirectionOperator =
{
    data: null,
    show: function (received_obj) {
        DirectionOperator.data = received_obj;
        objMain.mainF.removeF.clearGroup(objMain.buildingSelectionGroup);
        objMain.mainF.removeF.clearGroup(objMain.directionGroup);
        var newDirectionModle = objMain.ModelInput.direction.clone();
        newDirectionModle.rotateX(-Math.PI / 2);
        newDirectionModle.scale.set(0.4, 0.4, 0.4);//(Math.PI / 2);
        newDirectionModle.position.set(DirectionOperator.data.positionX, -0.1 + DirectionOperator.data.positionZ * objMain.heightAmplify, -DirectionOperator.data.positionY);

        objMain.directionGroup.add(newDirectionModle);
        for (var i = 0; i < DirectionOperator.data.direction.length; i++) {
            var newArrow = null;
            switch (i) {
                case 0:
                    {
                        newArrow = objMain.ModelInput.directionArrowA.obj.clone();
                    }; break;
                case 1:
                    {
                        newArrow = objMain.ModelInput.directionArrowB.obj.clone();
                    }; break;
                case 2:
                    {
                        newArrow = objMain.ModelInput.directionArrowC.obj.clone();
                    }; break;
            }
            if (newArrow != null) {
                //   var newArrow = objMain.ModelInput.directionArrow.obj.clone();
                newArrow.scale.set(0.03, 0.03, 0.03);//(Math.PI / 2);
                newArrow.position.set(DirectionOperator.data.positionX, -0.1 + DirectionOperator.data.positionZ * objMain.heightAmplify, -DirectionOperator.data.positionY);
                newArrow.rotation.y = DirectionOperator.data.direction[i];
                newArrow.userData = { objState: 0, postionCrossKey: received_obj.postionCrossKey };
                objMain.directionGroup.add(newArrow);
            }
        }
        //  operatePanel.refresh();
        objMain.selection.initialize();
    },
    showWhenIsWrong: function (postionCrossKey) {
        for (var i = 1; i < objMain.directionGroup.children.length; i++) {
            if (objMain.directionGroup.children[i].userData.objState == 1 && objMain.directionGroup.children[i].userData.postionCrossKey == postionCrossKey) {
                objMain.directionGroup.children[i].userData.objState = 2;
            }
        }
    },
    AdviseSelect: function (adviseObj) {
        if (objMain.directionGroup.visible) {
            //var minAngle = Math.PI / 20;
            var selectIndex = -1;
            for (var i = 1; i < objMain.directionGroup.children.length; i++) {

                if (i == 1 && adviseObj.select == 'A') {
                    selectIndex = i;
                }
                else if (i == 2 && adviseObj.select == 'B') {
                    selectIndex = i;
                }
                else if (i == 3 && adviseObj.select == 'C') {
                    selectIndex = i;
                }
            }
            if (selectIndex > 0) {

                var rotationY = objMain.directionGroup.children[selectIndex].rotation.y;

                if (objMain.directionGroup.children[selectIndex].userData.objState > 0) {
                }
                else {
                    var json = JSON.stringify({ c: 'ViewAngle', 'rotationY': rotationY, 'postionCrossKey': objMain.directionGroup.children[selectIndex].userData.postionCrossKey, 'uid': adviseObj.Detail.Uid });
                    objMain.ws.send(json);
                    //userData.objState
                    objMain.directionGroup.children[selectIndex].userData.objState = 1;

                }
                //    operatePanel.refresh();
            }
        }
    }
};

var ShowRoadCrossSelectionsOperator =
{
    data: null,
    show: function (received_obj) {
        ShowRoadCrossSelectionsOperator.data = received_obj;
        objMain.mainF.removeF.clearGroup(objMain.crossSelectionsOperator);

        for (var i = 0; i < received_obj.crossDirects.length; i += 6) {

            var c = new Complex(
                received_obj.crossDirects[i + 4] - received_obj.crossDirects[i + 1],
                -(received_obj.crossDirects[i + 3] - received_obj.crossDirects[i + 0]));
            c.toOne();


            // var geometryData = [-0.309, 0.0955, -0.0955, 0.0955, -0.0955, 0.7135];
            //var geometryData = [new Complex(-0.309, 0.0955), new Complex(-0.0955, 0.0955), new Complex(-0.0955, 0.7135)];
            const zoom = 0.35;
            {
                var geometryData = [new Complex(-0.309, 0.0955), new Complex(-0.0955, 0.0955), new Complex(-0.0955, 0.7135)];
                const points = [];

                for (var j = 0; j < geometryData.length; j++) {
                    var c1 = geometryData[j].multiply(c);//multiply
                    points.push(new THREE.Vector3(
                        received_obj.crossDirects[i + 0] + c1.r * zoom,
                        received_obj.crossDirects[i + 2] * objMain.heightAmplify,
                        -received_obj.crossDirects[i + 1] - c1.i * zoom)); // 起点坐标
                }
                const lineGeometry = new THREE.BufferGeometry().setFromPoints(points);
                const line = new THREE.Line(lineGeometry, ShowRoadCrossSelectionsOperator.lineMaterial);
                objMain.crossSelectionsOperator.add(line);
                //points.push(new THREE.Vector3(received_obj.crossDirects[i + 0], -received_obj.crossDirects[i + 2] * objMain.heightAmplify, -received_obj.crossDirects[i + 1])); // 起点坐标
                //points.push(new THREE.Vector3(received_obj.crossDirects[i + 3], -received_obj.crossDirects[i + 5] * objMain.heightAmplify, -received_obj.crossDirects[i + 4])); // 起点坐标
            }
            {
                var geometryData = [new Complex(0.309, 0.0955), new Complex(0.0955, 0.0955), new Complex(0.0955, 0.7135)];
                const points = [];
                for (var j = 0; j < geometryData.length; j++) {
                    var c1 = geometryData[j].multiply(c);//multiply
                    points.push(new THREE.Vector3(
                        received_obj.crossDirects[i + 0] + c1.r * zoom,
                        received_obj.crossDirects[i + 2] * objMain.heightAmplify,
                        -received_obj.crossDirects[i + 1] - c1.i * zoom)); // 起点坐标
                }
                const lineGeometry = new THREE.BufferGeometry().setFromPoints(points);
                const line = new THREE.Line(lineGeometry, ShowRoadCrossSelectionsOperator.lineMaterial);
                objMain.crossSelectionsOperator.add(line);
                //points.push(new THREE.Vector3(received_obj.crossDirects[i + 0], -received_obj.crossDirects[i + 2] * objMain.heightAmplify, -received_obj.crossDirects[i + 1])); // 起点坐标
                //points.push(new THREE.Vector3(received_obj.crossDirects[i + 3], -received_obj.crossDirects[i + 5] * objMain.heightAmplify, -received_obj.crossDirects[i + 4])); // 起点坐标
            }
        }
    },
    lineMaterial: new THREE.LineBasicMaterial({ color: 0x00ff00 })
}

var BuildingModelObj =
{

    copy: function (amodel, received_obj) {
        if (objMain.buildingModel[amodel] == undefined) {

        }
        else {
            if (objMain.buildingGroup.getObjectByName(received_obj.modelID) == undefined) {
                var obj = objMain.buildingModel[amodel].clone();
                obj.name = received_obj.modelID;
                obj.position.set(received_obj.x, received_obj.y * objMain.heightAmplify, received_obj.z);
                obj.rotation.set(0, received_obj.rotatey, 0, 'XYZ');
                obj.userData.modelType = received_obj.modelType;
                objMain.buildingGroup.add(obj);
                if (objMain.state == 'LookForBuildings') {
                    objMain.mainF.lookAtPosition2();
                }
                QueryReward.lookAt();
                objMain.buildingModel[amodel] = null;
                //  s
                var amodelIdWillDel = amodel + '';
                setTimeout(function () {
                    delete objMain.buildingModel[amodel];
                    // 这里放置要延迟执行的代码
                }, 30000);
                // delete objMain.buildingModel[amodel];
            }
        }
    },
    respon: function (received_obj) {
        throw '此方法已停用！';
        //var amodel = received_obj.amodel;
        //if (objMain.buildingModel[amodel] == undefined) {
        //    objMain.ws.send(JSON.stringify({ 'c': 'ModelNotExited' }));
        //}
        //else {
        //    objMain.ws.send(JSON.stringify({ 'c': 'ModelExited' }));
        //}
    },
    Refresh: function () {
        for (var dModeItem in objMain.buildingData.dModel) {

            var dItem = objMain.buildingData.dModel[dModeItem];

            if (objMain.buildingGroup.getObjectByName(dModeItem)) { }
            else {
                var amodelID = dItem.amodel;
                if (objMain.buildingModel[amodelID] == undefined) {
                    if (this.RequestTime[dModeItem] == undefined) {
                        this.RequestTime[dModeItem] = 0;
                    }
                    if (Date.now() - this.RequestTime[dModeItem] > 30000) {
                        this.RequestTime[dModeItem] = Date.now();
                        var url = "http://127.0.0.1:11001/objdata/" + amodelID;

                        if (objMain.debug != 2) {
                            url = "http://127.0.0.1:11001/objdata/" + amodelID;

                        }
                        else {
                            //  url = "https://www.nyrq123.com/objtaiyuan/" + amodelID;
                            //  url = "http://127.0.0.1:11001/objdata/"
                            url = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/objmodel/" + amodelID + ".json";
                        }
                        $.getJSON(url, function (json) {
                            var manager = new THREE.LoadingManager();
                            var amID = json.AmID;
                            var objText = json.objText;
                            var mtlText = json.mtlText;
                            var imgBase64 = json.imgBase64;
                            var modelType = json.modelType;
                            var mtlManaget = null;
                            var mtlOnload = function (materials) {
                                materials.preload();
                                var objL = new THREE.OBJLoader(manager)
                                    .setMaterials(materials)
                                    .loadTextOnly(objText, function (object) {
                                        object.userData.modelType = modelType;
                                        objMain.buildingModel[amID] = object;
                                        BuildingModelObj.Refresh();
                                        objL = null;
                                        manager = null;
                                        mtlManaget = null;
                                    }, function () { }, function () { });
                            };
                            //var imgUrl=
                            if (objMain.debug != 2) {
                                //   url = "http://127.0.0.1:11001/objdata/" + amodelID;
                                if (objMain.buildingGroup.getObjectByName(amID) == undefined) {
                                    //   var manager = new THREE.LoadingManager();
                                    mtlManaget = new THREE.MTLLoader(manager)
                                        .loadTextOnly(mtlText, 'data:image/jpeg;base64,' + imgBase64, mtlOnload);
                                }
                            }
                            else {
                                //  url = "https://www.nyrq123.com/objtaiyuan/" + amodelID;
                                //  url = "http://127.0.0.1:11001/objdata/"
                                //  url = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/objmodel/" + amodelID + ".json";
                                //var ima
                                if (objMain.buildingGroup.getObjectByName(amID) == undefined) {
                                    var imgUrl = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/objmodel/" + amID + ".jpg";
                                    var manager = new THREE.LoadingManager();
                                    mtlManaget = new THREE.MTLLoader(manager).loadTextWithImageUrl(mtlText, imgUrl, mtlOnload);
                                }
                            }

                        })
                    }

                }
                else {
                    BuildingModelObj.copy(amodelID, dItem);
                }
            }
            //var amodelID = dItem.amodel;

        }
    },
    Refresh_bak: function () {
        for (var dModeItem in objMain.buildingData.dModel) {

            var dItem = objMain.buildingData.dModel[dModeItem];
            var amodelID = dItem.amodel;
            if (objMain.buildingModel[amodelID] == undefined) {
                if (this.RequestTime[dModeItem] == undefined) {
                    this.RequestTime[dModeItem] = 0;
                }
                if (Date.now() - this.RequestTime[dModeItem] > 30000) {
                    this.RequestTime[dModeItem] = Date.now();
                    var url = "http://127.0.0.1:11001/objdata/" + amodelID;

                    if (objMain.debug != 2) {
                        url = "http://127.0.0.1:11001/objdata/" + amodelID;

                    }
                    else {
                        //  url = "https://www.nyrq123.com/objtaiyuan/" + amodelID;
                        //  url = "http://127.0.0.1:11001/objdata/"
                        url = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/objmodel/" + amodelID + ".json";
                    }
                    $.getJSON(url, function (json) {
                        var manager = new THREE.LoadingManager();
                        var amID = json.AmID;
                        var objText = json.objText;
                        var mtlText = json.mtlText;
                        var imgBase64 = json.imgBase64;
                        var modelType = json.modelType;
                        var mtlManaget = null;
                        var mtlOnload = function (materials) {
                            materials.preload();
                            var objL = new THREE.OBJLoader(manager)
                                .setMaterials(materials)
                                .loadTextOnly(objText, function (object) {
                                    object.userData.modelType = modelType;
                                    objMain.buildingModel[amID] = object;
                                    BuildingModelObj.Refresh();
                                    objL = null;
                                    manager = null;
                                    mtlManaget = null;
                                }, function () { }, function () { });
                        };
                        //var imgUrl=
                        if (objMain.debug != 2) {
                            //   url = "http://127.0.0.1:11001/objdata/" + amodelID;
                            if (objMain.buildingGroup.getObjectByName(amID) == undefined) {
                                //   var manager = new THREE.LoadingManager();
                                mtlManaget = new THREE.MTLLoader(manager)
                                    .loadTextOnly(mtlText, 'data:image/jpeg;base64,' + imgBase64, mtlOnload);
                            }
                        }
                        else {
                            //  url = "https://www.nyrq123.com/objtaiyuan/" + amodelID;
                            //  url = "http://127.0.0.1:11001/objdata/"
                            //  url = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/objmodel/" + amodelID + ".json";
                            //var ima
                            if (objMain.buildingGroup.getObjectByName(amID) == undefined) {
                                var imgUrl = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/objmodel/" + amID + ".jpg";
                                var manager = new THREE.LoadingManager();
                                mtlManaget = new THREE.MTLLoader(manager).loadTextWithImageUrl(mtlText, imgUrl, mtlOnload);
                            }
                        }

                    })
                }

            }
            else {
                BuildingModelObj.copy(amodelID, dItem);
            }
        }
    },


    RequestTime: {}
};

var roadDisplayFunction =
{
    roadData: {},
    show: function (dataHash, roadCode) {
        if (roadDisplayFunction.roadData[roadCode]) { return; }
        else {
            roadDisplayFunction.roadData[roadCode] = false;
            roadDisplayFunction.Refresh(dataHash);
        }
    },
    Refresh: function (dataHash) {
        for (var roadCode in roadDisplayFunction.roadData) {
            if (roadDisplayFunction.roadData[roadCode]) {
            }
            else {
                if (this.RequestTime[roadCode] == undefined) {
                    this.RequestTime[roadCode] = 0;
                }
                if (Date.now() - this.RequestTime[roadCode] > 30000) {
                    this.RequestTime[roadCode] = Date.now();
                    var url = "http://127.0.0.1:11001/roaddata/" + roadCode;

                    if (objMain.debug != 2) {
                        url = "http://127.0.0.1:11001/roaddata/" + roadCode;
                    }
                    else {
                        url = "https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/roadData/" + dataHash + "/" + roadCode + ".json";
                    }
                    $.getJSON(url, function (received_obj) {
                        //var meshPoints = json.meshPoints;
                        var basePoint = received_obj.basePoint;
                        var roadCodeOperating = received_obj.roadCode;
                        if (roadDisplayFunction.roadData[roadCodeOperating]) { return; }
                        else {
                            roadDisplayFunction.roadData[roadCodeOperating] = true;
                            for (var i = 0; i < received_obj.meshPoints.length; i += 12) {
                                var itemData = [
                                    (received_obj.meshPoints[i + 0] + basePoint[0]) / 1000000,
                                    (received_obj.meshPoints[i + 1] + basePoint[1]) / 1000000,
                                    (received_obj.meshPoints[i + 2] + basePoint[2]) / 1000000,
                                    (received_obj.meshPoints[i + 3] + basePoint[0]) / 1000000,
                                    (received_obj.meshPoints[i + 4] + basePoint[1]) / 1000000,
                                    (received_obj.meshPoints[i + 5] + basePoint[2]) / 1000000,
                                    (received_obj.meshPoints[i + 6] + basePoint[0]) / 1000000,
                                    (received_obj.meshPoints[i + 7] + basePoint[1]) / 1000000,
                                    (received_obj.meshPoints[i + 8] + basePoint[2]) / 1000000,
                                    (received_obj.meshPoints[i + 9] + basePoint[0]) / 1000000,
                                    (received_obj.meshPoints[i + 10] + basePoint[1]) / 1000000,
                                    (received_obj.meshPoints[i + 11] + basePoint[2]) / 1000000
                                ];
                                MapData.meshPoints.push(itemData);
                                // MapData.meshPoints.push(received_obj.meshPoints[i]);
                            }

                            var drawRoadInfomation = function () {

                                {
                                    var group = objMain.roadGroup;
                                    var startIndex = group.children.length - 1;
                                    for (var i = startIndex; i >= 0; i--) {
                                        //if (group.children[i].type == "Mesh")
                                        {
                                            group.children[i].geometry.dispose();
                                            group.children[i].material.dispose();
                                        }
                                    }
                                }

                                objMain.mainF.removeF.clearGroup(objMain.roadGroup);
                                //  objMain.F.clearGroup(
                                var obj = MapData.meshPoints;

                                var positions = [];
                                //   var colors = [];
                                for (var i = 0; i < obj.length; i++) {
                                    positions.push(
                                        MercatorGetXbyLongitude(obj[i][0]), MercatorGetXbyLongitude(obj[i][2]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][1]),
                                        MercatorGetXbyLongitude(obj[i][3]), MercatorGetXbyLongitude(obj[i][5]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][4]),
                                        MercatorGetXbyLongitude(obj[i][6]), MercatorGetXbyLongitude(obj[i][8]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][7]),
                                        MercatorGetXbyLongitude(obj[i][6]), MercatorGetXbyLongitude(obj[i][8]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][7]),
                                        MercatorGetXbyLongitude(obj[i][9]), MercatorGetXbyLongitude(obj[i][11]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][10]),
                                        MercatorGetXbyLongitude(obj[i][0]), MercatorGetXbyLongitude(obj[i][2]) * objMain.heightAmplify, -MercatorGetYbyLatitude(obj[i][1]),

                                    );
                                }
                                function disposeArray() {

                                    this.array = null;

                                }
                                //  console.log('p', positions);
                                //var vertices = new Float32Array(positions);
                                var geometry = new THREE.BufferGeometry();
                                geometry.addAttribute('position', new THREE.Float32BufferAttribute(positions, 3).onUpload(disposeArray));
                                //geometry.addAttribute('color', new THREE.BufferAttribute(colors, 3).onUpload(disposeArray));
                                geometry.computeBoundingSphere();
                                //var material = new THREE.MeshBasicMaterial({ vertexColors: THREE.VertexColors });
                                var material = new THREE.MeshBasicMaterial({ color: 0xeedd78 });
                                var mesh = new THREE.Mesh(geometry, material);

                                objMain.roadGroup.add(mesh);


                                var edges = new THREE.EdgesGeometry(geometry);
                                var line = new THREE.LineSegments(edges, new THREE.LineBasicMaterial({ color: 0xeeddf0 }));
                                objMain.roadGroup.add(line);

                            };
                            drawRoadInfomation();
                            roadDisplayFunction.Refresh(dataHash);
                        }
                    })
                }
            }
        }
    },
    RequestTime: {}
};

var SetBustPage = function () {
    window.cancelAnimationFrame(objMain.animateObj)
    document.body.innerHTML = '';
    var img = document.createElement('img');
    img.src = 'Pic/gameOver.jpg';
    //var div = document.createElement('div');
    //div.appendChild(img);
    img.style.position = 'absolute';
    img.style.top = '50%';
    img.style.left = '50%';
    img.style.width = 'calc(80%)';
    img.style.height = 'auto';
    img.style.maxWidth = 'calc(80%)';
    img.style.maxHeight = 'calc(80%)';
    img.style.minWidth = 'calc(20%)';
    img.style.minWidth = 'calc(20%)';
    img.style.transform = 'translate(-50%, -50%)';
    document.body.appendChild(img);
};
var drawGoodsSelection =
{
    f: function (received_obj) {
        /*
         * 依据objText，mtlText，base64画图
         */
        objMain.mainF.removeF.clearGroup(objMain.buildingSelectionGroup);
        drawGoodsSelection.data = [];
        for (var i = 0; i < received_obj.selections.length; i++) {
            var points = [];
            points.push(new THREE.Vector3(received_obj.x, received_obj.y * objMain.heightAmplify, received_obj.z));
            points.push(new THREE.Vector3(received_obj.positions[i * 3], received_obj.positions[i * 3 + 1] * objMain.heightAmplify, received_obj.positions[i * 3 + 2]));

            var geometry = new THREE.BufferGeometry().setFromPoints(points);
            var line = new THREE.Line(geometry, drawGoodsSelection.material);
            objMain.buildingSelectionGroup.add(line);
        }
        if (i > 0) {
            var v = new THREE.Vector3(received_obj.x, received_obj.y * objMain.heightAmplify, received_obj.z);
            drawGoodsSelection.data.push(v);
        }
        for (var i = 0; i < received_obj.selections.length; i++) {
            var v = new THREE.Vector3(received_obj.positions[i * 3], received_obj.positions[i * 3 + 1] * objMain.heightAmplify, received_obj.positions[i * 3 + 2]);
            drawGoodsSelection.data.push(v);
        }
    },
    material: new THREE.LineBasicMaterial({ color: 0x33FF00 }),
    data: []
};

var DiamondModel =
{
    black: null,
    blue: null,
    green: null,
    red: null,
    initialize: function (received_obj) {
        var indexStrs = ['black', 'blue', 'green', 'red'];
        for (var i = 0; i < 4; i++) {
            var indexV = i;
            var indexStr = indexStrs[indexV];
            var manager = new THREE.LoadingManager();
            new THREE.MTLLoader(manager)
                .loadTextOnly(received_obj.mtlText, 'data:image/jpeg;base64,' + received_obj.imageBase64s[indexV], function (materials) {
                    materials.preload();
                    // materials.depthTest = false;
                    new THREE.OBJLoader(manager)
                        .setMaterials(materials)
                        //.setPath('/Pic/')
                        .loadTextOnly(received_obj.objText, function (object) {
                            DiamondModel[indexStr] = object;
                        }, function () { }, function () { });
                });
        }

    }
};

var sceneYUpdate = function (deltaY) {


    //var v1 = new THREE.Vector3().subVectors(objMain.controls.target, objMain.camera.position);
    //if (v1.length() > 0) {
    //    v1.setLength(1);
    //    var v2 = new THREE.Vector3(0, 1, 0);
    //    var v3 = new THREE.Vector3().crossVectors(v1, v2);
    //    if (v3.length() > 0) {
    //        v3.setLength(1);
    //        v2 = new THREE.Vector3().crossVectors(v3, v1);

    //        var v4 = v2.applyAxisAngle(v3, (0.5 - 0.618) * 35 / 180 * Math.PI);
    //        var A = v4.x;
    //        var B = v4.y;
    //        var C = v4.z;
    //        var D = -A * objMain.camera.position.x - B * objMain.camera.position.y - C * objMain.camera.position.z;

    //        if (Math.abs(B) > 1e-4) {
    //            var calY = (-D - A * objMain.controls.target.x - C * objMain.controls.target.z) / B;
    //            if (calY > 25)
    //                calY = 25;
    //            else if (calY < -25)
    //                calY = -25;
    //            objMain.scene.position.y = calY - deltaY;
    //            //  objMain.scene.
    //            return calY;
    //        }
    //    }
    //}
}

var OperateHelp =
{
    isOn: false,
    f: function () {
        if (this.isOn && objMain.state == 'OnLine') {
            if (objMain.directionGroup != null) {
                if (objMain.directionGroup.visible) {
                    if (this.probability.direction < Math.random()) {
                        $.notify(
                            AMsg.directionNotify,
                            {
                                autoHide: true,
                                className: 'info',
                                autoHideDelay: 20000,
                            });
                        this.probability.direction += 0.015;
                    }
                }
            }

            if (objMain.carState.car) {
                if (objMain.carState.car == 'waitAtBaseStation') {
                    if (this.probability.waitAtBaseStation < Math.random()) {
                        $.notify(AMsg.waitAtStationNotify,
                            {
                                autoHide: true,
                                className: 'info',
                                autoHideDelay: 20000,
                                position: 'left',
                            });
                        this.probability.waitAtBaseStation += 0.015;
                    }
                }
            }
            //objMain.carState.car
        }
    },
    probability:
    {
        direction: 0,
        waitAtBaseStation: 0
    }
};
//setInterval("OperateHelp.f();", 12000);

document.addEventListener('gesturestart', function (e) {
    e.preventDefault();
    //组织缩放图片！
});

var UpdateOtherBasePoint = function () {
    for (var key in objMain.othersBasePoint) {
        var indexKey = key + '';
        var basePoint = objMain.othersBasePoint[key].basePoint;
        //  drawPoint('orange', basePoint, indexKey);
        objMain.mainF.drawLineOfFpToRoad(basePoint, objMain.playerGroup, 'orange', indexKey);
        objMain.mainF.initilizeCars(basePoint, 'orange', indexKey, false, objMain.othersBasePoint[key].positionInStation);
        //  console.log('哦哦', '出现了预料的情况！！！');
        //alert();
    }
}


var lookInfoForCar = function () {
    // objMain.mouse = new THREE.Vector2(1, 1);
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);

    //for (var i = 0; i < objMain.carGroup.children.length; i++) {
    //    //if (objMain.carGroup.children[i].type == "Group") {
    //    //    for (var j = 0; j < objMain.carGroup.children[i].children.length; j++) {
    //    //        var intersection = objMain.raycaster.intersectObject(objMain.carGroup.children[i].children[j]);
    //    //        if (intersection.length > 0) {
    //    //            var carName = objMain.carGroup.children[i].name;
    //    //            var indexKey = carName.split('_')[1];
    //    //            if (objMain.othersBasePoint[indexKey]) {
    //    //                $.notify('这是【' + objMain.othersBasePoint[indexKey].playerName + '】的车', 'msg');
    //    //                return;
    //    //            }
    //    //        }
    //    //    }
    //    //}
    //}


}
var lookInfoForSelect = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    for (var i = 0; i < objMain.roadGroup.children.length; i++) {
        var intersection = objMain.raycaster.intersectObject(objMain.roadGroup.children[i]);
        if (intersection.length > 0) {
            //alert('点击了无人机航线');
            drawCoreObj(objMain.roadGroup.children[i].userData);
            return true;
        }
    }
    return false;
}

var confirmSelect = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.targetGroup.children.length; i++) {
        var itemGroup = objMain.targetGroup.children[i];
        if (itemGroup != undefined) {
            for (var j = 0; j < itemGroup.children.length; j++) {
                var intersection = objMain.raycaster.intersectObject(itemGroup.children[j]);
                if (intersection.length > 0) {
                    // alert('点击了视角求');
                    //drawCoreObj(objMain.roadGroup.children[i].userData);
                    var userData = objMain.targetGroup.children[i].userData;
                    var objPass = { c: 'WebSelect', code: userData.c, height: userData.h };
                    objMain.ws.send(JSON.stringify(objPass));
                    console.log('obj', JSON.stringify(objPass));
                    return true;
                }
            }
        }
    }
    return false;
}

var compassClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.directionGroup.children.length; i++) {
        var itemGroup = objMain.directionGroup.children[i];
        for (var j = 0; j < itemGroup.children.length; j++) {
            var intersection = objMain.raycaster.intersectObject(itemGroup.children[j]);
            if (intersection.length > 0) {
                //  alert('点击了指针')
                objMain.ws.send(JSON.stringify({ 'c': 'Navigate' }));
                return true;
            }
        }

    }
    return false;
}

var goldObjClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.collectGroup.children.length; i++) {
        var itemGroup = objMain.collectGroup.children[i];
        for (var j = 0; j < itemGroup.children.length; j++) {
            var intersection = objMain.raycaster.intersectObject(itemGroup.children[j]);
            if (intersection.length > 0) {
                //alert('点击了金元宝');
                objMain.ws.send(JSON.stringify({ 'c': 'Collect' }));
                return true;
            }
        }

    }
    return false;
}

var turbineClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.buildingGroup.children.length; i++) {
        var itemGroup = objMain.buildingGroup.children[i];
        for (var j = 0; j < itemGroup.children.length; j++) {
            var intersection = objMain.raycaster.intersectObject(itemGroup.children[j]);
            if (intersection.length > 0) {
                //alert('点击了金元宝');
                objMain.ws.send(JSON.stringify({ 'c': 'Charge' }));
                return true;
            }
        }

    }
    return false;
}
var btcAddrClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.bitcoinCharacterGroup.children.length; i++) {
        var itemGroup = objMain.bitcoinCharacterGroup.children[i];
        var intersection = objMain.raycaster.intersectObject(itemGroup);

        if (intersection.length > 0) {
            var userData = itemGroup.userData;
            //alert('点击了比特币地址');
            objMain.ws.send(JSON.stringify({ c: 'LookForBuildings' }));
            setTransactionHtml.editRootContainer();
            setTransactionHtml.drawAddr(userData.btc);
            setTransactionHtml.drawAgreementEditor();
            setTransactionHtml.drawStockTable();
            setTransactionHtml.drawTradeTable();
            setTransactionHtml.originalTable();
            transactionBussiness().showAuthor('https://www.nyrq123.com');
            return true;
        }
        return false;
    }
}

var sataliteClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.getOutGroup.children.length; i++) {
        var itemGroup = objMain.getOutGroup.children[i];
        for (var j = 0; j < itemGroup.children.length; j++) {
            var intersection = objMain.raycaster.intersectObject(itemGroup.children[j]);
            if (intersection.length > 0) {
                //alert('点击了金元宝');
                objMain.ws.send(JSON.stringify({ 'c': 'ReturnHome' }));
                return true;
            }
        }

    }
    return false;
}

var batteryClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    for (var i = 0; i < objMain.batteryGroup.children.length; i++) {
        var itemGroup = objMain.batteryGroup.children[i];
        for (var j = 0; j < itemGroup.children.length; j++) {
            var intersection = objMain.raycaster.intersectObject(itemGroup.children[j]);

            if (intersection.length > 0) {
                var userData = itemGroup.userData;
                //alert('点击了比特币地址');
                objMain.ws.send(JSON.stringify({ c: 'Promote', pType: 'mile' }));
                //setTransactionHtml.editRootContainer();
                //setTransactionHtml.drawAddr(userData.btc);
                //setTransactionHtml.drawAgreementEditor();
                //setTransactionHtml.drawStockTable();
                //setTransactionHtml.drawTradeTable();
                //setTransactionHtml.originalTable();
                //transactionBussiness().showAuthor('https://www.nyrq123.com');
                return true;
            }
        }

        return false;
    }
}

var selfVehicleClicked = function () {
    objMain.raycaster.setFromCamera(objMain.mouse, objMain.camera);
    //objMain.targetGroup.children[0].children[0]
    //objMain.carGroup.getObjectByName('vehicle_'+objMain.indexKey).children[0].isMesh
    var selfObj = objMain.carGroup.getObjectByName('vehicle_' + objMain.indexKey);

    for (var i = 0; i < selfObj.children.length; i++) {
        var childObj = selfObj.children[i];
        if (childObj.isMesh) {
            var intersection = objMain.raycaster.intersectObject(childObj);
            if (intersection.length > 0) {
                objMain.ws.send(JSON.stringify({ 'c': 'Navigate' }));
                return true;
            }
        }
    }
    return false;
}
//////////
/*
 * 手柄类，此游戏只支持单手柄操作。
 */

/////////////
/*
 * 复数类
 */
function Complex(R, I) {
    if (isNaN(R) || isNaN(I)) { throw new TypeError('Complex params require Number'); }
    this.r = R;
    this.i = I;
}
// 加法
Complex.prototype.add = function (that) {
    return new Complex(this.r + that.r, this.i + that.i);
};
// 负运算
Complex.prototype.neg = function () {
    return new Complex(-this.r, -this.i);
};
// 乘法
Complex.prototype.multiply = function (that) {
    if (this.r === that.r && this.i + that.i === 0) {
        return this.r * this.r + this.i * this.i
    }
    return new Complex(this.r * that.r - this.i * that.i, this.r * that.i + this.i * that.r);
};
// 除法
Complex.prototype.divide = function (that) {
    var a = this.r;
    var b = this.i;
    var c = that.r;
    var d = that.i;
    return new Complex((a * c + b * d) / (c * c + d * d), (b * c - a * d) / (c * c + d * d));
};
// 模长
Complex.prototype.mo = function () {
    return Math.sqrt(this.r * this.r + this.i * this.i);
};
//变为角度
Complex.prototype.toAngle = function () {

    if (this.r > 1e-6) {
        var angle = Math.atan(this.i / this.r);
        angle = (angle + Math.PI * 4) % (Math.PI * 2);
        return angle;
    }
    else if (this.r < -1e-6) {
        var angle = Math.atan(this.i / this.r);
        angle = (angle + Math.PI * 3) % (Math.PI * 2);
        return angle;
    }
    else if (this.i > 0) {
        return Math.PI / 2;
    }
    else if (this.i < 0) {
        return Math.PI * 3 / 2;
    }
    else {
        throw 'this Complex can not change to angle';
    }
    return Math.sqrt(this.r * this.r + this.i * this.i);
};
Complex.prototype.toOne = function () {
    var m = this.mo();
    this.r /= m, this.i /= m;
    //return Math.sqrt(this.r * this.r + this.i * this.i);
};
Complex.prototype.isZero = function () {
    return this.mo() < 1e-4;
}
Complex.prototype.toString = function () {
    return "{" + this.r + "," + this.i + "}";
};
// 判断两个复数相等
Complex.prototype.equal = function (that) {
    return that !== null && that.constructor === Complex && this.r === that.r && this.i === that.i;
};
Complex.ZERO = new Complex(0, 0);
Complex.ONE = new Complex(1, 0);
Complex.I = new Complex(0, 1);
// 从普通字符串解析为复数
Complex.parse = function (s) {
    try {
        var execres = Complex.parseRegExp.exec(s);
        return new Complex(parseFloat(execres[1]), parseFloat(execres[2]));
    } catch (e) {
        throw new TypeError("Can't parse '" + s + "'to a complex");
    }
};
Complex.parseRegExp = /^\{([\d\s]+[^,]*),([\d\s]+[^}]*)\}$/;
window.c = Complex;
// console.log(/^\{([\d\s]+[^,]*),([\d\s]+[^}]*)\}$/.exec('{2,3}'));
// 示例代码

////////////
window.buttonClick = buttonClick;
window.objMain = objMain;
//export function mainObjBtnClick(v) {
//    buttonClick(v)
//}
