//
//  WeatherViewModel.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation

struct WeatherViewModel: Decodable {
    var AverageTemperature: Float
    var AverageHumidity: Float
    var current: Weather
    var forecast: [Weather]
    
    struct Weather: Decodable, Identifiable {
        var id: String { get { return DateTime } }
        
        var Temperature: Float
        var FeelsLikeTemperature: Float
        var MinimumTemperature: Float
        var MaximumTemperature: Float
        var Humidity: Int
        var Pressure: Int
        var Windspeed: Float
        var WindDirection: Int
        var CloudDescription: String
        var DateTime: String
        var Icon: String
    }
    
    static func empty() -> WeatherViewModel {
        WeatherViewModel(AverageTemperature: 0.0, AverageHumidity: 0.0, current: Weather(Temperature: 0.0, FeelsLikeTemperature: 0.0, MinimumTemperature: 0.0, MaximumTemperature: 0.0, Humidity: 0, Pressure: 0, Windspeed: 0.0, WindDirection: 0, CloudDescription: "", DateTime: "", Icon: ""), forecast: [])
    }
}
