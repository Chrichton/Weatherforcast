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
    @Published var currentCityName: String?
    
    private var weatherClient: WeatherClientProtocol
    private var currentCityId: Int?
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
                set(currentCityId: cityId, currentCityName: cityName)
        }
    }
    
    func set(currentCityId: Int, currentCityName: String) {
        cancellable = weatherClient.weather(cityId: currentCityId)
        .sink(receiveCompletion: { error in
            print(error)
        }, receiveValue: { weatherViewModel in
            self.weatherViewModel = weatherViewModel
            LocalStorage.set(currentCityId: currentCityId)
        })
    }
    
    // Adds cityId, cityName to favorites
    func add(cityId: Int, cityName: String) {
        LocalStorage.add(cityId: cityId, cityName: cityName)
    }
    
    func getCities(startingWith: String) -> [City] {
        return []
    }
    
    func getCities(forZipcode: String) -> [City] {
        return []
    }
}
