let loadAerialVehicle = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';

    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/objdata/Drone.obj';
        mtlUrl = 'http://127.0.0.1:11001/objdata/Drone.mtl';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/drone/simple2.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/drone/simple2.mtl';
    }
    var manager = new THREE.LoadingManager();
    new THREE.MTLLoader(manager).load(mtlUrl, function (materials) {
        materials.preload();
        new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
            console.log('obj', object);
            console.log('mtl', materials);
            object.scale.set(2, 2, 2);
            object.getObjectByName('propeller_01').position.x = -0.201797;
            object.getObjectByName('propeller_01').position.z = -0.19839;

            object.getObjectByName('propeller_02').position.x = 0.196888;
            object.getObjectByName('propeller_02').position.z = -0.199555
                ;

            object.getObjectByName('propeller_03').position.x = 0.19781;
            object.getObjectByName('propeller_03').position.z = 0.200141;

            object.getObjectByName('propeller_04').position.x = -0.188468;
            object.getObjectByName('propeller_04').position.z = 0.201515;
            fFinished(object);
        })
    });

    ////
    //// 假设你的物体是一个Mesh对象
    //const object = new THREE.Mesh(geometry, material);

    //// 定义旋转轴（例如绕 Y 轴旋转）
    //const axis = new THREE.Vector3(0, 1, 0).normalize(); // 归一化轴向量

    //// 定义旋转角度（以弧度为单位，1 弧度约等于 57.3 度）
    //const angle = Math.PI / 4; // 45 度

    //// 创建一个四元数，并设置物体的旋转
    //const quaternion = new THREE.Quaternion();
    //quaternion.setFromAxisAngle(axis, angle); // 设置四元数根据轴和角度旋转

    //// 将物体的四元数旋转状态设置为这个四元数
    //object.quaternion.copy(quaternion);
};
var animateAerialVehicle = function (objMain) {
    for (var i = 0; i < objMain.carGroup.children.length; i++) {
        var vehicle = objMain.carGroup.children[i];

        //  var moveValue1 = { x: -0.201797, y: 0.365639, z: -0.19839 };
        //var moveValue2 = { x: -0.19839, y: -0.365639, z: -0.201797 };
        var propeller_01 = vehicle.getObjectByName('propeller_01');
        var propeller_02 = vehicle.getObjectByName('propeller_02');
        var propeller_03 = vehicle.getObjectByName('propeller_03');
        var propeller_04 = vehicle.getObjectByName('propeller_04');

        var angle = Date.now() % 600 / 600 * Math.PI * 2;
        propeller_01.rotation.y = angle;
        propeller_02.rotation.y = -angle;
        propeller_03.rotation.y = angle;
        propeller_04.rotation.y = -angle;

        ////const axis = new THREE.Vector3(0, 1, 0).normalize(); // 归一化轴向量

        ////var moveToZero = function (obj, moveValue) {
        ////    obj.position.x -= moveValue.x;
        ////    obj.position.y -= moveValue.y;
        ////    obj.position.z -= moveValue.z;
        ////};
        //var rotateObj = function (obj, roateValue, angle) {
        //    const axis = new THREE.Vector3(roateValue.x, roateValue.y, roateValue.z).normalize();
        //    const quaternion = new THREE.Quaternion();
        //    quaternion.setFromAxisAngle(axis.normalize(), angle); // 设置新的旋转角度
        //    obj.quaternion.copy(quaternion); // 应用新的旋转
        //    // obj.rotation.y
        //}
        ////moveToZero(propeller_01, moveValue1);
        //var angle = Date.now() % 3000 / 3000 * Math.PI * 2;
        //rotateObj(propeller_01, { x: 0, y: 1, z: 0 }, angle);
        //// moveToZero(propeller_01, moveValue2);
    }

};

let ChangeBG = function (inputObj, objMain) {
    var fp = inputObj.fp;
    var fPCode = fp.fPCode;
    var Height = fp.Height;
    objMain.background.mainFP = fp;
    if (objMain.debug == 0) {
        objMain.background.path = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/"; //received_obj.path;
        objMain.background.change();
    }
    else {
        //objMain.background.path = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/"; //received_obj.path;
        //objMain.background.change();
        objMain.background.changeWithJson(fPCode, Height);
    }
    objMain.background.rotateOthers(inputObj.fp.ObjInSceneRotation);

    //rotateOthers
};

let loadCore = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/coredata/core.obj';
        mtlUrl = 'http://127.0.0.1:11001/coredata/core.mtl';
        picUrl = 'http://127.0.0.1:11001/coredata/px.jpg';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/core003.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/core003.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/px.jpg';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
    //var manager = new THREE.LoadingManager();
    // new THREE.MTLLoader(manager).load.load(mtlUrl, );
};

let loadGoldBaby = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/goldIcon/gold.obj';
        mtlUrl = 'http://127.0.0.1:11001/goldIcon/gold.mtl';
        picUrl = 'http://127.0.0.1:11001/goldIcon/material_baseColor.png';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/goldModel/gold.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/goldModel/gold.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/goldModel/material_baseColor.png';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
    //var manager = new THREE.LoadingManager();
    // new THREE.MTLLoader(manager).load.load(mtlUrl, );
};

let loadCompass = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/goldCompass/Compass.obj';
        mtlUrl = 'http://127.0.0.1:11001/goldCompass/Compass.mtl';
        picUrl = 'http://127.0.0.1:11001/goldCompass/compassBG.png';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/compass/Compass.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/compass/Compass.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/compass/compassBG.png';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
    //var manager = new THREE.LoadingManager();
    // new THREE.MTLLoader(manager).load.load(mtlUrl, );
};

let loadTurbine = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/turbine/turbine.obj';
        mtlUrl = 'http://127.0.0.1:11001/turbine/turbine.mtl';
        picUrl = '';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/turbine/turbine.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/turbine/turbine.mtl';
        picUrl = '';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
};

let loadSatelite = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/satelite/satelite.obj';
        mtlUrl = 'http://127.0.0.1:11001/satelite/satelite.mtl';
        picUrl = 'http://127.0.0.1:11001/satelite/satelite.png';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/satelite/satelite.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/satelite/satelite.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/satelite/satelite.png';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
};

var testCoreDraw = function () {
    var drawF = function (imgUrl, loadFinished) {
        objUrl = 'http://127.0.0.1:11001/coredata/core.obj';
        mtlUrl = 'http://127.0.0.1:11001/coredata/core.mtl';
        $.get(mtlUrl, function (txt) {
            // console.log('t', txt)
            var manager = new THREE.LoadingManager();
            new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, imgUrl, function (materials) {
                materials.preload();
                new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                    console.log('obj', object);
                    console.log('mtl', materials);
                    //fFinished(object);
                    //objMain.roadGroup.add(s1)
                    loadFinished(object);
                })
            })
        })
    }
    var fPCode = 'RUDCSSPVVX';
    var Height = 0;
    {
        var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/px.jpg";
        drawF(imgUrl, function (objInput) {
            objMain.roadGroup.add(objInput)
        });
    }
    {
        var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/nz.jpg";
        drawF(imgUrl, function (objInput) {
            objInput.rotateY(Math.PI / 2);
            objMain.roadGroup.add(objInput)
        });
    }
    {
        var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/nx.jpg";
        drawF(imgUrl, function (objInput) {
            objInput.rotateY(Math.PI);
            objMain.roadGroup.add(objInput)
        });
    }
    {
        var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/pz.jpg";
        drawF(imgUrl, function (objInput) {
            objInput.rotateY(Math.PI * 3 / 2);
            objMain.roadGroup.add(objInput)
        });
    }
    {
        var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/py.jpg";
        drawF(imgUrl, function (objInput) {
            objInput.rotateZ(Math.PI * 3 / 2);
            objInput.rotateX(Math.PI * 1 / 2);
            objMain.roadGroup.add(objInput)
        });
    }
    {
        var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/ny.jpg";
        drawF(imgUrl, function (objInput) {
            objInput.rotateZ(Math.PI * 1 / 2);
            objInput.rotateX(Math.PI * 3 / 2);
            objMain.roadGroup.add(objInput)
        });
    }
    //{
    //    var imgUrl = 'http://127.0.0.1:11001/coredata/px.jpg';
    //    drawF(imgUrl, function (objInput) {

    //    });
    //}
};

var drawCoreObj = function (inputData) {

    if (objMain.targetGroup.children.length > 0) {
        if (objMain.targetGroup.children[0].userData.c == inputData.c && objMain.targetGroup.children[0].userData.h == inputData.h) {
            var scale = objMain.targetGroup.children[0].scale.x;
            for (var i = 0; i < objMain.targetGroup.children.length; i++) {
                objMain.targetGroup.children[i].scale.set(scale * 1.3, scale * 1.3, scale * 1.3);
            }
            return;
        }
    }
    if (objMain.debug == 0) {
        var drawF = function (imgUrl, inputData, loadFinished) {
            var objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/core003.obj';
            var mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/core003.mtl';
            if (objMain.debug == 0) {
                objUrl = 'http://127.0.0.1:11001/coredata/core.obj';
                mtlUrl = 'http://127.0.0.1:11001/coredata/core.mtl';
            }
            else {

            }
            $.get(mtlUrl, function (txt) {
                // console.log('t', txt)
                var manager = new THREE.LoadingManager();
                new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, imgUrl, function (materials) {
                    materials.preload();
                    new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                        console.log('obj', object);
                        console.log('mtl', materials);
                        //fFinished(object);
                        //objMain.roadGroup.add(s1)
                        object.userData = inputData;
                        loadFinished(object);
                    })
                })
            })
        }
        objMain.mainF.removeF.clearGroup(objMain.targetGroup);
        var fPCode = inputData.c;
        var Height = inputData.h;
        var Height = inputData.h;
        {
            var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/px.jpg";
            if (objMain.debug == 0) {

            }
            else {

            }
            drawF(imgUrl, inputData, function (objInput) {
                objMain.targetGroup.add(objInput)
            });
        }
        {
            var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/nz.jpg";
            drawF(imgUrl, inputData, function (objInput) {
                objInput.rotateY(Math.PI / 2);
                objMain.targetGroup.add(objInput)
            });
        }
        {
            var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/nx.jpg";
            drawF(imgUrl, inputData, function (objInput) {
                objInput.rotateY(Math.PI);
                objMain.targetGroup.add(objInput)
            });
        }
        {
            var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/pz.jpg";
            drawF(imgUrl, inputData, function (objInput) {
                objInput.rotateY(Math.PI * 3 / 2);
                objMain.targetGroup.add(objInput)
            });
        }
        {
            var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/py.jpg";
            drawF(imgUrl, inputData, function (objInput) {
                objInput.rotateZ(Math.PI * 3 / 2);
                objInput.rotateX(Math.PI * 1 / 2);
                objMain.targetGroup.add(objInput)
            });
        }
        {
            var imgUrl = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/ny.jpg";
            drawF(imgUrl, inputData, function (objInput) {
                objInput.rotateZ(Math.PI * 1 / 2);
                objInput.rotateX(Math.PI * 3 / 2);
                objMain.targetGroup.add(objInput)
            });
        }
    }
    else {
        objMain.mainF.removeF.clearGroup(objMain.targetGroup);
        var fPCode = inputData.c;
        var Height = inputData.h;
        var drawF = function (imgUrl, inputData, loadFinished) {
            var objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/core003.obj';
            var mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/core/core003.mtl';
            $.get(mtlUrl, function (txt) {
                // console.log('t', txt)
                var manager = new THREE.LoadingManager();
                new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, imgUrl, function (materials) {
                    materials.preload();
                    new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                        console.log('obj', object);
                        console.log('mtl', materials);
                        //fFinished(object);
                        //objMain.roadGroup.add(s1)
                        object.userData = inputData;
                        loadFinished(object);
                    })
                })
            })
        }
        var url = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/bgImg/' + fPCode + '_' + Height + '.json';
        $.getJSON(url, function (params) {
            {
                var imgUrl = "data:image/jpeg;base64," + params.px;
                drawF(imgUrl, inputData, function (objInput) {
                    objMain.targetGroup.add(objInput)
                });
            }
            {
                var imgUrl = "data:image/jpeg;base64," + params.nz;
                drawF(imgUrl, inputData, function (objInput) {
                    objInput.rotateY(Math.PI / 2);
                    objMain.targetGroup.add(objInput)
                });
            }
            {
                var imgUrl = "data:image/jpeg;base64," + params.nx;
                drawF(imgUrl, inputData, function (objInput) {
                    objInput.rotateY(Math.PI);
                    objMain.targetGroup.add(objInput)
                });
            }
            {
                var imgUrl = "data:image/jpeg;base64," + params.pz;
                drawF(imgUrl, inputData, function (objInput) {
                    objInput.rotateY(Math.PI * 3 / 2);
                    objMain.targetGroup.add(objInput)
                });
            }
            {
                var imgUrl = "data:image/jpeg;base64," + params.py;
                drawF(imgUrl, inputData, function (objInput) {
                    objInput.rotateZ(Math.PI * 3 / 2);
                    objInput.rotateX(Math.PI * 1 / 2);
                    objMain.targetGroup.add(objInput)
                });
            }
            {
                var imgUrl = "data:image/jpeg;base64," + params.ny;
                drawF(imgUrl, inputData, function (objInput) {
                    objInput.rotateZ(Math.PI * 1 / 2);
                    objInput.rotateX(Math.PI * 3 / 2);
                    objMain.targetGroup.add(objInput)
                });
            }
        })
    }
};

var drawLineOfSelections = function (inputObj, objMain) {
    console.log('需要绘制', inputObj);
    var color = 0x00ff00;
    var selections = inputObj.selections;
    // var selections = ['', ''];
    objMain.mainF.removeF.clearGroup(objMain.roadGroup);
    for (var i = 0; i < selections.length; i++) {
        var sItem = selections[i];
        var start = new THREE.Vector3(0, 0, 0);
        var end = new THREE.Vector3(sItem.x, sItem.z, -sItem.y);
        var lineGeometry = new THREE.Geometry();
        lineGeometry.vertices.push(start);
        lineGeometry.vertices.push(end);
        var lineMaterial = new THREE.LineBasicMaterial({ color: color });
        var line = new THREE.Line(lineGeometry, lineMaterial);
        line.name = 'road_' + sItem.c;
        line.userData = sItem;
        objMain.roadGroup.add(line);
    }
};

var drawCompass = function (inputObj, objMain) {
    console.log('需要绘制', inputObj);
    var color = 0x00ff00;
    var selections = inputObj.selections;
    // var selections = ['', ''];
    objMain.mainF.removeF.clearGroup(objMain.directionGroup);
    var copyObj = objMain.ModelInput.compass.clone();
    copyObj.rotation.set(inputObj.position.rx, inputObj.position.ry, inputObj.position.rz, 'XYZ');
    copyObj.position.set(inputObj.position.x, inputObj.position.y, inputObj.position.z);
    copyObj.scale.set(inputObj.position.s, inputObj.position.s, inputObj.position.s);
    objMain.directionGroup.add(copyObj);
};

var drawGoldObj = function (inputObj) {
    console.log('需要绘制', inputObj);

    objMain.mainF.removeF.clearGroup(objMain.collectGroup);
    if (inputObj.hasValue) {
        var copyObj = objMain.ModelInput.GoldIngotIcon.clone();
        copyObj.rotation.set(inputObj.position.rx, inputObj.position.ry, inputObj.position.rz, 'XYZ');
        copyObj.position.set(inputObj.position.x, inputObj.position.y, inputObj.position.z);
        copyObj.scale.set(inputObj.position.s, inputObj.position.s, inputObj.position.s);
        objMain.collectGroup.add(copyObj);
    }
};

var drawTurbine = function (inputObj) {
    console.log('需要绘制', inputObj);

    objMain.mainF.removeF.clearGroup(objMain.buildingGroup);
    if (inputObj.hasValue) {
        var copyObj = objMain.ModelInput.turbine.clone();
        copyObj.children[1].position.set(-2.30335, 9.00124, 0.070831);
        copyObj.rotation.set(inputObj.position.rx, inputObj.position.ry, inputObj.position.rz, 'XYZ');
        copyObj.position.set(inputObj.position.x, inputObj.position.y, inputObj.position.z);
        copyObj.scale.set(inputObj.position.s, inputObj.position.s, inputObj.position.s);
        objMain.buildingGroup.add(copyObj);
    }
    //z=9.00124  y=-2.30335  x-0.070831
};

//getOutGroup
var drawStatelite = function (inputObj) {
    console.log('需要绘制', inputObj);

    objMain.mainF.removeF.clearGroup(objMain.getOutGroup);
    if (inputObj.hasValue) {
        var copyObj = objMain.ModelInput.satelite.clone();
        copyObj.rotation.set(inputObj.position.rx, inputObj.position.ry, inputObj.position.rz, 'XYZ');
        copyObj.position.set(inputObj.position.x, inputObj.position.y, inputObj.position.z);
        copyObj.scale.set(inputObj.position.s, inputObj.position.s, inputObj.position.s);
        objMain.getOutGroup.add(copyObj);
    }
};

var drawBattery = function (inputObj) {
    console.log('需要绘制', inputObj);

    objMain.mainF.removeF.clearGroup(objMain.batteryGroup);
    if (inputObj.hasValue) {
        var copyObj = objMain.ModelInput.battery.clone();
        copyObj.rotation.set(inputObj.position.rx, inputObj.position.ry, inputObj.position.rz, 'XYZ');
        copyObj.position.set(inputObj.position.x, inputObj.position.y, inputObj.position.z);
        copyObj.scale.set(inputObj.position.s, inputObj.position.s, inputObj.position.s);
        objMain.batteryGroup.add(copyObj);
    }
    else {

    }
};

var objPlaceSystemObj = function () {
    var obj =
    {
        'precision': 1,
        precisionAdd: function () {
            this.precision *= 2;
        },
        precisionMinus: function () {
            this.precision /= 2;
            if (this.precisionMinus < 0.01) {
                this.precision = 0.01;
            }
        },
        operateIndex: 0,
        operateGroups: [objMain.collectGroup, objMain.getOutGroup, objMain.buildingGroup, objMain.directionGroup, objMain.bitcoinCharacterGroup, objMain.batteryGroup],
        indexAdd: function () {
            this.operateIndex++;
            this.operateIndex %= this.operateGroups.length;
            this.addObj();
            switch (this.operateIndex) {
                case 0:
                    {
                        $.notify('元宝');
                    }; break;
                case 1:
                    {
                        $.notify('卫星');
                    }; break;
                case 2:
                    {
                        $.notify('风车');
                    }; break;
                case 3:
                    {
                        $.notify('指南针');
                    }; break;
                case 4:
                    {
                        $.notify('BTC地址');
                    }; break;
            }

        },
        indexMinus: function () {
            this.operateIndex--;
            this.operateIndex += this.operateGroups.length;
            this.operateIndex %= this.operateGroups.length;
            this.addObj();
            switch (this.operateIndex) {
                case 0:
                    {
                        $.notify('元宝');
                    }; break;
                case 1:
                    {
                        $.notify('卫星');
                    }; break;
                case 2:
                    {
                        $.notify('风车');
                    }; break;
                case 3:
                    {
                        $.notify('指南针');
                    } break;
            }
        },
        addObj: function () {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 0) {
                if (false) {
                    switch (this.operateIndex) {
                        case 0:
                            {
                                drawGoldObj(
                                    {
                                        'hasValue': true,
                                        position:
                                            { 'x': 0, 'y': 0, 'z': 0, 'rx': 0, 'ry': 0, 'rz': 0, 's': 1 }
                                    });
                            };
                        case 1:
                            {
                                drawStatelite(
                                    {
                                        'hasValue': true,
                                        position:
                                            { 'x': 0, 'y': 0, 'z': 0, 'rx': 0, 'ry': 0, 'rz': 0, 's': 1 }
                                    });
                            };
                        case 2:
                            {
                                drawTurbine(
                                    {
                                        'hasValue': true,
                                        position:
                                            { 'x': 0, 'y': 0, 'z': 0, 'rx': 0, 'ry': 0, 'rz': 0, 's': 1 }
                                    });
                            };
                        case 3:
                            {
                                drawCompass({
                                    'hasValue': true,
                                    position:
                                        { 'x': 0, 'y': 0, 'z': 0, 'rx': 0, 'ry': 0, 'rz': 0, 's': 1 }
                                });
                            } break;
                    }
                }
            }
        },
        add: function (p) {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                group.children[0].position[p] += this.precision;
                $.notify(p + '：' + group.children[0].position[p]);
            }
        },
        minus: function (p) {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                group.children[0].position[p] -= this.precision;
                $.notify(p + '：' + group.children[0].position[p]);
            }
        },
        rotate: function (axis, value) {

            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                group.children[0].rotation[axis] += value;
            }
        },
        scaleAdd: function () {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                // group.children[0].rotation[axis] += value;
                group.children[0].scale.x += 0.01;
                group.children[0].scale.x *= 1.01;

                group.children[0].scale.y += 0.01;
                group.children[0].scale.y *= 1.01;

                group.children[0].scale.z += 0.01;
                group.children[0].scale.z *= 1.01;
            }
        },
        scaleMinus: function () {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                // group.children[0].rotation[axis] += value;
                group.children[0].scale.x -= 0.01;
                group.children[0].scale.x *= 0.99;


                group.children[0].scale.y -= 0.01;
                group.children[0].scale.y *= 0.99;

                group.children[0].scale.z -= 0.01;
                group.children[0].scale.z *= 0.99;

                if (group.children[0].scale.x <= 0) {
                    group.children[0].scale.x = 0.01;
                    group.children[0].scale.y = 0.01;
                    group.children[0].scale.z = 0.01;
                }
            }
        },
        model: 0,
        changeModelAKey: function () {
            this.model++;
            this.model = this.model % 10;
            switch (this.model) {
                case 0:
                    {
                        $.notify('调整移动精度模式');
                    }; break;
                case 1:
                    {
                        $.notify('x move模式');
                    }; break;
                case 2:
                    {
                        $.notify('z move模式');
                    }; break;
                case 3:
                    {
                        $.notify('y move模式');
                    }; break;
                case 4:
                    {
                        $.notify('x rotate模式');
                    }; break;
                case 5:
                    {
                        $.notify('z rotate模式');
                    }; break;
                case 6:
                    {
                        $.notify('y rotatemove模式');
                    }; break;
                case 7:
                    {
                        $.notify('sacle rotatemove模式');
                    }; break;
                case 8:
                    {
                        $.notify('切换调整对象');
                    }; break;
                case 9:
                    {
                        $.notify('位置、比例放大');
                    }; break;
            }
        },
        addSkey: function () {
            switch (this.model % 10) {
                case 0:
                    {
                        this.precisionAdd();
                        $.notify('精度：' + this.precision);
                    }; break;
                case 1:
                    {
                        this.add('x');
                    }; break;
                case 2:
                    {
                        this.add('z');
                    }; break;
                case 3:
                    {
                        this.add('y');
                    }; break;
                case 4:
                    {
                        this.rotate('x', 0.05);
                    }; break;
                case 5:
                    {
                        this.rotate('z', 0.05);
                    }; break;
                case 6:
                    {
                        this.rotate('y', 0.05);
                    }; break;
                case 7:
                    {
                        this.scaleAdd();
                    }; break;
                case 8:
                    {
                        this.indexAdd();
                    }; break;
                case 9:
                    {
                        this.scaleAndLengthZoom(true);
                    }; break;
            }
        },
        minusDkey: function () {
            switch (this.model % 10) {
                case 0:
                    {
                        this.precisionMinus();
                        $.notify('精度：' + this.precision);
                    }; break;
                case 1:
                    {
                        this.minus('x');
                    }; break;
                case 2:
                    {
                        this.minus('z');
                    }; break;
                case 3:
                    {
                        this.minus('y');
                    }; break;
                case 4:
                    {
                        this.rotate('x', -0.05);
                    }; break;
                case 5:
                    {
                        this.rotate('z', -0.05);
                    }; break;
                case 6:
                    {
                        this.rotate('y', -0.05);
                    }; break;
                case 7:
                    {
                        this.scaleMinus();
                    }; break;
                case 8:
                    {
                        this.indexMinus();
                    }; break;
                case 9:
                    {
                        this.scaleAndLengthZoom(false);
                    }; break;
            }
        },
        downloadKeyF: function () {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                var fileName = '';
                switch (this.operateIndex) {
                    case 0:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'goldobj' + '.json';
                        }; break;
                    case 1:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'satelite' + '.json';
                        }; break;
                    case 2:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'turbine' + '.json';
                        }; break;
                    case 3:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'compass' + '.json';
                        }; break;
                    default: return;
                }
                var data = {
                    'x': group.children[0].position.x,
                    'y': group.children[0].position.y,
                    'z': group.children[0].position.z,
                    'rx': group.children[0].rotation.x,
                    'ry': group.children[0].rotation.y,
                    'rz': group.children[0].rotation.z,
                    's': group.children[0].scale.x
                };
                const jsonString = JSON.stringify(data, null, 2); // null, 2 参数用于美化 JSON

                // 创建一个 Blob 对象，类型为 JSON
                const blob = new Blob([jsonString], { type: "application/json" });

                // 创建一个下载链接
                const link = document.createElement("a");

                // 创建下载的文件名
                link.download = fileName;// objMain.background.mainFP

                // 创建 URL 供下载使用
                link.href = URL.createObjectURL(blob);

                // 触发下载
                link.click();

                // 释放 URL 对象
                URL.revokeObjectURL(link.href);
            }


            // 将 JSON 对象转换为字符串

        },
        uploadKeyF: function () {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                var fileName = '';
                switch (this.operateIndex) {
                    case 0:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'goldobj' + '.json';
                        }; break;
                    case 1:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'satelite' + '.json';
                        }; break;
                    case 2:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'turbine' + '.json';
                        }; break;
                    case 3:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'compass' + '.json';
                        }; break;
                    case 4:
                        {
                            fileName = objMain.background.mainFP.fPCode + objMain.background.mainFP.Height + 'btc' + '.json';
                        }; break;
                    default: return;
                }
                var data = {
                    'x': group.children[0].position.x,
                    'y': group.children[0].position.y,
                    'z': group.children[0].position.z,
                    'rx': group.children[0].rotation.x,
                    'ry': group.children[0].rotation.y,
                    'rz': group.children[0].rotation.z,
                    's': group.children[0].scale.x
                };
                const jsonString = JSON.stringify(data, null, 2); // null, 2 参数用于美化 JSON

                // 创建一个 Blob 对象，类型为 JSON
                var passObj = { 'c': 'UploadPositionJson', 'fileName': fileName, 'jsonString': jsonString };
                objMain.ws.send(JSON.stringify(passObj));
            }
        },
        scaleAndLengthZoom: function (add) {
            var group = this.operateGroups[this.operateIndex];
            if (group.children.length == 1) {
                if (add) {
                    // group.children[0].rotation[axis] += value; 
                    group.children[0].scale.x *= 2;
                    group.children[0].scale.y *= 2;
                    group.children[0].scale.z *= 2;

                    group.children[0].position.x *= 2;
                    group.children[0].position.y *= 2;
                    group.children[0].position.z *= 2;
                }
                else {
                    group.children[0].scale.x /= 2;
                    group.children[0].scale.y /= 2;
                    group.children[0].scale.z /= 2;

                    group.children[0].position.x /= 2;
                    group.children[0].position.y /= 2;
                    group.children[0].position.z /= 2;
                }
                $.notify('比例：' + group.children[0].scale.x);
            }
        }
    };
    return obj;
};
let placeObj = null;
var setUpPlaceObj = function () {
    placeObj = objPlaceSystemObj();
}
var loadFont = function (inputObj) {
    objMain.mainF.removeF.clearGroup(objMain.bitcoinCharacterGroup);
    if (inputObj.hasValue) {
        var loader = new THREE.FontLoader();
        loader.load('fonts/optimer_regular.typeface.json', function (response) {
            font = response;
            refreshText(font, inputObj);

        });
    }


}
function refreshText(font, inputObj) {

    //   updatePermalink();

    // group.remove(textMesh1);
    //if (mirror) group.remove(textMesh2);

    //if (!text) return;

    createText(font, inputObj);

}
function createText(font, inputObj) {
    var text = inputObj.bitcoinAddr;

    var height = 10;
    var size = 40;
    var hover = 30;

    var curveSegments = 4;
    var bevelThickness = 2;
    var bevelSize = 1.5;
    var bevelEnabled = true;

    //  var    font = undefined,

    // fontName = "optimer", // helvetiker, optimer, gentilis, droid sans, droid serif
    //   fontWeight = "bold"; // normal bold

    var textGeo = new THREE.TextGeometry(text, {

        font: font,

        size: size,
        height: height,
        curveSegments: curveSegments,

        bevelThickness: bevelThickness,
        bevelSize: bevelSize,
        bevelEnabled: bevelEnabled

    });

    textGeo.computeBoundingBox();
    textGeo.computeVertexNormals();

    // "fix" side normals by removing z-component of normals for side faces
    // (this doesn't work well for beveled geometry as then we lose nice curvature around z-axis)

    //if (!bevelEnabled)
    {

        var triangleAreaHeuristics = 0.1 * (height * size);

        for (var i = 0; i < textGeo.faces.length; i++) {

            var face = textGeo.faces[i];

            if (face.materialIndex == 1) {

                for (var j = 0; j < face.vertexNormals.length; j++) {

                    face.vertexNormals[j].z = 0;
                    face.vertexNormals[j].normalize();

                }

                var va = textGeo.vertices[face.a];
                var vb = textGeo.vertices[face.b];
                var vc = textGeo.vertices[face.c];

                var s = THREE.GeometryUtils.triangleArea(va, vb, vc);

                if (s > triangleAreaHeuristics) {

                    for (var j = 0; j < face.vertexNormals.length; j++) {

                        face.vertexNormals[j].copy(face.normal);

                    }

                }

            }

        }

    }

    // var centerOffset = - 0.5 * (textGeo.boundingBox.max.x - textGeo.boundingBox.min.x);

    textGeo = new THREE.BufferGeometry().fromGeometry(textGeo);
    var materials = [
        new THREE.MeshPhongMaterial({ color: 0xFFD700, flatShading: true }), // front
        new THREE.MeshPhongMaterial({ color: 0xFFD700 }) // side
    ];
    var textMesh1 = new THREE.Mesh(textGeo, materials);

    //textMesh1.position.x = centerOffset;
    //textMesh1.position.y = hover;
    //textMesh1.position.z = 0;

    //textMesh1.rotation.x = 0;
    //textMesh1.rotation.y = Math.PI * 2;

    textMesh1.rotation.set(inputObj.position.rx, inputObj.position.ry, inputObj.position.rz, 'XYZ');
    textMesh1.position.set(inputObj.position.x, inputObj.position.y, inputObj.position.z);
    textMesh1.scale.set(inputObj.position.s, inputObj.position.s, inputObj.position.s);

    textMesh1.userData = { 'btc': inputObj.bitcoinAddr };

    objMain.bitcoinCharacterGroup.add(textMesh1);

    //if (mirror) {

    //    textMesh2 = new THREE.Mesh(textGeo, materials);

    //    textMesh2.position.x = centerOffset;
    //    textMesh2.position.y = - hover;
    //    textMesh2.position.z = height;

    //    textMesh2.rotation.x = Math.PI;
    //    textMesh2.rotation.y = Math.PI * 2;

    //    group.add(textMesh2);

    //}

};
let loadBattery = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/battery/battery.obj';
        mtlUrl = 'http://127.0.0.1:11001/battery/battery.mtl';
        picUrl = 'http://127.0.0.1:11001/battery/battery.png';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/battery/battery.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/battery/battery.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/battery/battery.png';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
};
let loadDoubleRewardIcon = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/doublerewardicon/doubleReward.obj';
        mtlUrl = 'http://127.0.0.1:11001/doublerewardicon/doubleReward.mtl';
        picUrl = 'http://127.0.0.1:11001/doublerewardicon/doubleReward.png';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/doublerewardicon/doubleReward.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/doublerewardicon/doubleReward.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/doublerewardicon/doubleReward.png';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                fFinished(object);
            })
        })
    })
};
let loadSpeedIcon = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/speedicon/speedicon.obj';
        mtlUrl = 'http://127.0.0.1:11001/speedicon/speedicon.mtl';
        picUrl = 'http://127.0.0.1:11001/speedicon/speedicon.png';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/doublerewardicon/doubleReward.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/doublerewardicon/doubleReward.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/doublerewardicon/doubleReward.png';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                object.children[0].material.opacity = 0.7;
                fFinished(object);
            })
        })
    })
};
let loadGearIcon = function (objMain, fFinished) {
    var mtlUrl = '';
    var objUrl = '';
    var picUrl = '';
    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/gearicon/gearicon.obj';
        mtlUrl = 'http://127.0.0.1:11001/gearicon/gearicon.mtl';
        picUrl = 'http://127.0.0.1:11001/gearicon/gearicon.jpg';
    }
    else {
        objUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/gearicon/gearicon.obj';
        mtlUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/gearicon/gearicon.mtl';
        picUrl = 'https://yrqmodeldata.oss-cn-beijing.aliyuncs.com/h6_0/model/gearicon/gearicon.jpg';
    }
    $.get(mtlUrl, function (txt) {
        // console.log('t', txt)
        var manager = new THREE.LoadingManager();
        new THREE.MTLLoader(manager).loadTextWithImageUrl(txt, picUrl, function (materials) {
            materials.preload();
            new THREE.OBJLoader(manager).setMaterials(materials).load(objUrl, function (object) {
                console.log('obj', object);
                console.log('mtl', materials);
                object.children[0].material.opacity = 0.7;
                fFinished(object);
            })
        })
    })
}

