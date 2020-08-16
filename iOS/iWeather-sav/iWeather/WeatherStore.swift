//
//  WeatherStore.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation
import Combine

class WeatherStore: ObservableObject {
    private var cancellable: AnyCancellable?
    
    @Published private var weather = WeatherViewModel()
    
    init() {
        cancellable = URLSession.shared.dataTaskPublisher(for: URL(fileURLWithPath: ""))
        .map { $0.data }
        .decode(type: WeatherViewModel.self, decoder: JSONDecoder())
        .replaceError(with: WeatherViewModel())
        .sink(receiveValue: { weatherViewModel in
            self.weather = WeatherViewModel()
        })
    }
}
