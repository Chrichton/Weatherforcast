//
//  ContentView.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import SwiftUI

struct ContentView: View {
    @ObservedObject var weatherStore: WeatherStore
    
    var body: some View {
        VStack(alignment: .leading) {
            Text("Mittlere Temperatur: " +
                String(format: "%.1f", weatherStore.weatherViewModel.AverageTemperature) +
                " Celsius")
            Text("Mittlere Feuchte: " +
                String(format: "%.1f", weatherStore.weatherViewModel.AverageHumidity))
            Text("Temperatur: " +
                String(format: "%.1f", weatherStore.weatherViewModel.current.Temperature) +
            " Celsius")
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView(weatherStore: WeatherStore(cityId: nil))
    }
}
