//
//  LocalStorage.swift
//
//  Created by Heiko Goes on 19.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation

enum LocalStorage {
    static private var CityIdKey = "CityIdKey"
    static private var IdsCitiesKey = "IdsCitiesKey"
    
    static func getCurrentCityId() -> Int? {
        return UserDefaults.standard.integer(forKey: CityIdKey)
    }

    static func GetCurrentCityName() -> String {
        if let cityId = getCurrentCityId(),
            let dict = UserDefaults.standard.dictionary(forKey: IdsCitiesKey) as? [String : String] {
            return dict[String(cityId) , default: ""]
        }
        
        return ""
    }
    
    static func set(currentCityId: Int?) {
        if let cityId = currentCityId {
            UserDefaults.standard.set(cityId, forKey: CityIdKey)
        } else {
            UserDefaults.standard.removeObject(forKey: CityIdKey)
        }
    }
    
    static func add(cityId: Int, cityName: String) {
        // UserDefaults Dictionary has to be ["String" : Any]
        var newDict: [String:String]
        
        if let dict = UserDefaults.standard.object(forKey: IdsCitiesKey) as? [String:String] {
            newDict = dict
        } else {
            newDict = [String:String]()
        }
        
        newDict[String(cityId)] = cityName
        UserDefaults.standard.set(newDict, forKey: IdsCitiesKey)
    }
}
