//
//  WeatherViewModel.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation

class Weather: Decodable, Identifiable {
    var id: String { get { return DateTime } }
    
    var Temperature: Float = 0.0
    var DateTime: String = ""
}

class WeatherViewModel: Decodable {
    var AverageTemperature: Float = 0.0
    var AverageHumidity: Float = 0.0
    var current: Weather = Weather()
    var forecast: [Weather] = []
    
    init() {
    }
}
