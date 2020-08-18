//
//  ContentView.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import SwiftUI
import SwiftUIRefresh

struct ContentView: View {
    @ObservedObject var weatherStore: WeatherStore
    
    @State private var isShowing = false
    private let isoDateFormatter = DateFormatter()
    private let timeFormatter = DateFormatter()
    

    var body: some View {
        VStack(alignment: .leading) {
            Text("Datum: \(isoDateToTime(isoDateString: weatherStore.weatherViewModel.current.DateTime) ?? "")")
            Text("Mittlere Temperatur: " +
                String(format: "%.1f Celsius", weatherStore.weatherViewModel.AverageTemperature))
            Text("Mittlere Feuchte: " +
                String(format: "%.1f", weatherStore.weatherViewModel.AverageHumidity))
            Text("Temperatur: " +
                String(format: "%.1f Celsius", weatherStore.weatherViewModel.current.Temperature))
            List(weatherStore.weatherViewModel.forecast) { forecastModel in
                HStack{
                    Text(self.isoDateToTime(isoDateString: forecastModel.DateTime) ?? "")
                    Text(String(format: "%.1f Celsius", forecastModel.Temperature))
                }
            }
            .pullToRefresh(isShowing: $isShowing) {
                DispatchQueue.main.asyncAfter(deadline: .now() + 1) {
                    self.weatherStore.refresh() // TODO reference-cycle
                    self.isShowing = false
                }
            }
        }
    }
    
    init(weatherStore: WeatherStore) {
        self.weatherStore = weatherStore
        isoDateFormatter.dateFormat = "yyyy-MM-dd'T'HH:mm:ss'Z'"
        timeFormatter.dateFormat = "dd.MM HH:mm"
    }
    
    private func isoDateToTime(isoDateString: String) -> String? {
        guard let date = isoDateFormatter.date(from: isoDateString) else { return nil }
        return timeFormatter.string(from: date)
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView(weatherStore: WeatherStore(cityId: nil))
    }
}
