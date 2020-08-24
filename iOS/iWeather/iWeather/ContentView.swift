//
//  ContentView.swift
//  iWeather
//
//  Created by Heiko Goes on 16.08.20.
//  Copyright © 2020 Heiko Goes. All rights reserved.
//

import SwiftUI
import SwiftUIRefresh
import URLImage

struct ContentView: View {
    @ObservedObject var weatherStore: WeatherStore
    
    @State private var isShowing = false
    private let isoDateFormatter = DateFormatter()
    private let localtimeFormatter = DateFormatter()
    

    var body: some View {
        NavigationView {
            VStack(alignment: .center) {
                URLImage(URL(string: getUrlFor(icon: weatherStore.weatherViewModel.current.Icon))!,
                    placeholder: {_ in
                        Image(systemName: "circle")
                        .resizable()
                        .frame(width: 160.0, height: 120.0)
                    }).frame(width: 160, height: 120)
                Text("Datum: \(isoDateToTime(isoDateString: weatherStore.weatherViewModel.current.DateTime) ?? "")")
                Text(String(format: "Temperatur: %.1f Celsius",
                            weatherStore.weatherViewModel.current.Temperature))
                Text(String(format: "Mittlere Temperatur: %.1f Celsius",
                            weatherStore.weatherViewModel.AverageTemperature))
                Text(String(format: "Mittlere Temperatur: %.1f Celsius",
                            weatherStore.weatherViewModel.AverageTemperature))
                Text(String(format: "Mittlere Feuchte: %.1f %%",
                            weatherStore.weatherViewModel.AverageHumidity))
                List(weatherStore.weatherViewModel.forecast) { forecastModel in
                    HStack{
                        Text(self.isoDateToTime(isoDateString: forecastModel.DateTime) ?? "")
                        Text(String(format: "%.1f Celsius", forecastModel.Temperature))
                        URLImage(URL(string: "https://openweathermap.org/img/wn/\(forecastModel.Icon).png")!)
                            .padding(.leading, 40)
                        .frame(width: 16, height: 12)
                    }
                }
                .pullToRefresh(isShowing: $isShowing) {
                    DispatchQueue.main.asyncAfter(deadline: .now() + 1) {
                        self.weatherStore.refresh() // TODO reference-cycle
                        self.isShowing = false
                    }
                }
            } .navigationBarTitle(Text(LocalStorage.GetCurrentCityName()), displayMode: .inline)
        }
    }
    
    init(weatherStore: WeatherStore) {
        self.weatherStore = weatherStore
        isoDateFormatter.timeZone = NSTimeZone(name: "UTC") as TimeZone?
        isoDateFormatter.dateFormat = "yyyy-MM-dd'T'HH:mm:ss'Z'"
        
        localtimeFormatter.timeZone = NSTimeZone.local
        localtimeFormatter.dateFormat = "dd.MM HH:mm"
    }
    
    private func getUrlFor(icon: String) -> String {
        "https://openweathermap.org/img/wn/\(icon)@2x.png"
    }
    
    private func isoDateToTime(isoDateString: String) -> String? {
        guard let utcDate = isoDateFormatter.date(from: isoDateString) else { return nil }

        return localtimeFormatter.string(from: utcDate)
    }

}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView(weatherStore: WeatherStore(cityId: nil, cityName: nil))
    }
}
