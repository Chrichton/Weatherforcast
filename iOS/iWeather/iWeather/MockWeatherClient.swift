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
                    Icon: "04n"),
                forecast: [WeatherViewModel.Weather]()
            )
        )
        .setFailureType(to: Error.self)
        .delay(for: 2, scheduler: DispatchQueue.main)
        .eraseToAnyPublisher()
    }
}
