//
//  WeatherStore.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation
import Combine

class WeatherStore : ObservableObject {
    @Published var weatherViewModel: WeatherViewModel = WeatherViewModel.empty()
    
    var weatherClient: WeatherClientProtocol
    
    private var currentCityId: Int?
    private var currentCityName: String?
    private var cancellable: AnyCancellable?
    
    init(cityId: Int?, cityName: String?, weatherClient: WeatherClientProtocol) {
        currentCityId = cityId
        currentCityName = cityName
        self.weatherClient = weatherClient
        
        refresh()
    }
    
    func refresh() -> Void {
        if let cityId = currentCityId,
            let cityName = currentCityName {
                set(cityId: cityId, cityName: cityName)
        }
    }
    
    func set(cityId: Int, cityName: String) {
        cancellable = weatherClient.weather(cityId: cityId)
        .sink(receiveCompletion: { error in
            print(error)
        }, receiveValue: { weatherViewModel in
            self.weatherViewModel = weatherViewModel
            LocalStorage.add(cityId: cityId, cityName: cityName)
        })
    }
    
    func getCities(startingWith: String) -> [City] {
        return []
    }
    
    func getCities(forZipcode: String) -> [City] {
        return []
    }
}
