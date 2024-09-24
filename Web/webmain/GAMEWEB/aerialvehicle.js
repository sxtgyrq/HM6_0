let loadAerialVehicle = function (fFinished) {
    var mtlUrl = '';
    var objUrl = '';

    if (objMain.debug == 0) {
        objUrl = 'http://127.0.0.1:11001/objdata/Drone.obj';
        mtlUrl = 'http://127.0.0.1:11001/objdata/Drone.mtl';
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
    //.loadTextOnly(received_obj.Mtl, 'data:image/jpeg;base64,' + received_obj.Img, function (materials) {
    //    materials.preload();
    //    new THREE.OBJLoader(manager)
    //        .setMaterials(materials)
    //        //.setPath('/Pic/')
    //        .loadTextOnly(received_obj.Obj, function (object) {
    //            //console.log('o', object);
    //            //for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
    //            //    if (object.children[iOfO].isMesh) {
    //            //        for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
    //            //            object.children[iOfO].material[mi].transparent = true;
    //            //            object.children[iOfO].material[mi].opacity = 1;
    //            //            object.children[iOfO].material[mi].side = THREE.FrontSide;
    //            //            object.children[iOfO].material[mi].color = new THREE.Color(0.45, 0.45, 0.45);
    //            //        }
    //            //    }
    //            //}
    //            //console.log('o', object);
    //            if (config.scale == undefined)
    //                object.scale.set(0.003, 0.003, 0.003);
    //            else
    //                object.scale.set(config.scale.x, config.scale.y, config.scale.z);
    //            if (config.rotateX == undefined) { }
    //            else {
    //                object.rotateX(config.rotateX);
    //            }
    //            // object.rotateX(-Math.PI / 2);
    //            //  objMain.Model
    //            // objMain.rmbModel[field] = object;
    //            if (config.transparent != undefined) {
    //                for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
    //                    if (object.children[iOfO].isMesh) {
    //                        if (object.children[iOfO].material.isMaterial) {
    //                            object.children[iOfO].material.transparent = true;
    //                            object.children[iOfO].material.opacity = config.transparent.opacity;
    //                        }
    //                        else
    //                            for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
    //                                object.children[iOfO].material[mi].transparent = true;
    //                                object.children[iOfO].material[mi].opacity = config.transparent.opacity;
    //                            }
    //                    }
    //                }
    //            }
    //            if (config.color != undefined) {
    //                for (var iOfO = 0; iOfO < object.children.length; iOfO++) {
    //                    if (object.children[iOfO].isMesh) {
    //                        for (var mi = 0; mi < object.children[iOfO].material.length; mi++) {
    //                            object.children[iOfO].material[mi].color = new THREE.Color(config.color.r, config.color.g, config.color.b);
    //                        }
    //                    }
    //                }
    //            }
    //            if (config.rotate != undefined) {
    //                object.rotateX(config.rotate.x);
    //                object.rotateX(config.rotate.y);
    //                object.rotateX(config.rotate.z);
    //            }
    //            if (config.bind != undefined) {
    //                config.bind(object);
    //            }



    //        }, function () { }, function () { });
    //});
};

let ChangeBG = function (inputObj) {
    var fp = inputObj.fp;
    var fPCode = fp.fPCode;
    var Height = fp.Height;
    if (objMain.debug == 0) {
        objMain.background.path = "http://127.0.0.1:11001/pic/" + fPCode + "/" + Height + "/"; //received_obj.path;
        objMain.background.change();
    }
    else { }
    objMain.background.rotateOthers(inputObj.fp.ObjInSceneRotation);

}
