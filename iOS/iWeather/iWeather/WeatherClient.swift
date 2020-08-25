//
//  WeatherClient.swift
//  iWeather
//
//  Created by Heiko Goes on 25.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation
import Combine

struct WeatherClient: WeatherClientProtocol {
    func weather(cityId: Int) -> AnyPublisher<WeatherViewModel, Error> {
        let urlString = "https://vueweatherapi.azurewebsites.net/api/weather/forecast/id/\(cityId)"
        let url = URL(string: urlString)!
        
        return URLSession.shared.dataTaskPublisher(for: url)
        .map { $0.data }
        .decode(type: WeatherViewModel.self, decoder: JSONDecoder())
        .receive(on: DispatchQueue.main)
        .eraseToAnyPublisher()
    }
}
