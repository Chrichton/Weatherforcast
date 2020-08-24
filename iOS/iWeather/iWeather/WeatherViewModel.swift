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
    var FeelsLikeTemperature: Float = 0.0
    var MinimumTemperature: Float = 0.0
    var MaximumTemperature: Float = 0.0
    var Humidity: Int = 0
    var Pressure: Int = 0
    var Windspeed: Float = 0.0
    var WindDirection: Int = 0
    var CloudDescription: String = ""
    var DateTime: String = ""
    var Icon: String = ""
    
}

class WeatherViewModel: Decodable {
    var AverageTemperature: Float = 0.0
    var AverageHumidity: Float = 0.0
    var current: Weather = Weather()
    var forecast: [Weather] = []
    
    init() {
    }
}
