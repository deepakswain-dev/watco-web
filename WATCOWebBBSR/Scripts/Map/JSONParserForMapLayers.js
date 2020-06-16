var MapLayerTypes = { "WMSLayer": "wmslayer", "WFSLayer": "wfslayer", "VectorLayer": "vectorlayer" };
var layerColumnMapping = [
    { KeyString: "consumer", ValueString: "name_own,ws_c_no,status_con,isaneswar basti,buld_use,ws_mtr_no,buld_type,section,sub_div,division" },
    { KeyString: "flow_meter", ValueString: "code,make,yr_instl,man_dig,isaneswar basti,null,section,sub_div,division" },
    { KeyString: "valve", ValueString: "code,type,material,man_auto,make,dia_size,yr_instl,isaneswar basti,null,section,sub_div,division" },
    { KeyString: "pump", ValueString: "assest_code,type,make,yr_instl,isaneswar basti,manu_auto,null,section,sub_div,division" },
    { KeyString: "reservoir", ValueString: "code,type,isaneswar basti,material,cap_ltr,yr_const,shp,null,section,sub_div,division" },
    { KeyString: "water_supply_line", ValueString: "code,material,dia_siz_mm,yr_instl,isaneswar basti,len_mtr,null,section,sub_div,division" },
    { KeyString: "clear_water_line", ValueString: "code,material,dia_siz_mm,yr_instl,isaneswar basti,len_mtr,null,section,sub_div,division" },
];
var layerColumnAlias = [
    {
        "layername": "consumer", "properties": {
            "name_own": "Consumer Name", "ws_c_no": "Consumer No.", "status_con": "Application Status", "isaneswar basti": "Pilot Zone  ", "buld_use": "Connection Type", "ws_mtr_no": "Meter No.", "buld_type": "Building Type", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    },
    {
        "layername": "flow_meter", "properties": {
            "code": "Asset Code", "make": "Make", "yr_instl": "Installation Year", "man_dig": "Operation Type", "isaneswar basti": "Location", "null": "Last Repair Date", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    },
    {
        "layername": "valve", "properties": {
            "code": "Asset Code", "type": "Type", "material": "Material Type", "man_auto": "Operation Type", "make": "Make", "dia_size": "Dia (mm)", "yr_instl": "Installation Year", "isaneswar basti": "Location", "null": "Last Repair Date", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    },
    {
        "layername": "pump", "properties": {
            "code": "Asset Code", "type": "Type", "make": "Make", "yr_instl": "Installation Year", "isaneswar basti": "Location", "manu_auto": "Operation Type", "null": "Last Repair Date", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    },
    {
        "layername": "reservoir", "properties": {
            "code": "Asset Code", "type": "Type", "isaneswar basti": "Location", "material": "Material Type", "cap_ltr": "Capacity in Ltr", "yr_const": "Installation Year", "shp": "Shape", "null": "Last Repair Date", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    },
    {
        "layername": "water_supply_line", "properties": {
            "code": "Asset Code", "material": "Material Type", "dia_siz_mm": "Dia (mm)", "yr_instl": "Year of Installation", "isaneswar basti": "Location", "len_mtr": "Length in Meter", "null": "Last Repair Date", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    },
    {
        "layername": "clear_water_line", "properties": {
            "code": "Asset Code", "material": "Material Type", "dia_siz_mm": "Dia (mm)", "yr_instl": "Year of Installation", "isaneswar basti": "Location", "len_mtr": "Length in Meter", "null": "Last Repair Date", "section": "Section Name", "sub_div": "Sub Division  Name", "division": "Division  Name"
        }
    }
];

function GetValueFromKey(keyString) {
    for (var i = 0; i < layerColumnMapping.length; i++) {
        if (layerColumnMapping[i].KeyString.toLowerCase().trim() == keyString.toLowerCase().trim()) {
            return layerColumnMapping[i].ValueString;
        }
    }
}
function GetColumnAliasByName(layerName, colName) {
    var valueObject;
    for (var key in layerColumnAlias) {
        if (layerColumnAlias[key].layername.toLowerCase().trim() == layerName.toLowerCase().trim()) {
            valueObject = layerColumnAlias[key].properties;
            break;
        }
    }
    for (var key in valueObject) {
        if (key.toLowerCase().trim() == colName.toLowerCase().trim()) {
            return valueObject[key];
        }
    }
}

function ParseJSONResponseToHTML(jsonObject, layerType, layerName) {
    //for (var key in jsonObject) {
    //    alert("Key: " + key + " value: " + jsonObject[key]);
    //}
    var htmlString = "";
    switch (layerType.toLowerCase()) {
        case "wmslayer":
            ////=========================display info as it is======================//
            //htmlString = "<table class='table-bordered featureInfo'>";
            //htmlString += "<thead><tr><th colspan='2' class='text-center'>" + layerName.toUpperCase() + "</tr>";
            //htmlString += "<tr><th>Attributes</th><th>Values</th></tr></thead>";
            //htmlString += "<tbody>";
            //for (var key in jsonObject) {
            //    if (key.toUpperCase() == 'BBOX' || key.toUpperCase() == 'SL_NO' || key.toUpperCase() == 'ID') {
            //        //skip
            //    }
            //    else {
            //        htmlString += "<tr><td>" + key.toUpperCase() + "</td><td>" + jsonObject[key] + "</td></tr>";
            //    }
            //}
            //htmlString += "</tbody>";
            //htmlString += "</table>";
            ////===============================================================================//
            //=========================Customize info=============================//
            layerName = layerName.indexOf("cite:bbsr_") != -1 ? layerName.replace("cite:bbsr_", "") : layerName;
            layerName = layerName.indexOf("kn_") != -1 ? layerName.replace("kn_", "") : layerName;
            var valueList = GetValueFromKey(layerName);
            if (valueList == undefined) {
                htmlString = "<table class='table-bordered featureInfo'>";
                htmlString += "<thead><tr><th colspan='2' class='text-center'>" + layerName.toUpperCase().replace(new RegExp("_", "g"), " ") + "</tr>";
                //htmlString += "<tr><th>Attributes</th><th>Values</th></tr></thead>";
                htmlString += "<tbody>";
                for (var key in jsonObject) {
                    if (key.toUpperCase() == 'BBOX' || key.toUpperCase() == 'SL_NO' || key.toUpperCase() == 'ID') {
                        //skip
                    }
                    else {
                        htmlString += "<tr><td>" + key.toUpperCase() + "</td><td>" + jsonObject[key] + "</td></tr>";
                    }
                }
                htmlString += "</tbody>";
                htmlString += "</table>";
            }
            else {
                htmlString = "<table class='table-bordered featureInfo'>";
                htmlString += "<thead><tr><th colspan='2' class='text-center'>" + layerName.toUpperCase() + "</tr>";
                htmlString += "<tr><th>Attributes</th><th>Values</th></tr></thead>";
                htmlString += "<tbody>";

                var refArray = valueList.split(',');
                //create a temporary array to sort the fields later
                var mainArray = [];
                for (var key in jsonObject) {
                    if (valueList.toString().toLowerCase().indexOf(key.toLowerCase().trim()) != -1) {
                        mainArray.push({ "Attribute": key, "Value": jsonObject[key] });
                    }
                    else {
                        //dont display column in popup
                    }
                }
                //sort the temp array
                mainArray.sort(function (a, b) {
                    return refArray.indexOf(a.Attribute) - refArray.indexOf(b.Attribute);
                });
                //set custom column names by column alias
                for (var x = 0; x < mainArray.length; x++) {
                    htmlString += "<tr><td>" + GetColumnAliasByName(layerName, mainArray[x].Attribute) + "</td><td>" + mainArray[x].Value + "</td></tr>";
                }

                htmlString += "</tbody>";
                htmlString += "</table>";
            }
            //===============================================================================//
            break;
        case "wfslayer":
            break;
        case "vectorlayer":
            htmlString = "<table class='table-bordered featureInfo'>";
            htmlString += "<thead><tr><th colspan='2' class='text-center'>" + layerName.toUpperCase() + "</tr></thead>";
            htmlString += "<thead><tr><th>Attributes</th><th>Values</th></tr></thead>";
            htmlString += "<tbody>";
            for (var key in jsonObject) {
                htmlString += "<tr><td>" + key.toUpperCase() + "</td><td>" + jsonObject[key] + "</td></tr>";
            }
            htmlString += "</tbody>";
            htmlString += "</table>"; 
            break;
        default:
            break;
    }

    return htmlString;
}

function ParseHTMLResponseToHTML(response) {
    //debugger;
    var startIndex = response.indexOf("<table");
    var endIndex = response.lastIndexOf("</table>");
    var subString = response.substring(startIndex, endIndex);

    if (subString != '') {
        var thSubString = subString.substring(subString.indexOf("<th"), subString.lastIndexOf("</th") + 2);
        var th = thSubString.split("<th");

        var tdSubString = subString.substring(subString.indexOf("<td"), subString.lastIndexOf("</td") + 2);
        var td = tdSubString.split("<td");

        var attributes = [];
        var values = [];
        for (var i = 2; i < th.length; i++) {
            //alert(th[i].substring(th[i].indexOf(">") + 1, th[i].lastIndexOf("</")));
            attributes.push(th[i].substring(th[i].indexOf(">") + 1, th[i].lastIndexOf("</")));
        }

        for (var i = 2; i < td.length; i++) {
            values.push(td[i].substring(td[i].indexOf(">") + 1, td[i].lastIndexOf("</")));
        }

        //alert(attributes.length + '/' + values.length);

        var htmlstring = "<table class='featureInfo'>";
        htmlstring += "<tr><th>Attributes</th><th>Values</th></tr>";
        for (var i = 0; i < attributes.length; i++) {
            htmlstring += "<tr>";
            htmlstring += "<td>" + attributes[i] + "</td>";
            htmlstring += "<td>" + values[i] + "</td>";
            htmlstring += "</tr>";
        }
        htmlstring += "</table>";

        return htmlstring;
    }
    else {
        return subString;
    }
}