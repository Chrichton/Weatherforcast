//
//  WeatherViewModel.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation

class WeatherViewModel: Decodable {
    var AverageTemperature: Float = 0.0
    var AverageHuminity: Float = 0.0
//    var Current: Weather = Weather()
//    var Forecast: [Weather] = []
    
    init() {
    }
}
