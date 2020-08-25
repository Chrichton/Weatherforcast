//
//  WeatherClientProtocol.swift
//  iWeather
//
//  Created by Heiko Goes on 25.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import Foundation
import Combine

protocol WeatherClientProtocol {
  func weather(cityId: Int) -> AnyPublisher<WeatherViewModel, Error>
}
