//
//  WeatherViewModel.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation

public class Weather: Decodable {
    public var Temperature: Float = 0.0
}

public class WeatherViewModel: Decodable {
    public var AverageTemperature: Float = 0.0
    public var AverageHumidity: Float = 0.0
    var current: Weather = Weather()
    var forecast: [Weather] = []
    
    init() {
    }
}
