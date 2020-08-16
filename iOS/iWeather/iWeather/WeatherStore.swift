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
    
    init() {
        let url = URL(fileURLWithPath: "")
        self.cancellable = URLSession.shared.dataTaskPublisher(for: url)
            .map{ $0.data }
            .decode(type: WeatherViewModel.self, decoder: JSONDecoder())
            .replaceError(with: WeatherViewModel())
        .sink(receiveValue: { weatherViewModel in
            self.weatherViewModel = weatherViewModel
        })
    }
    
    func setCityId(_ cityId: Int) {
        cancellable = URLSession.shared.dataTaskPublisher(for: URL(fileURLWithPath: ""))
        .map { $0.data }
        .decode(type: WeatherViewModel.self, decoder: JSONDecoder())
        .replaceError(with: WeatherViewModel())
        .sink(receiveValue: { weatherViewModel in
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
