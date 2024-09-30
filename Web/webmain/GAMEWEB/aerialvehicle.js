let loadAerialVehicle = function (fFinished) {
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
            fFinished(object);
        })
    });
};

let ChangeBG = function (inputObj) {
    var fp = inputObj.fp;
    var fPCode = fp.fPCode;
    var Height = fp.Height;
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
};

let loadCore = function (fFinished) {
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

var drawLineOfSelections = function (inputObj) {
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
