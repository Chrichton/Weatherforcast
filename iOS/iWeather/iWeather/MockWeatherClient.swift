//
//  MockWeatherClient.swift
//  iWeather
//
//  Created by Heiko Goes on 25.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation
import Combine

struct MockWeatherClient: WeatherClientProtocol {
    func weather(cityId: Int) -> AnyPublisher<WeatherViewModel, Error> {
        Just(
            WeatherViewModel(
                AverageTemperature: 20.0,
                AverageHumidity: 42.0,
                current: WeatherViewModel.Weather(
                    Temperature: 25.0,
                    FeelsLikeTemperature: 27.0,
                    MinimumTemperature: 23,
                    MaximumTemperature: 30,
                    Humidity: 60,
                    Pressure: 1020,
                    Windspeed: 3,
                    WindDirection: 270,
                    CloudDescription: "Wolking",
                    DateTime: "2020-08-24T04:00:14Z",
                    Icon: "02d"),
                forecast: [
                    WeatherViewModel.Weather(
                        Temperature: 26.0,
                        FeelsLikeTemperature: 28.0,
                        MinimumTemperature: 24,
                        MaximumTemperature: 31,
                        Humidity: 61,
                        Pressure: 1021,
                        Windspeed: 4,
                        WindDirection: 271,
                        CloudDescription: "Gewitter",
                        DateTime: "2020-08-24T07:00:14Z",
                        Icon: "10d"),
                    WeatherViewModel.Weather(
                    Temperature: 27.0,
                    FeelsLikeTemperature: 97.0,
                    MinimumTemperature: 25,
                    MaximumTemperature: 32,
                    Humidity: 62,
                    Pressure: 1022,
                    Windspeed: 5,
                    WindDirection: 272,
                    CloudDescription: "Sonnig",
                    DateTime: "2020-08-24T10:00:14Z",
                    Icon: "04d"),
                ]
            )
        )
        .setFailureType(to: Error.self)
        .delay(for: 2, scheduler: DispatchQueue.main)
        .eraseToAnyPublisher()
    }
}
