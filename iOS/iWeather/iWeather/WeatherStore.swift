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
    @Published var weatherViewModel: WeatherViewModel = WeatherViewModel()
    
    private var cancellable: AnyCancellable?
    
    init(cityId: Int?) {
        if let cityId = cityId {
            setCityId(cityId)
        }
    }
    
    func setCityId(_ cityId: Int) {
        let urlString = "https://vueweatherapi.azurewebsites.net/api/weather/forecast/id/\(cityId)"
        let url = URL(string: urlString)!
        
        cancellable = URLSession.shared.dataTaskPublisher(for: url)
        .map { $0.data }
        .decode(type: WeatherViewModel.self, decoder: JSONDecoder())
        .receive(on: DispatchQueue.main)
        .sink(receiveCompletion: { error in
            print(error)
        }, receiveValue: { weatherViewModel in
            self.weatherViewModel = weatherViewModel
        })
    }
    
    func getCities(startingWith: String) -> [City] {
        return []
    }
    
    func getCities(forZipcode: String) -> [City] {
        return []
    }
}
