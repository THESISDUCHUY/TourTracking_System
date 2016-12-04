//
//  Warning.swift
//  TourGuideTracking
//
//  Created by Quoc Huy Ngo on 11/24/16.
//  Copyright © 2016 Quoc Huy Ngo. All rights reserved.
//

import ObjectMapper

class Warning: Mappable{
    var warning_id:Int?
    var name:String?
    var isGroup:Int?
    var type:String?
    var description:String?
    var status:String?
    var distance:Double?
    var location:Location?
    
    required init?(map: Map){
        
    }
    
    func mapping(map: Map) {
        self.warning_id <- map["warning_id"]
        self.name <- map["name"]
        self.isGroup <- map["isGroup"]
        self.type <- map["type"]
        self.description <- map["description"]
        self.status <- map["status"]
        self.distance <- map["distance"]
        self.location <- map["location"]
    }
    
    init(warningData:Dictionary<String, Any>){
        self.name = warningData["WarningName"] as? String
        self.type = warningData["CategoryWarnig"] as? String
        self.description = warningData["DescriptionWarning"] as? String
        self.location = Location()
        self.location?.latitude = (warningData["Lat"] as? Double)!
        self.location?.longitude = (warningData["Long"] as? Double)!
        self.distance = warningData["Distance"] as? Double
        self.warning_id = warningData["WarningId"] as? Int

    }
}
